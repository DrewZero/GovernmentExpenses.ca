using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Metamorph {
	public partial class show_top_titles : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			Page.Title = "Canadian Government Expenses - Top Spenders Per Title";
			PageUtils.AddDescription(Page, "Find out the details about the " + Page.Title);

			ctlTopSpenders.DataSource = Reports.GetTopTitles();
			ctlTopSpenders.DataBind();
		}
	}
}
