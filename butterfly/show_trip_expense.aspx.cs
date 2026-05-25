using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Metamorph {
	public partial class show_trip_expense : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			int i;
			int year = 0;
			if (!int.TryParse(Request.QueryString["id"], out i)) Response.Redirect("/");
			int.TryParse(Request.QueryString["year"], out year);

			lblMember.Text = Request.QueryString["member_name"];
			Page.Title = "Canadian Government Expense for " + lblMember.Text;

			DataTable dt;
			if (year != 0) {
				dt = Reports.GetLongestTrips(year);
			} else if (Request.QueryString["avg"] != null) {
				dt = Reports.GetLongestAverageTripLength();
				Response.Redirect("/drilldown.aspx?member_id=" + (int)dt.Rows[i]["member_id"] + "&member_name=" + (string)dt.Rows[i]["member_name"]);
			} else {
				dt = Reports.GetLongestTrips();
			}
			Reports.ShowIndividualExpense(pnlExpense, Reports.ExpenseType.Travel, (int)dt.Rows[i]["id"]);
		}
	}
}
