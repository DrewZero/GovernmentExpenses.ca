using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Metamorph {
	public partial class lowest_expenses : System.Web.UI.UserControl {

		protected string m_query;

		public DataTable DataSource {
			set {
				ctlLowestExpenses.DataSource = value;
			}
		}

		public string QueryParameters {
			set {
				m_query = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e) {
			ctlLowestExpenses.DataBind();
		}
	}
}