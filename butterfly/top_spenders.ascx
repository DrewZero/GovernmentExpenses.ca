<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="top_spenders.ascx.cs" Inherits="Metamorph.top_spenders" %>

	<asp:Repeater ID="ctlTopSpenders" runat="server">
		<HeaderTemplate>
			<table class="tablelist">
			<tr>
				<th>Member Name</th>
				<th>Department</th>
				<th>Title</th>
				<th>Total Amount</th>
				<th>Number of Expenses</th>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td>
					<a href="/drilldown.aspx?member_id=<%# DataBinder.Eval(Container.DataItem, "member_id") %>&member_name=<%# HttpUtility.UrlEncode(DataBinder.Eval(Container.DataItem, "member_name").ToString()) %>&<%# m_query %>">
						<%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "member_name").ToString()) %>
					</a>
				</td>
				<td><%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "department_name").ToString()) %></td>
				<td><%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "member_title").ToString()) %></td>
				<td><%# ((decimal)DataBinder.Eval(Container.DataItem, "total")).ToString("C") %></td>
				<td><%# DataBinder.Eval(Container.DataItem, "count")%></td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
