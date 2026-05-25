using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;


namespace Metamorph {
	public partial class search_travel : System.Web.UI.UserControl {
		
		public event EventHandler SearchComplete;


		protected void Page_Load(object sender, EventArgs e) {
			pnlResults.Visible = false;

			if (!Page.IsPostBack) {
				for (int y = 2003; y <= DateTime.Today.Year; y++) {
					ddlYearFrom.Items.Add(y.ToString());
					ddlYearTo.Items.Add(y.ToString());
				}
				ddlYearFrom.SelectedIndex = 0;
				ddlYearTo.SelectedIndex = ddlYearTo.Items.Count - 1;

				for (int m = 1; m <= 12; m++) {
					ddlMonthFrom.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m), m.ToString()));
					ddlMonthTo.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m), m.ToString()));
				}
				ddlMonthFrom.SelectedIndex = 9;
				ddlMonthTo.SelectedIndex = DateTime.Today.Month - 1;

				for (int d = 1; d <= 31; d++) {
					ddlDayFrom.Items.Add(d.ToString());
					ddlDayTo.Items.Add(d.ToString());
				}
				ddlDayFrom.SelectedIndex = 0;
				ddlDayTo.SelectedIndex = DateTime.Today.Day - 1;


				DataTable dt = Reports.GetMembers();
				for (int i = 0; i < dt.Rows.Count; i++) {
					ddlMember.Items.Add(new ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["id"].ToString()));
				}

				dt = Reports.GetDepartments();
				DataRow[] rows = dt.Select(null, "department_name asc");
				for (int i = 0; i < rows.Length; i++) {
					ddlDepartment.Items.Add(new ListItem(rows[i]["department_name"].ToString(), rows[i]["department_id"].ToString()));
				}

			}
		}

		protected void btnSearch_Click(object sender, EventArgs e) {

			DateTime dateFrom = DateTime.Today;
			try {
				dateFrom = new DateTime(ddlYearFrom.SelectedIndex + 2003, ddlMonthFrom.SelectedIndex + 1, ddlDayFrom.SelectedIndex + 1);
			} catch {
				ctlValidateDateFrom.IsValid = false;
			}

			DateTime dateTo = DateTime.Today;
			try {
				dateTo = new DateTime(ddlYearTo.SelectedIndex + 2003, ddlMonthTo.SelectedIndex + 1, ddlDayTo.SelectedIndex + 1);
			} catch {
				ctlValidateDateTo.IsValid = false;
			}

			if (!Page.IsValid) return;

			int? memberId = null;
			if (ddlMember.SelectedIndex > 0) memberId = int.Parse(ddlMember.SelectedValue);
			int? departmentId = null;
			if (ddlDepartment.SelectedIndex > 0) departmentId = int.Parse(ddlDepartment.SelectedValue);
			string purpose = null;
			if (txtPurpose.Text != "") purpose = txtPurpose.Text;
			string destination = null;
			if (txtDestination.Text != "") destination = txtDestination.Text;
			decimal? commercialFlightMin = Reports.ParseDecimal(txtCommercialFlightMin.Text);
			decimal? commercialFlightMax = Reports.ParseDecimal(txtCommercialFlightMax.Text);
			decimal? charteredFlightMin = Reports.ParseDecimal(txtCharteredFlightMin.Text);
			decimal? charteredFlightMax = Reports.ParseDecimal(txtCharteredFlightMax.Text);
			decimal? governmentAircraftMin = Reports.ParseDecimal(txtGovernmentAircraftMin.Text);
			decimal? governmentAircraftMax = Reports.ParseDecimal(txtGovernmentAircraftMax.Text);
			decimal? otherTransportationMin = Reports.ParseDecimal(txtOtherTransportationMin.Text);
			decimal? otherTransportationMax = Reports.ParseDecimal(txtOtherTransportationMax.Text);
			decimal? accommodationMin = Reports.ParseDecimal(txtAccommodationMin.Text);
			decimal? accommodationMax = Reports.ParseDecimal(txtAccommodationMax.Text);
			decimal? mealsIncidentalsMin = Reports.ParseDecimal(txtMealsIncidentalsMin.Text);
			decimal? mealsIncidentalsMax = Reports.ParseDecimal(txtMealsIncidentalsMax.Text);
			decimal? otherMin = Reports.ParseDecimal(txtOtherMin.Text);
			decimal? otherMax = Reports.ParseDecimal(txtOtherMax.Text);
			decimal? totalMin = Reports.ParseDecimal(txtTotalMin.Text);
			decimal? totalMax = Reports.ParseDecimal(txtTotalMax.Text);


			DataTable dt = Reports.SearchTravel(dateFrom, dateTo, memberId, departmentId, purpose, destination, commercialFlightMin, commercialFlightMax, charteredFlightMin, charteredFlightMax, governmentAircraftMin, governmentAircraftMax, otherTransportationMin, otherTransportationMax, accommodationMin, accommodationMax, mealsIncidentalsMin, mealsIncidentalsMax, otherMin, otherMax, totalMin, totalMax);
			pnlResults.Visible = true;
			pnlCriteria.Visible = false;
			lblResultCount.Text = dt.Rows.Count.ToString();
			lnkDownloadReport.NavigateUrl = "/download_report.aspx?t=t"
				+ "&df=" + HttpUtility.UrlEncode(dateFrom.ToString("yyyy-MM-dd"))
				+ "&dt=" + HttpUtility.UrlEncode(dateTo.ToString("yyyy-MM-dd"))
				+ "&m=" + HttpUtility.UrlEncode(memberId.ToString())
				+ "&dd=" + HttpUtility.UrlEncode(departmentId.ToString())
				+ "&p=" + HttpUtility.UrlEncode(txtPurpose.Text)
				+ "&d=" + HttpUtility.UrlEncode(txtDestination.Text)
				+ "&=c1" + HttpUtility.UrlEncode(txtCommercialFlightMin.Text)
				+ "&=c2" + HttpUtility.UrlEncode(txtCommercialFlightMax.Text)
				+ "&=cf1" + HttpUtility.UrlEncode(txtCharteredFlightMin.Text)
				+ "&=cf2" + HttpUtility.UrlEncode(txtCharteredFlightMax.Text)
				+ "&=g1" + HttpUtility.UrlEncode(txtGovernmentAircraftMin.Text)
				+ "&=g2" + HttpUtility.UrlEncode(txtGovernmentAircraftMax.Text)
				+ "&=ot1" + HttpUtility.UrlEncode(txtOtherTransportationMin.Text)
				+ "&=ot2" + HttpUtility.UrlEncode(txtOtherTransportationMax.Text)
				+ "&=a1" + HttpUtility.UrlEncode(txtAccommodationMin.Text)
				+ "&=a2" + HttpUtility.UrlEncode(txtAccommodationMax.Text)
				+ "&=m1" + HttpUtility.UrlEncode(txtMealsIncidentalsMin.Text)
				+ "&=m2" + HttpUtility.UrlEncode(txtMealsIncidentalsMax.Text)
				+ "&=o1" + HttpUtility.UrlEncode(txtOtherMin.Text)
				+ "&=o2" + HttpUtility.UrlEncode(txtOtherMax.Text)
				+ "&=t1" + HttpUtility.UrlEncode(txtTotalMin.Text)
				+ "&=t2" + HttpUtility.UrlEncode(txtTotalMax.Text)
				;

			SearchComplete(this, e);
		}

	}
}