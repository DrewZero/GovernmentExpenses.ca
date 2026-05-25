using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Metamorph {
	public partial class show_lowest_expense : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			int i;
			if (!int.TryParse(Request.QueryString["id"], out i)) Response.Redirect("/");

			int year = 0;
			int.TryParse(Request.QueryString["year"], out year);

			lblMember.Text = Request.QueryString["member_name"];
			Page.Title = "Canadian Government Expense for " + lblMember.Text;

			DataTable dt;
			if (year == 0) {
				dt = Reports.GetSmallestExpenses();
			} else {
				dt = Reports.GetSmallestExpensesForYear(year);
			}
			Reports.ShowIndividualExpense(pnlExpense, (Reports.ExpenseType)dt.Rows[i]["expense_type"], (int)dt.Rows[i]["id"]);
		}
	}
}
