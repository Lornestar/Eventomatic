<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayForwardmp.aspx.cs" Inherits="Eventomatic.PayForwardmp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Groupstore Payment</title>
    <script src="Addons/sencha-touch-debug-w-comments.js" type="text/javascript"></script>    
<!--    <script src="Addons/sencha-touch.js" type="text/javascript"></script>-->
    <script src="Payforward/Plugins/data.js" type="text/javascript"></script>    
           
    <link href="Payforward/css/sencha-touch.css" rel="stylesheet" type="text/css" />    
    <link href="Payforward/css/PayForward.css" rel="stylesheet" type="text/css" />   
<!--    <link rel="stylesheet" type="text/css" href="Payforward/css/pfsencha.css" />-->            
    <script src="Payforward/directpayment.js" type="text/javascript"></script>
    <script src="Payforward/Sell.js" type="text/javascript"></script>    
    <script src="Addons/gosquared.js" type="text/javascript"></script>
    <script src="Payforward/jpeg_encoder_basic.js" type="text/javascript"></script>
    <script src="Payforward/sencha-touch-signature.js" type="text/javascript"></script>

    <script src="PayForwardmp.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdipaddress" runat=server Value="0" />    
    <asp:HiddenField ID="hdshowwelcome" runat=server Value="0" />    
    <asp:HiddenField ID="hdtxkey" runat=server Value="0" />    
    <asp:HiddenField ID="hdfbAppid" runat=server Value="0" />
    <asp:HiddenField ID="hdStore_URL" runat=server Value="http://localhost:31578/" />
    <asp:HiddenField ID="hdisDemo" runat=server Value="False" />
    <asp:HiddenField ID="hdAlreadyPaid" runat=server Value="False" />
    <div>
    groupstore
    </div>


    </form>
</body>
</html>
