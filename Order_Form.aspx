<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order_Form.aspx.cs"  Inherits="Eventomatic.Order_Form" %>



<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<%@ Register src="Addons/Questions.ascx" tagname="Questions" tagprefix="uc2" %>

<%@ Register src="Addons/Questions_Order_Form.ascx" tagname="Questions_Order_Form" tagprefix="uc1" %>
<%@ Register src="Addons/FaceMash.ascx" tagname="FaceMash" tagprefix="uc3" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd" >
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"  xml:lang="en" lang="en">
<head runat=server>    
    <link href="Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" />    
    <link REL="SHORTCUT ICON" HREF="http://www.thegroupstore.com/favicon.ico">
    <script src="/Addons/meteora/meteora.js" type="text/javascript"></script>
    <script src="/Addons/DateTools.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts.js"></script>
    <script src="Addons/CountdownTime.js" type="text/javascript"></script>
    <script src="http://connect.facebook.net/en_US/all.js#xfbml=1"></script>
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

<telerik:RadScriptManager ID="ScriptManager" runat="server" />            
        <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">            
        </telerik:RadCodeBlock>
<telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true" Behaviors="Close" OnClientClose="OnClientClosefbGuestList">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" 
                NavigateUrl="addons/fbGuestList.aspx" >
            </telerik:RadWindow>                        
        </Windows>
    </telerik:RadWindowManager>
 
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


function initpage(){
if (document.getElementById("UltraWebTab1_ctl02_GridView3") != null)
{document.getElementById("UltraWebTab1_ctl02_GridView3").style.display = "none";}

if (document.getElementById("btnPaypal") != null)
{
document.getElementById("btnPaypal").style.display = "none";
           document.getElementById("btnCC").style.display = "none";  
           document.getElementById("btnFree").style.display = "none";  
           if (document.getElementById("chkPaypal") != null)
           {
           document.getElementById("chkPaypal").checked = false;         
           document.getElementById("chkCC").checked = false;
           }
           
          //document.getElementById("GuestList").style.display = "none";        
ensureChecked();
start_counter();
 }
 

 
var myUltraWebTab = igtab_getTabById('UltraWebTab1');
var hdgotopayment = document.getElementById("hdGoToPayment");
var lblerror = document.getElementById("lblError");
if (lblerror != null){
    if ((lblerror.style.display == "none")&&(hdgotopayment.value == "0")){        
        myUltraWebTab.setSelectedIndex(0);
        }
    else if(hdgotopayment.value == "1"){
        myUltraWebTab.setSelectedIndex(2);
    }
    else{
        myUltraWebTab.setSelectedIndex(1);
    }
}
else if(hdgotopayment.value == "1"){
        myUltraWebTab.setSelectedIndex(2);
    }
else{
        myUltraWebTab.setSelectedIndex(0);
    }

var BkImg = document.getElementById("hdBackgroundImage");
if (BkImg.value != 0)
    {
        document.body.style.backgroundImage = 'url('+BkImg.value+')';
    }
}

function changetab(){
var myUltraWebTab = igtab_getTabById('UltraWebTab1');
var lblerror = document.getElementById("lblErrorClient");
if (myUltraWebTab.getSelectedIndex() == 0){
   if (confirmQuantitySelected()){
   
        myUltraWebTab.setSelectedIndex(1);
        lblerror.style.display = "none";
        ensureChecked();
         }
   else{
        
        //show error
        lblerror.style.display = "block";
        lblerror.innerHTML = "Please select the Ticket Quantity you wish to purchase.";
       }
   }
else if (myUltraWebTab.getSelectedIndex() == 1){   
    if((document.getElementById("UltraWebTab1_ctl01_txtList_FirstName").value.length == 0)||(document.getElementById("UltraWebTab1_ctl01_txtList_LastName").value.length==0)){
        lblerror.style.display = "block";
        lblerror.innerHTML = "Please enter the First & Last Name.";
    }
    else{
        __doPostBack('btnContinue2','');
    }
  }
}



