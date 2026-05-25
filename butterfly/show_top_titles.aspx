<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="show_top_titles.aspx.cs" Inherits="Metamorph.show_top_titles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Top Spending by Title</h3>

	<asp:Repeater ID="ctlTopSpenders" runat="server">
		<HeaderTemplate>
			<table class="tablelist">
			<tr>
				<th>Title</th>
				<th>Amount</th>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td>
					<%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "member_title").ToString()) %>
				</td>
				<td><%# ((decimal)DataBinder.Eval(Container.DataItem, "total")).ToString("C") %></td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>

</asp:Content>
