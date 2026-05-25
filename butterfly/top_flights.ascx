<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="top_flights.ascx.cs" Inherits="Metamorph.top_flights" %>

	<asp:Repeater ID="ctlTopExpenses" runat="server">
		<HeaderTemplate>
			<table class="tablelist">
			<tr>
				<th>Member Name</th>
				<th>Department</th>
				<th>Title</th>
				<th>Amount</th>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td>
					<a href="/show_top_flight.aspx?id=<%# Container.ItemIndex %>&member_name=<%# HttpUtility.UrlEncode(DataBinder.Eval(Container.DataItem, "member_name").ToString()) %>&<%# m_query %>">
						<%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "member_name").ToString()) %>
					</a>
				</td>
				<td><%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "department_name").ToString()) %></td>
				<td><%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "member_title").ToString()) %></td>
				<td><%# ((decimal)DataBinder.Eval(Container.DataItem, "total")).ToString("C") %></td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
