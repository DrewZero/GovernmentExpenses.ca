<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="year_dollar.ascx.cs" Inherits="Metamorph.year_dollar" %>

	<asp:Repeater ID="ctlYearValue" runat="server">
		<HeaderTemplate>
			<table class="tablelist">
			<tr>
				<th>Year</th>
				<th>Total</th>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td><%# DataBinder.Eval(Container.DataItem, "year") %></td>
				<td><%# ((decimal)DataBinder.Eval(Container.DataItem, "total")).ToString("C") %></td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
