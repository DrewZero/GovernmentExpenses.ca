using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Metamorph {
	public class PDFParser {
		/// BT = Beginning of a text object operator
		/// ET = End of a text object operator
		/// Td move to the start of next line
		///  5 Ts = superscript
		/// -5 Ts = subscript

		#region Fields

		#region _numberOfCharsToKeep
		/// <summary>
		/// The number of characters to keep, when extracting text.
		/// </summary>
		private static int _numberOfCharsToKeep = 15;
		#endregion

		#endregion


		// Flag showing if we are we currently inside a text object
		bool inTextObject = false;

		#region ExtractTextFromPDFBytes
		/// <summary>
		/// This method processes an uncompressed Adobe (text) object
		/// and extracts text.
		/// </summary>
		/// <param name="input">uncompressed</param>
		/// <returns></returns>
		public string ExtractTextFromPDFBytes(byte[] input) {
			if (input == null || input.Length == 0) return "";

			try {
				string resultString = "";

				// Flag showing if the next character is literal
				// e.g. '\\' to get a '\' character or '\(' to get '('
				bool nextLiteral = false;

				// () Bracket nesting level. Text appears inside ()
				int bracketDepth = 0;

				// Keep previous chars to get extract numbers etc.:
				char[] previousCharacters = new char[_numberOfCharsToKeep];
				for (int j = 0; j < _numberOfCharsToKeep; j++) previousCharacters[j] = ' ';


				for (int i = 0; i < input.Length; i++) {
					char c = (char)input[i];

					if (inTextObject) {
						// Position the text
						if (bracketDepth == 0) {
							if (CheckToken(new string[] { "TD", "Td" }, previousCharacters)) {
								resultString += "\r\n";
							} else {
								if (CheckToken(new string[] { "'", "T*", "\"" }, previousCharacters)) {
									resultString += "\r\n";
								} else {
									if (CheckToken(new string[] { "Tj" }, previousCharacters)) {
										resultString += "";
									}
								}
							}
						}

						// End of a text object, also go to a new line.
						if (bracketDepth == 0 &&
							CheckToken(new string[] { "ET" }, previousCharacters)) {

							inTextObject = false;
						} else {
							// Start outputting text
							if ((c == '(') && (bracketDepth == 0) && (!nextLiteral)) {
								bracketDepth = 1;
								resultString += "{";
							} else {
								// Stop outputting text
								if ((c == ')') && (bracketDepth == 1) && (!nextLiteral)) {
									bracketDepth = 0;
									resultString += "}";
								} else {
									// Just a normal text character:
									if (bracketDepth == 1) {
										if (nextLiteral) {
											// Interpret the reverse solidus character appropriately
											if (c == 'n') resultString += "\n";
											else if (c == 'r') resultString += "\r";
											else if (c == 't') resultString += "\t";
											else if (c == 'b') resultString += "\b";
											else if (c == 'f') resultString += "\f";
											else if (c == ')') resultString += ")";
											else if (c == '(') resultString += "(";
											else if (c == '\\') resultString += "\\";
											else if (c >= '0' && c <= '9') {
												resultString += Encoding.GetEncoding("ISO-8859-1").GetString(new byte[] {DecodeOctalString(input[i]-48, input[i + 1]-48, input[i + 2]-48)});
												i += 2;
											}
											nextLiteral = false;
										} else if (c == '\\') {
											// Reverse solidus
											nextLiteral = true;
										} else {
											// Regular character
											if (((c >= ' ') && (c <= '~')) ||
												((c >= 128) && (c < 255))) {
												resultString += c.ToString();
											}
										}
									}
								}
							}
						}
					}

					// Store the recent characters for
					// when we have to go back for a checking
					for (int j = 0; j < _numberOfCharsToKeep - 1; j++) {
						previousCharacters[j] = previousCharacters[j + 1];
					}
					previousCharacters[_numberOfCharsToKeep - 1] = c;

					// Start of a text object
					if (!inTextObject && CheckToken(new string[] { "BT" }, previousCharacters)) {
						inTextObject = true;
					}
				}
				return resultString;
			} catch {
				return "";
			}
		}
		#endregion

		public static byte DecodeOctalString(string octalValue) {
			int a = int.Parse(octalValue.Substring(1, 1));
			int b = int.Parse(octalValue.Substring(2, 1));
			int c = int.Parse(octalValue.Substring(3, 1));
			return DecodeOctalString(a, b, c);
		}
		public static byte DecodeOctalString(char a, char b, char c) {
			return DecodeOctalString(int.Parse(a.ToString()), int.Parse(b.ToString()), int.Parse(c.ToString()));
		}
		public static byte DecodeOctalString(int a, int b, int c) {
			return (byte)((a<<6) | (b<<3) | (c));
		}

		#region CheckToken
		/// <summary>
		/// Check if a certain 2 character token just came along (e.g. BT)
		/// </summary>
		/// <param name="search">the searched token</param>
		/// <param name="recent">the recent character array</param>
		/// <returns></returns>
		private bool CheckToken(string[] tokens, char[] recent) {
			foreach (string token in tokens) {
				if (token.Length > 1) {
					if ((recent[_numberOfCharsToKeep - 3] == token[0]) &&
						(recent[_numberOfCharsToKeep - 2] == token[1]) &&
						((recent[_numberOfCharsToKeep - 1] == ' ') ||
						(recent[_numberOfCharsToKeep - 1] == 0x0d) ||
						(recent[_numberOfCharsToKeep - 1] == 0x0a)) &&
						((recent[_numberOfCharsToKeep - 4] == ' ') ||
						(recent[_numberOfCharsToKeep - 4] == 0x0d) ||
						(recent[_numberOfCharsToKeep - 4] == 0x0a))
						) {
						return true;
					}
				} else {
					return false;
				}

			}
			return false;
		}
		#endregion


		// Parses the PDF document and returns the formatted text
		public string ParseDocument(PdfSharp.Pdf.PdfDocument i_doc) {
			string result = "";
			for (int i = 0; i < i_doc.Pages.Count; i++) {
				result += ParsePage(i_doc.Pages[i]);
			}
			return result;
		}
		
		private string ParsePage(PdfSharp.Pdf.PdfPage i_page) {
			string result = "";
			for (int i = 0; i < i_page.Contents.Elements.Count; i++) {
				result += ParseDictionary(i_page.Contents.Elements.GetDictionary(i));
			}
			return result;
		}

		private string ParseDictionary(PdfSharp.Pdf.PdfDictionary i_dictionary) {
			string result = "";
			result += ParseStream(Encoding.ASCII.GetString(i_dictionary.Stream.Value));
			return result;
		}

		private string ParseStream(string i_stream) {
			string result = "";
			string[] lines = i_stream.Split('\n');
			for (int i=0 ; i<lines.Length ; i++) {
				string[] tokens = lines[i].Trim().Split(' ');
				string instruction = tokens[tokens.Length - 1];
				// TODO? Handle BT and ET?
				if (instruction == "Tj") {
					result += tokens[tokens.Length - 2];
				} else if (instruction == "T*") {
				} else if (instruction == "Td") {
				}
			}
			return result;
		}
		



	}
}
