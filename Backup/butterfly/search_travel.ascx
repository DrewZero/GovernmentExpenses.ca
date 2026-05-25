<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="search_travel.ascx.cs" Inherits="Metamorph.search_travel" %>

<asp:Panel ID="pnlCriteria" runat="server">

<b>Search Travel Records</b>


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
<legend>Purpose</legend>
<asp:TextBox ID="txtPurpose" runat="server" Width="430px"></asp:TextBox>
</fieldset>

<fieldset>
<legend>Destination</legend>
<asp:TextBox ID="txtDestination" runat="server" Width="430px"></asp:TextBox>
</fieldset>

<fieldset>
<legend>Commercial Flight</legend>

	Min: <asp:TextBox ID="txtCommercialFlightMin" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator1" runat="server" 
		ControlToValidate="txtCommercialFlightMin" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>

	&nbsp;&nbsp;&nbsp;&nbsp;

	Max: <asp:TextBox ID="txtCommercialFlightMax" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator2" runat="server" 
		ControlToValidate="txtCommercialFlightMax" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>

</fieldset>

<fieldset>
<legend>Chartered Flight</legend>
	Min: <asp:TextBox ID="txtCharteredFlightMin" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator3" runat="server" 
		ControlToValidate="txtCharteredFlightMin" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
	&nbsp;&nbsp;&nbsp;&nbsp;
	Max: <asp:TextBox ID="txtCharteredFlightMax" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator4" runat="server" 
		ControlToValidate="txtCharteredFlightMax" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
</fieldset>

<fieldset>
<legend>Government Aircraft</legend>
	Min: <asp:TextBox ID="txtGovernmentAircraftMin" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator5" runat="server" 
		ControlToValidate="txtGovernmentAircraftMin" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
	&nbsp;&nbsp;&nbsp;&nbsp;
	Max: <asp:TextBox ID="txtGovernmentAircraftMax" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator6" runat="server" 
		ControlToValidate="txtGovernmentAircraftMax" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
</fieldset>

<fieldset>
<legend>Other Transportation</legend>
	Min: <asp:TextBox ID="txtOtherTransportationMin" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator7" runat="server" 
		ControlToValidate="txtOtherTransportationMin" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
	&nbsp;&nbsp;&nbsp;&nbsp;
	Max: <asp:TextBox ID="txtOtherTransportationMax" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator8" runat="server" 
		ControlToValidate="txtOtherTransportationMax" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
</fieldset>

<fieldset>
<legend>Accommodation</legend>
	Min: <asp:TextBox ID="txtAccommodationMin" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator9" runat="server" 
		ControlToValidate="txtAccommodationMin" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
	&nbsp;&nbsp;&nbsp;&nbsp;
	Max: <asp:TextBox ID="txtAccommodationMax" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator10" runat="server" 
		ControlToValidate="txtAccommodationMax" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
</fieldset>

<fieldset>
<legend>Meals and Incidentals</legend>
	Min: <asp:TextBox ID="txtMealsIncidentalsMin" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator11" runat="server" 
		ControlToValidate="txtMealsIncidentalsMin" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
	&nbsp;&nbsp;&nbsp;&nbsp;
	Max: <asp:TextBox ID="txtMealsIncidentalsMax" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator12" runat="server" 
		ControlToValidate="txtMealsIncidentalsMax" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
</fieldset>

<fieldset>
<legend>Other</legend>
	Min: <asp:TextBox ID="txtOtherMin" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator13" runat="server" 
		ControlToValidate="txtOtherMin" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
	&nbsp;&nbsp;&nbsp;&nbsp;
	Max: <asp:TextBox ID="txtOtherMax" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator14" runat="server" 
		ControlToValidate="txtOtherMax" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
</fieldset>

<fieldset>
<legend>Total</legend>
	Min: <asp:TextBox ID="txtTotalMin" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator15" runat="server" 
		ControlToValidate="txtTotalMin" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
	&nbsp;&nbsp;&nbsp;&nbsp;
	Max: <asp:TextBox ID="txtTotalMax" runat="server"></asp:TextBox>
	<asp:RangeValidator ID="RangeValidator16" runat="server" 
		ControlToValidate="txtTotalMax" Display="Dynamic" 
		ErrorMessage="Value must be a number" Type="Double" MaximumValue="9999999999" 
		MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
</fieldset>

<br />

<asp:Button ID="btnSearch" runat="server" Text="Generate Travel Report" 
onclick="btnSearch_Click" />

<br /><br />

</asp:Panel>

<asp:Panel ID="pnlResults" runat="server" Visible="false">
	There were <asp:Label ID="lblResultCount" runat="server" Font-Bold="true"></asp:Label> results found.
	<br /><br />
	<asp:HyperLink ID="lnkDownloadReport" runat="server">Click here to download the report in Microsoft Excel CSV format</asp:HyperLink>
</asp:Panel>
