using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Metamorph {
	public partial class largest_expenses : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			Page.Title = "Canadian Government Expenses - Largest Expenses";
			PageUtils.AddDescription(Page, "Find out the details about the largest Canadian government travel and hospitality expenses ever claimed since 2003.");

			ctlLargestExpenses.DataSource = Reports.GetLargestExpenses();
			ctlMostExpensiveFlights.DataSource = Reports.GetMostExpensiveFlights();

			ctlLargestExpenses2009.DataSource = Reports.GetLargestExpensesForYear(2009);
			ctlLargestExpenses2009.QueryParameters = "year=2009";
			ctlLargestExpenses2008.DataSource = Reports.GetLargestExpensesForYear(2008);
			ctlLargestExpenses2008.QueryParameters = "year=2008";
			ctlLargestExpenses2007.DataSource = Reports.GetLargestExpensesForYear(2007);
			ctlLargestExpenses2007.QueryParameters = "year=2007";
			ctlLargestExpenses2006.DataSource = Reports.GetLargestExpensesForYear(2006);
			ctlLargestExpenses2006.QueryParameters = "year=2006";
			ctlLargestExpenses2005.DataSource = Reports.GetLargestExpensesForYear(2005);
			ctlLargestExpenses2005.QueryParameters = "year=2005";
			ctlLargestExpenses2004.DataSource = Reports.GetLargestExpensesForYear(2004);
			ctlLargestExpenses2004.QueryParameters = "year=2004";
		}
	}
}
