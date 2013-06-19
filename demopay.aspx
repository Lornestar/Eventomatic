<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demopay.aspx.cs" Inherits="Eventomatic.demopay" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register src="/Addons/fbloggedin.ascx" tagname="fbloggedin" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Groupstore - Demo Payment</title>
    <link href="Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" /> 
    <link href="Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />  
    <script type="text/javascript" src="Scripts.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdisdemogeneric" runat=server Value=0 />

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">   
<script type="text/javascript">
    function pageLoad(sender, eventArgs) {
        if (!eventArgs.get_isPartialLoad()) {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("InitialPageLoad");
        }
    }      
</script>   
</telerik:RadCodeBlock>   
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
        <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="btnContinue">
           <UpdatedControls>
               <telerik:AjaxUpdatedControl ControlID="pnldemopay" LoadingPanelID="RadAjaxLoadingPanel1" />
           </UpdatedControls>
       </telerik:AjaxSetting>
       <telerik:AjaxSetting AjaxControlID="btnTest">
           <UpdatedControls>
               <telerik:AjaxUpdatedControl ControlID="pnldemopay" LoadingPanelID="RadAjaxLoadingPanel1" />
           </UpdatedControls>
       </telerik:AjaxSetting>
   </AjaxSettings>
</telerik:RadAjaxManager>


<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Height="50px" IsSticky="true" ZIndex="90000">   
</telerik:RadAjaxLoadingPanel> 


<asp:Panel ID=pnldemopay runat=server>
            <div id="divEntiretable" class="DemoPay_Entiretable">
            
            <table>
            <tr>
                <td>
                <uc4:fbloggedin ID="fbloggedin1" runat="server" Visible="false"/>
                </td>
            </tr>
            <tr>
                    <td>
                    <img src="/images/demopay.jpg" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="DemoPay_Box">
                        <table width=100%>
                            <tr>
                                <td>
                                    <table class="DemoPay_InsideBox" width=100%>                                        
                                        <tr>
                                            <td style="text-align:center; font-weight:bold;">GROUPSTORE DEMO</td>
                                        </tr>
                                    <tr>
                                            <td><hr /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            This is an example of what the payment page looks like.
                                            As you can see in this example, $20 will go to your PayPal Account, and $0.88 will go into our account.  We never touch your money.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><hr /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            For purposes of this demo we will deposit $0.02 into your PayPal account.<br />
                                            What is your PayPal email address?<br />                                            
                                            <telerik:RadTextBox ID=txtPayPal runat=server EmptyMessage="your@email.com" Width=250px/>                                            
                                            <div style="color:Red;">***This is optional***</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><hr /></td>
                                        </tr>
                                        <tr>
                                            <td>We will also send you a copy of an e-ticket. What is your email address?
                                            <br />                                            
                                            <telerik:RadTextBox ID=txteticket runat=server EmptyMessage="your@email.com" Width=250px/>                                            
                                            <div style="color:Red;">***This is optional***</div>

                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td style="text-align:center;">                                             
                                                <asp:Button ID=btnContinue runat=server Text="Continue Demo" 
                                                    onclick="btnContinue_Click" />
                                            </td>
                                        </tr>
                                    </table>                                
                                </td>
                            </tr>
                        </table>                        
                        </div>                                                                        
                    </td>
                </tr>                
            </table>

            </div>
    </asp:Panel>                    

<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat=server  LoadingPanelID="LoadingPanel1"/>    

    </form>

<script language=javascript>



function Updatecenters()
{    
    posX = getScreenCenterX();
    posX = posX - 400;
    dv = document.getElementById("divEntiretable");
    dv.style.left = posX + "px"; // .cssText = "position:absolute;top:"+posY+"px;left:"+posX+"px;";*/    
}

Updatecenters();

function hasdonedemopay(messageback,newurl) {
    Updatecenters();
    alert(messageback);
    location.href = newurl;
}

function hasdonedemopaynomsg(newurl) {
    Updatecenters();    
    location.href = newurl;
}

</script>

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
