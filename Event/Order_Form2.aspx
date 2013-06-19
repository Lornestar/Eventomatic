<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order_Form2.aspx.cs" Inherits="Eventomatic.Order_Form2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<%@ Register src="../Addons/Questions.ascx" tagname="Questions" tagprefix="uc2" %>

<%@ Register src="../Addons/Questions_Order_Form.ascx" tagname="Questions_Order_Form" tagprefix="uc1" %>
<%@ Register src="../Addons/FaceMash.ascx" tagname="FaceMash" tagprefix="uc3" %>
<%@ Register src="../Addons/fbloggedin.ascx" tagname="fbloggedin" tagprefix="uc4" %>
<%@ Register src="../Addons/LeaderBoard.ascx" tagname="LeaderBoard" tagprefix="uc5" %>
<%@ Register src="../Addons/EventNewsFeed.ascx" tagname="EventNewsFeed" tagprefix="uc6" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd" >
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"  xml:lang="en" lang="en">
<head id="Head1" runat="server">    
    <link href="../Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />    
    <link href="../Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" />    
    <link href="../Eventomatic_Mobile.css" media="screen" rel="stylesheet" type="text/css" />
    <link rel="SHORTCUT ICON" href="http://www.thegroupstore.com/favicon.ico"/>
    <script src="/Addons/meteora/meteora.js" type="text/javascript"></script>
    <script src="/Addons/DateTools.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts.js"></script>
    <script src="../Addons/CountdownTime.js" type="text/javascript"></script>    
    <script type="text/javascript" src="http://platform.linkedin.com/in.js"></script>
    <script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>    
    <style type="text/css">     
        .style2
        {
            width: 120px;
        }
        .style3
        {
            height: 26px;
        }
    </style>
</head>
<body onload="initpage()" style="background: rgb(255, 255, 255) no-repeat fixed center top; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;">
    
    <form id="form1" runat="server" >
<asp:HiddenField ID=hdSFP Value=0.0499 runat=server />
<asp:HiddenField ID=hdSFC Value=0.0 runat=server />    
<asp:HiddenField ID=hdSFM Value=2.5 runat=server />    
<asp:HiddenField ID=hdTicketMax Value=10 runat=server />
<asp:HiddenField ID=hdFreeTicket Value=False runat=server />
<asp:HiddenField ID=hdBackgroundImage Value="../Images/BackgroundImages/100.jpg" runat=server />
<asp:HiddenField ID=hdServiceFee Value="0" runat=server/>
<asp:HiddenField ID=hdOverallTotal Value="0" runat=server />
<asp:HiddenField ID=hdGoToPayment Value="0" runat=server />
<asp:HiddenField ID=hdFirstName Value="" runat=server />    
<asp:HiddenField ID=hdLastName Value="" runat=server />
<asp:HiddenField ID=hdTrial Value="False" runat=server />
<asp:HiddenField ID=hdLastTicketDate Value="0" runat=server />
<asp:HiddenField ID=hdCurrentDate Value="0" runat=server />
<asp:HiddenField ID=hdspecificuser Value="False" runat=server />
<asp:HiddenField ID=hdgvoffset Value=0 runat=server />
<asp:HiddenField ID=hdcurrencysymbol Value=0 runat=server />
<asp:HiddenField ID=hdnfbid runat=server Value=0 />
<asp:HiddenField ID="hdeventkey" runat=server Value=0 />
<asp:HiddenField ID="hdisdemopay" runat=server Value=0 />
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
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="pnlTickets" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnContinue">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlTickets" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                    <telerik:AjaxUpdatedControl ControlID="pnlButtons" />
                    <telerik:AjaxUpdatedControl ControlID="pnlQuestions" LoadingPanelID="RadAjaxLoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="pnlDetails"/>
                    <telerik:AjaxUpdatedControl ControlID="pnlTerms" />
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />                                                                                
                    <telerik:AjaxUpdatedControl ControlID="hdisdemopay" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>            
            <telerik:AjaxSetting AjaxControlID="btnBack">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlTickets"/>                    
                    <telerik:AjaxUpdatedControl ControlID="pnlButtons" />
                    <telerik:AjaxUpdatedControl ControlID="pnlQuestions" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetails" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTerms" />
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSaveDetails">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="pnlDetails" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Thumbnail" />
                    <telerik:AjaxUpdatedControl ControlID="Thumbnail2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnPreview">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="pnlfbloggedin" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>                        
        </AjaxSettings>
    </telerik:RadAjaxManager>
    
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
        <Windows>            
            <telerik:RadWindow ID="Tickets2" runat="server" OnClientClose="OnClientCloseTickets"
                NavigateUrl="../addons/Tickets2.aspx" Title="Edit Tickets" Behaviors="Close" >
            </telerik:RadWindow>            
            <telerik:RadWindow ID="Questions2" runat="server" OnClientClose="OnClientCloseQuestions"
                NavigateUrl="../addons/Questions2.aspx" Title="Edit Questions" Behaviors="Close" >
            </telerik:RadWindow>            
            <telerik:RadWindow ID="Background2" runat="server" OnClientClose="OnClientCloseBackground"
                NavigateUrl="../addons/BackgroundImage2.aspx" Title="Change Background" Behaviors="Close" >
            </telerik:RadWindow>            
            <telerik:RadWindow ID="Selltix" runat="server" Height="550px" Width="580px"
                NavigateUrl="../mobile.aspx" Title="Sell Tix" >
            </telerik:RadWindow>            
            <telerik:RadWindow ID="ShareApp" runat="server" Height="600px" Width="500px"
                NavigateUrl="../mobileshare.aspx" Title="Mobile App" >
            </telerik:RadWindow>            
        </Windows>
    </telerik:RadWindowManager>

