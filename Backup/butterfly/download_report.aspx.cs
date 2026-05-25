using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;


namespace Metamorph {
	public partial class download_report : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			// Check authorization here
			string username = Request.Cookies["username"] == null ? null : Request.Cookies["username"].Value;
			string password = Request.Cookies["password"] == null ? null : Request.Cookies["password"].Value;
			if (Page.IsPostBack) {
				username = txtUsername.Text;
				password = txtPassword.Text;
				Response.Cookies.Add(new HttpCookie("username", username));
				Response.Cookies.Add(new HttpCookie("password", password));
			}
			if (Reports.Authorize(username, password)) GenerateReport();
		}

		private void GenerateReport() {
			// Clear the response and get it ready for a file download
			Response.Clear();
			Response.ContentType = "text/csv";
			Response.AddHeader("Content-Disposition", "attachment;filename=report.csv");


			// Generate the report
			string reportType = Request.QueryString["t"];
			if (reportType == "t") {
				DateTime dateFrom = DateTime.Parse(Request.QueryString["df"].ToString());
				DateTime dateTo = DateTime.Parse(Request.QueryString["dt"].ToString());
				int? memberId = Reports.ParseInt(Request.QueryString["m"]);
				int? departmentId = Reports.ParseInt(Request.QueryString["dd"]);
				string purpose = Reports.ParseString(Request.QueryString["p"]);
				string destination = Reports.ParseString(Request.QueryString["d"]);
				decimal? commercialFlightMin = Reports.ParseDecimal(Request.QueryString["c1"]);
				decimal? commercialFlightMax = Reports.ParseDecimal(Request.QueryString["c2"]);
				decimal? charteredFlightMin = Reports.ParseDecimal(Request.QueryString["cf1"]);
				decimal? charteredFlightMax = Reports.ParseDecimal(Request.QueryString["cf2"]);
				decimal? governmentAircraftMin = Reports.ParseDecimal(Request.QueryString["g1"]);
				decimal? governmentAircraftMax = Reports.ParseDecimal(Request.QueryString["g2"]);
				decimal? otherTransportationMin = Reports.ParseDecimal(Request.QueryString["ot1"]);
				decimal? otherTransportationMax = Reports.ParseDecimal(Request.QueryString["ot2"]);
				decimal? accommodationMin = Reports.ParseDecimal(Request.QueryString["a1"]);
				decimal? accommodationMax = Reports.ParseDecimal(Request.QueryString["a2"]);
				decimal? mealsIncidentalsMin = Reports.ParseDecimal(Request.QueryString["m1"]);
				decimal? mealsIncidentalsMax = Reports.ParseDecimal(Request.QueryString["m2"]);
				decimal? otherMin = Reports.ParseDecimal(Request.QueryString["o1"]);
				decimal? otherMax = Reports.ParseDecimal(Request.QueryString["o2"]);
				decimal? totalMin = Reports.ParseDecimal(Request.QueryString["t1"]);
				decimal? totalMax = Reports.ParseDecimal(Request.QueryString["t2"]);

				DataTable dt = Reports.SearchTravel(dateFrom, dateTo, memberId, departmentId, purpose, destination, commercialFlightMin, commercialFlightMax, charteredFlightMin, charteredFlightMax, governmentAircraftMin, governmentAircraftMax, otherTransportationMin, otherTransportationMax, accommodationMin, accommodationMax, mealsIncidentalsMin, mealsIncidentalsMax, otherMin, otherMax, totalMin, totalMax);
				WriteCSVFile(dt, Response.OutputStream);
			} else if (reportType == "h") {
				DateTime dateFrom = DateTime.Parse(Request.QueryString["df"].ToString());
				DateTime dateTo = DateTime.Parse(Request.QueryString["dt"].ToString());
				int? memberId = Reports.ParseInt(Request.QueryString["m"]);
				int? departmentId = Reports.ParseInt(Request.QueryString["dd"]);
				string description = Reports.ParseString(Request.QueryString["d"]);
				string location = Reports.ParseString(Request.QueryString["l"]);
				string establishment = Reports.ParseString(Request.QueryString["e"]);
				int? employeesMin = Reports.ParseInt(Request.QueryString["e1"]);
				int? employeesMax = Reports.ParseInt(Request.QueryString["e2"]);
				int? guestsMin = Reports.ParseInt(Request.QueryString["g1"]);
				int? guestsMax = Reports.ParseInt(Request.QueryString["g1"]);
				decimal? totalMin = Reports.ParseInt(Request.QueryString["t1"]);
				decimal? totalMax = Reports.ParseInt(Request.QueryString["t1"]);
				
				DataTable dt = Reports.SearchHospitality(dateFrom, dateTo, memberId, departmentId, description, location, establishment, employeesMin, employeesMax, guestsMin, guestsMax, totalMin, totalMax);
				WriteCSVFile(dt, Response.OutputStream);
			}

			// Finish the response
			Response.End();
		}


		public void WriteCSVFile(DataTable dt, Stream i_stream) {
			StreamWriter sw = new StreamWriter(i_stream);

			// First we will write the headers.
			int iColCount = dt.Columns.Count;
			for (int i = 0; i < iColCount; i++) {
				sw.Write("\"" + dt.Columns[i] + "\"");
				if (i < iColCount - 1) {
					sw.Write(",");
				}
			}

			sw.Write(sw.NewLine);

			// Now write all the rows.
			foreach (DataRow dr in dt.Rows) {
				for (int i = 0; i < iColCount; i++) {
					if (!Convert.IsDBNull(dr[i])) {
						sw.Write("\"" + dr[i].ToString().Replace("\"", "\"\"") + "\"");
					}
					if (i < iColCount - 1) {
						sw.Write(",");
					}
				}
				sw.Write(sw.NewLine);
			}
			sw.Close();
		}


	}
}