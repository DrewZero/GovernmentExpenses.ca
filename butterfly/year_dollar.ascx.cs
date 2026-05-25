using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Metamorph {
	public partial class year_dollar : System.Web.UI.UserControl {
		public DataTable DataSource {
			set {
				ctlYearValue.DataSource = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e) {
			ctlYearValue.DataBind();
		}
	}
}