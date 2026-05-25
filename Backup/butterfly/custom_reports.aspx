<%@ Page Title="Canadian Government Expenses - Generate Custom Report" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="custom_reports.aspx.cs" Inherits="Metamorph.custom_reports" %>
<%@ Register src="search_hospitality.ascx" tagname="search_hospitality" tagprefix="uc1" %>
<%@ Register src="search_travel.ascx" tagname="search_travel" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<h3>Generate a Custom Report or Find Specific Data</h3>


	<asp:Panel ID="pnlChoose" runat="server">
		<fieldset>
		<legend>Choose Record Types to Search</legend>
			<asp:RadioButton ID="rdoTravel" runat="server" AutoPostBack="True" 
				Checked="True" GroupName="SearchTabs" Text="Travel" 
				oncheckedchanged="rdoTravel_CheckedChanged" CssClass="tab" />
		
			&nbsp;&nbsp;&nbsp;&nbsp;

			<asp:RadioButton ID="rdoHospitality" runat="server" 
				AutoPostBack="True" GroupName="SearchTabs" Text="Hospitality" 
				oncheckedchanged="rdoHospitality_CheckedChanged" CssClass="tab" />
		</fieldset>
	</asp:Panel>

	<br />

	<asp:MultiView ID="ctlMultiView" runat="server" ActiveViewIndex="0">
		<asp:View ID="View1" runat="server">
			<uc2:search_travel ID="ctlSearchTravel" runat="server" />
		</asp:View>
		<asp:View ID="View2" runat="server">
			<uc1:search_hospitality ID="ctlSearchHospitality" runat="server" />
		</asp:View>
	</asp:MultiView>


	<asp:Panel ID="pnlSubscribe" runat="server" Visible="false">
	
		<br /><br />

		<i>PLEASE NOTE: To reduce server load, a maximum of 1000 records are shown in the custom search, for more records, please <a href="mailto:govexp@bine.ca">contact us</a>.</i>
		<br /><br />


	</asp:Panel>

</asp:Content>
