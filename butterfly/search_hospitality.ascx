<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="search_hospitality.ascx.cs" Inherits="Metamorph.search_hospitality" %>

<asp:Panel ID="pnlCriteria" runat="server">

<b>Search Hospitality Records</b>


<fieldset>
	<legend>Specify Date Range</legend>
	<b>From:</b>
	<asp:DropDownList ID="ddlYearFrom" runat="server">
	</asp:DropDownList>
	<asp:DropDownList ID="ddlMonthFrom" runat="server">
	</asp:DropDownList>
	<asp:DropDownList ID="ddlDayFrom" runat="server">
	</asp:DropDownList>

	&nbsp;
	<asp:CustomValidator ID="ctlValidateDateFrom" runat="server" 
		Display="Dynamic" ErrorMessage="Not a valid date"></asp:CustomValidator>
	&nbsp;&nbsp;&nbsp;

	<b>To:</b>
	<asp:DropDownList ID="ddlYearTo" runat="server">
	</asp:DropDownList>
	<asp:DropDownList ID="ddlMonthTo" runat="server">
	</asp:DropDownList>
	<asp:DropDownList ID="ddlDayTo" runat="server">
	</asp:DropDownList>

&nbsp;<asp:CustomValidator ID="ctlValidateDateTo" runat="server" Display="Dynamic" 
		ErrorMessage="Not a valid date"></asp:CustomValidator>
</fieldset>

<fieldset>
<legend>Select Government Official</legend>

	<asp:DropDownList ID="ddlMember" runat="server">
		<asp:ListItem Selected="True">All Members</asp:ListItem>
	</asp:DropDownList>

</fieldset>

<fieldset>
<legend>Select Government Department</legend>
	<asp:DropDownList ID="ddlDepartment" runat="server">
		<asp:ListItem Selected="True">All Departments</asp:ListItem>
	</asp:DropDownList>
</fieldset>

<fieldset>
<legend>Description</legend>
<asp:TextBox ID="txtDescription" runat="server" Width="430px"></asp:TextBox>
</fieldset>

<fieldset>
<legend>Location</legend>
<asp:TextBox ID="txtLocation" runat="server" Width="430px"></asp:TextBox>
</fieldset>
	
<fieldset>
<legend>Establishment</legend>
<asp:TextBox ID="txtEstablishment" runat="server" Width="430px"></asp:TextBox>
</fieldset>

<fieldset>
<legend>Employees</legend>
Min:<asp:TextBox ID="txtEmployeesMin" runat="server"></asp:TextBox>
<asp:RangeValidator ID="RangeValidator1" runat="server" 
		ControlToValidate="txtEmployeesMin" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Integer" MaximumValue="99999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
&nbsp;&nbsp;&nbsp;&nbsp;
Max:<asp:TextBox ID="txtEmployeesMax" runat="server"></asp:TextBox>
<asp:RangeValidator ID="RangeValidator2" runat="server" 
		ControlToValidate="txtEmployeesMax" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Integer" MaximumValue="99999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
</fieldset>

<fieldset>
<legend>Guests</legend>
Min:<asp:TextBox ID="txtGuestsMin" runat="server"></asp:TextBox>
<asp:RangeValidator ID="RangeValidator3" runat="server" 
		ControlToValidate="txtGuestsMin" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Integer" MaximumValue="99999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
&nbsp;&nbsp;&nbsp;&nbsp;
Max:<asp:TextBox ID="txtGuestsMax" runat="server"></asp:TextBox>
<asp:RangeValidator ID="RangeValidator4" runat="server" 
		ControlToValidate="txtGuestsMax" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Integer" MaximumValue="99999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
</fieldset>

<fieldset>
<legend>Total</legend>
Min:<asp:TextBox ID="txtTotalMin" runat="server"></asp:TextBox>
<asp:RangeValidator ID="RangeValidator5" runat="server" 
		ControlToValidate="txtTotalMin" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
&nbsp;&nbsp;&nbsp;&nbsp;
Max:<asp:TextBox ID="txtTotalMax" runat="server"></asp:TextBox>
<asp:RangeValidator ID="RangeValidator6" runat="server" 
		ControlToValidate="txtTotalMax" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
</fieldset>


<br />

<asp:Button ID="btnSearch" runat="server" Text="Generate Hospitality Report" onclick="btnSearch_Click" />

<br /><br />

</asp:Panel>

<asp:Panel ID="pnlResults" runat="server" Visible="false">
	There were <asp:Label ID="lblResultCount" runat="server" Font-Bold="true"></asp:Label> results found.
	<br />
	<asp:HyperLink ID="lnkDownloadReport" runat="server">Click here to download the report in Microsoft Excel CSV format</asp:HyperLink>
</asp:Panel>

