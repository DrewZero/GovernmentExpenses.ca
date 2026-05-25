<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="download_report.aspx.cs" Inherits="Metamorph.download_report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link rel="stylesheet" href="/default.css" type="text/css" media="all" />
    <link rel="SHORTCUT ICON" href="/favicon.ico" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="master">

		<h3>Please support the continuing Government Expenses project</h3>

		NOTE: reports are limited to 1000 records.  If you want more, you may purchase the
		entire database in SQL Server format below.
		
		<br /><br />

		<b>Subscribe for a <b>year</b> for only $20/month (a savings of $60!)</b>
		<br />
		
		<a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=BNHQJ89HQ753N"><img src="https://www.paypal.com/en_US/i/btn/btn_subscribeCC_LG.gif" border="0" /></a>

		<br /><br />
		<B>Regular Monthly Rate ($25 / month):</B>
		<br />
		<a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=L9ZDX2W8GVEBU"><img src="https://www.paypal.com/en_US/i/btn/btn_subscribeCC_LG.gif" border="0" /></a>

		<br /><br />

		<b>Purchase the entire database now:</b>
		<br />

		<a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=RH5CE84CN6TRA"><img src="https://www.paypal.com/en_US/i/btn/btn_buynowCC_LG.gif" border="0" /></a>

		<br /><br />

		We will try to fulfill your order and send you access information within 1-2 business days at most.

		<br /><br />    
    	
		<h3>If you already have a username and password, please enter it now</h3>
		<table>
		<tr>
			<th>Username:</th>
			<td><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<th>Password:</th>
			<td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
		</tr>
		<tr>
			<td colspan="2" align="right">
				<asp:Button ID="btnLogin" runat="server" Text="Login" />    
			</td>
		</tr>
		</table>
		
		
    </div>
    
    </form>
</body>
</html>
