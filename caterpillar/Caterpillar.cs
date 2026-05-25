using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Net;
using System.Globalization;
using System.Threading;


namespace Metamorph {
	public class RegexTimeout {
		private Regex m_regex;
		private string m_input;
		private MatchCollection m_return;

		private void DoMatchWithTimeout() {
			m_return = m_regex.Matches(m_input);
			int x = m_return.Count;  // Force the regex to be fully evaluated
		}
		
		public MatchCollection MatchWithTimeout(Regex i_regex, string i_input) {
			m_regex = i_regex;
			m_input = i_input;
			Thread thread = new Thread(new ThreadStart(DoMatchWithTimeout));
			thread.Start();
			if (!thread.Join(30000)) {
				// the regex is taking too long
				thread.Abort();
				return null;
			} else {
				return m_return;
			}
		}


	}

    public class Caterpillar {

		private Hashtable m_databases = new Hashtable();
		private Hashtable m_recordTypes = new Hashtable();
		private Hashtable m_regexes = new Hashtable();
		private Hashtable m_urlTypes = new Hashtable();

		// The list of links visited, to avoid having to load a page that was clearly already loaded
		private ArrayList m_visitedLinks = new ArrayList();
		
		// The list of actual response URIs since often times there is a redirect and a different
		// URI responds, so several links can point to the same page
		private ArrayList m_visitedUrls = new ArrayList();
		
		private string m_rootHost;
		private ResultList m_result;
		private CookieContainer m_cookies = new CookieContainer();


		// Processes the specified config filename
		// Returns true if there were no errors
		// Returns false and the list of errors if any errors occurred
		public ResultList ProcessConfig(string i_configFile) {
			if (!File.Exists(i_configFile)) {
				throw new Exception("File not found: " + i_configFile);
			}

			// Read the XML config file
			XmlDocument config = new XmlDocument();
			config.Load(i_configFile);

			// Get the logging info from the config.xml
			XmlNode logging = config.SelectSingleNode("/CATERPILLAR/LOGGING");
			if (logging == null && m_result == null) {
				// Specify a default logging level if none is specified
				m_result = new ResultList(0, "errors.log");
			} else {
				m_result = new ResultList(int.Parse(logging.Attributes["WARNINGLEVEL"].Value), logging.Attributes["FILENAME"].Value);
			}
			m_result.Start();
			m_result.AppendInformationMessage("START PROCESSING '" + i_configFile + "'");

			if (config.DocumentElement.Name != "CATERPILLAR") throw new Exception("CATERPILLAR Root node not found");

			// Go through the XML config file and process the main entries
			foreach (XmlNode child in config.DocumentElement.ChildNodes) {
				if (child.Name == "REGEX") {
					// store the REGEX's to reference them for reuse
					ProcessRegex(child, ref m_result);
				} else if (child.Name == "URLTYPE") {
					ProcessURLType(child, ref m_result);
				} else if (child.Name == "ROOTPAGE") {
					// Process the page
					Uri url = new Uri(child.Attributes["URL"].Value);
					XmlNode urlType = (XmlNode)m_urlTypes[child.Attributes["URLTYPE"].Value];
					if (urlType == null) {
						m_result.AppendErrorMessage(0, "ERROR: URLTYPE '" + child.Attributes["URLTYPE"].Value + "' not found");
					} else {
						// Set the root host so that we don't go off-server
						m_rootHost = url.Host;
						// Add the rootpage to the list of visited links
						m_visitedLinks.Add(url);

						m_result.AppendInformationMessage("BEGIN processing ROOTPAGE for url: " + url);
						ProcessURL(urlType, url, new Stack<Match>(), child, null, ref m_result);
						m_result.AppendInformationMessage("END processing ROOTPAGE for url: " + url);
					}
				} else if (child.Name == "DATABASE") {
					// Store the database definition
					ProcessDatabase(child, ref m_result);
				} else if (child.Name == "RECORDTYPE") {
					// Store the record type definition
					ProcessRecordType(child, ref m_result);
				}
			}

			m_result.Stop();
			m_result.AppendInformationMessage("FINISHED PROCESSING '" + i_configFile + "'.  Total Time = " + m_result.ExecutionTime);
			m_result.AppendInformationMessage("    " + m_result.RecordsStored + " records stored");
			m_result.AppendInformationMessage("    " + m_result.Errors + " errors logged");
			m_result.AppendInformationMessage("    " + m_result.Warnings + " warnings logged");
			m_result.AppendInformationMessage("    " + m_result.UrlsLoaded + " URLs loaded");
			m_result.AppendInformationMessage("    " + m_result.UrlsSkipped + " URLS skipped");

			return m_result;
        }

