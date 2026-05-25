using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Metamorph {
	public partial class custom_reports : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			ctlSearchTravel.SearchComplete += new EventHandler(SearchComplete);
			ctlSearchHospitality.SearchComplete += new EventHandler(SearchComplete);
		}

		public void SearchComplete(object sender, EventArgs e) {
			pnlChoose.Visible = false;
			pnlSubscribe.Visible = true;
		}

		protected void rdoTravel_CheckedChanged(object sender, EventArgs e) {
			if (rdoTravel.Checked) ctlMultiView.ActiveViewIndex = 0;
			else ctlMultiView.ActiveViewIndex = 1;
		}

		protected void rdoHospitality_CheckedChanged(object sender, EventArgs e) {
			if (rdoTravel.Checked) ctlMultiView.ActiveViewIndex = 0;
			else ctlMultiView.ActiveViewIndex = 1;
		}

	}
}