<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="individual_expenses.aspx.cs" Inherits="Metamorph.individual_expenses" %>
<%@ Register src="top_expenses.ascx" tagname="top_expenses" tagprefix="uc2" %>
<%@ Register src="lowest_expenses.ascx" tagname="lowest_expenses" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<h3>Stats About Individual Expenses</h3>

	Air Travel vs. Non Air Travel
	
	<h4>Largest Expenses Ever Claimed</h4>
	<uc2:top_expenses ID="ctlLargestExpenses" runat="server" />

	<br />
	<h4>Smallest Expenses Ever Claimed</h4>
	<uc2:lowest_expenses ID="ctlSmallestExpenses" runat="server" />

</asp:Content>
