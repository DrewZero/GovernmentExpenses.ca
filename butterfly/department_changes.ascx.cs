using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Metamorph {
	public partial class department_changes : System.Web.UI.UserControl {
		public DataTable DataSource {
			set {
				ctlDepartmentChanges.DataSource = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e) {
			ctlDepartmentChanges.DataBind();
		}
	}
}