<asp:Panel ID="Panel1" runat="server">
   <asp:Panel ID="Panel2" Visible="false" runat="server">
       My content:
   </asp:Panel>
</asp:Panel>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">  
</telerik:RadAjaxLoadingPanel> 


<script language="Javascript">
    /*function ChangeTotal() {
    var newtext = document.form1.ddlQuantity.selectedIndex;
    var total1 = document.getElementById("Total");
    total1.firstChild.nodeValue=newtext;
    }
    var OverallTotal = document.getElementById("GridView1_ctl04_lblTotalOverall");
    OverallTotal.firstChild.nodeValue = "test";

    function SubForm()
    {
    //document.form1.submit();
    var form1 = document.getElementById("form1");
    //form1.submit();
    alert('test');
    }*/


    function initpage() {
    var BkImg = document.getElementById("hdBackgroundImage");
    if (BkImg.value != 0)
        {
            document.body.style.backgroundImage = 'url('+BkImg.value+')';
        }

        start_counter();
        if (document.getElementById("GridView1") != null) {
            ManipulateGrid(0);
        }        
    }

    function ManipulateGrid(gvid) {

        var gv1 = document.getElementById("GridView1");
        //alert(gv.rows.length);
        var OverallTotal = 0;
        var QuantityTotal = 0;
        var ServiceFeeTotal = 0;
        var ServiceFeeRow = 0;
        var SFP = document.getElementById("hdSFP");
        var SFC = document.getElementById("hdSFC");
        var SFM = document.getElementById("hdSFM");
        var gvoffset = document.getElementById("hdgvoffset");

        var gv = gv1;
        for (i = 1; i < gv.rows.length + parseInt(gvoffset.value); i++) {
            var extradigit = "0";
            if (i > 9) {
                extradigit = "";
            }
            //var cell = gv.rows[i].cells;
            //var HTML = cell[0].innerHTML;
            //var Price = HTML.indexof("lblPrice");
            var Price = document.getElementById("GridView1_ctl" + extradigit + i + "_lblPrice");
            var Quantity = document.getElementById("GridView1_ctl" + extradigit + i + "_ddlQuantity");
            var Total = document.getElementById("GridView1_ctl" + extradigit + i + "_lblTotal");
            var Donatetxt = document.getElementById("GridView1_ctl" + extradigit + i + '_txtDonate');

            
                        
            if (Price != null && Quantity != null) {
                var tempPrice = Price.innerHTML.replace(",", "");
                var RowTotal = (replacedollarsigns(tempPrice) * Quantity.selectedIndex);
                Total.innerHTML = getcurrencysymbol() + " " + RowTotal.toFixed(2);                                
                OverallTotal = OverallTotal + RowTotal;
                QuantityTotal = QuantityTotal + Quantity.selectedIndex;
                ServiceFeeRow = 0;
                if (RowTotal != 0) {
                    ServiceFeeRow = (RowTotal * parseFloat(SFP.value)) + parseFloat(SFC.value * Quantity.selectedIndex);
                    if (parseFloat(ServiceFeeRow) > parseFloat(SFM.value * Quantity.selectedIndex)) {
                        ServiceFeeRow = parseFloat(SFM.value * Quantity.selectedIndex);

                    }
                    ServiceFeeTotal += ServiceFeeRow;
                }
                //var total = parseFloat();
                //alert(Price.innerHTML.replace("$", "") * Quantity.selectedIndex);                      
            }
            else if (Donatetxt != null) {
                if (checkForNumeric(Donatetxt)) {
                    var RowTotal = parseFloat(Donatetxt.value);

                    Total.innerHTML = getcurrencysymbol() + " " + RowTotal.toFixed(2);
                    OverallTotal = OverallTotal + RowTotal;
                    var ServiceFeeRow = 0;
                    //Service fee row
                    if (RowTotal != 0) {
                        ServiceFeeRow = (RowTotal * parseFloat(SFP.value)) + parseFloat(SFC.value * 1);
                        if (parseFloat(ServiceFeeRow) > parseFloat(SFM.value * 1)) {
                            ServiceFeeRow = parseFloat(SFM.value * 1);

                        }
                        ServiceFeeTotal += ServiceFeeRow;
                    }
                } //if its numeric
            } //donatetxt != null
            else if (i == gv.rows.length - 1 + parseInt(gvoffset.value)) {
                /*var ServiceFeeTotal = 0;
                if (OverallTotal != 0)
                {                    
                ServiceFeeTotal = (OverallTotal * parseFloat(SFP.value)) + parseFloat(SFC.value);   
                if ((ServiceFeeTotal* QuantityTotal) > parseFloat(SFM.value))
                {
                ServiceFeeTotal = parseFloat(SFM.value* QuantityTotal);
                }
                } */
                if (Total == null) {
                    var i2 = i - 1;
                    Total = document.getElementById("GridView1_ctl" + extradigit + i2 + "_lblTotal");
                }
                Total.innerHTML = getcurrencysymbol() + " " + ServiceFeeTotal.toFixed(2);
                OverallTotal = OverallTotal + ServiceFeeTotal;    
            }
        }
        if (i > 9) {
            extradigit = "";
        }
        var OverallTotalText = document.getElementById("GridView1_ctl" + extradigit + i + "_lblTotalOverall");
        var i3 = i - 1;
        if (OverallTotalText == null) {
            OverallTotalText = document.getElementById("GridView1_ctl" + extradigit + i3 + "_lblTotalOverall");
        }        
        OverallTotalText.innerHTML = getcurrencysymbol() + " " + OverallTotal.toFixed(2);        
        document.getElementById("hdServiceFee").value = ServiceFeeTotal;
        document.getElementById("hdOverallTotal").value = OverallTotal.toFixed(2);
    }

    function replacedollarsigns(tempPrice) {
        tempPrice = tempPrice.replace("$", "");
        tempPrice = tempPrice.replace("€", "");
        tempPrice = tempPrice.replace("£", "");
        tempPrice = tempPrice.replace("₪", "");
        return tempPrice;
    }

    function getcurrencysymbol() {
        return document.getElementById("hdcurrencysymbol").value;
    }

    function doRemoveTicket(ticketid){
    var answer = confirm("Are you sure you want to Remove the Ticket?")
	if (answer){
		__doPostBack('doRemoveTicket',ticketid);
	}
	else{
	//
	}
}

