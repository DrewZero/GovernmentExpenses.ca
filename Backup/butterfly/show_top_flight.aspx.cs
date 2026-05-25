using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Metamorph {
	public partial class show_top_flight : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			int i;
			if (!int.TryParse(Request.QueryString["id"], out i)) Response.Redirect("/");
			
			int year = 0;
			int.TryParse(Request.QueryString["year"], out year);

			lblMember.Text = Request.QueryString["member_name"];
			Page.Title = "Canadian Government Expense for " + lblMember.Text;

			DataTable dt = null;
			if (year == 0) {
				dt = Reports.GetMostExpensiveFlights();
			} else {
				//dt = Reports.GetLargestExpensesForYear(year);
			}
			Reports.ShowIndividualExpense(pnlExpense, (Reports.ExpenseType)dt.Rows[i]["expense_type"], (int)dt.Rows[i]["id"]);
		}
	}
}
