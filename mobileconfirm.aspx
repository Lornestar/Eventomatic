<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mobileconfirm.aspx.cs" Inherits="Eventomatic.mobileconfirm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Groupstore Confirmation</title>
    <link href="Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div class='ConfirmationPopup'>
    Thank you for your Registration, it is now complete.<br /><span class='ConfirmationPopupSmallerText'>You should receive an email with your e-ticket.</span>
    </div>        
    <br />
    <div class='ConfirmationPopupGotoFB'>
    <br />
    Thank you for using Groupstore Mobile....and have a great time at the event.
    <asp:Label id=lbldemo runat=server ForeColor="Red" Visible=false></asp:Label>
    </div>
    <script type="text/javascript">
        var GoSquared = {};
        GoSquared.acct = "GSN-105322-Y";
        (function (w) {
            function gs() {
                w._gstc_lt = +(new Date); var d = document;
                var g = d.createElement("script"); g.type = "text/javascript"; g.async = true; g.src = "//d1l6p2sc9645hc.cloudfront.net/tracker.js";
                var s = d.getElementsByTagName("script")[0]; s.parentNode.insertBefore(g, s);
            }
            w.addEventListener ? w.addEventListener("load", gs, false) : w.attachEvent("onload", gs);
        })(window);
</script>
    </form>
</body>
</html>
