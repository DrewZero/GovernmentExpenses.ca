using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Metamorph {
	public partial class individual_expenses : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			Page.Title = "Canadian Government Expenses - Statistics About Individual Expenses";
			PageUtils.AddDescription(Page, "Find out the details about the " + Page.Title);

			ctlLargestExpenses.DataSource = Reports.GetLargestExpenses();
			ctlSmallestExpenses.DataSource = Reports.GetSmallestExpenses();
		}
	}
}
