using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Metamorph {
	public partial class trip_duration : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			Page.Title = "Canadian Government Expenses - Trip Durations";
			PageUtils.AddDescription(Page, "Find out the details about the " + Page.Title);

			ctlLongestTrips.DataSource = Reports.GetLongestTrips();

			ctlLongestTrips2004.DataSource = Reports.GetLongestTrips(2004);
			ctlLongestTrips2004.QueryParameters = "year=2004";

			ctlLongestTrips2005.DataSource = Reports.GetLongestTrips(2005);
			ctlLongestTrips2005.QueryParameters = "year=2005";

			ctlLongestTrips2006.DataSource = Reports.GetLongestTrips(2006);
			ctlLongestTrips2006.QueryParameters = "year=2006";

			ctlLongestTrips2007.DataSource = Reports.GetLongestTrips(2007);
			ctlLongestTrips2007.QueryParameters = "year=2007";

			ctlLongestTrips2008.DataSource = Reports.GetLongestTrips(2008);
			ctlLongestTrips2008.QueryParameters = "year=2008";

			ctlLongestTrips2009.DataSource = Reports.GetLongestTrips(2009);
			ctlLongestTrips2009.QueryParameters = "year=2009";

			ctlLongestAverageTrips.DataSource = Reports.GetLongestAverageTripLength();
			ctlLongestAverageTrips.QueryParameters = "avg=1";
		}
	}
}
