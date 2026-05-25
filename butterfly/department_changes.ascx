<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="department_changes.ascx.cs" Inherits="Metamorph.department_changes" %>

	<asp:Repeater ID="ctlDepartmentChanges" runat="server">
		<HeaderTemplate>
			<table class="tablelist">
			<tr>
				<th>Department</th>
				<th>Change Amount</th>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td>
					<a href="/departments.aspx?id=<%# DataBinder.Eval(Container.DataItem, "department_id")%>&department=<%# DataBinder.Eval(Container.DataItem, "department_name") %>">
						<%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "department_name").ToString()) %>
					</a>
				</td>
				<td><%# ((decimal)DataBinder.Eval(Container.DataItem, "total")).ToString("C")%></td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
