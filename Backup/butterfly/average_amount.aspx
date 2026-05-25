<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="average_amount.aspx.cs" Inherits="Metamorph.average_amount" %>
<%@ Register src="top_spenders.ascx" tagname="top_spenders" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<h3>Average Department Claims</h3>

	<%-- TODO: allow this table to be sorted by different columns --%>
	<asp:Repeater id="ctlDepartments" runat="server">
		<HeaderTemplate>
			<table class="tablelist">
				<tr>
					<td><b>Department</b></td>
					<td><b>Average Total Per Employee</b></td>
					<td><b>Average Amount Per Claim</b></td>
					<td><b>Average Claims Per Employee</b></td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td><a href="departments.aspx?id=<%# DataBinder.Eval(Container.DataItem, "department_id") %>&department=<%# HttpUtility.UrlEncode(DataBinder.Eval(Container.DataItem, "department_name").ToString()) %>"><%# DataBinder.Eval(Container.DataItem, "department_name") %></a></td>
				<td><%# ((decimal)DataBinder.Eval(Container.DataItem, "average_amount_per_employee")).ToString("C") %></td>
				<td><%# ((decimal)DataBinder.Eval(Container.DataItem, "average_amount_per_claim")).ToString("C") %></td>
				<td><%# DataBinder.Eval(Container.DataItem, "average_claims_per_employee") %></td>
			</tr>
		</ItemTemplate>
		<SeparatorTemplate></SeparatorTemplate>
		<FooterTemplate></table></FooterTemplate>
	</asp:Repeater>
	

	<br /><br />
		
	<h3>Averages Across All Departments</h3>
	
	<h4>Largest average claim amount</h4>
	<uc1:top_spenders ID="ctlHighestAverage" runat="server" />	
	
	<br /><br />
	
	<h4>Smallest average claim amount</h4>
	<uc1:top_spenders ID="ctlLowestAverage" runat="server" />	

</asp:Content>