function confirmQuantitySelected(){
    var gv = document.getElementById("UltraWebTab1_ctl00_GridView1");
    var QuantityTotal = 0;
    for (i=1; i<gv.rows.length; i++)
    { 
     var extradigit = "0";
     if (i > 9)
     {
        extradigit = "";
     }
     var Quantity = document.getElementById("UltraWebTab1_ctl00_GridView1_ctl" + extradigit + i + "_ddlQuantity");
     var txtDonate = document.getElementById("UltraWebTab1_ctl00_GridView1_ctl" + extradigit + i + "_txtDonate");
     if (Quantity != null)
        {QuantityTotal = QuantityTotal + Quantity.selectedIndex;}     
     else if (txtDonate != null)
     {
        if (parseFloat(txtDonate.value)> 0)
        {
            QuantityTotal += 1;
        }
     }
    }
if (QuantityTotal>0){
return true;}
else
{return false;}
}

function submitit(){
ManipulateGrid(0);
document.getElementById("Page_Loader").style.display='block';
var btncontinue = document.getElementById("btnContinue");
 var gv2 = document.getElementById("UltraWebTab1_ctl01_GridView2");
btncontinue.style.display = "none";
if (gv2 == null){
    document.getElementById("btnBack").style.display = "none";
    }
setTimeout("document.form1.submit();", 7000);

}
function backtab(){        
    var hdgotopayment = document.getElementById("hdGoToPayment");
    var myUltraWebTab = igtab_getTabById('UltraWebTab1');
    if (myUltraWebTab.getSelectedIndex() == 2)
    {
        hdgotopayment.value = "0";         
        if (myUltraWebTab.Tabs[1].visible)
            {
                myUltraWebTab.setSelectedIndex(1);
            }        
        else{
                myUltraWebTab.setSelectedIndex(0);
            }
        ensureChecked();
    }
    else{
        myUltraWebTab.setSelectedIndex(0);    
        ensureChecked();
    }        
}

function btnPurchasecheck(){
var cb = document.getElementById("chkTerms");
var lblerror = document.getElementById("lblErrorClient");
var myUltraWebTab = igtab_getTabById('UltraWebTab1'); 
if (( cb.checked == false ) || ((myUltraWebTab.Tabs[1].visible == false) && (confirmQuantitySelected() == false))) {
    lblerror.style.display = "block";
    if ( cb.checked == false ){
    lblerror.innerHTML = "Please read and accept the Terms of Service.";    
    }
    else{
    lblerror.innerHTML = "Please select the Ticket Quantity you wish to purchase."; 
    }
} 
else {
    lblerror.style.display = "none";    
    document.getElementById("hdFirstName").value = document.getElementById("UltraWebTab1_ctl01_txtList_FirstName").value;
    document.getElementById("hdLastName").value = document.getElementById("UltraWebTab1_ctl01_txtList_LastName").value;
    if (document.getElementById("btnPaypal").style.display == "block")
        {                   
            __doPostBack('btnPaypal','');
        }
    else if (document.getElementById("btnCC").style.display == "block")
        {
            __doPostBack('btnCC','');
        } 
    else if (document.getElementById("btnFree").style.display == "block")
        {
            __doPostBack('btnFree','');
        } 
    
}
}

