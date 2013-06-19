<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileFacebookSettings.aspx.cs" Inherits="Eventomatic.MobileFacebookSettings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Eventomatic_Mobile.css" media="screen" rel="stylesheet" type="text/css" />        
</head>
<body>
<div id="mobilefacebooksettings"> 
<div  >
    <form id="form1" runat="server">
    <script language=javascript>
        
        function goback()
        { window.history.go(-2); }

    </script>
    <div>
<table width=100% >
<tr>
                    <td>
                    <center>
                    <table style="border:1px solid black; width:500px;">
                        <tr valign=top>
                            <td class="Store_Header">Facebook Settings 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID=lblfbsettings runat=server>Groupstore will post a News Story on your Facebook Wall each time you sell a ticket.</asp:Label>
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                        <asp:Button ID=btnAdjustWall Text="Turn OFF Wall Post" runat=server 
                                    onclick="btnAdjustWall_Click" />
                                        </td>
                                        <td>                                            
                                            <a href="#" onclick="goback()">Back</a>
                                        </td>
                                    </tr>
                                </table>                                
                            </td>
                        </tr>
                    </table>
</center>
</td>
</tr>
</table>
    
    </div>
    </form>
</div>
</div>
</body>
</html>
