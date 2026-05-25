using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Metamorph {
	public partial class average_amount : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			Page.Title = "Canadian Government Expenses - Average Amounts Claimed Per Expense";
			PageUtils.AddDescription(Page, "Find out the details about the " + Page.Title);

			ctlDepartments.DataSource = Reports.GetAveragesAcrossDepartments();
			ctlDepartments.DataBind();

			ctlHighestAverage.DataSource = Reports.GetHighestAverageClaims();
			ctlLowestAverage.DataSource = Reports.GetLowestAverageClaims();
		}
	}
}