function ensureChecked() {
            var btncontinue = document.getElementById("btnContinue");
            var btncontinue2 = document.getElementById("btnContinue2");
            var btnback = document.getElementById("btnBack");    
            var btnpurchasespan = document.getElementById("btnPurchaseSpan");
            var cb = document.getElementById("chkTerms");
            var cbwhole = document.getElementById("Terms");
            var myUltraWebTab = igtab_getTabById('UltraWebTab1');  
            var hdgotopayment = document.getElementById("hdGoToPayment");           
            //btnpurchase.href = "javascript:btnPurchasecheck();";
            document.getElementById("btnCC").href = "javascript:btnPurchasecheck();";
            document.getElementById("btnPaypal").href = "javascript:btnPurchasecheck();";
            document.getElementById("btnFree").href = "javascript:btnPurchasecheck();";
            btncontinue2.href = "javascript:changetab();";
            var gv2 = document.getElementById("UltraWebTab1_ctl01_GridView2");              
            var QuestionsSection = document.getElementById("QuestionsSection");              
            //alert(gv2);
            if (gv2 == null)//no questions
            {
                /*myUltraWebTab.Tabs[2].setEnabled(false);
                myUltraWebTab.Tabs[1].setEnabled(false);
                myUltraWebTab.Tabs[1].setVisible(false);*/
                //QuestionsSection.style.display = "none";
            }
            else{
                myUltraWebTab.Tabs[2].setEnabled(false);
                gv2.style.display = "none";
            }
            //
            //cbwhole.style.display = "none";
            //alert(hdgotopayment.value);            
            if (((myUltraWebTab.getSelectedIndex() == 1) || (myUltraWebTab.Tabs[1].visible == false))&&(hdgotopayment.value == "0")){                
                cbwhole.style.display = "none";  
                //btnpurchase.style.display = "block";              
                btncontinue.style.display = "none";
                btncontinue2.style.display = "block";
                
                if (myUltraWebTab.Tabs[1].visible == true){
                    btnback.style.display = "block";                
                    myUltraWebTab.Tabs[0].setEnabled(false);
                }
                else{
                    btnback.style.display = "none";                
                }
                
                if ( cb.checked == true ) {
                var lblerror = document.getElementById("lblErrorClient");
                lblerror.style.display = "none";
                        //btnpurchase.disabled = false;
                        //btnpurchase.Attributes["disabled"] = "enabled";
                        //btnpurchase.href="javascript:__doPostBack('btnPurchase','');";
                        //alert('enable btnpurchase');
                        //btnpurchasespan.innerHTML = "<a id='btnPurchase' class='OrderFormButtons' href="+ String.fromCharCode(34)+ "javascript:__doPostBack('btnPurchase','')"+ String.fromCharCode(34)+ ">Purchase</a>";
                } 
                else {
                // btnpurchase.disabled = false;
                 //btnpurchase.setAttribute["disable"] = "disabled";
                 //btnpurchasespan.innerHTML = "<span class='OrderFormButtons'>Purchase</span>";
                 //alert(btnpurchase.disabled);
                 //btnpurchasespan.innerHTML = "<span id='btnPurchase' class='OrderFormButtons'>Purchase</span>";
                 
                    }
           }
           else if(hdgotopayment.value == "1"){//go to payment
                btncontinue.style.display = "none";
                btncontinue2.style.display = "none";
                cbwhole.style.display = "block"; 
                myUltraWebTab.setSelectedIndex(2);
                myUltraWebTab.Tabs[2].setEnabled(true);
                myUltraWebTab.Tabs[0].setEnabled(false);
                myUltraWebTab.Tabs[1].setEnabled(false);
                ManipulateGrid(0);
           }
           else{
           myUltraWebTab.Tabs[1].setEnabled(false);
           cbwhole.style.display = "none";            
            btnback.style.display = "none";
            btncontinue2.style.display = "none";              
            btncontinue.style.display = "block";
            btnback.style.display = "none";
            ManipulateGrid(0);
           }
           if (myUltraWebTab.getSelectedIndex() != 2)
           {
           document.getElementById("btnPaypal").style.display = "none";
           document.getElementById("btnCC").style.display = "none";            
           document.getElementById("btnFree").style.display = "none";
           //document.getElementById("GuestList").style.display = "none";        
           }           
           if ((myUltraWebTab.getSelectedIndex() == 2) && (document.getElementById("hdFreeTicket").value == "True"))
           {
           document.getElementById("btnFree").style.display = "block";
           }
}   
function ManipulateGrid(gvid)
        {
        
            var gv1 = document.getElementById("UltraWebTab1_ctl00_GridView1");
            var gv2 = document.getElementById("UltraWebTab1_ctl02_GridView3");
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
            var gvother = gv2;
            if (gvid == 1){
                gv = gv2;
                gvother = gv1;
            }
            for (i=1; i<gv.rows.length+parseInt(gvoffset.value); i++)
            { 
            var extradigit = "0";
             if (i > 9)
             {
                extradigit = "";
             }
                //var cell = gv.rows[i].cells;
                //var HTML = cell[0].innerHTML;
                //var Price = HTML.indexof("lblPrice");
             var Price1 = document.getElementById("UltraWebTab1_ctl00_GridView1_ctl" + extradigit + i + "_lblPrice");
             var Quantity1 = document.getElementById("UltraWebTab1_ctl00_GridView1_ctl" + extradigit + i + "_ddlQuantity");
             var Total1 = document.getElementById("UltraWebTab1_ctl00_GridView1_ctl" + extradigit + i + "_lblTotal");
             var Donatetxt = document.getElementById("UltraWebTab1_ctl00_GridView1_ctl" + extradigit + i + '_txtDonate');
             
             var Price2 = document.getElementById("UltraWebTab1_ctl02_GridView3_ctl" + extradigit + i + "_lblPrice");
             var Quantity2 = document.getElementById("UltraWebTab1_ctl02_GridView3_ctl" + extradigit + i + "_ddlQuantity");
             var Total2 = document.getElementById("UltraWebTab1_ctl02_GridView3_ctl" + extradigit + i + "_lblTotal");                       
             
            
             var Price = Price1;
             var Quantity = Quantity1;
             var Total = Total1;             
             var Quantityother = Quantity2;
             var Totalother = Total2;
             if (gvid == 1){                
                Quantity = Quantity2;
                Total = Total2;
                Quantityother = Quantity1;
                Totalother = Total1;
             }
              if (Price != null && Quantity != null)
              {                            
              var tempPrice = Price.innerHTML.replace(",", "");
              var RowTotal = (replacedollarsigns(tempPrice) * Quantity.selectedIndex);
              Total.innerHTML = getcurrencysymbol() + " " + RowTotal.toFixed(2);
              Totalother.innerHTML = getcurrencysymbol() + " " + RowTotal.toFixed(2);
              Quantityother.selectedIndex = Quantity.selectedIndex;
              OverallTotal = OverallTotal + RowTotal;
              QuantityTotal = QuantityTotal + Quantity.selectedIndex;
              ServiceFeeRow = 0;
              if (RowTotal != 0)
                {                
                ServiceFeeRow = (RowTotal * parseFloat(SFP.value)) + parseFloat(SFC.value * Quantity.selectedIndex);                                                   
                if (parseFloat(ServiceFeeRow) > parseFloat(SFM.value * Quantity.selectedIndex))
                    {
                        ServiceFeeRow = parseFloat(SFM.value* Quantity.selectedIndex);
                        
                    }                    
                ServiceFeeTotal += ServiceFeeRow;
                }                                                        
              //var total = parseFloat();
              //alert(Price.innerHTML.replace("$", "") * Quantity.selectedIndex);                      
              }
              else if (Donatetxt != null)
              {
                if (checkForNumeric(Donatetxt))
                {
                    var RowTotal = parseFloat(Donatetxt.value);
                    
                    Total.innerHTML = getcurrencysymbol() + " " + RowTotal.toFixed(2);
                    OverallTotal = OverallTotal + RowTotal;
                    var ServiceFeeRow = 0;
                    //Service fee row
                    if (RowTotal != 0)
                    {                
                    ServiceFeeRow = (RowTotal * parseFloat(SFP.value)) + parseFloat(SFC.value * 1);
                    if (parseFloat(ServiceFeeRow) > parseFloat(SFM.value * 1))
                        {
                            ServiceFeeRow = parseFloat(SFM.value* 1);
                            
                        }                    
                    ServiceFeeTotal += ServiceFeeRow;
                    }                                                                            
                 }//if its numeric
              }//donatetxt != null
              else if(i == gv.rows.length-1+parseInt(gvoffset.value)){
                /*var ServiceFeeTotal = 0;
                if (OverallTotal != 0)
                {                    
                    ServiceFeeTotal = (OverallTotal * parseFloat(SFP.value)) + parseFloat(SFC.value);   
                    if ((ServiceFeeTotal* QuantityTotal) > parseFloat(SFM.value))
                    {
                        ServiceFeeTotal = parseFloat(SFM.value* QuantityTotal);
                    }
                } */               
                Total.innerHTML = getcurrencysymbol() + " " + ServiceFeeTotal.toFixed(2);
                Totalother.innerHTML = getcurrencysymbol() + " " + ServiceFeeTotal.toFixed(2);
                OverallTotal = OverallTotal + ServiceFeeTotal;                                
              }                                          
            }  
            if (i > 9)
             {
                extradigit = "";
             }                      
            var OverallTotalText = document.getElementById("UltraWebTab1_ctl00_GridView1_ctl" + extradigit + i + "_lblTotalOverall");
            var OverallTotalText2 = document.getElementById("UltraWebTab1_ctl02_GridView3_ctl" + extradigit + i + "_lblTotalOverall");
            OverallTotalText.innerHTML = getcurrencysymbol() + " " + OverallTotal.toFixed(2);
            OverallTotalText2.innerHTML = getcurrencysymbol() + " " + OverallTotal.toFixed(2);
            document.getElementById("hdServiceFee").value = ServiceFeeTotal;
            document.getElementById("hdOverallTotal").value = OverallTotal.toFixed(2);            
        }
        
