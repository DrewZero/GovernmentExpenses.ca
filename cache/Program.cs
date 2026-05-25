using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Cache {
	class Program {
		static void Main(string[] args) {
			Crawl(new Uri("http://governmentexpenses.ca"), 0);
		}

		private static void Crawl(Uri i_url, int i_depth) {
			string page = GetPage(ref i_url);
			Regex hrefs = new Regex(@"<A\s+[^>]*HREF\s*=\s*('(?'URL'[^>']+)'|""(?'URL'[^>""]+)""|(?'URL'[^>\s]+))[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			MatchCollection matches;

			/*
			// Fetch the images
			Regex imgs = new Regex(@"<IMG\s+[^>]*SRC\s*=\s*('(?'URL'[^>']+)'|""(?'URL'[^>""]+)""|(?'URL'[^>\s]+))[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			matches = imgs.Matches(page);
			foreach (Match match in matches) {
				Uri newUrl = new Uri(i_url, match.Groups["URL"].Value);
				if (newUrl.Host == i_url.Host) GetPage(ref newUrl);
			}
			*/

			if (i_depth < 1) {
				matches = hrefs.Matches(page);
				foreach (Match match in matches) {
					Uri newUrl = new Uri(i_url, match.Groups["URL"].Value);
					if (newUrl.Host == i_url.Host) Crawl(newUrl, i_depth + 1);
				}
			}
		}

		private static string GetPage(ref Uri i_url) {
			HttpWebRequest req;
			HttpWebResponse res;
			req = (HttpWebRequest)WebRequest.Create(i_url);
			res = (HttpWebResponse)req.GetResponse();
			i_url = res.ResponseUri;
			StreamReader reader = new StreamReader(res.GetResponseStream());
			return reader.ReadToEnd();
		}

	}
}
