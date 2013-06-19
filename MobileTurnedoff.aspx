<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileTurnedoff.aspx.cs" Inherits="Eventomatic.MobileTurnedoff" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    The event host has not turned on Mobile Payments.  The only way you can complete this transaction is to pay on a computer.
    <br />
            <table>                    
                    <tr>
                        <td><asp:TextBox onclick="clearemail()" ID="txtEmail" Text="my@email.com" ForeColor=Gray runat="server" Width="300px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat=server ID=btnEmail Text="Send yourself Payment link by Email" 
                                onclick="btnEmail_Click" /></td>
                    </tr>
                </table>
    <br />
    If you are the event host, log into Groupstore, go to Settings and click on Enable Mobile Sales. This will accelerating ticket sales by allowing everyone to sell ticket on your behalf with their smartphones.
    </div>
    </form>
    <script language=javascript>        

        function clearemail() {
            document.getElementById('txtEmail').value = "";
        }
    </script>
</body>
</html>
