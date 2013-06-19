<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayForward_LoginFB.aspx.cs" Inherits="Eventomatic.PayForward_LoginFB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PayForward Login</title>
    <link href="Eventomatic_Mobile.css" media="screen" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" src="Addons/gosquared.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0b1/jquery.mobile-1.0b1.min.css" />
	<script type="text/javascript" src="http://code.jquery.com/jquery-1.6.1.min.js"></script>
	<script type="text/javascript" src="http://code.jquery.com/mobile/1.0b1/jquery.mobile-1.0b1.min.js"></script>            
</head>
<body>
    <form id="form1" runat="server">
    <div id="payforward" data-role="page"> 
	    <div  data-role="header" style="height:35px;">
            <table width=100%>
            <tr>
                <td>
                    PayForward Login
                </td>
                <td style="text-align:right;">
                    <span id="fbcheckin">
                        <asp:LinkButton id="btncheckinpaypal" CssClass="SigninPayPal" Text="&nbsp;" OnClick="btncheckinpaypal_Click" runat=server Width=87 Height=21 data-role="button" data-theme="none"/>
                    </span>
                </td>
            </tr>
            </table>
        </div>
        <div  data-role="content" style="height:440px;">
            You are currently not logged in.  You must log into PayForward with your Facebook ID.
        </div>
    </div>
    </form>
</body>
</html>
