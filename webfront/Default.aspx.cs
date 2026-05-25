using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Net;

namespace webfront {
	public partial class _Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

		}

		protected void btnExecute_Click(object sender, EventArgs e) {
			try {
				litOutput.Text = "Process is now running...";

				//System.Diagnostics.Process.Start(System.Configuration.ConfigurationManager.AppSettings["Program"], "ObscureAdministrator", password, null);

				System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(System.Configuration.ConfigurationManager.AppSettings["Program"]);
				System.IO.FileInfo finfo = new System.IO.FileInfo(psi.FileName);
				psi.WorkingDirectory = finfo.DirectoryName;
				psi.RedirectStandardOutput = true;
				psi.RedirectStandardInput = true;
				psi.RedirectStandardError = true;
				psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;
				psi.Arguments = txtArguments.Text;
				System.Diagnostics.Process listFiles;
				listFiles = System.Diagnostics.Process.Start(psi);
				listFiles.StandardInput.WriteLine("");
				System.IO.StreamReader myOutput = listFiles.StandardOutput;
				listFiles.WaitForExit(2000);
				if (listFiles.HasExited) {
					string output = myOutput.ReadToEnd();
					litOutput.Text = output;
				}
			} catch (Exception ex) {
				litOutput.Text = ex.Message + ex.StackTrace;
			}
		}

		private void KillAllProcesses(string name) {
			Process[] processes = Process.GetProcessesByName(name);
			foreach (Process p in processes)
				p.Kill();
		}
		
		protected void btnKill_Click(object sender, EventArgs e) {
			FileInfo filename = new FileInfo(ConfigurationManager.AppSettings["Program"]);
			string name = filename.Name;
			name = name.Substring(0, name.Length - filename.Extension.Length);
			KillAllProcesses(name);
		}

		protected void btnUpdateCache_Click(object sender, EventArgs e) {
			int i;

			// Touch the files
			i = 0;
			while (true) {
				string file = ConfigurationManager.AppSettings["FileToTouch" + i];
				if (file == null) break;
				File.SetLastWriteTime(file, DateTime.Now);
				i += 1;
			}

			// Load the URLs
			i = 0;
			while (true) {
				string url = ConfigurationManager.AppSettings["URL"+i];
				if (url == null) break;
				GetUrl(url);
				i += 1;
			}

			if (ConfigurationManager.AppSettings["FolderToDelete"] != null) {
				// Maybe to do... could be dangerous
			}

		}

		private void GetUrl(string i_url) {
			HttpWebRequest req;
			HttpWebResponse res;
			req = (HttpWebRequest)WebRequest.Create(i_url);
			res = (HttpWebResponse)req.GetResponse();
		}

	}
}
