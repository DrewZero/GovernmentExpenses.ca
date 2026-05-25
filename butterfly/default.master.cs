using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Metamorph {
	public partial class _default : System.Web.UI.MasterPage {

		protected void Page_Load(object sender, EventArgs e) {
			//Response.Redirect("/onstrike.aspx");  // TODO - on strike


            if (Request.Url.AbsolutePath == "/custom_reports.aspx") {
				pnlTopAds.Visible = false;
			} else {
				pnlTopAds.Visible = true;
			}

			Response.Cache.SetExpires(DateTime.Now.AddMonths(1));
			Response.Cache.VaryByParams["*"] = true;
			Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
			Response.Cache.SetValidUntilExpires(true);
			

			lblGenerated.Text = "Generated on " + DateTime.Now.ToString();
		}


	}
}
