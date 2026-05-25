using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Metamorph {
	public partial class smallest_expenses : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			Page.Title = "Canadian Government Expenses - Smallest Expenses";
			PageUtils.AddDescription(Page, "Find out the details about the smallest Canadian government travel and hospitality expenses ever claimed since 2003.");

			ctlSmallestExpenses.DataSource = Reports.GetSmallestExpenses();

			ctlSmallestExpenses2009.DataSource = Reports.GetSmallestExpensesForYear(2009);
			ctlSmallestExpenses2009.QueryParameters = "year=2009";
			ctlSmallestExpenses2008.DataSource = Reports.GetSmallestExpensesForYear(2008);
			ctlSmallestExpenses2008.QueryParameters = "year=2008";
			ctlSmallestExpenses2007.DataSource = Reports.GetSmallestExpensesForYear(2007);
			ctlSmallestExpenses2007.QueryParameters = "year=2007";
			ctlSmallestExpenses2006.DataSource = Reports.GetSmallestExpensesForYear(2006);
			ctlSmallestExpenses2006.QueryParameters = "year=2006";
			ctlSmallestExpenses2005.DataSource = Reports.GetSmallestExpensesForYear(2005);
			ctlSmallestExpenses2005.QueryParameters = "year=2005";
			ctlSmallestExpenses2004.DataSource = Reports.GetSmallestExpensesForYear(2004);
			ctlSmallestExpenses2004.QueryParameters = "year=2004";
		}
	}
}
