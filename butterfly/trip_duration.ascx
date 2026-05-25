<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="trip_duration.ascx.cs" Inherits="Metamorph.trip_duration1" %>

	<asp:Repeater ID="ctlLowestExpenses" runat="server">
		<HeaderTemplate>
			<table class="tablelist">
			<tr>
				<th>Member Name</th>
				<th>Department</th>
				<th>Title</th>
				<th>Trip Duration (days)</th>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td>
					<a href="/show_trip_expense.aspx?id=<%# Container.ItemIndex %>&member_name=<%# HttpUtility.UrlEncode(DataBinder.Eval(Container.DataItem, "member_name").ToString()) %>&<%# m_query %>">
						<%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "member_name").ToString()) %>
					</a>
				</td>
				<td><%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "department_name").ToString()) %></td>
				<td><%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "member_title").ToString()) %></td>
				<td><%# DataBinder.Eval(Container.DataItem, "days") %></td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
