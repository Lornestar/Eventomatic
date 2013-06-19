<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Promo_Demo.aspx.cs" Inherits="Eventomatic.Promo_Demo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Demo Groupstore POS</title>
    <link href="../Eventomatic_Mobile.css" media="screen" rel="stylesheet" type="text/css" />    
    <script language=javascript src="Addons/gosquared.js"></script>
</head>
<body>
<div id="mobileshare" > 
	<div>    
    <form id="form1" runat="server">
    <center>
    <table width=300 >                
        <tr>
            <td class="Section_Header" style="text-align:center;">
            <asp:Label ID=lblheader runat=server>Try Groupstore Demo</asp:Label>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID=lblaction runat=server>Send the Demo link to your own iPhone, iPad, Android or BlackBerry Torch.</asp:Label></td>
        </tr>
        <tr>
            <td>
                <table>                    
                    <tr>
                        <td><asp:TextBox onclick="clearsms()" ID="txtPhoneNumber" Text="555-555-5555" ForeColor=Gray runat="server" Width="300px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat=server ID=btnphone Text="Get Demo link by SMS" 
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
                        <td><asp:Button runat=server ID=btnEmail Text="Get Demo link by email" 
                                onclick="btnEmail_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>                    
                    <tr>
                        <td><asp:Label ID=lblqrcheckout runat=server>Get Demo link by qr Code</asp:Label></td>
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
    <div  data-role="footer">© Copyright Groupstore 2011</div>           
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
