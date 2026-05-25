<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="departments.aspx.cs" Inherits="Metamorph.departments" %>
<%@ Register src="top_spenders.ascx" tagname="top_spenders" tagprefix="uc1" %>
<%@ Register src="year_dollar.ascx" tagname="year_dollar" tagprefix="uc2" %>

<%@ Register assembly="WebChart" namespace="WebChart" tagprefix="Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<h2><asp:Label ID="lblDepartment" runat="server"></asp:Label></h2>


The total spending for <asp:Label ID="lblDepartment1" runat="server" Font-Bold="true"></asp:Label> since the end of 2003 was <asp:Label ID="lblTotal" Font-Bold="true" runat="server"></asp:Label>

<br /><br />

<h3>Spending Trends for <asp:Label ID="lblDepartment2" runat="server" Font-Bold="true"></asp:Label></h3>

	<table width="100%">
		<tr>
			<td>
				<uc2:year_dollar ID="ctlYearTotal" runat="server" />
				
			</td>
			<td>
				<Web:ChartControl ID="ctlChart" runat="server" BorderStyle="Outset" 
					BorderWidth="5px" ChartPadding="30" GridLines="Both" HasChartLegend="False" 
					ShowTitlesOnBackground="False" TopPadding="20" YCustomEnd="0" YCustomStart="0" 
					YValuesInterval="0">
					<Border Color="CornflowerBlue" />
<YAxisFont StringFormat="Far,Near,Character,LineLimit"></YAxisFont>

<XTitle StringFormat="Center,Far,Character,LineLimit" font="Tahoma, 8pt, style=Bold" 
						forecolor="SteelBlue"></XTitle>

					<PlotBackground Angle="90" EndPoint="100, 400" ForeColor="#FFFFC0" 
						Type="LinearGradient" />

<XAxisFont StringFormat="Center,Near,Character,LineLimit"></XAxisFont>

<Background Color="CornflowerBlue" angle="90" endpoint="100, 400" forecolor="#80FF80" 
						type="LinearGradient"></Background>

<ChartTitle StringFormat="Center,Near,Character,LineLimit" font="Tahoma, 10pt, style=Bold" 
						forecolor="White"></ChartTitle>

<YTitle StringFormat="Near,Near,Character,DirectionVertical" font="Tahoma, 8pt, style=Bold" 
						forecolor="SteelBlue"></YTitle>
				</Web:ChartControl>
			</td>
		</tr>
	</table>

	
	<br />

<h3>Top spenders for <asp:Label ID="lblDepartment3" runat="server"></asp:Label></h3>
Please note: These totals are across all years (since 2003).  Some individuals may
not have been in the department for all years whereas some may have been there more
years than others so please keep that in mind when reviewing the totals.
<br />

	<asp:Repeater ID="ctlTopSpenders" runat="server">
		<HeaderTemplate>
			<table class="tablelist">
			<tr>
				<th>Member Name</th>
				<th>Title</th>
				<th>Total Amount Spent</th>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td>
					<a href="/drilldown.aspx?member_id=<%# DataBinder.Eval(Container.DataItem, "member_id") %>&member_name=<%# HttpUtility.UrlEncode(DataBinder.Eval(Container.DataItem, "member_name").ToString()) %>">
						<%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "member_name").ToString()) %>
					</a>
				</td>
				<td><%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "member_title").ToString()) %></td>
				<td><%# ((decimal)DataBinder.Eval(Container.DataItem, "total")).ToString("C") %></td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>

	<br />

<h3>Top Spenders By Year</h3>
	
	<h4>2010</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2010" runat="server" Year="2010" />

	<h4>2009</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2009" runat="server" Year="2009" />

	<h4>2008</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2008" runat="server" Year="2008" />

	<h4>2007</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2007" runat="server" Year="2007" />

	<h4>2006</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2006" runat="server" Year="2006" />

	<h4>2005</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2005" runat="server" Year="2005" />

	<h4>2004</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2004" runat="server" />
	
	

</asp:Content>