function doRemoveQuestion(ticketid) {
    var answer = confirm("Are you sure you want to Remove the Question?")
    if (answer) {
        __doPostBack('doRemoveQuestion', ticketid);
    }
    else {
        //
    }
}

function PromptSaved() {
    alert('Your Changes have been Saved'); location.href = 'Default.aspx';
}

function checkForNumeric(obj) {
    if (/[^0-9\.]|-{2,}/gi.test(obj.value)) {
        alert("Must enter a positive numeric value");
        obj.focus();
        obj.select();
        return false;
    }
    return true;
}

function goback(url) {
  //  alert(url);
    window.location = url;
}

</script>
<center>
<div class="Order_Form_1">
    
        <div class="Order_Form_2">
            <div class="Order_Form_3">            
            
<table width=100%>
    <tr>
        <td align=center class="EntireStoreBox">
        
    <table>
    <tr>
        <td>
            <table width=100%>
                <tr>
                    <td align="left" >
                    <asp:Panel ID=pnlfbloggedin runat=server Visible=false>
                    <table class="Store_Content" >
                        <tr>
                            <td><uc4:fbloggedin ID="fbloggedin1" runat="server"/></td>
                            <td style="text-align:center;">
                            <center>
                            <asp:Panel ID=pnladmintools runat=server Visible=false>
                            <table>
                                <tr>
                                    <td>
                                    <div id="cih9f7" style="z-index:100;position:absolute"></div><div id="sch9f7" style="display:inline"></div><div id="sdh9f7" style="display:none"></div><script type="text/javascript">                                                                                                                                                                                               var seh9f7 = document.createElement("script"); seh9f7.type = "text/javascript"; var seh9f7s = (location.protocol.indexOf("https") == 0 ? "https" : "http") + "://image.providesupport.com/js/lornestar/safe-standard.js?ps_h=h9f7&ps_t=" + new Date().getTime(); setTimeout("seh9f7.src=seh9f7s;document.getElementById('sdh9f7').appendChild(seh9f7)", 1)</script><noscript><div style="display:inline"><a href="http://www.providesupport.com?messenger=lornestar">Live Help Desk</a></div></noscript>                                    
                                    </td>
                                    <td>
                                    <asp:LinkButton id="btnPreview" class="OrderFormButtons_Top" Text="Preview Page" OnClick="btnPreview_Click" runat="server" Visible="false"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                                <a href=#>                     
                            <asp:Label ID=lblgoback runat=server class="OrderFormButtons_Top" Text="Go to My Events" Font-Size="Smaller" Visible=false></asp:Label>                            
                            </a>                                                                                           
                                    </td>
                                    <td>
                                    <a href=#>                     
                            <asp:Label ID=lblBackground runat=server class="OrderFormButtons_Top" Text="Edit Background" Font-Size="Smaller" Visible=false></asp:Label>
                            </a>    
                                    </td>
                                </tr>
                            </table>                            
                            </asp:Panel>
                            </center>
                            </td>
                        </tr>
                        <tr>
                            <td colspan=2  style="font-size:smaller; font-weight:bold; font-family:Arial; background-color:#30004C; color:White;">
                            <div style="display:none;">
                                Your Event URL: <asp:Label ID=lbleventurl runat=server/>
                                </div>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    
                        </td>
                    <td align="right">
                    <table style="background-color:White;" >
                        <tr valign=middle>
                            <td>
                            <table>
                                <tr>
                                    <td>
                                    <asp:Image ID=imgGroup runat=server Height="30px" />
                            <telerik:RadBinaryImage runat="server"  ResizeMode="Fit" Height="30px"
                ID="Thumbnail2"  AlternateText="Thumbnail" CssClass="binary-image" Visible=false/>                                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                                        <telerik:RadAsyncUpload runat="server" ID="AsyncUpload2" MaxFileInputsCount="1" OnClientFileUploaded="fileUploaded"
                                                            OnFileUploaded="AsyncUpload2_FileUploaded" AllowedFileExtensions="jpeg,jpg" OnClientValidationFailed="validationFailed"
                                                             Width="100px" Visible=false MaxFileSize="524288" >
                                                            <Localization Select="Change Picture"/>
                                                        </telerik:RadAsyncUpload>
                                    </td>
                                </tr>
                            </table>                                                                
                            </td>
                            <td><asp:Label ID=lblGroupName runat=server></asp:Label></td>
                            <td><span class=SellAll>Groupstore</span></td>
                        </tr>                
                    </table>
                    </td>
                </tr>                
            </table>            
            
        </td>
    </tr>
    <tr>
        <td class="Store_Header"><asp:Label ID="lblEvent_Name" runat="server" Text="lblEvent_Name">        
        </asp:Label>        
        </td>
    </tr>
    <tr>
        <td>                      
            <telerik:RadTabStrip runat="server" ID="RadTabStrip1" Orientation="HorizontalTop"
                SelectedIndex="0" MultiPageID="RadMultiPage1" EnableEmbeddedSkins=false>
                <Tabs>
                    <telerik:RadTab Text="Tickets" SelectedCssClass="SelectedTab_Tel" CssClass="UnSelectedTab_Tel" >
                    </telerik:RadTab>
                    <telerik:RadTab Text="  Questions" SelectedCssClass="SelectedTab_Tel" CssClass="UnSelectedTab_Tel" Enabled=false>
                    </telerik:RadTab>                    
                </Tabs>
            </telerik:RadTabStrip>                        
                    <table width=100% class="OrderFormText">
                    <tr>
                    <td>
                    <table>
                            <tr valign=top>
                                <td>
                                <table>
                                <tr>
                                <td>
                                
                                <asp:Panel ID=pnlDetails runat=server Visible=true>
                            <table width=100% class="Store_Content">
                                <tr>
                                    <td colspan=2 class="Section_Header">Details</td>
                                </tr>
                                <tr>
                                    <td colspan=2 class="Before_Each_Question">
                                    <asp:Panel ID=pnltxtEventName runat=server Visible=false>
                                        Event Name: <telerik:RadTextBox ID="txtEventName" EmptyMessage="Your Event Name" runat="server" Width="300px"/>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                            <td class="Before_Each_Question">Start Date:</td>
                            <td class="Regular_Text_Areas"><asp:Label ID="lblStart" runat="server"></asp:Label>
                            <telerik:RadDatePicker ID="rdpStartDate" runat="server" Width=100px Visible=false/>
                            <telerik:RadTimePicker ID="rdpStartTime" runat="server" Width=80px Visible=false/>
                            </td>
                        </tr>
                        <tr valign=top>
                            <td class="Before_Each_Question">End Date:</td>
                            <td class="Regular_Text_Areas"><asp:Label ID="lblEnd" runat="server"></asp:Label>
                            <telerik:RadDatePicker ID="rdpEndDate" runat="server" Width=100px Visible=false/>
                            <telerik:RadTimePicker ID="rdpEndTime" runat="server" Width=80px Visible=false />
                            </td>
                        </tr>
                        <tr>
                            <td colspan=2 class="Before_Each_Question">
                            <asp:Panel ID=pnlddlTimezone runat=server Visible=false>
                                Timezone:<asp:DropDownList ID=ddlTimezone runat=server Width=300px></asp:DropDownList>                        
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr valign=top>
                            <td class="Before_Each_Question">Location:</td>
                            <td class="Regular_Text_Areas"><table>
                                <tr>
                                    <td><asp:Label ID="lblLocation" runat="server"></asp:Label>
                                    <telerik:RadTextBox ID="txtLocation" EmptyMessage="Venue Name" runat="server" Width="300px" Visible="false"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblStreet" runat="server"></asp:Label>
                                    <telerik:RadTextBox ID="txtStreet" EmptyMessage="Street Name" runat="server" Width="300px" Visible="false"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblCity" runat="server"></asp:Label>
                                    <telerik:RadTextBox ID="txtCity" EmptyMessage="City Name" runat="server" Width="300px" Visible="false"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:HyperLink Font-Size="Small" ForeColor="Blue" ID="hypMapit" Text="View Map" Target="_blank" runat="server"></asp:HyperLink></td>
                                </tr>
                                
                                </table>
                            </td>
                        </tr>
                         
                          <tr valign=top>
                            <td class="Before_Each_Question">Host:</td>
                            <td class="Regular_Text_Areas"><asp:Label ID="lblHost" runat="server"></asp:Label>
                            <telerik:RadTextBox ID="txtHost" EmptyMessage="Your Event Name" runat="server" Width="300px" Visible="false"/>
                            </td>
                        </tr>
                        <tr valign=top>
                            <td class="Before_Each_Question">Contact:</td>
                                    <td class="Regular_Text_Areas">
                                        <table>
                                            <tr>
                                                <td><asp:Label ID="lblPhone" runat="server"></asp:Label>                                                
                                                </td>
                                                <td><asp:Label ID="lblEmail" runat="server"></asp:Label>                                                
                                                </td>
                                            </tr>
                                        </table>
                                        <telerik:RadMaskedTextBox Width="200px" ID="txtPhone" runat="server" Mask="(###)###-####" Visible=false/>                                           
                                        <br />                                        
                                        <telerik:RadTextBox ID="txtEmailInput" EmptyMessage="email@yoursite.com" runat="server" Width="300px" Visible="false"/>
                                    </td>
                                </tr>
                              <tr valign=top>
                            <td colspan=2 width=100% align=left >
                                
                                    <span class="Before_Each_Question">Description:</span>
                                    <br />
                                    
                                        <asp:Label ID="lblComments" runat="server" ></asp:Label>
                <telerik:RadTextBox ID="txtComments" EmptyMessage="Describe your event here" runat="server" Width="400px" TextMode=MultiLine Height="200px" Visible="false"/>
                                    
                            </td>
                                    
                                    
                                </tr> 
                                <tr>
                                    <td colspan=2 class="Before_Each_Question">
                                    <asp:Panel ID=pnlConfirmation runat=server Visible=false>
                                    Order Confirmation:
                                    <br />
                                    <telerik:RadTextBox ID="txtConfirmation" EmptyMessage="This is the message your customers recieve after they have purchased their tickets. An example would be 'Thank you for your purchase'" runat="server" Width="400px" TextMode=MultiLine Height="100px" />
                                    </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan=2>
                                    <asp:Panel ID=pnltixdetails runat=server Visible=false>
                                        <table>
                                            <tr>    
                                                <td>Display how many tickets are available? <asp:CheckBox ID="chkDisplayAvailable" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Maximum amount of tickets can be purchased at a time<asp:DropDownList ID=ddlMaxTickets runat=server>                        
                                                        <asp:ListItem value="1">1</asp:ListItem>
                                                        <asp:ListItem value="2">2</asp:ListItem>
                                                        <asp:ListItem value="3">3</asp:ListItem>
                                                        <asp:ListItem value="4">4</asp:ListItem>
                                                        <asp:ListItem value="5">5</asp:ListItem>
                                                        <asp:ListItem value="6">6</asp:ListItem>
                                                        <asp:ListItem value="7">7</asp:ListItem>
                                                        <asp:ListItem value="8">8</asp:ListItem>
                                                        <asp:ListItem value="9">9</asp:ListItem>
                                                        <asp:ListItem value="10" selected="True">10</asp:ListItem>
                                                </asp:DropDownList></td>
                                            </tr>                    
                                        </table>
                                    </asp:Panel>                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td align=right colspan=2>
                                    <asp:LinkButton id="btnSaveDetails" class="OrderFormButtons" Text="Save Details" OnClick="btnSaveDetails_Click" runat="server" Visible="false"/>
                                    </td>
                                </tr>
                            </table> 
                            </asp:Panel>                               
                            </td>
                            </tr>                                                            
                                <tr>
                                    <td align="left">
                                        <table>                                                                                        
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlQuestions" runat="server" Visible="false">                                                        
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                    <table class="Store_Content" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <span ID="QuestionsSection">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td class="Section_Header">
                                                                                            Questions</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        <telerik:RadListView ID="RadListView3" runat="server" Width=500 OnItemDataBound="RadListView3_ItemDataBound"
                                                                                            ItemPlaceholderID="ListViewContainer" >            
                                                                                            <LayoutTemplate>                                                                                                                
                                                                                                    <asp:PlaceHolder id="ListViewContainer" runat="server" />                                                                                                                   
                                                                                            </LayoutTemplate>            
                                                                                            <ItemTemplate>                                                                                                
                                                                                            
                                                                                            <table style="border-bottom:1px solid black;">
                                                                                        <tr>
                                                                                        <td>
                                                                                            <span ID="GuestList">
                                                                                            
                                                                                            <table width="100%">
                                                                                                <!--  <tr>
                                                        <td class="Section_Header" colspan=2><asp:Label ID=lblGuestlist runat=server>Guest List</asp:Label></td>
                                                    </tr>-->
                                                                                                <tr>
                                                                                                                <td colspan=2><asp:Label ID=lbltixname runat=server Text=<%#Eval("tix_Name")%>/></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="Fine_Print">
                                                                                                        <asp:Label ID="lblGuestListComment" runat="server">What name do you want showing on the guest list?</asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table>                                                                                                            
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <span class="Mandatory_Star">*</span> First Name:</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtList_FirstName" runat="server" Width="200px"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="style3">
                                                                                                                    <span class="Mandatory_Star">*</span> Last Name:</td>
                                                                                                                <td class="style3">
                                                                                                                    <asp:TextBox ID="txtList_LastName" runat="server" Width="200px"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>                                                                                                            
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            </span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <uc1:Questions_Order_Form ID="Questions_Order_Form1" runat="server"/>                                                                                                                                                                                  
                                                                                        </td>
                                                                                    </tr>
                                                                                        </table>  
                                                                                                                                                                                        
                                                                                            </ItemTemplate>
                                                                                            </telerik:RadListView>

                                                                                                                                                                              
                                                                                        </td>
                                                                                    </tr>                                                                                    
                                                                                </table>
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            <asp:Panel ID="pnlemail" runat="server" Visible="false">
                                                                                <table>
                                                                                    <tr>
                                                                                                                <td>
                                                                                                                    
                                                                                                                        <span class="Mandatory_Star">*</span>Email:
                                                                                                                </td>
                                                                                                                <td>                                                                                                              
                                                                                                                        <asp:TextBox ID="txtFreeEmail" runat="server" Width="200px"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                </table>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="Fine_Print">
                                                                                <br />
                                                                                <span class="Mandatory_Star">*</span> Mandatory questions</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>                                    
    </tr>
    </table>
    </td>
    <td>
                                                    <table>
                                                    <tr>
                                                        <td>
                                                    <asp:Image ID="imgEvent" runat="server" ImageUrl="Images/Events/no_image.jpg" 
                                                        Width="200px" />
                                                        <telerik:RadBinaryImage runat="server"  ResizeMode="Fit" Width="200px"
                ID="Thumbnail"  AlternateText="Thumbnail" CssClass="binary-image" Visible=false/>
                                                        <br />
                                                        <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" MaxFileInputsCount="1" OnClientFileUploaded="fileUploaded"
                                                            OnFileUploaded="AsyncUpload1_FileUploaded" AllowedFileExtensions="jpeg,jpg" OnClientValidationFailed="validationFailed"
                                                             Width="200px" Visible=false MaxFileSize="524288" >
                                                            <Localization Select="Change Picture"/>
                                                        </telerik:RadAsyncUpload>
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <table>
                                            <tr>
                                                <td class="HelpPromote">
                                                    Help Promote this Event</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center;">                                                
                                                    <table style="text-align:center; vertical-align:bottom;">
                                                        <tr>
                                                            <td>
                                                                <script type="in/share"></script>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsharebtn" runat="server" Visible="false"></asp:Label>
                                                                <asp:Panel ID="pnltwitter" runat="server">
                                                                    <a href="http://twitter.com/share" class="twitter-share-button" data-count="none">Tweet</a>
                                                                </asp:Panel>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblfblike" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>                                            
                                        </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                            <td >
                                <asp:Panel ID=pnlAttending runat=server Visible=false>
                                <table width=100%>                                    
                                    <tr>
                                        <td>
                                        <asp:Panel ID=pnlfbattending runat=server Visible=false>
                                            <table>
                                                <tr>
                                                    <td colspan=2><hr /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID=lblAttending runat=server># Attending</asp:Label>
                                                    </td>
                                                    <td>
                                                    <asp:LinkButton  ID="lbviewattending" runat="server" Text="View"
                                                    OnClientClick="openWinfbGuestList(#,1); return false;" />                                        
                                                </tr>
                                                <tr>
                                                    <td colspan=2>
                                                        <uc3:FaceMash ID="FaceMash1" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        </td>                                        
                                    </tr>    
                                    <tr>
                                        <td>
                                            <asp:Panel ID=pnlfbattending2 runat=server Visible=false>
                                            <table>
                                                <tr>
                                                    <td colspan=2><hr /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID=lblAttending2 runat=server># Maybe Attending</asp:Label>
                                                    </td>
                                                    <td>
                                                    <asp:LinkButton  ID="lbviewattending2" runat="server" Text="View"
                                                    OnClientClick="openWinfbGuestList(#,2); return false;" /> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan=2>
                                                        <uc3:FaceMash ID="FaceMash2" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>               
                                    <tr>
                                        <td>
                                            <asp:Panel ID=pnlfbattending3 runat=server Visible=false>
                                            <table>
                                                <tr>
                                                    <td colspan=2><hr /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID=lblAttending3 runat=server># Not Attending</asp:Label>
                                                    </td>
                                                    <td><asp:LinkButton  ID="lbviewattending3" runat="server" Text="View"
                                                    OnClientClick="openWinfbGuestList(#,3); return false;" /> </td>
                                                </tr>
                                                <tr>
                                                    <td colspan=2>
                                                        <uc3:FaceMash ID="FaceMash3" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>                                                                                         
                                </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID=pnlMobile runat=server Visible=false >
                                 
                                 <table style="width:240px;">
                                    <tr>
                                        <td class="HelpPromote">Help Sell this Event</td>
                                    </tr>
                                    <tr>
                                        <td>                                        
                                        <center><asp:LinkButton id="btnshare" class="Mobile_SellNow" runat=server Width=107 Height=26 Enabled=false/><br /></center>
                                        <asp:HyperLink ID=hypbbapp runat=server onclick="javascript:openWinShareApp();" Visible=false>Click here to Download Blackberry App</asp:HyperLink>                                        
                                        <asp:Label ID=lblgskey runat=server  Visible=false/>
                                        <br />
                                        <asp:Image ID=imgMobileSell runat=server Width="200px"/></td>
                                    </tr>
                                </table>
                                <table style="background-color:White;">
                                    <tr>
                                        <td style="text-align:left;">
                                        <asp:Label ID=lblLeaderPrize runat=server></asp:Label><br />
                                        Leaderboard: <asp:HyperLink ID=hypmobileleader runat=server NavigateUrl="../MobileLeader.aspx?event=0" Text="See All Leaders"/><br />
                                        <uc5:LeaderBoard ID="LeaderBoard1" runat="server" />
                                        </td>
                                    </tr>               
                                    <tr>
                                        <td style="text-align:left;">
                                        Event Newsfeed: <asp:HyperLink ID=hypmobilenews runat=server NavigateUrl="../MobileNews.aspx?event=0" Text="See All News"/>                                    
                                            <uc6:EventNewsFeed ID="EventNewsFeed1" runat="server" />                                    
                                        </td>
                                    </tr>   
                                 </table>
                                    
                                </asp:Panel>
                                <asp:Panel ID=pnlgivemobilepermission runat=server Visible=false Width=200>
                                Mobile Sales Force - If you are the event host, log into Groupstore, go to Settings and click on Enable Mobile Sales. This will accelerating ticket sales by allowing everyone to sell ticket on your behalf with their smartphones.
                                </asp:Panel>
                            </td>
                        </tr>
                                                        </table>
                                                </td>
    </td>
    </tr>
    </table>
                                    
                                </td>
                            </tr>                            
                            <tr>
                                <td class="Store_Content">
                                    <asp:Panel ID=pnlTickets runat=server>
                                    <table width=100%>
                                        <tr>
                                    <td class="Section_Header">Tickets</td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="lblSellingDeadline" runat=server Visible=false CssClass="Fine_Print"></asp:Label>            
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" 
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true 
                  Width="700px" CssClass="ticket_selector">