function replacedollarsigns(tempPrice)
{    
    tempPrice = tempPrice.replace("$", "");
    tempPrice = tempPrice.replace("€", "");
    tempPrice = tempPrice.replace("£", "");
    tempPrice = tempPrice.replace("₪", "");
    return tempPrice;
}

function getcurrencysymbol()
{
    return document.getElementById("hdcurrencysymbol").value;
}
        
function checkForNumeric(obj) {
	if( /[^0-9\.]|-{2,}/gi.test(obj.value) ) {
		alert("Must enter a positive numeric value");
		obj.focus();
		obj.select();
		return false;
	}
	return true;
}
        
function clickedPaypal(){
    document.getElementById("btnPaypal").style.display = "block";
    document.getElementById("btnCC").style.display = "none";             
    //document.getElementById("GuestList").style.display = "block";
}

function clickedCC(){
    document.getElementById("btnCC").style.display = "block";      
    document.getElementById("btnPaypal").style.display = "none";    
    //document.getElementById("GuestList").style.display = "block";
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
        <td align=right >
        
            <table style="background-color:White;" >
                <tr valign=middle>
                    <td><asp:Image ID=imgGroup runat=server Height="30px" /></td>
                    <td><asp:Label ID=lblGroupName runat=server></asp:Label></td>
                    <td><span class=SellAll>Groupstore</span></td>
                </tr>                
            </table>
            
        </td>
    </tr>
    <tr>
        <td class="Store_Header"><asp:Label ID="lblEvent_Name" runat="server" Text="lblEvent_Name"></asp:Label>
        </td>
    </tr>
    <tr>
        <td >
            <igtab:UltraWebTab ID="UltraWebTab1" runat="server" Width=100% BorderStyle=None>
                <Tabs>
                    <igtab:Tab Text="Ticket selection <img src='Images/arrow_forward.gif' />" >
                        <ContentTemplate>
                        
                        <table width=100% class="OrderFormText">
                            <tr valign=top>
                                <td>
                            <table width=100%>
                                <tr>
                                    <td colspan=2 class="Section_Header">Details</td>
                                </tr>
                                <tr>
                            <td class="Before_Each_Question">Start Date:</td>
                            <td class="Regular_Text_Areas"><asp:Label ID="lblStart" runat="server"></asp:Label></td>
                        </tr>
                        <tr valign=top>
                            <td class="Before_Each_Question">End Date:</td>
                            <td class="Regular_Text_Areas"><asp:Label ID="lblEnd" runat="server"></asp:Label></td>
                        </tr>
                        <tr valign=top>
                            <td class="Before_Each_Question">Location:</td>
                            <td class="Regular_Text_Areas"><table>
                                <tr>
                                    <td><asp:Label ID="lblLocation" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblStreet" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblCity" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><asp:HyperLink Font-Size="Small" ForeColor="Blue" ID="hypMapit" Text="View Map" Target="_blank" runat="server"></asp:HyperLink></td>
                                </tr>
                                
                                </table>
                        </tr>
                         
                          <tr valign=top>
                            <td class="Before_Each_Question">Host:</td>
                            <td class="Regular_Text_Areas"><asp:Label ID="lblHost" runat="server"></asp:Label></td>
                        </tr>
                        <tr valign=top>
                            <td class="Before_Each_Question">Contact:</td>
                                    <td class="Regular_Text_Areas">
                                        <table>
                                            <tr>
                                                <td><asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                                                <td><asp:Label ID="lblEmail" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                              <tr valign=top>
                            <td colspan=2 width=100% align=left>
                                
                                    <span class="Before_Each_Question">Description:</span>
                                    <br />
                                    
                                        <asp:Label ID="lblComments" runat="server" ></asp:Label>
                
                                    
                            </td>
                                    
                                    
                                </tr> 
                            </table>    
                                </td>
                                
                                <td align=right >
                                    <table>
                                        <tr>
                                            <td><asp:Image ID=imgEvent runat=server ImageUrl="Images/Events/no_image.jpg" Width=200px />
                                            </td>
                                        </tr>
                
                                        <tr>
                                            <td align=center>
  
                    <asp:Label id="lblfbcomments" runat="server" Visible=false></asp:Label>
                    <table>
                        <tr>
                            <td style="text-align:center; color:#410067; font-weight:bold; font-size:medium;">Help Promote this Event</td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                            <table style="text-align:center;">
                                <tr>
                                    <td>
                                    <asp:Label runat=server ID=lblfblike></asp:Label>                                    
                                    </td>                                    
                                    <td>
                                    <asp:Label runat=server ID=lblsharebtn Visible=false></asp:Label>
                                    <asp:Panel ID=pnltwitter runat=server>
                                    <a href="http://twitter.com/share" class="twitter-share-button" data-count="horizontal">Tweet</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>
                                    </asp:Panel>
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
        </td>
    </tr>
    </table>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="height:10px" colspan=2></td>
                            </tr>
                            <tr>
                                    <td colspan=2 class="Section_Header">Tickets</td>
                                </tr>
                                <tr>
                                    <td colspan=2>
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
                        </ItemTemplate>
                    </asp:TemplateField>                 
                    <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("PriceRounded","{0:0.00}") %>'></asp:Label>
                        <asp:Label ID="lblTicketKey" runat="server" Text='<%# Bind("Ticket_Key") %>' Visible=false></asp:Label>
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
                                    <td colspan=2 style="text-align:left;">
                                    <asp:Panel ID=pnlCouponCode runat=server Visible=false>
                                        Coupon Code:<asp:TextBox ID=txtCouponCode runat=server MaxLength=10></asp:TextBox>
                                        <asp:Button ID=btnCouponCode runat=server text="Enter" 
                                            onclick="btnCouponCode_Click"/>
                                            <br />
                                            <asp:Label ID=lblnomatch runat=server ForeColor=Blue Visible=false>No match, please try again.</asp:Label>
                                    </asp:Panel>
                                    </td>
                                </tr>
                        </table>
                           
                        </ContentTemplate>                                                                        
                    </igtab:Tab>
                    <igtab:Tab Text="  Questions <img src='Images/arrow_forward.gif' />">                    
                        <ContentTemplate>
                            <table width=100% class="OrderFormText">                            
                            <tr valign=top>
                                <td>
                                <table width=100%>
                                    <tr>
                                        <td>
                                        <span id=QuestionsSection>
                                            <table width=100%>
                                <tr>
                                    <td colspan=2 class="Section_Header">Questions</td>
                                </tr>
                                <tr>
                                        <td>
                                            <span id=GuestList>
                                                <table width=100%>
                                                   <!--  <tr>
                                                        <td class="Section_Header" colspan=2><asp:Label ID=lblGuestlist runat=server>Guest List</asp:Label></td>
                                                    </tr>-->
                                                    <tr>
                                                        <td colspan=2 class="Fine_Print"><asp:Label ID=lblGuestListComment runat=server>What name do you want showing on the guest list?</asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                        <td><span class="Mandatory_Star">*</span> First Name:</td>
                                                        <td><asp:TextBox ID=txtList_FirstName runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span class="Mandatory_Star">*</span> Last Name:</td>
                                                        <td><asp:TextBox ID=txtList_LastName runat=server Width=200px></asp:TextBox></td>
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
                                        
                                    
                                    <uc1:Questions_Order_Form ID="Questions_Order_Form1" runat="server" />
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound"
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true ShowHeader=false Visible=false
                 AlternatingRowStyle-CssClass="Gridview_bottomBorder" RowStyle-CssClass="Gridview_bottomBorder"  Width="100%" >
<RowStyle Wrap="True" BorderStyle="None" VerticalAlign=Top></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Question_Text" ReadOnly="True" Visible=false />
                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID=Question_Text Text='<%# Bind("Question_Text") %>' runat=server CssClass="Each_Question"></asp:Label><br />
                            <asp:TextBox ID=txtAnswer runat=server Width=480></asp:TextBox>                            
                        </ItemTemplate>                        
                    </asp:TemplateField>
                    <asp:TemplateField  >
                        <ItemTemplate>
                            <asp:Label ID=Mandatory Text='<%# Bind("Mandatory") %>' runat=server Visible=false></asp:Label>
                            <asp:Label ID=Question_Key Text='<%# Bind("Question_Key") %>' runat=server Visible=false></asp:Label>
                        </ItemTemplate>                        
                    </asp:TemplateField>                    
                    
                </Columns>
            </asp:GridView>
            <asp:Panel ID=pnlFreeQs runat=server Visible=false>
            <br />
                <table>
                    <tr>
                        <td>First Name:</td>
                        <td><asp:TextBox ID=txtFirstName runat=server Width=480></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Last Name:</td>
                        <td><asp:TextBox ID=txtLastName runat=server Width=480></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Email:</td>
                        <td><asp:TextBox ID=txtEmail runat=server Width=480></asp:TextBox></td>
                    </tr>
                </table>
            </asp:Panel>
            
                                    </td>
                                </tr> 
                                                                  
                                
                            </table>
                                        </span>
                                        </td>
                                    </tr>
                                     
                                <tr>
                                    <td class="Fine_Print"><br /><span class="Mandatory_Star">*</span> Mandatory questions</td>
                                </tr>
                                </table>
                            
                            </td>
                            <td align=right>
                                    <asp:Image ID=imgEvent2 runat=server ImageUrl="Images/Events/no_image.jpg" Width=200px />
                                </td>
                            </tr>                            
                            </table>
                        </ContentTemplate>
                    </igtab:Tab>
                    <igtab:Tab Text="Checkout">
                        <ContentTemplate>
                        <table width=100% class="OrderFormText">
                            <tr valign=top><td>
                                <table width=100%>
                                    <tr>
                                        <td class="Section_Header">Checkout</td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView3_RowDataBound" ShowFooter="true" 
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true 
                  Width="500px" CssClass="ticket_selector" >
<RowStyle Wrap="True" BorderStyle="None"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Ticket_Description" HeaderText="Description" 
                        ReadOnly="True" ItemStyle-Width="300px"/>
                    <asp:BoundField DataField="Quantity_Remaining" HeaderText="Remaining" 
                        ReadOnly="True" Visible=false/>   
                    <asp:TemplateField HeaderText="Sale Ends" Visible=false>
                        <ItemTemplate>
                        <asp:Label id="lblEnds" runat="Server" text='<%# Bind("Sale_Ends") %>' Width=100px></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                 
                    <asp:TemplateField HeaderText="Price" Visible=false>
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("PriceRounded","{0:c}") %>' Visible=false></asp:Label>
                        <asp:Label ID="lblTicketKey" runat="server" Text='<%# Bind("Ticket_Key") %>' Visible=false></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlQuantity" runat="server" onchange="ManipulateGrid(1);">
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
                                        <td><asp:Panel ID=pnlChoosePayment runat=server>
                                        <table>
                                                <tr>
                                                    <td ><b>How would you like to pay?</b></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ul>
                                                            <li>PayPal Account<input type=radio id=chkPaypal name="PaymentMethod" value="Paypal" onClick="clickedPaypal()"/>                                                                
                                                            </li>
                                                            <li>Credit Card <input type=radio id=chkCC name="PaymentMethod" value="CC" onClick="clickedCC()"/>                                                                
                                                            </li>
                                                        </ul>
                                                    </td> 
                                                </tr>
                                            </table>
                                            </asp:Panel>
                                            <asp:Panel ID=pnlFreeEventEmail runat=server Visible=false>
                                                <table>
                                                    <tr>
                                                        <td><span class="Mandatory_Star">*</span>What email address would you like to receive your e-ticket?</td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:TextBox ID=txtFreeEmail runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>                                                                                                           
                                </table></td>
                                <td align=right>
                                    <asp:Image ID=imgEvent3 runat=server ImageUrl="Images/Events/no_image.jpg" Width=200px />
                                </td>
                            </tr>                            
                        </table>
                        </ContentTemplate>
                    </igtab:Tab>
                </Tabs>                 
                <SelectedTabStyle CssClass="SelectedTab" BorderStyle="None">
                </SelectedTabStyle>                
                <DefaultTabStyle CssClass="UnSelectedTab" BorderStyle="None"></DefaultTabStyle>
            </igtab:UltraWebTab>
        </td>
    </tr>
    <tr>
        <td width=100%>
        <table width=100%>
        <tr>
                    <td colspan=2 align=center><asp:Label ID=lblError runat=server Visible=false></asp:Label>
                    <span id=lblErrorClient style="color:Red;"></span></td>
                </tr>
            <tr>
                    <td><span id=Terms><asp:CheckBox ID="chkTerms" runat="server" onClick="ensureChecked()" />I have read and accept the <a href=Ticket_Buyer_TOS.htm target="_blank">Terms of Service</a>, and understand the Service Fee is not refundable.<br />All refund requests should be made directly to the Event Host no later than 1 business day after the event ends.
                    </span></td>                    
                </tr>
                <tr>
                    <td>
                    <table width=100% >
                        <tr>
                            <td style="width:120px;"><br /><div class="PaypalLogo"></div></td>
                    <td><center>
                        <asp:Panel ID=pnlButtons runat=server>
                        <table>
                            <tr>
                                <td><a href="javascript:backtab();" id=btnBack class="OrderFormButtons">Back</a></td>
                                <td><a  href="javascript:changetab();" id=btnContinue class="OrderFormButtons">Continue</a></td>
                                <td>
                               <span id=btnContinue2Span> <asp:LinkButton  ID="btnContinue2" runat="server" Text="Continue"
                                        CssClass="OrderFormButtons" onclick="btnContinue2_Click"></asp:LinkButton></span></td>
                                <td>
                               <span id=Span1> <asp:LinkButton  ID="btnPaypal" runat="server" Text=""
                                        CssClass="BuynowButton" onclick="btnPaypal_Click"></asp:LinkButton></span>
                                        
                               <span id=Span2> <asp:LinkButton  ID="btnCC" runat="server" Text="Checkout"
                                        CssClass="OrderFormButtons" onclick="btnCC_Click"></asp:LinkButton></span>
                               <span id=Span3> <asp:LinkButton  ID="btnFree" runat="server" Text="Confirm"
                                        CssClass="OrderFormButtons" onclick="btnFree_Click"></asp:LinkButton></span></td>
                            </tr>
                        </table>
                        </asp:Panel>
                        </center>
                    </td>
                    <td align=right style="width:120px;"><a href="http://www.thegroupstore.com" target=_blank><div class="GroupStoreLogo"></div></a></td>
                        </tr>
                    </table>
                    </td>                    
                </tr>
                
                <tr>
                    <td colspan=2><asp:Button ID="btnTest" runat="server" Text="Test" 
                            onclick="btnTest_Click" Visible=false /></td>                    
                </tr>
                <tr>
                    <td colspan=2><asp:Label ID=lblTest runat=server Visible=false></asp:Label>
                </tr>
        </table>
        </td>
    </tr>
    </table>
    
            </td>
            </tr>
        <tr>
        <td align="center"><asp:Label ID=lblDemo runat="Server"></asp:Label></td>
    </tr>    
        <tr>
            <td align="center" class="Fine_Print">© Copyright Groupstore 2010</td>
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
<b>You are now being directed to PayPal to complete your purchase. </b><br />
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

<!--<script src="http://static.ak.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php" type="text/javascript">
</script>
<script language=javascript>
 FB.init("<%= ConfigurationSettings.AppSettings["APIKey"] %>", "/Addons/xd_receiver.htm");
</script>-->


</form>


</body>
</html>
    