using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

namespace Metamorph {
	public partial class search_hospitality : System.Web.UI.UserControl {

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
			if (!Page.IsValid) return;

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

			int? memberId = null;
			if (ddlMember.SelectedIndex > 0) memberId = int.Parse(ddlMember.SelectedValue);
			int? departmentId = null;
			if (ddlDepartment.SelectedIndex > 0) departmentId = int.Parse(ddlDepartment.SelectedValue);
			string description = null;
			if (txtDescription.Text != "" ) description = txtDescription.Text;
			string location = null;
			if (txtLocation.Text != "") location = txtLocation.Text;
			string establishment = null;
			if (txtEstablishment.Text != "") establishment = txtEstablishment.Text;
			int? employeesMin = Reports.ParseInt(txtEmployeesMin.Text);
			int? employeesMax = Reports.ParseInt(txtEmployeesMax.Text);
			int? guestsMin = Reports.ParseInt(txtGuestsMin.Text);
			int? guestsMax = Reports.ParseInt(txtGuestsMax.Text);
			decimal? totalMin = Reports.ParseDecimal(txtTotalMin.Text);
			decimal? totalMax = Reports.ParseDecimal(txtTotalMax.Text);

			DataTable dt = Reports.SearchHospitality(dateFrom, dateTo, memberId, departmentId, description, location, establishment, employeesMin, employeesMax, guestsMin, guestsMax, totalMin, totalMax);
			pnlResults.Visible = true;
			pnlCriteria.Visible = false;
			lblResultCount.Text = dt.Rows.Count.ToString();
			lnkDownloadReport.NavigateUrl = "/download_report.aspx?t=h" 
				+ "&df=" + HttpUtility.UrlEncode(dateFrom.ToString("yyyy-MM-dd"))
				+ "&dt=" + HttpUtility.UrlEncode(dateTo.ToString("yyyy-MM-dd"))
				+ "&m=" + HttpUtility.UrlEncode(memberId.ToString())
				+ "&dd=" + HttpUtility.UrlEncode(departmentId.ToString())
				+ "&d=" + HttpUtility.UrlEncode(txtDescription.Text)
				+ "&l=" + HttpUtility.UrlEncode(txtLocation.Text)
				+ "&e=" + HttpUtility.UrlEncode(txtEstablishment.Text)
				+ "&=e1" + HttpUtility.UrlEncode(txtEmployeesMin.Text)
				+ "&=e2" + HttpUtility.UrlEncode(txtEmployeesMax.Text)
				+ "&=g1" + HttpUtility.UrlEncode(txtGuestsMin.Text)
				+ "&=g2" + HttpUtility.UrlEncode(txtGuestsMax.Text)
				+ "&=t1" + HttpUtility.UrlEncode(txtTotalMin.Text)
				+ "&=t2" + HttpUtility.UrlEncode(txtTotalMax.Text)
				;
			
			SearchComplete(this, e);
		}
	}
}