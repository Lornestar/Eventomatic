<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transaction_Details.aspx.cs" Inherits="Eventomatic.Transaction_Details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Transaction Details</title>
</head>
<body>
<form id=form1 runat=server>
<table width=100%>
    <tr>
        <td align=center>
        
    <table width=500px style="border-width:thin; border-color:Black; border-style:solid;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>Paypal Transaction ID</td>
                        <td><asp:Label ID=lbltxn_id runat=server></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Buyer Name</td>
                        <td><asp:Label ID=lblbuyer runat=server></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Buyer Email</td>
                        <td><asp:Label ID=lblbuyer_email runat=server></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Transaction Amount</td>
                        <td><asp:Label ID=lblamount runat=server></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Transaction Date</td>
                        <td><asp:Label ID=lbldate runat=server></asp:Label></td>
                    </tr>
                </table>
            
            
            
            
            </td>
        </tr>
        <tr>
            <td><!--Gridview of tickets purchased--></td>
        </tr>
        <tr>
            <td>
            <asp:Button ID=btnsendemail runat=server Text="Send Eticket" 
                    onclick="btnsendemail_Click"  />
            </td>
        </tr>
    </table>
    
    </td>
    </tr>
</table>    
</form>
</body>
</html>
