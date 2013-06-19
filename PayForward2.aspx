<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayForward2.aspx.cs" Inherits="Eventomatic.PayForward2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Groupstore POS</title>
    <script src="Addons/sencha-touch-debug-w-comments.js" type="text/javascript"></script>    
    <script src="Payforward/Plugins/data.js" type="text/javascript"></script>    
           
    <link href="Payforward/css/sencha-touch.css" rel="stylesheet" type="text/css" />    
    <link href="Payforward/css/PayForward.css" rel="stylesheet" type="text/css" />   
<!--    <link rel="stylesheet" type="text/css" href="Payforward/css/pfsencha.css" />-->
    <script src="Payforward/Share.js" type="text/javascript"></script>
    <script src="Payforward/Help.js" type="text/javascript"></script>
    <script src="Payforward/amountbox.js" type="text/javascript"></script>    
    <script src="Payforward/Report.js" type="text/javascript"></script>
    <script src="Payforward/Sell_Items.js" type="text/javascript"></script>
    <script src="Payforward/directpayment.js" type="text/javascript"></script>
    <script src="Payforward/Sell.js" type="text/javascript"></script>
    <script src="Payforward/Settings_User.js" type="text/javascript"></script>
    <script src="Payforward/Settings.js" type="text/javascript"></script>    
    <script src="Payforward/Settings_ChangeStore.js" type="text/javascript"></script>
    <script src="Addons/gosquared.js" type="text/javascript"></script>
    <script src="Payforward/sencha-touch-signature.js" type="text/javascript"></script>
    <script src="Payforward/jpeg_encoder_basic.js" type="text/javascript"></script>

    <script src="PayForward.js" type="text/javascript"></script>
    <script src="Payforward/Plugins/iosnativebridge.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdkeypadplace" runat=server Value="0" />
    <asp:HiddenField ID="hdonsms" runat=server Value="0" />
    <asp:HiddenField ID="hdnfbid" runat=server Value="0" />
    <asp:HiddenField ID="hdnfbfirstname" runat=server Value="0" />
    <asp:HiddenField ID="hdnfblastname" runat=server Value="0" />
    <asp:HiddenField ID="hdResource_Key" runat=server Value="0" />
    <asp:HiddenField ID="hdisadmin" runat=server Value="False" />
    <asp:HiddenField ID="hdipaddress" runat=server Value="0" />
    <asp:HiddenField ID="hdStore_URL" runat=server Value="http://localhost:31578/" />
    <asp:HiddenField ID="hdfbAppid" runat=server Value="0" />
    <asp:HiddenField ID="hdStoreName" runat=server Value="0" />
    <asp:HiddenField ID="hddoppauth" runat=server Value="0" />
    <asp:HiddenField ID="hdhaveppauth" runat=server Value="False" />
    <asp:HiddenField ID="hdResourceCurrency" runat=server Value="CAD" />
    <asp:HiddenField ID="hdppemail" runat=server Value="0" />
    <asp:HiddenField ID="hdnumberofstores" runat=server Value="0" />
    <asp:HiddenField ID="hdisDemo" runat=server Value="False" />
    <asp:HiddenField ID="hddemoresourcekey" runat=server Value="137" />
    <asp:HiddenField ID="hdshowwelcome" runat=server Value="0" />
    <asp:HiddenField ID="hddodirectpayment" runat=server Value="False" />
    <asp:HiddenField ID="hdisnative" runat=server Value="True" />
    <asp:HiddenField ID="hdos" runat=server Value="browser" />    
    <div>
    snappay
    </div>
    <style type="text/css">
    body 
    {
          background-image:url('/images/loading.gif');
          background-repeat:no-repeat;
          background-position:center;          
          }
    </style>

    </form>
</body>
</html>
