using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Metamorph {
	public partial class drilldown : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			int memberId;
			int year = 0;

			int.TryParse(Request.QueryString["year"], out year);

			lblMember.Text = Request.QueryString["member_name"];

			Page.Title = "Canadian Government Expenses for " + Request.QueryString["member_name"];
			if (year > 0) Page.Title += " for Year " + year;
			
			PageUtils.AddDescription(Page, "Find out the details about the " + Page.Title);

			if (int.TryParse(Request.QueryString["member_id"], out memberId)) {
				if (year == 0) {
					Reports.ShowDrilldownReport(pnlReport, memberId);
				} else {
					Reports.ShowDrilldownReportForYear(pnlReport, memberId, year);
				}
			} else {
				Response.Redirect("/");
			}
		}
	}
}
