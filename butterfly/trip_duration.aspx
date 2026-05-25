<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="trip_duration.aspx.cs" Inherits="Metamorph.trip_duration" %>
<%@ Register src="trip_duration.ascx" tagname="trip_duration" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<h3>Travel Trip Duration Statistics</h3>
	
	<h4>Longest trips ever taken</h4>
	<uc1:trip_duration ID="ctlLongestTrips" runat="server" />
	
	<br />
	
	<h4>Longest trips in 2009</h4>
	<uc1:trip_duration ID="ctlLongestTrips2009" runat="server" />

	<h4>Longest trips in 2008</h4>
	<uc1:trip_duration ID="ctlLongestTrips2008" runat="server" />

	<h4>Longest trips in 2007</h4>
	<uc1:trip_duration ID="ctlLongestTrips2007" runat="server" />

	<h4>Longest trips in 2006</h4>
	<uc1:trip_duration ID="ctlLongestTrips2006" runat="server" />

	<h4>Longest trips in 2005</h4>
	<uc1:trip_duration ID="ctlLongestTrips2005" runat="server" />

	<h4>Longest trips in 2004</h4>
	<uc1:trip_duration ID="ctlLongestTrips2004" runat="server" />

	<br />
		
	<h4>Longest average trip length</h4>
	<uc1:trip_duration ID="ctlLongestAverageTrips" runat="server" />

</asp:Content>
