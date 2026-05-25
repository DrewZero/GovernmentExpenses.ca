using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Metamorph {
	public partial class _Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			lblTotal.Text = Reports.GetTotalSpent().ToString("C");
			lblCount.Text = Reports.GetExpenseCount().ToString();

			ctlTopSpenders.DataSource = Reports.GetTopSpenders();

			ctlTopSpendersForYear2004.DataSource = Reports.GetTopSpendersForYear(2004);
			ctlTopSpendersForYear2004.QueryParameters = "year=2004";
			ctlTopSpendersForYear2005.DataSource = Reports.GetTopSpendersForYear(2005);
			ctlTopSpendersForYear2005.QueryParameters = "year=2005";
			ctlTopSpendersForYear2006.DataSource = Reports.GetTopSpendersForYear(2006);
			ctlTopSpendersForYear2006.QueryParameters = "year=2006";
			ctlTopSpendersForYear2007.DataSource = Reports.GetTopSpendersForYear(2007);
			ctlTopSpendersForYear2007.QueryParameters = "year=2007";
			ctlTopSpendersForYear2008.DataSource = Reports.GetTopSpendersForYear(2008);
			ctlTopSpendersForYear2008.QueryParameters = "year=2008";
			ctlTopSpendersForYear2009.DataSource = Reports.GetTopSpendersForYear(2009);
			ctlTopSpendersForYear2009.QueryParameters = "year=2009";
			ctlTopSpendersForYear2010.DataSource = Reports.GetTopSpendersForYear(2010);
			ctlTopSpendersForYear2010.QueryParameters = "year=2010";


			ctlDepartments.DataSource = Reports.GetDepartments();
			ctlDepartments.DataBind();


			ctlDepartmentIncreases.DataSource = Reports.GetLargestDepartmentIncreases();
			ctlDepartmentDecreases.DataSource = Reports.GetLargestDepartmentDecreases();
		}
	}
}
