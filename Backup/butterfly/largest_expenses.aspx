<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="largest_expenses.aspx.cs" Inherits="Metamorph.largest_expenses" %>
<%@ Register src="top_expenses.ascx" tagname="top_expenses" tagprefix="uc2" %>
<%@ Register src="top_flights.ascx" tagname="top_flights" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%-- 
TODO
Most Expensive Flights?
	<a href="#">Overall</a> | <a href="#">Hospitality</a> | <a href="#">Air Travel</a> | <a href="#">Non-Air Travel</a>

	<br />
--%>

	<h4>Largest Expenses Ever Claimed</h4>
	<uc2:top_expenses ID="ctlLargestExpenses" runat="server" />

	<h4>Most Expensive Flights</h4>
	<uc3:top_flights ID="ctlMostExpensiveFlights" runat="server" />

	<h4>Largest Expenses in 2009</h4>
	<uc2:top_expenses ID="ctlLargestExpenses2009" runat="server" />

	<h4>Largest Expenses in 2008</h4>
	<uc2:top_expenses ID="ctlLargestExpenses2008" runat="server" />

	<h4>Largest Expenses in 2007</h4>
	<uc2:top_expenses ID="ctlLargestExpenses2007" runat="server" />

	<h4>Largest Expenses in 2006</h4>
	<uc2:top_expenses ID="ctlLargestExpenses2006" runat="server" />

	<h4>Largest Expenses in 2005</h4>
	<uc2:top_expenses ID="ctlLargestExpenses2005" runat="server" />

	<h4>Largest Expenses in 2004</h4>
	<uc2:top_expenses ID="ctlLargestExpenses2004" runat="server" />

</asp:Content>
