<%@ Page Title="Detailed Government Expenses in Canada" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="drilldown.aspx.cs" Inherits="Metamorph.drilldown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Expenses for <asp:Label ID="lblMember" runat="server"></asp:Label></h2>
    
    Please Note: We only show up to 10 records in the free online version.  
    <br />
    For full expense data or custom reports, please <a href="mailto:govexp@bine.ca?Subject=Full government expense data requested">contact us today!</a>
    
    <br /><br />
    
    <div>
		<asp:Panel id="pnlReport" runat="server">
		</asp:Panel>
    </div>


</asp:Content>
