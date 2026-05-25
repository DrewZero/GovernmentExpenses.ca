<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="show_top_expense.aspx.cs" Inherits="Metamorph.show_top_expense" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<h3>Expense for <asp:Label ID="lblMember" runat="server"></asp:Label></h3>
	
	<asp:Panel ID="pnlExpense" runat="server"></asp:Panel>
</asp:Content>