		private void ProcessURLType(XmlNode i_config, ref ResultList io_result) {
			m_urlTypes.Add(i_config.Attributes["NAME"].Value, i_config);
		}

		// Process the site, loading the data into the database
		// Returns the number of errors encountered while parsing records
		private void ProcessURL(XmlNode i_urlType, Uri i_url, Stack<Match> i_matches, XmlNode i_rootpage, string i_ignoreParameters, ref ResultList io_result) {
			string originalPage = null;

			// Download the page
			int attempts = 1;
			while (true) {
				try {
					if (i_rootpage.Attributes["THROTTLE"] != null) {
						// Pause for a moment so as not to overload a server with too many requests
						Thread.Sleep(int.Parse(i_rootpage.Attributes["THROTTLE"].Value));
					}
					originalPage = GetPage(i_url, i_ignoreParameters, ref io_result);

					if (originalPage == null) return;
					else break; // Successful page load, so continue processing
				} catch (Exception ex) {
					io_result.AppendErrorMessage(0, "ERROR: Could not load URL (attempt #" + attempts + ") " + i_url + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
				}

				// Give up after 3 failed attempts
				if (attempts >= 3) {
					io_result.AppendErrorMessage(0, "ERROR: Could not load URL after " + attempts + " attempts " + i_url);
					return;  
				}

				attempts += 1;
			}


			foreach (XmlNode child in i_urlType) {
				if (child.Name == "URLLIST") {
					string regex = (string)m_regexes[child.Attributes["REGEX"].Value];
					if (regex == null) throw new Exception("REGEX '" + child.Attributes["REGEX"].Value + "' not defined");
					string ignoreParameters = child.Attributes["IGNOREPARAMETERS"] == null ? null : child.Attributes["IGNOREPARAMETERS"].Value;
					Match prefix, suffix;
					Regex rx = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

					// Trim the prefix and suffix from the page
					string page = TrimPage(originalPage, child.Attributes["PREFIX"].Value, child.Attributes["SUFFIX"].Value, out prefix, out suffix, i_url.AbsoluteUri, ref io_result);
					if (page == null) continue;
					
					// Find all urls on the page
					RegexTimeout regexTimeout = new RegexTimeout();
					MatchCollection matches = regexTimeout.MatchWithTimeout(rx, page);

					if (matches == null) {
						io_result.AppendErrorMessage(0, "ERROR: Regex took too long to evaluate and was terminated '" + child.Attributes["REGEX"].Value + "' on page " + i_url);
						io_result.AppendErrorMessage(2, "INFO: Page source: \r\n" + page);
						continue;
					} else if (matches.Count == 0) {
						// why were there no matches found??
						io_result.AppendErrorMessage(1, "WARNING: No matches found for URLLIST '" + child.Attributes["REGEX"].Value + "' on page " + i_url);
						io_result.AppendErrorMessage(2, "INFO: Page source: \r\n" + page);
						continue;
					}
					
					for (int i = 0; i < matches.Count; i++) {
						string url = matches[i].Groups["URL"].Value;
						url = HttpUtility.HtmlDecode(url);
						Uri newUrl = new Uri(i_url, url);
						// Check to make sure we don't re-scan URLs or go off-server
						if (ValidateURL(newUrl, ignoreParameters, ref io_result)) {
							i_matches.Push(suffix);
							i_matches.Push(prefix);
							i_matches.Push(matches[i]);
							ProcessURL(child, newUrl, i_matches, i_rootpage, ignoreParameters, ref io_result);
							i_matches.Pop();
							i_matches.Pop();
							i_matches.Pop();
						}
					}
				} else if (child.Name == "RECORDLIST") {
					// Parse the records on the page
					string regex = (string)m_regexes[child.Attributes["REGEX"].Value];
					if (regex == null) throw new Exception("REGEX '" + child.Attributes["REGEX"].Value + "' not defined");
					Match prefix, suffix;
					string dateFormat = child.Attributes["DATEFORMAT"] == null ? null : child.Attributes["DATEFORMAT"].Value;
					string dateFormat2 = child.Attributes["DATEFORMAT2"] == null ? null : child.Attributes["DATEFORMAT2"].Value;
					RecordType recType = (RecordType)m_recordTypes[child.Attributes["RECORDTYPE"].Value];
					string databaseName = child.Attributes["DATABASE"].Value;
					Regex rx = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

					// Trim the prefix and suffix from the page
					string page = TrimPage(originalPage, child.Attributes["PREFIX"].Value, child.Attributes["SUFFIX"].Value, out prefix, out suffix, i_url.AbsoluteUri, ref io_result);
					if (page == null) continue;

					// Find all the records on the page
					RegexTimeout regexTimeout = new RegexTimeout();
					MatchCollection matches = regexTimeout.MatchWithTimeout(rx, page);

					if (matches == null) {
						io_result.AppendErrorMessage(0, "ERROR: Regex took too long to evaluate and was terminated '" + child.Attributes["REGEX"].Value + ":" + rx.ToString() + "' on page " + i_url);
						io_result.AppendErrorMessage(2, "INFO: Page source: \r\n" + page);
						continue;
					} else if (matches.Count == 0) {
						// why were there no matches found??
						io_result.AppendErrorMessage(1, "WARNING: No matches found for RECORDLIST '" + child.Attributes["REGEX"].Value + ":" + rx.ToString() + "' on page " + i_url);
						io_result.AppendErrorMessage(2, "INFO: Page source: \r\n" + page);
						continue;
					}


					// Open the table that we are going to insert the record into
					Cocoon.OpenDataTable(databaseName, recType, i_url, ref io_result);

					// Fetch the records
					for (int i = 0; i < matches.Count; i++) {
						Record rec = new Record();
						rec.m_typeName = recType.m_typeName;
						rec.m_parameterValues = new object[recType.m_parameters.Length];

						// Retrieve all the parameters for this record
						for (int j = 0; j < recType.m_parameters.Length; j++) {
							string paramName = recType.m_parameters[j].m_parameterName;
							string paramValue = null;

							if (matches[i].Groups[paramName].Success) {
								// Try to retrieve the value from the parsed REGEX
								paramValue = matches[i].Groups[paramName].Value;
							} else if (prefix != null && prefix.Groups[paramName].Success) {
								// Try to retrieve the value from the PREFIX
								paramValue = prefix.Groups[paramName].Value;
							} else if (suffix != null && suffix.Groups[paramName].Success) {
								// Try to retrieve the value from the SUFFIX
								paramValue = suffix.Groups[paramName].Value;
							} else if (child.Attributes[paramName] != null) {
								// Try to retrieve the value from the RECORDLIST entry
								paramValue = child.Attributes[paramName].Value;
							} else {
								// Then proceed to scan the previous URLLIST prefix and suffix matches
								foreach (Match m in i_matches) {
									if (m != null && m.Groups[paramName].Success) {
										paramValue = m.Groups[paramName].Value;
										break;
									}
								}

								// If a match for the parameter is still not found,
								if (paramValue == null) {
									// Try to retrieve the value from the ROOTPAGE entry
									if (i_rootpage.Attributes[paramName] != null) {
										paramValue = i_rootpage.Attributes[paramName].Value;
									}
								}
							}
							

							// Only try to process the record field if it was captured, or has a recordlist entry
							// Sometimes certain fields are not captured on purpose, so we don't want to throw an error message in that case
							if (paramValue != null) {
								// Clean up the parameter value
								paramValue = paramValue.Replace("\n", "");
								paramValue = paramValue.Replace("\r", "");
								paramValue = paramValue.Replace("&nbsp;", " ");
								paramValue = paramValue.Replace("&#8209;", "-");
								paramValue = paramValue.Replace("&#8211;", "-");
								paramValue = paramValue.Trim();

								switch (recType.m_parameters[j].m_parameterType) {
									case ParameterType.StringParameter:
										rec.m_parameterValues[j] = HttpUtility.HtmlDecode(paramValue);
										break;
									case ParameterType.IntParameter:
										int intVal;
										if (int.TryParse(paramValue, out intVal)) rec.m_parameterValues[j] = intVal;
										else io_result.AppendErrorMessage(1, "WARNING: Could not parse INT value from '" + paramValue + "' for parameter '" + paramName + "' on page: " + i_url);
										break;
									case ParameterType.DateParameter:
										DateTime dateVal;
										if (dateFormat != null && dateFormat != "") {  // Use the specified date format
											if (DateTime.TryParseExact(paramValue, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateVal)) rec.m_parameterValues[j] = dateVal;
											else if (dateFormat2 != null && dateFormat2 != "" && DateTime.TryParseExact(paramValue, dateFormat2, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateVal)) rec.m_parameterValues[j] = dateVal;
											else io_result.AppendErrorMessage(1, "WARNING: Could not parse DATE value from '" + paramValue + "'  using FORMAT '" + dateFormat + "' for parameter '" + paramName + "' on page: " + i_url);
										} else {  // No format information specified
											if (DateTime.TryParse(paramValue, out dateVal)) rec.m_parameterValues[j] = dateVal;
											else io_result.AppendErrorMessage(1, "WARNING: Could not parse DATE value from '" + paramValue + "' for parameter '" + paramName + "' on page: " + i_url);
										}
										break;
									case ParameterType.BoolParameter:
										bool boolVal;
										if (bool.TryParse(paramValue, out boolVal)) rec.m_parameterValues[j] = boolVal;
										else io_result.AppendErrorMessage(1, "WARNING: Could not parse BOOL value from '" + paramValue + "' for parameter '" + paramName + "' on page: " + i_url);
										break;
									case ParameterType.DecimalParameter:
										// Remove optional dollar sign(s) and spaces after it
										paramValue = paramValue.Trim('$', ' ', '\t', '\r', '\n').Trim();
										// Replace a comma or space with a decimal point as the third last character - to handle french style money values and some typos
										if (paramValue.Length >= 3 && (paramValue.Substring(paramValue.Length - 3, 1) == "," || paramValue.Substring(paramValue.Length - 3, 1) == " ")) paramValue = paramValue.Substring(0, paramValue.Length - 3) + "." + paramValue.Substring(paramValue.Length - 2);
										// Remove any extra decimal points (probably typos, should be commas)
										int pos = paramValue.LastIndexOf('.');
										while (pos > 0) {
											pos = paramValue.LastIndexOf('.', pos-1);
											if (pos < 0) break;  // decimal not found
											paramValue = paramValue.Remove(pos, 1);
										}
										// Remove spaces or commas or quotes
										paramValue = paramValue.Replace(" ", "").Replace(",", "").Replace("\"", "");
										decimal decimalVal;
										if (decimal.TryParse(paramValue, out decimalVal)) rec.m_parameterValues[j] = decimalVal;
										else io_result.AppendErrorMessage(1, "WARNING: Could not parse DECIMAL value from '" + paramValue + "' for parameter '" + paramName + "' on page: " + i_url);
										break;
								}
							}
						}

						// Store the record in the database
						Cocoon.StoreRecord(databaseName, recType, rec, i_url, ref io_result);
						Cocoon.UpdateDataTable(databaseName, i_url, ref io_result);
					}

				}
			}

		}


		// Returns true if we haven't scanned the URL already and if it's on the same server
		private bool ValidateURL(Uri i_uri, string i_ignoreParameters, ref ResultList io_result) {
			// If the URL points to a different server, then skip it
			if (i_uri.Host != m_rootHost) {
				io_result.AppendErrorMessage(2, "INFO: URL points to different server " + i_uri);
				return false;
			}

			if (FindURI(i_uri, m_visitedLinks, i_ignoreParameters)) {
				io_result.AppendErrorMessage(2, "INFO: URL already scanned " + i_uri);
				return false;
			}
			m_visitedLinks.Add(i_uri);
			
			// Otherwise, it's good to scan
			return true;
		}

		// Returns true if the URI is not in the list
		private bool FindURI(Uri i_uri, ArrayList i_uriList, string i_ignoreParameters) {
			// If we've already scanned the URL, then skip it
			for (int i = 0; i < i_uriList.Count; i++) {
				if (IsSamePage((Uri)i_uriList[i], i_uri, i_ignoreParameters)) {
					return true;
				}
			}
			return false;
		}


		// Returns true if the two URIs point to the same page
		private bool IsSamePage(Uri i_uri1, Uri i_uri2, string i_ignoreParameters) {
			string query1 = i_uri1.Query.TrimStart('?');
			string query2 = i_uri2.Query.TrimStart('?');

			query1 = StripParameters(query1, i_ignoreParameters);
			query2 = StripParameters(query2, i_ignoreParameters);
			return i_uri1.Scheme == i_uri2.Scheme && i_uri1.Host == i_uri2.Host && i_uri1.Port == i_uri2.Port && i_uri1.AbsolutePath == i_uri2.AbsolutePath && query1 == query2;
		}

		// Strips the specified query string parameters
		private string StripParameters(string i_query, string i_ignoreParameters) {
			if (i_ignoreParameters == null) return i_query;
			string[] strip = i_ignoreParameters.Split(',');
			string[] parameters = i_query.Split('&');
			string result = "";
			bool toStrip;

			for (int i = 0; i < parameters.Length; i++) {
				string[] nameValue = parameters[i].Split('=');
				toStrip = false;
				for (int j = 0; j < strip.Length; j++) {
					if (strip[j] == nameValue[0]) toStrip = true;
				}
				if (!toStrip) result += parameters[i] + "&";
			}
			return result.Trim('&');
		}


		// Trim off everything before the prefix and after the suffix from the page before processing it
		// uses a RegEx here instead of just a static string for prefix and suffix
		private string TrimPage(string i_page, string i_prefix, string i_suffix, out Match o_prefix, out Match o_suffix, string i_url, ref ResultList io_result) {
			Regex rx;
			o_prefix = null;
			o_suffix = null;
			
			if (!string.IsNullOrEmpty(i_prefix)) {
				rx = new Regex(i_prefix, RegexOptions.IgnoreCase | RegexOptions.Singleline);
				o_prefix = rx.Match(i_page);
				if (!o_prefix.Success) {
					io_result.AppendErrorMessage(1, "WARNING: PREFIX '" + i_prefix + "' not found on page " + i_url);
					return null;
				} else {
					i_page = i_page.Substring(o_prefix.Index + o_prefix.Length);
				}
			}
			if (!string.IsNullOrEmpty(i_suffix)) {
				rx = new Regex(i_suffix, RegexOptions.IgnoreCase | RegexOptions.Singleline);
				o_suffix = rx.Match(i_page);
				if (!o_suffix.Success) {
					io_result.AppendErrorMessage(1, "WARNING: SUFFIX '" + i_suffix + "' not found on page " + i_url);
					return null;
				} else if (o_suffix.Length > 0) {
					// If we match a zero length string, then use the whole page, instead of truncating it
					i_page = i_page.Substring(0, o_suffix.Index);
				}
			}
			return i_page;
		}


		// Process a database definition in the config.xml
		private void ProcessDatabase(XmlNode i_config, ref ResultList io_result) {
			Cocoon.AddDatabase(i_config.Attributes["NAME"].Value, i_config.Attributes["CONNECTIONSTRING"].Value, i_config.Attributes["TABLE"].Value);
		}


		// Process a regex defintiion in the config.xml
		private void ProcessRegex(XmlNode i_config, ref ResultList io_result) {
			m_regexes.Add(i_config.Attributes["NAME"].Value, i_config.Attributes["REGEX"].Value);
		}


		// Process a recordtype definition in the config.xml
		private void ProcessRecordType(XmlNode i_config, ref ResultList io_result) {
			RecordType rec = new RecordType();
			ArrayList parms = new ArrayList();
			rec.m_typeName = i_config.Attributes["NAME"].Value;

			foreach (XmlNode child in i_config.ChildNodes) {
				if (child.Name == "PARAMETER") {
					Parameter p = new Parameter();
					p.m_parameterName = child.Attributes["NAME"].Value;
					string type = child.Attributes["TYPE"].Value;
					try {
						p.m_parameterType = (ParameterType)Enum.Parse(typeof(ParameterType), type, true);
					} catch {
						io_result.AppendErrorMessage(0, "ERROR: Could not interpret PARAMETER TYPE '" + type + "' for PARAMETER '" + p.m_parameterName + "' in RECORDTYPE '" + rec.m_typeName + "'");
						// Fatal error - do not continue
						throw new Exception("Could not interpret PARAMETER TYPE '" + type + "' for PARAMETER '" + p.m_parameterName + "' in RECORDTYPE '" + rec.m_typeName + "'");
					}
					parms.Add(p);
				}
			}

			if (parms.Count == 0) io_result.AppendErrorMessage(0, "ERROR: Record type '" + rec.m_typeName + "' contains no parameters");
			rec.m_parameters = (Parameter[])parms.ToArray(typeof(Parameter));
			m_recordTypes.Add(rec.m_typeName, rec);
		}


		// Retrieves the Page at the specified URL and returns it as a string
		private string GetPage(Uri i_url, string i_ignoreParameters, ref ResultList io_result) {
			// Only handle HTTP for now
			if (i_url.Scheme.ToUpper() != "HTTP") {
				io_result.AppendErrorMessage(2, "INFO: Skipping non-HTTP url " + i_url);
				io_result.UrlsSkipped += 1;
				return null;
			}

			io_result.AppendErrorMessage(3, "DEBUG: Loading URL: " + i_url);
			io_result.AppendUrlLoaded(i_url.AbsoluteUri);


			HttpWebRequest req = null;
			HttpWebResponse res = null;
			try {
				req = (HttpWebRequest)WebRequest.Create(i_url);
				req.CookieContainer = m_cookies;
				//req.Headers["Accept-Charset"] = null;
				//req.UserAgent = "Mozilla/5.0 (Caterpillar)";
				req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.2; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0)";
				try {
					res = (HttpWebResponse)req.GetResponse();
				} catch (WebException ex) {
					if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound) {
						// Handle 404 errors as a warning, not an actual error
						io_result.AppendErrorMessage(1, "WARNING: 404 Page Not Found " + i_url);
						return null;
					} else {
						throw;
					}
				}

				if (res.StatusCode == HttpStatusCode.NotFound) {
					io_result.AppendErrorMessage(1, "WARNING: 404 Page Not Found " + i_url);
					return null;
				} else if (res.StatusCode != HttpStatusCode.OK) {
					io_result.AppendErrorMessage(0, "ERROR: Cannot retrieve URL: " + i_url + "\r\nHTTP Status Code = " + res.StatusCode + " " + res.StatusDescription);
					return null;
				}

				// Don't visit the same URL twice 
				// This is checked before loading the page, but sometimes the response URI is 
				// different than the link URL, so oftentimes different links can point to the same page
				if (FindURI(req.Address, m_visitedUrls, i_ignoreParameters)) return null;
				m_visitedUrls.Add(req.Address);


				string page = null;

				if (res.ContentType.StartsWith("text/html")) {

					Stream result = res.GetResponseStream();
					// Use standard windows encoding if none specified so that special chars can be stored properly
					Encoding encoding = null;
					if (res.CharacterSet == null || res.CharacterSet == "") {
						// Try windows encoding first
						// TODO - am I supposed to assume ISO-8859-1 according to HTTP specs???
						encoding = Encoding.GetEncoding(1252);
						StreamReader reader = new StreamReader(result, encoding);
						page = reader.ReadToEnd();

						// Find the true encoding
						Encoding trueEncoding = FindEncoding(page);
						if (trueEncoding != null) {
							// Interpret the page under the true encoding
							page = trueEncoding.GetString(encoding.GetBytes(page));
						}
					} else {
						string charset = res.CharacterSet;
						charset = charset.Split(',')[0]; // in case they add stuff to it like one department did
						encoding = Encoding.GetEncoding(charset);
						StreamReader reader = new StreamReader(result, encoding);
						page = reader.ReadToEnd();
					}
					result.Close();

				} else if (res.ContentType.StartsWith("TODO -- PDF NOT SUPPORTED YET application/pdf")) {
					/*
					// Alternate way to retrieve the PDF
					//java.net.URL url = new java.net.URL(i_url.AbsoluteUri);
					//PDDocument doc = PDDocument.load(url);
					ikvm.io.InputStreamWrapper stream = new ikvm.io.InputStreamWrapper(res.GetResponseStream());
					PDDocument doc = PDDocument.load(stream);
					PDFTextStripper stripper = new PDFTextStripper();
					page = stripper.getText(doc);
					*/
					//OOGroup.Pdf.PdfReader reader = new OOGroup.Pdf.PdfReader(res.GetResponseStream());
					//OOGroup.Pdf.PdfReader reader = OOGroup.Pdf.PdfReader.GetPdfReader("try.pdf");
					//reader.WritePdf(File.Create("test.pdf"));

					PdfSharp.Pdf.PdfDocument doc = PdfSharp.Pdf.IO.PdfReader.Open(CopyStream(res.GetResponseStream()), PdfSharp.Pdf.IO.PdfDocumentOpenMode.ReadOnly);
					PDFParser parser = new PDFParser();
					page = "";
					for (int i = 0; i < doc.Pages.Count; i++) {
						for (int j = 0; j < doc.Pages[i].Contents.Elements.Count; j++) {
							PdfSharp.Pdf.PdfDictionary.PdfStream stream = doc.Pages[i].Contents.Elements.GetDictionary(j).Stream;
							// Print the original PDF stream source for developer debugging
							io_result.AppendErrorMessage(4, "DEVELOPER: PDF Stream:\r\n" + Encoding.ASCII.GetString(stream.Value));
							page += parser.ExtractTextFromPDFBytes(stream.Value);
						}
					}

					io_result.AppendErrorMessage(3, "INFO: Page Source:\r\n" + page);
				} else {
					io_result.AppendErrorMessage(1, "WARNING: Unexpected Content Type '" + res.ContentType + "' at URL: " + i_url);
				}

				return page;
			} finally {
				if (res != null) res.Close();
			}
		}

		// Finds an HTML encoding
		private Encoding FindEncoding(string i_page) {
			// Find all meta tags
			Regex meta = new Regex("<meta[^>]*>");
			MatchCollection matches = meta.Matches(i_page);
			foreach (Match match in matches) {
				int pos = match.Value.IndexOf("charset=");
				if (pos >= 0) {
					pos += 8;
					int endpos = match.Value.IndexOfAny(new char[] {' ', '\'', '"','\r','\n','\t'}, pos);
					if (endpos < 0) endpos = match.Value.Length;
					string enc = match.Value.Substring(pos, endpos - pos);
					Encoding result;
					try {
						result = Encoding.GetEncoding(enc);
					} catch {
						continue;
					}
					return result;
				}
			}
			return null;
		}

		// Copies the specified stream to a memorystream
		private MemoryStream CopyStream(Stream i_inputStream) {
			MemoryStream outputStream = new MemoryStream();
			byte[] buf = new byte[10240];

			while (true) {
				int count = i_inputStream.Read(buf, 0, buf.Length);
				if (count == 0) break;
				outputStream.Write(buf, 0, count);
			}

			return outputStream;
		}


    }
}
