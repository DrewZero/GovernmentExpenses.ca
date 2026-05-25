<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="webfront._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<table width="700">
		<tr>
			<td>
			   	Arguments: 
				<asp:TextBox ID="txtArguments" Text="types.xml rootpages.xml" 
					runat="server" Width="200px"></asp:TextBox>
			   	<asp:Button ID="btnExecute" runat="server" Text="Execute" 
				onclick="btnExecute_Click" />
			</td>
			<td>
				<asp:Button ID="btnKill" runat="server" onclick="btnKill_Click" 
				Text="Kill Process" />
			</td>
			<td>
				<asp:Button ID="btnUpdateCache" runat="server" Text="Update Cache" 
					onclick="btnUpdateCache_Click" />
			</td>
		</tr>
		</table>
		<br />
		<asp:Literal ID="litOutput" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
