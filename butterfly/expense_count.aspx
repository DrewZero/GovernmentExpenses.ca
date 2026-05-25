<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="expense_count.aspx.cs" Inherits="Metamorph.expense_count" %>
<%@ Register src="most_expenses.ascx" tagname="most_expenses" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<h3>Total Number of Expenses Claimed</h3>
	
	There have been a total of <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label> expenses claimed since the end of 2003.

	<br /><br />

	<h4>Total count of expenses of each dollar value range</h4>
	<asp:Repeater ID="ctlRangeReport" runat="server">
		<HeaderTemplate>
			<table class="tablelist">
			<tr>
				<th>Amount</th>
				<th>Number of Expenses</th>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td><%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "range").ToString()) %></td>
				<td><%# DataBinder.Eval(Container.DataItem, "count") %></td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	
	
	<br /><br />
	
	<h4>Largest number of expenses in a year</h4>
	
	<h4>2009</h4>
	<uc3:most_expenses ID="ctlMostExpenses2009" runat="server" Year="2009" />

	<h4>2008</h4>
	<uc3:most_expenses ID="ctlMostExpenses2008" runat="server" Year="2008" />

	<h4>2007</h4>
	<uc3:most_expenses ID="ctlMostExpenses2007" runat="server" Year="2007" />

	<h4>2006</h4>
	<uc3:most_expenses ID="ctlMostExpenses2006" runat="server" Year="2006" />

	<h4>2005</h4>
	<uc3:most_expenses ID="ctlMostExpenses2005" runat="server" Year="2005" />

	<h4>2004</h4>
	<uc3:most_expenses ID="ctlMostExpenses2004" runat="server" />

	<br />
		
	<h4>Fewest expenses claimed in a year</h4>
	
	<h4>2009</h4>
	<uc3:most_expenses ID="ctlLeastExpenses2009" runat="server" Year="2009" />

	<h4>2008</h4>
	<uc3:most_expenses ID="ctlLeastExpenses2008" runat="server" Year="2008" />

	<h4>2007</h4>
	<uc3:most_expenses ID="ctlLeastExpenses2007" runat="server" Year="2007" />

	<h4>2006</h4>
	<uc3:most_expenses ID="ctlLeastExpenses2006" runat="server" Year="2006" />

	<h4>2005</h4>
	<uc3:most_expenses ID="ctlLeastExpenses2005" runat="server" Year="2005" />

	<h4>2004</h4>
	<uc3:most_expenses ID="ctlLeastExpenses2004" runat="server" />



</asp:Content>
