<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirmation_Social.aspx.cs" Inherits="Eventomatic.Addons.Confirmation_Social" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <script src="../Scripts.js" language="javascript" type="text/javascript"></script>    
    <link href="../Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript" src="http://platform.linkedin.com/in.js"></script>
    <script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>    
    <script src="http://connect.facebook.net/en_US/all.js#xfbml=1"></script>
</head>
<body onload="AdjustRadWidowConfirmationSocial();">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>     
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <div id="fb-root"></div>
    <script type="text/javascript">        

        function CloseWindow()
        {            
            GetRadWindow().close('0');            
       }

       window.fbAsyncInit = function () {
           FB.init({ appId: "cb4d81e23b90fbc40d88dc3bf02db2e9", status: true, cookie: true,
               xfbml: true
           });

       };

       function AdjustRadWidowConfirmationSocial(){
           var oWindow = GetRadWindow();
           oWindow.scroll = "no";
           setTimeout(function () {
               oWindow.autoSize(true);
               if ($telerik.isChrome || $telerik.isSafari) {
                   ChromeSafariFixConfirmationSocial(oWindow);
               }
               else {
                   var bounds = oWindow.getWindowBounds();
                   var x = bounds.x;
                   oWindow.moveTo(x,2);
               }

           }, 2);
       }

       //fix for Chrome/Safari due to absolute positioned popup not counted as part of the content page layout
       function ChromeSafariFixConfirmationSocial(oWindow) {
           var iframe = oWindow.get_contentFrame();
           var body = iframe.contentWindow.document.body;

           setTimeout(function () {
               var height = body.scrollHeight;
               var width = body.scrollWidth;

               var iframeBounds = $telerik.getBounds(iframe);
               var heightDelta = height - iframeBounds.height;
               var widthDelta = width - iframeBounds.width;

               if (heightDelta > 0) oWindow.set_height(oWindow.get_height() + heightDelta);
               if (widthDelta > 0) oWindow.set_width(oWindow.get_width() + widthDelta);
               oWindow.center();
               var bounds = oWindow.getWindowBounds();
               var x = bounds.x;
               oWindow.moveTo(x, 50);

           }, 50);
       }

    </script>    
    <div style="width: 460px; height:250px; text-align:center;">
    <div class='ConfirmationPopup'>
    Thank you for your Registration, it is now complete.<br /><span class='ConfirmationPopupSmallerText'>You should receive an email with your e-ticket.</span>
    </div>        
    <br />
    <div class='ConfirmationPopupGotoFB'>
    <center>
    <table width=300px>
        <tr>
            <td style="text-align:left;" width=33%>Like it</td>
            <td class="ConfirmationSocial" width=33%>Tweet it</td>
            <td class="ConfirmationSocial" width=33%>Link it</td>
        </tr>
        <tr>
            <td style="text-align:left;"><asp:Label ID=lblfblike runat=server/></td>
            <td class="ConfirmationSocial"><asp:Label ID=lbltweet runat=server/></td>
            <td class="ConfirmationSocial"><asp:Label ID=lbllinkedin runat=server/></td>
        </tr>
    </table>
    </center>
    <br />
    This event will be much better with your friends.<br />Let them know you're going.   
    </div>
    </div>


    </form>
</body>
</html>
