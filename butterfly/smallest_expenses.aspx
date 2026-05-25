<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="smallest_expenses.aspx.cs" Inherits="Metamorph.smallest_expenses" %>
<%@ Register src="lowest_expenses.ascx" tagname="lowest_expenses" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%-- 
TODO
	<a href="#">Overall</a> | <a href="#">Hospitality</a> | <a href="#">Air Travel</a> | <a href="#">Non-Air Travel</a>

	<br />
--%>

	<h4>Smallest Expenses Ever Claimed</h4>
	<uc2:lowest_expenses ID="ctlSmallestExpenses" runat="server" />

	<h4>Smallest Expenses for 2009</h4>
	<uc2:lowest_expenses ID="ctlSmallestExpenses2009" runat="server" />

	<h4>Smallest Expenses for 2008</h4>
	<uc2:lowest_expenses ID="ctlSmallestExpenses2008" runat="server" />

	<h4>Smallest Expenses for 2007</h4>
	<uc2:lowest_expenses ID="ctlSmallestExpenses2007" runat="server" />

	<h4>Smallest Expenses for 2006</h4>
	<uc2:lowest_expenses ID="ctlSmallestExpenses2006" runat="server" />

	<h4>Smallest Expenses for 2005</h4>
	<uc2:lowest_expenses ID="ctlSmallestExpenses2005" runat="server" />

	<h4>Smallest Expenses for 2004</h4>
	<uc2:lowest_expenses ID="ctlSmallestExpenses2004" runat="server" />

</asp:Content>
