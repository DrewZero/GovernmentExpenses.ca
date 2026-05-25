using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Metamorph {
	public partial class name_mappings : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			string letter = Request.QueryString["letter"];
			if (letter != null) Reports.ShowNameMappings(pnlNameMappings, letter);
		}
	}
}
