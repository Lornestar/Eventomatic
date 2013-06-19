<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Promo.aspx.cs" Inherits="Eventomatic.Promo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>The Groupstore</title>    
    <link href="Stylesheet1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table width=100%>
        <tr>
            <td align=center>
            
            
                <table width=600px>
                    <tr>
                        <td style="background-color:#410067; width:100%; text-align:left;">
                            <table width=100%>
                                <tr valign=bottom>
                                    <td><img src="Images/GroupStore5.jpg" height=75px /></td>
                                    <td align=right><div style="color:White; font-size:small; text-align:center;">Empowering Facebook Groups<br />with online stores.</div></td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align=left>
                            <table>
                                <tr>
                                    <td><a href="Promo.aspx">Home</a></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan=2>The easiest way to sell Facebook events online.</td>
                                </tr>
                                <tr>
                                    <td>Click here to get started</td><td><asp:LinkButton ID=btnadd runat=server CssClass="Button" Text="Add Groupstore"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td><hr /></td>
                    </tr>
                    <tr>
                        <td>@Copyright Groupstore 2010</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