<RowStyle Wrap="True" BorderStyle="None"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Ticket_Description" HeaderText="Description" 
                        ReadOnly="True" ItemStyle-Width="300px"/>
                    <asp:BoundField DataField="Quantity_Remaining" HeaderText="Remaining" 
                        ReadOnly="True" Visible=false/>                       
                    <asp:TemplateField HeaderText="Sale Ends">
                        <ItemTemplate>
                        <asp:Label id="lblEnds" runat="Server" text='<%# Bind("Sale_Ends") %>' Width=100px></asp:Label>
                        <asp:Label id="Timezoneshort" runat="Server" text='<%# Bind("Timezoneshort") %>' Visible=false></asp:Label>
                        <asp:Label ID="lbldescription" runat=server Text='<%# Bind("Ticket_Description") %>' Visible=false></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                 
                    <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("PriceRounded","{0:0.00}") %>'></asp:Label>
                        <asp:Label ID="lblTicketKey" runat="server" Text='<%# Bind("Ticket_Key") %>' Visible=false></asp:Label>
                        <asp:Label ID="lblIsDemo" runat="server" Text='<%# Bind("IsDemo") %>' Visible=false></asp:Label>
                        <asp:Label id=lblDollarSign runat=server Visible=false>$</asp:Label>
                        <asp:TextBox id="txtDonate" Maxlength=6 runat="server" onKeyUp="ManipulateGrid(0);" visible=false width=45px text="0.00"></asp:TextBox>                        
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlQuantity" runat="server" onchange="ManipulateGrid(0);">
                                <asp:ListItem value="0" selected="True">0</asp:ListItem>
                                <asp:ListItem value="1">1</asp:ListItem>
                                <asp:ListItem value="2">2</asp:ListItem>   
                                <asp:ListItem value="3">3</asp:ListItem>
                                <asp:ListItem value="4">4</asp:ListItem>
                                <asp:ListItem value="5">5</asp:ListItem>
                                <asp:ListItem value="6">6</asp:ListItem>
                                <asp:ListItem value="7">7</asp:ListItem>
                                <asp:ListItem value="8">8</asp:ListItem>
                                <asp:ListItem value="9">9</asp:ListItem>
                                <asp:ListItem value="10">10</asp:ListItem>                             
                            </asp:DropDownList>
                            <asp:Label ID=lblServiceFee runat=server Visible=false>Service fee</asp:Label>
                            <asp:Label id=lblDonate runat=server visible=false >1</asp:Label>
                            <asp:Label ID=lblCouponCode runat=server Visible=false Text='<%# Bind("Coupon_Code") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" FooterStyle-CssClass="TicketPurchase_TotalFooter" >
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" Width="80" >$ 0.00</asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalOverall" runat="server" Width="100" >$ 0.00</asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>

