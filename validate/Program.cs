using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace validate {
	class Program {
		
		private static string m_connStr = "Data Source=bine.ca;Database=metamorph;uid=caterpillar;pwd=metamorphosis";

		static void Main(string[] args) {
			SqlConnection conn = new SqlConnection(m_connStr);
			SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM members", conn);
			DataTable dt = new DataTable();
			//conn.Open();
			adapter.Fill(dt);

			// Go through each member name and see if it's the same as any others without accents
			for (int i = 0; i < dt.Rows.Count; i++) {
				string name = RemoveDiacritics(dt.Rows[i]["name"].ToString());
				for (int j = i+1; j < dt.Rows.Count; j++) {
					if (name == RemoveDiacritics(dt.Rows[j]["name"].ToString())) {
						// Found a match
						Console.WriteLine("ID = " + dt.Rows[i]["id"].ToString() + ", ID = " + dt.Rows[j]["id"].ToString() + ", NAME = " + name);
					}
				}
			}
			Console.WriteLine("ALL DONE!");
			Console.ReadLine();
		}

		static string RemoveDiacritics(string stIn) {
			string stFormD = stIn.Normalize(NormalizationForm.FormD);
			StringBuilder sb = new StringBuilder();

			for (int ich = 0; ich < stFormD.Length; ich++) {
				UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
				if (uc != UnicodeCategory.NonSpacingMark) {
					sb.Append(stFormD[ich]);
				}
			}

			return (sb.ToString().Normalize(NormalizationForm.FormC));
		}

	}
}
