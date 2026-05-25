using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Metamorph {
	public class Reports {

		public enum ExpenseType {
			Travel = 0,
			Hospitality = 1
		}

		public static DataTable GetMembers() {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("get_members");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetTopTitles() {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_top_titles");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetLongestTrips() {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_longest_trips");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return dt;
		}

		public static DataTable GetLargestDepartmentIncreases() {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_largest_department_increases");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return dt;
		}

		public static DataTable GetLargestDepartmentDecreases() {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_largest_department_decreases");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return dt;
		}

		public static DataTable GetLongestAverageTripLength() {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_longest_average_trip_length");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return dt;
		}

		public static DataTable GetLongestTrips(int i_year) {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_longest_trips_by_year");
			cmd.Parameters.AddWithValue("@year", i_year);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return dt;
		}

		public static decimal GetDepartmentTotal(int i_deptId) {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("get_department_total");
			cmd.Parameters.AddWithValue("@department_id", i_deptId);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			object result = dt.Rows[0][0];
			if (result == DBNull.Value) return 0;
			else return (decimal)result;
		}

		public static decimal GetTotalSpent() {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("get_total_spent");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return (decimal)dt.Rows[0][0];
		}

		public static DataTable GetDepartments() {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("get_departments");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return dt;
		}

		public static void ShowDepartments(Panel i_outputPanel) {
			DataTable dt = GetDepartments();

			Table tblDepartments = new Table();
			tblDepartments.Width = Unit.Percentage(100);

			TableRow row = null;
			TableCell cell;
			for (int i = 0; i < dt.Rows.Count; i++) {
				if (i % 2 == 0) {
					row = new TableRow();
					tblDepartments.Rows.Add(row);
				}
				cell = new TableCell();
				HyperLink link = new HyperLink();
				link.NavigateUrl = "/departments.aspx?id=" + dt.Rows[i]["id"];
				link.Text = dt.Rows[i]["department_name"].ToString();
				cell.Controls.Add(link);
				row.Cells.Add(cell);
			}

			i_outputPanel.Controls.Add(tblDepartments);
		}

		public static DataTable GetLargestExpenses() {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_largest_expenses");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetMostExpensiveFlights() {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_most_expensive_flights");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetLargestExpensesForYear(int i_year) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_largest_expenses_by_year");
			cmd.Parameters.AddWithValue("@year", i_year);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetSmallestExpenses() {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_smallest_expenses");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetSmallestExpensesForYear(int i_year) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_smallest_expenses_by_year");
			cmd.Parameters.AddWithValue("@year", i_year);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetTopSpenders() {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_top_spenders");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetTopSpendersForYear(int i_year) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_top_spenders_by_year");
			cmd.Parameters.AddWithValue("@year", i_year);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetAveragesAcrossDepartments() {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_averages_across_departments");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetHighestAverageClaims() {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_highest_average_claims");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetLowestAverageClaims() {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_lowest_average_claims");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetTopSpendersForYearAndDepartment(int i_year, int i_deptId) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_top_spenders_by_year_and_department");
			cmd.Parameters.AddWithValue("@year", i_year);
			cmd.Parameters.AddWithValue("@dept_id", i_deptId);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetMostExpensesForYear(int i_year) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_most_expenses_by_year");
			cmd.Parameters.AddWithValue("@year", i_year);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetLeastExpensesForYear(int i_year) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_least_expenses_by_year");
			cmd.Parameters.AddWithValue("@year", i_year);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetTopSpendersForDepartment(int i_deptId) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_top_spenders_by_department");
			cmd.Parameters.AddWithValue("@dept_id", i_deptId);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public static DataTable GetYearTotalsForDepartment(int i_deptId) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_year_total_by_department");
			cmd.Parameters.AddWithValue("@dept_id", i_deptId);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}
	
/*

		// Writes the data table to the output stream on the specified panel
		public static void ShowReport(Table i_tblOutput, DataTable i_dt, int i_year) {

			// Output all the result rows
			for (int r = 0; r < i_dt.Rows.Count; r++) {
				TableRow row = new TableRow();
				
				TableCell cell = new TableCell();
				HyperLink link = new HyperLink();
				link.Text = HttpUtility.HtmlEncode(i_dt.Rows[r]["name"].ToString());
				link.NavigateUrl = "/drilldown.aspx?id=" + i_dt.Rows[r]["id"] + "&year=" + i_year + "&name=" + HttpUtility.UrlEncode((string)i_dt.Rows[r]["name"]);
				nameCell.Controls.Add(link);
				row.Cells.Add(nameCell);

				TableCell departmentCell = new TableCell();
				departmentCell.Text = i_dt.Rows[r]["department_name"].ToString();
				row.Cells.Add(departmentCell);

				TableCell valueCell = new TableCell();
				valueCell.Text = ((decimal)i_dt.Rows[r]["total"]).ToString("C");
				row.Cells.Add(valueCell);

				ctlTable.Rows.Add(row);
			}

		}
*/


		private static void RenderExpenseHeader(Panel i_outputPanel, ExpenseType i_expenseType) {
			Literal header = new Literal();
			if (i_expenseType == ExpenseType.Travel) {
				header.Text = "<h4>Travel Expense</h4>";
			} else if (i_expenseType == ExpenseType.Hospitality) {
				header.Text = "<h4>Hospitality Expense</h4>";
			}
			i_outputPanel.Controls.Add(header);
		}

		private static void RenderTravelExpense(Panel i_outputPanel, DataRow i_row) {
			Table ctlTable = new Table();
			TableRow row;
			TableHeaderCell header;
			TableCell cell;

			ctlTable.CssClass = "tablelist";
			ctlTable.Width = 600;

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Width = 150;
			header.Text = "Member Name";
			cell.Text = HttpUtility.HtmlEncode(i_row["original_name"].ToString());
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Title";
			cell.Text = i_row["original_title"].ToString();
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Purpose";
			cell.Text = HttpUtility.HtmlEncode(i_row["purpose"].ToString());
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Date From";
			if (i_row["date_from"] == DBNull.Value) {
				cell.Text = "Unknown Date";
			} else {
				cell.Text = ((DateTime)i_row["date_from"]).ToString("yyyy-MM-dd");
			}
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Date To";
			if (i_row["date_to"] == DBNull.Value) {
				cell.Text = "Unknown Date";
			} else {
				cell.Text = ((DateTime)i_row["date_to"]).ToString("yyyy-MM-dd");
			}
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Destination";
			cell.Text = HttpUtility.HtmlEncode(i_row["destination"].ToString());
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			if (i_row["commercial_flight"] != DBNull.Value) {
				row = new TableRow();
				header = new TableHeaderCell();
				cell = new TableCell();
				header.Text = "Commercial Flight";
				cell.Text = ((decimal)i_row["commercial_flight"]).ToString("C");
				row.Cells.Add(header);
				row.Cells.Add(cell);
				ctlTable.Rows.Add(row);
			}

			if (i_row["chartered_flight"] != DBNull.Value) {
				row = new TableRow();
				header = new TableHeaderCell();
				cell = new TableCell();
				header.Text = "Chartered Flight";
				cell.Text = ((decimal)i_row["chartered_flight"]).ToString("C");
				row.Cells.Add(header);
				row.Cells.Add(cell);
				ctlTable.Rows.Add(row);
			}

			if (i_row["government_aircraft"] != DBNull.Value) {
				row = new TableRow();
				header = new TableHeaderCell();
				cell = new TableCell();
				header.Text = "Government Aircraft";
				cell.Text = ((decimal)i_row["government_aircraft"]).ToString("C");
				row.Cells.Add(header);
				row.Cells.Add(cell);
				ctlTable.Rows.Add(row);
			}

			if (i_row["other_transportation"] != DBNull.Value) {
				row = new TableRow();
				header = new TableHeaderCell();
				cell = new TableCell();
				header.Text = "Other Transportation";
				cell.Text = ((decimal)i_row["other_transportation"]).ToString("C");
				row.Cells.Add(header);
				row.Cells.Add(cell);
				ctlTable.Rows.Add(row);
			}

			if (i_row["accommodation"] != DBNull.Value) {
				row = new TableRow();
				header = new TableHeaderCell();
				cell = new TableCell();
				header.Text = "Accommodation";
				cell.Text = ((decimal)i_row["accommodation"]).ToString("C");
				row.Cells.Add(header);
				row.Cells.Add(cell);
				ctlTable.Rows.Add(row);
			}

			if (i_row["meals_incidentals"] != DBNull.Value) {
				row = new TableRow();
				header = new TableHeaderCell();
				cell = new TableCell();
				header.Text = "Meals and Incidentals";
				cell.Text = ((decimal)i_row["meals_incidentals"]).ToString("C");
				row.Cells.Add(header);
				row.Cells.Add(cell);
				ctlTable.Rows.Add(row);
			}

			if (i_row["other"] != DBNull.Value) {
				row = new TableRow();
				header = new TableHeaderCell();
				cell = new TableCell();
				header.Text = "Other";
				cell.Text = ((decimal)i_row["other"]).ToString("C");
				row.Cells.Add(header);
				row.Cells.Add(cell);
				ctlTable.Rows.Add(row);
			}

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Total";
			cell.Text = ((decimal)i_row["total"]).ToString("C");
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Source URL";
			HyperLink link = new HyperLink();
			link.Text = HttpUtility.HtmlEncode(i_row["url"].ToString());
			link.Attributes["rel"] = "nofollow";
			link.NavigateUrl = i_row["url"].ToString();
			cell.Controls.Add(link);
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			i_outputPanel.Controls.Add(ctlTable);

			Literal br = new Literal();
			br.Text = "<BR>";
			i_outputPanel.Controls.Add(br);
		}

		private static void RenderHospitalityExpense(Panel i_outputPanel, DataRow i_row) {
			Table ctlTable = new Table();
			TableRow row;
			TableHeaderCell header;
			TableCell cell;

			ctlTable.CssClass = "tablelist";
			ctlTable.Width = 600;

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Width = 150;
			header.Text = "Member Name";
			cell.Text = HttpUtility.HtmlEncode(i_row["original_name"].ToString());
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Title";
			cell.Text = i_row["original_title"].ToString();
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Description";
			cell.Text = i_row["description"].ToString();
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Date From";
			if (i_row["date_from"] == DBNull.Value) {
				cell.Text = "??";
			} else {
				cell.Text = ((DateTime)i_row["date_from"]).ToString("yyyy-MM-dd");
			}
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Date To";
			if (i_row["date_to"] == DBNull.Value) {
				cell.Text = "??";
			} else {
				cell.Text = ((DateTime)i_row["date_to"]).ToString("yyyy-MM-dd");
			}
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			if (i_row["employees"] != DBNull.Value) {
				row = new TableRow();
				header = new TableHeaderCell();
				cell = new TableCell();
				header.Text = "Employees";
				cell.Text = i_row["employees"].ToString();
				row.Cells.Add(header);
				row.Cells.Add(cell);
				ctlTable.Rows.Add(row);
			}

			if (i_row["guests"] != DBNull.Value) {
				row = new TableRow();
				header = new TableHeaderCell();
				cell = new TableCell();
				header.Text = "Guests";
				cell.Text = i_row["guests"].ToString();
				row.Cells.Add(header);
				row.Cells.Add(cell);
				ctlTable.Rows.Add(row);
			}

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Location";
			cell.Text = i_row["location"].ToString();
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Establishment/Caterer";
			cell.Text = i_row["establishment"].ToString();
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Form of Hospitality";
			cell.Text = i_row["form_of_hospitality"].ToString();
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);
		
			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Total";
			cell.Text = ((decimal)i_row["total"]).ToString("C");
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			row = new TableRow();
			header = new TableHeaderCell();
			cell = new TableCell();
			header.Text = "Source URL";
			HyperLink link = new HyperLink();
			link.Text = HttpUtility.HtmlEncode(i_row["url"].ToString());
			link.Attributes["rel"] = "nofollow";
			link.NavigateUrl = i_row["url"].ToString();
			cell.Controls.Add(link);
			row.Cells.Add(header);
			row.Cells.Add(cell);
			ctlTable.Rows.Add(row);

			i_outputPanel.Controls.Add(ctlTable);

			Literal br = new Literal();
			br.Text = "<BR>";
			i_outputPanel.Controls.Add(br);
		}

		// Executes stored procedure identified by i_reportName and returns the resulting data set
		public static void RenderDrilldownReport(Panel i_outputPanel, DataSet i_ds) {

			// Show the travel expenses
			Literal travelHeading = new Literal();
			travelHeading.Text = "<h3>Travel Expenses</h3>";
			if (i_ds.Tables[0].Rows.Count == 0) travelHeading.Text += "<i>No Travel Expenses</i>";
			i_outputPanel.Controls.Add(travelHeading);

			for (int i = 0; i < i_ds.Tables[0].Rows.Count; i++) {
				RenderTravelExpense(i_outputPanel, i_ds.Tables[0].Rows[i]);
			}


			// Show the hospitality expenses
			Literal hospitalityHeading = new Literal();
			hospitalityHeading.Text = "<h3>Hospitality Expenses</h3>";
			if (i_ds.Tables[1].Rows.Count == 0) hospitalityHeading.Text += "<i>No Hospitality Expenses</i>";
			i_outputPanel.Controls.Add(hospitalityHeading);

			for (int i = 0; i < i_ds.Tables[1].Rows.Count; i++) {
				RenderHospitalityExpense(i_outputPanel, i_ds.Tables[1].Rows[i]);
			}

		}


		// Shows the drilldown report for a particular year
		public static void ShowDrilldownReportForYear(Panel i_outputPanel, int i_memberId, int i_year) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand("member_expenses_for_year");
			cmd.Parameters.AddWithValue("@member_id", i_memberId);
			cmd.Parameters.AddWithValue("@year", i_year);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(ds);

			RenderDrilldownReport(i_outputPanel, ds);
		}


		public static void ShowDrilldownReport(Panel i_outputPanel, int i_memberId) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand("member_expenses");
			cmd.Parameters.AddWithValue("@member_id", i_memberId);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(ds);

			RenderDrilldownReport(i_outputPanel, ds);
		}

		public static void ShowIndividualExpense(Panel i_outputPanel, ExpenseType i_expenseType, int i_expenseId) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataSet ds = new DataSet();

			RenderExpenseHeader(i_outputPanel, i_expenseType);
			
			if (i_expenseType == ExpenseType.Travel) {
				SqlCommand cmd = new SqlCommand("get_travel_expense");
				cmd.Parameters.AddWithValue("@id", i_expenseId);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = conn;
				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				adapter.Fill(ds);

				RenderTravelExpense(i_outputPanel, ds.Tables[0].Rows[0]);
			} else if (i_expenseType == ExpenseType.Hospitality) {
				SqlCommand cmd = new SqlCommand("get_hospitality_expense");
				cmd.Parameters.AddWithValue("@id", i_expenseId);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = conn;
				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				adapter.Fill(ds);

				RenderHospitalityExpense(i_outputPanel, ds.Tables[0].Rows[0]);
			}
		}

		public static DataSet GetDrilldownReport(int i_memberId) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand("member_expenses");
			cmd.Parameters.AddWithValue("@member_id", i_memberId);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(ds);

			return ds;
		}

		public static int GetExpenseCount() {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand("get_expense_count");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(ds);
			return (int)ds.Tables[0].Rows[0][0];
		}

		public static void ShowNameMappings(Panel i_outputPanel, string i_prefix) {
			// Run the stored procedure and retrieve the raw data table
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("get_name_mappings");
			cmd.Parameters.AddWithValue("@prefix", i_prefix);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);

			string member_name = null;

			Table ctlTable = new Table();
			i_outputPanel.Controls.Add(ctlTable);
			TableRow row = new TableRow();
			ctlTable.Rows.Add(row);
			TableCell header = new TableCell();
			row.Cells.Add(header);
			header.Font.Bold = true;
			header.Text = "Member Name";
			header = new TableCell();
			row.Cells.Add(header);
			header.Font.Bold = true;
			header.Text = "Name Entered on Claim";
			for (int i = 0; i < dt.Rows.Count; i++) {
				row = new TableRow();
				ctlTable.Rows.Add(row);

				TableCell cell1 = new TableCell();
				row.Cells.Add(cell1);
				if (member_name != dt.Rows[i]["member_name"].ToString()) {
					member_name = dt.Rows[i]["member_name"].ToString();
					cell1.Text = "<a href='/drilldown.aspx?member_id=" + dt.Rows[i]["id"].ToString() + "&member_name=" + HttpUtility.UrlEncode(member_name) + "&'>" + member_name + "</a>";
				}
	
				TableCell cell2 = new TableCell();
				row.Cells.Add(cell2);
				cell2.Text = dt.Rows[i]["original_name"].ToString();
			}
		}

		public static DataTable GetRangeReport() {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_expense_ranges");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return dt;
		}


		public static DataTable SearchTravel(DateTime i_dateFrom, DateTime i_dateTo, int? i_memberId, int? i_departmentId, string i_purpose, string i_destination, decimal? i_commercialFlightMin, decimal? i_commercialFlightMax, decimal? i_charteredFlightMin, decimal? i_charteredFlightMax, decimal? i_governmentAircraftMin, decimal? i_governmentAircraftMax, decimal? i_otherTransportationMin, decimal? i_otherTransportationMax, decimal? i_accommodationMin, decimal? i_accommodationMax, decimal? i_mealsIncidentalsMin, decimal? i_mealsIncidentalsMax, decimal? i_otherMin, decimal? i_otherMax, decimal? i_totalMin, decimal? i_totalMax) {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_custom_travel");
			cmd.Parameters.AddWithValue("@date_from", i_dateFrom);
			cmd.Parameters.AddWithValue("@date_to", i_dateTo);
			cmd.Parameters.AddWithValue("@member_id", i_memberId);
			cmd.Parameters.AddWithValue("@department_id", i_departmentId);
			cmd.Parameters.AddWithValue("@purpose", i_purpose);
			cmd.Parameters.AddWithValue("@destination", i_destination);
			cmd.Parameters.AddWithValue("@commercial_flight_min", i_commercialFlightMin);
			cmd.Parameters.AddWithValue("@commercial_flight_max", i_commercialFlightMax);
			cmd.Parameters.AddWithValue("@chartered_flight_min", i_charteredFlightMin);
			cmd.Parameters.AddWithValue("@chartered_flight_max", i_charteredFlightMax);
			cmd.Parameters.AddWithValue("@government_aircraft_min", i_governmentAircraftMin);
			cmd.Parameters.AddWithValue("@government_aircraft_max", i_governmentAircraftMax);
			cmd.Parameters.AddWithValue("@other_transportation_min", i_otherTransportationMin);
			cmd.Parameters.AddWithValue("@other_transportation_max", i_otherTransportationMax);
			cmd.Parameters.AddWithValue("@accommodation_min", i_accommodationMin);
			cmd.Parameters.AddWithValue("@accommodation_max", i_accommodationMax);
			cmd.Parameters.AddWithValue("@meals_incidentals_min", i_mealsIncidentalsMin);
			cmd.Parameters.AddWithValue("@meals_incidentals_max", i_mealsIncidentalsMax);
			cmd.Parameters.AddWithValue("@other_min", i_otherMin);
			cmd.Parameters.AddWithValue("@other_max", i_otherMax);
			cmd.Parameters.AddWithValue("@total_min", i_totalMin);
			cmd.Parameters.AddWithValue("@total_max", i_totalMax);
			foreach (SqlParameter Parameter in cmd.Parameters) {
				if (Parameter.Value == null) {
					Parameter.Value = DBNull.Value;
				}
			} 
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return dt;
		}

		public static DataTable SearchHospitality(DateTime i_dateFrom, DateTime i_dateTo, int? i_memberId, int? i_departmentId, string i_description, string i_location, string i_establishment, int? i_employeesMin, int? i_employeesMax, int? i_guestsMin, int? i_guestsMax, decimal? i_totalMin, decimal? i_totalMax) {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_custom_hospitality");
			cmd.Parameters.AddWithValue("@date_from", i_dateFrom);
			cmd.Parameters.AddWithValue("@date_to", i_dateTo);
			cmd.Parameters.AddWithValue("@member_id", i_memberId);
			cmd.Parameters.AddWithValue("@department_id", i_departmentId);
			cmd.Parameters.AddWithValue("@description", i_description);
			cmd.Parameters.AddWithValue("@location", i_location);
			cmd.Parameters.AddWithValue("@establishment", i_establishment);
			cmd.Parameters.AddWithValue("@employees_min", i_employeesMin);
			cmd.Parameters.AddWithValue("@employees_max", i_employeesMax);
			cmd.Parameters.AddWithValue("@guests_min", i_guestsMin);
			cmd.Parameters.AddWithValue("@guests_max", i_guestsMax);
			cmd.Parameters.AddWithValue("@total_min", i_totalMin);
			cmd.Parameters.AddWithValue("@total_max", i_totalMax);
			foreach (SqlParameter Parameter in cmd.Parameters) {
				if (Parameter.Value == null) {
					Parameter.Value = DBNull.Value;
				}
			}
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return dt;
		}


		public static DataTable GenerateCustomReport(string i_description) {
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("report_custom");
			cmd.Parameters.AddWithValue("@description", i_description);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return dt;
		}


		// Returns true if authorized, false otherwise
		public static bool Authorize(string i_username, string i_password) {
			return true; // Make the custom reports free for now -- TODO -- maybe change this??


			if (string.IsNullOrWhiteSpace(i_username)) return false;

			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("user_authenticate");
			cmd.Parameters.AddWithValue("@username", i_username);
			cmd.Parameters.AddWithValue("@password", i_password);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(dt);
			return (dt.Rows.Count > 0);
		}


		public static decimal? ParseDecimal(string i_input) {
			if (string.IsNullOrWhiteSpace(i_input)) return null;
			else return decimal.Parse(i_input);
		}
		public static int? ParseInt(string i_input) {
			if (string.IsNullOrWhiteSpace(i_input)) return null;
			else return int.Parse(i_input);
		}
		public static string ParseString(string i_input) {
			if (string.IsNullOrWhiteSpace(i_input)) return null;
			else return i_input;
		}

	}
}