<FooterStyle BackColor="White"></FooterStyle>

<HeaderStyle CssClass="TicketPurchase_Header" HorizontalAlign="Left"></HeaderStyle>
            </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left;">
                                    <asp:Panel ID=pnlCouponCode runat=server Visible=false>
                                        Coupon Code:<asp:TextBox ID=txtCouponCode runat=server MaxLength=20></asp:TextBox>
                                        <asp:Button ID=btnCouponCode runat=server text="Enter" 
                                            onclick="btnCouponCode_Click"/>
                                            <br />
                                            <asp:Label ID=lblnomatch runat=server ForeColor=Blue Visible=false>No match, please try again.</asp:Label>
                                    </asp:Panel>
                                    </td>
                                </tr>
                                    </table>
                                    </asp:Panel>
                                </td>
                                </tr>  
                                <tr>
                                    <td class="Store_Content">
                                    <asp:Panel ID="pnlTickets_Edit" runat="server" Visible="false">
                                        <table width=100% >
                                        <tr>
                                    <td class="Section_Header">Tickets</td>
                                </tr>
            <tr>
                <td>
                <telerik:RadListView ID="RadListView1" runat="server" Width="500"
                        ItemPlaceholderID="ProductTitlePlaceHolder" OnItemDataBound="RadListView1_ItemDataBound">                        
                        <LayoutTemplate>       
                             <table id="products" class="Edit_Events_Tickets">
                        <thead>
                            <tr>                                
                                <th style="width:150px;">
                                    Description
                                </th>
                                <th style="width:80px;">
                                    Sale Begins
                                </th>
                                <th style="width:80px;">
                                    Sale Ends
                                </th>
                                <th style="width:80px;">
                                    Price
                                </th>
                                <th style="width:80px;">
                                    Capacity
                                </th>
                                <th style="width:100px;">
                                Action      
                                </th>
                            </tr>
                        </thead>
                        <tbody >
                            <tr id="ProductTitlePlaceHolder" runat="server">
                            </tr>
                        </tbody>
                    </table>         
                            
                        </LayoutTemplate>            
                        <ItemTemplate>                                                            
                                <tr valign=top >                                    
                                    <td >
                                    <asp:label ID=lbldescription runat=server Text='<%#Eval("Ticket_Description")%>'/> </td>
                                    <td><asp:Label runat=server ID=lblSaleBegins Text='<%#Eval("Begin_Selling")%>'></asp:Label> </td>
                                    <td><asp:Label runat=server ID=lblSaleEnds Text='<%#Eval("Selling_Deadline")%>'></asp:Label> </td>
                                    <td><asp:Label runat=server ID=lblPrice Text='<%#Eval("PriceRounded","{0:0.00}")%>'></asp:Label> </td>
                                    <td><asp:label ID=lblcapacity runat=server Text='<%#Eval("Capacity")%>'/></td>
                                    <td class="Action_Background" style="text-align:center;"><asp:label ID=lblAction runat=server />
                                    <asp:label ID=lblticketkey runat=server Text='<%#Eval("Ticket_Key")%>' Visible=false />
                                    </td>
                                </tr>                            
                        </ItemTemplate>
                        </telerik:RadListView>
                </td>
            </tr>
            <tr>
                <td>
                <a href=#>                     
        <asp:Label ID=lblAddNew runat=server Text="Add Ticket" CssClass="OrderFormButtons" ></asp:Label>  </a>    
        
                </td>
            </tr>
                                           
                </table>
                </asp:Panel>
                                    </td>
                                </tr>  
                                <tr>
                                    <td>
                                    <asp:Panel ID=pnlQuestions_Edit runat=server Visible=false CssClass="Store_Content">
                                    <table width="100%">
                                    <tr>
                                        <td class="Section_Header">
                                            Questions</td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <telerik:RadListView ID="RadListView2" runat="server" Width=500
                        ItemPlaceholderID="ProductTitlePlaceHolder" OnItemDataBound="RadListView2_ItemDataBound">                        
                        <LayoutTemplate>       
                             <table id="products" class="Edit_Events_Tickets">                        
                        <tbody >
                            <tr id="ProductTitlePlaceHolder" runat="server">
                            </tr>
                        </tbody>
                    </table>         
                            
                        </LayoutTemplate>            
                        <ItemTemplate>                                                            
                                <tr valign=top >                                    
                                    <td >
                                    <asp:Label ID=lblQuestionsMandatory runat=server ForeColor=Red Text="*" Visible=false></asp:Label>
                             <asp:Label ID=lblQuestion_Text Text='<%#Eval("The_Question")%>' runat=server CssClass="Each_Question"></asp:Label>:<br />
                             <asp:TextBox ID=txtQuestionsAnswer runat=server Width=400></asp:TextBox>
                             <asp:DropDownList ID=ddlQuestionsAnswer runat=server Visible=false></asp:DropDownList>                                                                 
                             <asp:Label ID=lblQuestion_Type runat=server Text='<%#Eval("Question_Type")%>' Visible=false/>
                             <asp:Label ID=lblMandatory runat=server Text='<%#Eval("Mandatory")%>' Visible=false/>
                             <asp:Label ID=lblquestionkey runat=server Text='<%#Eval("Question_Key")%>' Visible=false/>
                                    </td>
                                    <td class="Action_Background" style="text-align:center;"><asp:label ID=lblQuestionAction runat=server />
                                    </td>
                                </tr>                            
                        </ItemTemplate>
                        </telerik:RadListView>
                                        
                                        </td>
                                    </tr>
                                    
        <tr>
            <td>
                   <a href='#'>                                    
        <asp:Label ID=lblAddnewQuestion runat=server Text="Add Question" CssClass="QuestionsButton"/>
                                    </a>
            </td>
        </tr>                     
                                    <tr>
        <td style="font-size:smaller;"><asp:Label ID=lblPleaseNote runat=server>*Please note that Groupstore is already asking for First name, Last name & Email.</asp:Label></td>
    </tr>
