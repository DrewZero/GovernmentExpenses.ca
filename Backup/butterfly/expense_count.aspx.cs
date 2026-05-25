using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Metamorph {
	public partial class expense_count : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			Page.Title = "Canadian Government Expenses - Total Number of Expenses Claimed";
			PageUtils.AddDescription(Page, "Find out the details about the " + Page.Title);

			lblCount.Text = Reports.GetExpenseCount().ToString();

			ctlRangeReport.DataSource = Reports.GetRangeReport();
			ctlRangeReport.DataBind();

			ctlMostExpenses2004.DataSource = Reports.GetMostExpensesForYear(2004);
			ctlMostExpenses2004.QueryParameters = "year=2004";
			ctlMostExpenses2005.DataSource = Reports.GetMostExpensesForYear(2005);
			ctlMostExpenses2005.QueryParameters = "year=2005";
			ctlMostExpenses2006.DataSource = Reports.GetMostExpensesForYear(2006);
			ctlMostExpenses2006.QueryParameters = "year=2006";
			ctlMostExpenses2007.DataSource = Reports.GetMostExpensesForYear(2007);
			ctlMostExpenses2007.QueryParameters = "year=2007";
			ctlMostExpenses2008.DataSource = Reports.GetMostExpensesForYear(2008);
			ctlMostExpenses2008.QueryParameters = "year=2008";
			ctlMostExpenses2009.DataSource = Reports.GetMostExpensesForYear(2009);
			ctlMostExpenses2009.QueryParameters = "year=2009";


			ctlLeastExpenses2004.DataSource = Reports.GetLeastExpensesForYear(2004);
			ctlLeastExpenses2004.QueryParameters = "year=2004";
			ctlLeastExpenses2005.DataSource = Reports.GetLeastExpensesForYear(2005);
			ctlLeastExpenses2005.QueryParameters = "year=2005";
			ctlLeastExpenses2006.DataSource = Reports.GetLeastExpensesForYear(2006);
			ctlLeastExpenses2006.QueryParameters = "year=2006";
			ctlLeastExpenses2007.DataSource = Reports.GetLeastExpensesForYear(2007);
			ctlLeastExpenses2007.QueryParameters = "year=2007";
			ctlLeastExpenses2008.DataSource = Reports.GetLeastExpensesForYear(2008);
			ctlLeastExpenses2008.QueryParameters = "year=2008";
			ctlLeastExpenses2009.DataSource = Reports.GetLeastExpensesForYear(2009);
			ctlLeastExpenses2009.QueryParameters = "year=2009";
		}
	}
}
