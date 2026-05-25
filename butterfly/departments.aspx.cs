using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using WebChart;

namespace Metamorph {
	public partial class departments : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			int id;
			if (!int.TryParse(Request.QueryString["id"], out id)) Response.Redirect("/");

			lblDepartment.Text = Request.QueryString["department"];
			lblDepartment1.Text = lblDepartment.Text;
			lblDepartment2.Text = lblDepartment.Text;
			lblDepartment3.Text = lblDepartment.Text;
			lblTotal.Text = Reports.GetDepartmentTotal(id).ToString("C");

			Page.Title = "Canadian Government Expenses - " + lblDepartment.Text;

			ctlTopSpenders.DataSource = Reports.GetTopSpendersForDepartment(id);
			ctlTopSpenders.DataBind();

			DataTable yearData = Reports.GetYearTotalsForDepartment(id);
			ctlYearTotal.DataSource = yearData;

			ctlTopSpendersForYear2004.DataSource = Reports.GetTopSpendersForYearAndDepartment(2004, id);
			ctlTopSpendersForYear2004.QueryParameters = "year=2004";
			ctlTopSpendersForYear2005.DataSource = Reports.GetTopSpendersForYearAndDepartment(2005, id);
			ctlTopSpendersForYear2005.QueryParameters = "year=2005";
			ctlTopSpendersForYear2006.DataSource = Reports.GetTopSpendersForYearAndDepartment(2006, id);
			ctlTopSpendersForYear2006.QueryParameters = "year=2006";
			ctlTopSpendersForYear2007.DataSource = Reports.GetTopSpendersForYearAndDepartment(2007, id);
			ctlTopSpendersForYear2007.QueryParameters = "year=2007";
			ctlTopSpendersForYear2008.DataSource = Reports.GetTopSpendersForYearAndDepartment(2008, id);
			ctlTopSpendersForYear2008.QueryParameters = "year=2008";
			ctlTopSpendersForYear2009.DataSource = Reports.GetTopSpendersForYearAndDepartment(2009, id);
			ctlTopSpendersForYear2009.QueryParameters = "year=2009";
			ctlTopSpendersForYear2010.DataSource = Reports.GetTopSpendersForYearAndDepartment(2010, id);
			ctlTopSpendersForYear2010.QueryParameters = "year=2010";


			// Draw the year totals chart
			SmoothLineChart objChart = new SmoothLineChart();
			for (int i = 0; i < yearData.Rows.Count; i++) {
				string year = yearData.Rows[i]["year"].ToString();
				float total = Convert.ToSingle((decimal)yearData.Rows[i]["total"]);
				objChart.Data.Add(new ChartPoint(year, total));
			}
			ctlChart.Charts.Add(objChart);

			// Set the chart options and display strings
			ChartText title = new ChartText();
			title.Text = "Spending Trends for " + Request.QueryString["department"].ToString();
			ctlChart.ChartTitle = title;

			ctlChart.RedrawChart();
		}
	}
}