</table>

                                    </asp:Panel>
                                    </td>
                                </tr>     
                                <tr>
                    <td class="style2" style="width:100%">
                        <asp:Panel ID=pnlTerms runat=server Visible=false>
                            <asp:CheckBox ID="chkTerms" runat="server" />I have read and accept the <a href=Ticket_Buyer_TOS.htm target="_blank">Terms of Service</a>, and understand the Service Fee is not refundable.<br />All refund requests should be made directly to the Event Host no later than 1 business day after the event ends.
                        </asp:Panel>
                    </td>                    
                </tr>
    <tr>
        <td width=100%>
        <asp:Panel ID=pnlButtons runat=server>
        <table width=100%>
        <tr>
                    <td align=center ><asp:Label ID=lblError runat=server Visible=false CssClass="Store_Content"></asp:Label>
                    <span id=lblErrorClient style="color:Red;"></span></td>
                </tr>            
                <tr>
                    <td>
                    <table width=100% >                            
                        <tr>
                            <td class="style2"><br /><div class="PaypalLogo"></div></td>
                    <td><center>
                        
                        <table>
                            <tr>
                                <td><asp:LinkButton id="btnBack" class="OrderFormButtons" Text="Back" Visible=false OnClick="btnBack_Click" runat=server /></td>
                                <td><asp:LinkButton id="btnContinue" class="OrderFormButtons" Text="Continue" OnClick="btnContinue_Click" runat=server/></td>
                            </tr>
                            <tr>
                                <td colspan=2><asp:Label id=lblbuyguest runat=server style="text-align:center; font-size:small;" Visible=false></asp:Label></td>
                            </tr>
                        </table>
                        
                        </center>
                    </td>
                    <td align=right style="width:160px;"><a href="http://www.thegroupstore.com" target=_blank><div class="GroupStoreLogo"></div></a></td>
                        </tr>
                    </table>

                    </td>                    
                </tr>
                
                <tr>
                    <td><asp:Button ID="btnTest" runat="server" Text="Test" 
                            onclick="btnTest_Click" Visible=false /></td>                    
                </tr>
                <tr>
                    <td><asp:Label ID=lblTest runat=server Visible=false></asp:Label>
                </tr>
        </table>
        </asp:Panel>
        </td>
    </tr>
    
        <tr>
        <td align="center"><asp:Label ID=lblDemo runat="Server"></asp:Label></td>
    </tr>    
        <tr>
            <td align="center" class="Fine_Print">© Copyright Groupstore 2010</td>
        </tr>

