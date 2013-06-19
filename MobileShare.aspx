<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileShare.aspx.cs" Inherits="Eventomatic.MobileShare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Groupstore Mobile Share Event</title>
    <link href="../Eventomatic_Mobile.css" media="screen" rel="stylesheet" type="text/css" />    
    <script language=javascript src="Addons/gosquared.js"></script>
</head>
<body>
<div id="mobileshare" > 
	<div>    
    <form id="form1" runat="server">
    <center>
    <table width=300>                
        <tr>
            <td class="Section_Header">
            <asp:Label ID="lblEvent_Name" runat="server" Text="lblEvent_Name"/>
            </td>
        </tr>
        <tr>
            <td>
                <table>                    
                    <tr>
                        <td><asp:TextBox onclick="clearsms()" ID="txtPhoneNumber" Text="555-555-5555" ForeColor=Gray runat="server" Width="300px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat=server ID=btnphone Text="Send buyer Payment by SMS" 
                                onclick="btnphone_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>                    
                    <tr>
                        <td><asp:TextBox onclick="clearemail()" ID="txtEmail" Text="my@email.com" ForeColor=Gray runat="server" Width="300px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat=server ID=btnEmail Text="Send buyer Payment by Email" 
                                onclick="btnEmail_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>                    
                    <tr>
                        <td><asp:Label ID=lblqrcheckout runat=server>Send buyer Payment by qr code</asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                        <asp:Image ID=imgqr runat=server />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table> 
    <div  data-role="footer">© Copyright Groupstore 2010</div>           
    </center>
    <script language=javascript>
        function clearsms() {
            document.getElementById('txtPhoneNumber').value = "";
        }

        function clearemail() {
            document.getElementById('txtEmail').value = "";
        }
        function goback()
        { window.history.go(-1); }
    </script>
    </form>
    </div>
    </div>
</body>
</html>
