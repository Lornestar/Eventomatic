<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Windy_Confirmation.aspx.cs" Inherits="Eventomatic.Windy_Confirmation" %>
<%@ Register src="Addons/Payment_Details.ascx" tagname="Payment_Details" tagprefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml" >
<head id="Head1" runat="server">
    <title>Order Confirmation</title>
    <link href="Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" /> 
    <link href="Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />      
    <link REL="SHORTCUT ICON" HREF="http://www.thegroupstore.com/favicon.ico"/>      
    <link href="Addons/boxy.css" rel="stylesheet" type="text/css" />
    <script src="Addons/jquery.js" type="text/javascript"></script>
    <script src="Addons/jquery.boxy.js" type="text/javascript"></script>
    <script src="http://connect.facebook.net/en_US/all.js"></script>
    <script type="text/javascript" src="Scripts.js"></script>
    <script type="text/javascript" src="http://platform.linkedin.com/in.js"></script>
    <script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>    
    </head>
<body >
    <form id="form1" runat="server">
    <asp:HiddenField ID=hdeventkey runat=server Value=0 />
    <asp:HiddenField ID=hdeventname runat=server Value=0 />
    <asp:HiddenField ID=hdfirstname runat=server Value=0 />
    <asp:HiddenField ID=hdtxkey runat=server Value=0 />

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server"/>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true"  OffsetElementID="theoffset">
        <Windows>            
            <telerik:RadWindow ID="Confirmation_Social" runat="server" OnClientClose="OnClientCloseConfirmationSocial"
                NavigateUrl="addons/Confirmation_Social.aspx" Behaviors="Close">
            </telerik:RadWindow>                        
        </Windows>
    </telerik:RadWindowManager>
    <center>
    <table width=500px >
    <tr>
        <td class="Store_Header">Order Confirmation<div id="theoffset"/>
        </td>
    </tr>
    <tr >
        <td >
            <table width=100%>
                <tr valign=top>
                <td width=100%>
                <table width=100%>                
                <tr>
                    <td width=100%>
                    <asp:Panel ID=pnlTxDetails runat=server>
                    <table width=100%>
                        <tr>
                            <td class="Section_Header">Transaction Details</td>
                        </tr>
                        <tr>
                            <td><uc1:Payment_Details ID="Payment_Details1" runat="server" /></td>
                        </tr>
                    </table> 
                    </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td width=100%>
                        <asp:Panel ID=pnlCustomMessage runat=server>
                        <table width=100%>
                            <tr>
                                <td class="Section_Header">Message from host</td>
                            </tr>
                            <tr>
                                <td><asp:Label ID=lblConfirmation runat=server>lblconfirmation</asp:Label></td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
                </td>
                <td align=right>
                                    <asp:Image ID=imgEvent runat=server ImageUrl="Images/Events/no_image.jpg" Width=200px />
                                </td>
                                </tr>
            </table>            
       </td>
    </tr>
    <tr>
        <td align=center> You should receive an email with your e-ticket.
        <br /> Your transaction is now complete.  You may now close this window.</td>
    </tr>
    
    <tr>
        <td align="center"><hr />
        <asp:Label id=lbldemo2 runat=server ForeColor="Red" Visible=false></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center">© Copyright Groupstore 2010</td>
    </tr>
    </table>
    
    </center>

    <div id="tzcdFrame" class="Confirmation_Background"></div>
<div id="fb-root"></div>    


<div id='thesocialpopup' style="width: 460px; text-align:center; position:absolute; top:145px; background-color:White; border:1px solid black;opacity:0.95;filter:alpha(opacity=95);z-index: 102;">
    <div style="float:right; background-color:Black;">
        <a href='#' onclick='ClosePopup2()'>
        <img src='Images/Closebutton.jpg'/>
        </a>
    </div>
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
    <asp:Label id=lbldemo runat=server ForeColor="Red" Visible=false></asp:Label>
    </div>
    </div>