</table>
</td>
</tr>
</table>
</div>
    </div>
            </div>
</center>
<div id="Page_Loader" class="Page_Loader">
<table width=100% height=100%>
    <tr valign=middle>
        <td >&nbsp;</td>
        <td width=500px align=center >
            <div id="Page_Loader2" class="Page_Loader2">
   <img src="/images/page_loader.gif" /> 
<b>You are now being directed to PayPal to complete your purchase. /b><br />
You will receive two e-mails:
<ul>
    <li>Sell it All e-ticket</li>
    <li>PayPal receipt</li>
</ul>
Thank you for your purchase.
</div>

        </td>
        <td>&nbsp;</td>
    </tr>
</table>

</div>

<div id="tzcdFrame" class="Countdown_Timer">
<table >
    <tr>
        <td align=center><div id="tzcd_text"></div>
        </td>
    </tr>
    <tr>
        <td align=center><div id="tzcd"></div>
        </td>
    </tr>
</table>
</div>
<div id="fb-root"></div>    
<script src="http://connect.facebook.net/en_US/all.js"></script>
<script type="text/javascript">
window.fbAsyncInit = function() {
    FB.init({ appId: "cb4d81e23b90fbc40d88dc3bf02db2e9", status: true, cookie: true,
             xfbml: true});

     };

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