<script language="javascript" type="text/javascript">
var publishfb;
function Openboxy() {
    //Boxy.alert("Please confirm:", function () { alert('Confirmed!'); }, { title: 'Message' });

    publishfb = new Boxy("<div class='ConfirmationPopup'>Thank you for your Registration, it is now complete.<br /><span class='ConfirmationPopupSmallerText'>You should receive an email with your e-ticket.</span></div><br /><br /><div class='ConfirmationPopupGotoFB'>This event will be much better with your friends.<br />Let them know you're going.<br /><br /><a href='#' onclick='Signupfb()'><img src='Images/fbshare2.png' /></a></div>", { title: 'Transaction is Complete',closeText: "<img src='Images/Closebutton.jpg'/>"});    
    publishfb.moveToY(50);            
    publishfb.options.afterDrop = function(){ ClosePopup();}
    document.getElementById("tzcdFrame").style.display = 'block';        
return false;    
}

function Signupfb(){

FB.login(function(response) {
  if (response.session) {
    if (response.perms) {
      // user is logged in and granted some permissions.
      // perms is a comma separated list of granted permissions
      //alert('1');
    } else {
      // user is logged in, but did not grant any permissions
      // Both clicked on allow & Already Logged in
      //alert('2');            
      Publishfb();     
      Updatefbid(); 
    }
  } else {
    // user is not logged in
    //Clicked on Don't allow
    //alert('3');
  }
}, {});

}

function ClosePopup(){
document.getElementById("tzcdFrame").style.display = 'none';
publishfb.hide();
}

function ClosePopup2(){
document.getElementById("tzcdFrame").style.display = 'none';
document.getElementById("thesocialpopup").style.display = 'none';
}

function Publishfb(){

FB.ui(
   {
     method: 'feed',
     name: 'Groupstore - ' + document.getElementById("hdeventname").value,
     link: 'http://www.thegroupstore.com/order_form.aspx?event_key='+ document.getElementById("hdeventkey").value,
     picture: 'http://www.thegroupstore.com/images/Events/' + document.getElementById("hdeventkey").value + '.jpg',
     //caption: 'Reference Documentation',
     description: document.getElementById("hdfirstname").value + ' bought a ticket to ' + document.getElementById("hdeventname").value,
     //message: 'Come join me at the party.'
   },
   function(response) {
     if (response && response.post_id) {
       //alert('Post was published.');
       ClosePopup();
     } else {
       //alert('Post was not published.');
       ClosePopup();
     }
   }
 );

}

window.fbAsyncInit = function() {
    FB.init({appId: "<%= ConfigurationSettings.AppSettings["APIKey"] %>", status: true, cookie: true,
             xfbml: true});
               
  };
  
  (function() {
    var e = document.createElement('script'); e.async = true;
    e.src = document.location.protocol +
      '//connect.facebook.net/en_US/all.js';
    document.getElementById('fb-root').appendChild(e);
  }());

  function Updatefbid()
  {    

    $.ajax({
        type: "POST",
        url: "/Order_Confirmation1.asmx/SaveFBtx",        
        data: "fbid=" + FB.getSession().uid + "&txkey=" + document.getElementById("hdtxkey").value,
        success: function(msg) {            
            //alert($(msg).text());
        },
        error: AjaxFailed
    });
  }
  
  function AjaxSucceeded(result) {
    alert(result);
}

function AjaxFailed(result) {
//alert(result.status + ' ' + result.statusText);

} 

function Openradwindow()
{
    document.getElementById("tzcdFrame").style.display = 'block';
    openWinConfirmationSocial(document.getElementById("hdeventkey").value);    
}

function SetupPopupSocial()
{

    posX = getScreenCenterX();
    posX = posX - 230;
    dv = document.getElementById("thesocialpopup");
    dv.style.left = posX + "px"; // .cssText = "position:absolute;top:"+posY+"px;left:"+posX+"px;";*/    
}

document.getElementById("tzcdFrame").style.display = 'block';
SetupPopupSocial();
  </script>      
  </form>
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
</body>
</html>