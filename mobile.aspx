<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mobile.aspx.cs" Inherits="Eventomatic.mobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="Addons/LeaderBoard.ascx" tagname="LeaderBoard" tagprefix="uc1" %>
<%@ Register src="Addons/EventNewsFeed.ascx" tagname="EventNewsFeed" tagprefix="uc2" %>
<%@ Register src="Addons/Questions_Order_Form.ascx" tagname="Questions_Order_Form" tagprefix="uc3" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Groupstore Mobile</title>
    <link href="Eventomatic_Mobile.css" media="screen" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" src="Addons/gosquared.js"></script>
</head>
<body onload="initpage()" >
 <center>
<div id="mobile" style="width:400px;"> 
	

<form id="form1" runat="server" onsubmit="Validationform()">
<asp:HiddenField ID=hdSFP Value=0.0 runat=server />
<asp:HiddenField ID=hdSFC Value=0.0 runat=server />    
<asp:HiddenField ID=hdSFM Value=0 runat=server />    
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
<asp:HiddenField ID=hdnfbname runat=server Value=0 />
<asp:HiddenField ID="hdeventkey" runat=server Value=0 />
<asp:HiddenField ID="hdisdemopay" runat=server Value=0 />
<asp:HiddenField ID="hdgotourl" runat=server Value="" />



<script language=javascript>      

    function Validationform() {
        //alert('here');
        return false;
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
                Total.innerHTML = getcurrencysymbol() + " " + ServiceFeeTotal.toFixed(2);
                OverallTotal = OverallTotal + ServiceFeeTotal;
            }
        }
        if (i > 9) {
            extradigit = "";
        }
        var OverallTotalText = document.getElementById("GridView1_ctl" + extradigit + i + "_lblTotalOverall");
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

    function gotosettings() {        
        //var gotourl = "MobileFacebookSettings.aspx?fbid=" + document.getElementById("hdnfbid").value + "&event=" + document.getElementById("hdeventkey").value;
        //$.mobile.changePage(gotourl, { transition: "slideup" });        
        window.location = "MobileFacebookSettings.aspx?fbid=" + document.getElementById("hdnfbid").value + "&event=" + document.getElementById("hdeventkey").value;
    }

    function gotourl(theurl) {
        $.mobile.changePage(theurl, { transition: "slide" });
    }

    function showloading() {
        $.mobile.showPageLoadingMsg();
    }

    function callpostback(buttonname) {
        showloading();
        __doPostBack(buttonname, '');
    }

    function validateForm() {
        var x = document.getElementById("txtList_Name").value;
        if (x == null || x == "") {
            alert("First name must be filled out");
            return false;
        }
        else {
            document.forms[0].submit();                        
        }
    }
    

</script>

<table width=100% >
<tr>
                    <td>
                    <center>
                    <table >
                    <tr valign=top>
                           <td style="vertical-align:top; text-align:center;">
                           <span id="fbcheckin">
                           <asp:LinkButton id="btncheckinpaypal" CssClass="SigninPayPal" Text="&nbsp;" OnClick="btncheckinpaypal_Click" runat=server Width=87 Height=21 data-role="button" data-theme="none"/><asp:Label ID=lblfbstatus runat=server> to get credit for this ticket sale</asp:Label>
                           </span>
                           <table width=100%>
                            <tr>
                                <td colspan=2 style="text-align:right;"><span id="fbcheckedin" style="display:none;"></span>
                                <a href="#" onclick="gotosettings();return false"><span  id="fbOptions" style="display:none; ">Edit Facebook Settings</span></a>
                                </td>
                            </tr>
                           </table>
                           
                           <!--<span id="fbcheckedinStreamSettingsOn" style="display:none;">A story of this sale will be posted to your Facebook Wall.</span>
                           <span id="fbcheckedinStreamSettingsOff" style="display:none;">A story of this sale will not be posted to your Facebook Wall.<span class="OrderFormButtons">Turn on</span></span>-->
                           <asp:Label ID=lblError2 runat=server Visible=false ForeColor=Red></asp:Label>
                           </td>
                     </tr>               
            
                    <tr>
        <td class="Section_Header">        
        <asp:Label ID="lblEvent_Name" runat="server" Text="lblEvent_Name"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td><asp:HyperLink ID=hypefullsite runat=server>Go to Full Site</asp:HyperLink></td>
                </tr>
                <tr>
                    <td colspan=2>What name does the buyer want showing on the guest list?</td>
                </tr>
                <tr>
                    <td colspan=2>
                        <span class="Mandatory_Star">*</span>Name:                  
                        <asp:TextBox ID="txtList_Name" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan=2>
                        <asp:Panel ID=pnlemail1 runat=server Visible=false>
                        <span class="Mandatory_Star">*</span> Email:
                        </asp:Panel>                                                
                        <asp:Panel ID=pnlemail2 runat=server Visible=false>
                        <asp:TextBox ID="txtFreeEmail" runat="server" data-role="fieldcontain"></asp:TextBox>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan=2>
                    <uc3:Questions_Order_Form ID="Questions_Order_Form1" runat="server"/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
                        <tr>
                            <td>
                            
                              <asp:Label ID="lblSellingDeadline" runat=server Visible=false CssClass="Fine_Print"></asp:Label>            
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" 
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true 
                   CssClass="ticket_selector">
<RowStyle Wrap="True" BorderStyle="None"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Ticket_Description" HeaderText="Description" 
                        ReadOnly="True" ItemStyle-Width="250px"/>
                    <asp:BoundField DataField="Quantity_Remaining" HeaderText="Remaining" 
                        ReadOnly="True" Visible=false/>                       
                    <asp:TemplateField HeaderText="Sale Ends" Visible=false>
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
                            <asp:DropDownList ID="ddlQuantity" runat="server" onchange="ManipulateGrid(0);" data-role="none">
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
                    <asp:TemplateField HeaderText="Total" FooterStyle-CssClass="TicketPurchase_TotalFooter" ItemStyle-Width=100px>
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" Width="80px" data-role="none">$ 0.00</asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalOverall" runat="server" Width="80px" data-role="none">$ 0.00</asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>

<FooterStyle ></FooterStyle>

<HeaderStyle CssClass="TicketPurchase_Header" HorizontalAlign="Left"></HeaderStyle>
            </asp:GridView>
                            </td>
                        </tr>                            
                        <tr>
                            <td style="text-align:center;">
                            <asp:Label ID=lblError runat=server Visible=false ForeColor=Red></asp:Label>
                            </td>
                        </tr>                    
                        <tr>
                            <td style="text-align:center;">
                            <center>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton  id="btnContinue" class="OrderFormButtons" Text="Get Tix" OnClick="btnContinue_Click" runat=server Visible=false/>
                                        <asp:LinkButton  id="btnPayPal" Class="Mobile_BuyNow" OnClick="btnPayPal_Click" runat=server Width=107 Height=26/>
                                        
                                    </td>
                                    <td>
                                    <asp:LinkButton id="btnshare" class="Mobile_SellNow" OnClick="btnShare_Click" runat=server Width=107 Height=26  />

                                    </td>
                                </tr>           
                                <tr>
                                    <td colspan=2 style="text-align:center;">
                                        <img src="Images/visamc.png" />
                                    </td>
                                </tr>                     
                                <!--
                                <tr>
                                    <td style="font-size:smaller;">
                                    Share this link to allow others buy tix from their phone
                                    </td>
                                </tr>-->                                
                            </table>
                            </center>
                            </td>
                        </tr>           
                        <tr>
                                    <td>
                                        <asp:Label runat=server ID=lblismobile Visible=false></asp:Label>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td style="text-align:left;">
                                    <asp:Label ID=lblLeaderPrize runat=server></asp:Label><br />
                                    Leaderboard: <asp:HyperLink ID=hypmobileleader runat=server NavigateUrl="MobileLeader.aspx?event=0" Text="See All Leaders" /><br />
                                    <uc1:LeaderBoard ID="LeaderBoard1" runat="server" />
                                    </td>
                                </tr>               
                                <tr>
                                    <td style="text-align:left;">
                                    Event Newsfeed: <asp:HyperLink ID=hypmobilenews runat=server NavigateUrl="MobileNews.aspx?event=0" Text="See All News"/>                                    
                                        <uc2:EventNewsFeed ID="EventNewsFeed1" runat="server" /> 
                                    </td>
                                </tr>                                
                    </table>
                    </center>
                    </td>
                </tr>
                <tr>
        <td align="center"><asp:Label ID=lblDemo runat="Server"></asp:Label></td>
    </tr>    
        <tr>
            <td align="center" class="Fine_Print"><div  data-role="footer">© Copyright Groupstore 2010</div></td>
        </tr>
</table>

<div id="fb-root"></div>    
<script src="http://connect.facebook.net/en_US/all.js"></script>
    <script language=javascript>

function initpage(){    

if (document.getElementById("hdgotourl").value != "")
{
    var thegotourl = document.getElementById("hdgotourl").value;
    document.getElementById("hdgotourl").value = "";
    gotourl(thegotourl);
}


ManipulateGrid(0);
    document.getElementById("txtList_Name").focus();
    
    FB.init({appId: "<%= ConfigurationSettings.AppSettings["APIKey"] %>", status: true, cookie: true,
             xfbml: true});
 
  if (FB.getSession())
  {
   // alert('exists1');
    document.getElementById("hdnfbid").value = FB.getSession().uid;      
      turnonfbname();
      FB.api('/me', function(response) {
         // alert(response.name);
          document.getElementById("fbcheckedin").innerHTML = response.name + " will get credit for this sale";
          document.getElementById("hdnfbname").value = response.name;
          
        });
  }
  else
  {
    //alert('not exists1');
    if (document.getElementById("hdnfbid").value != '0')
    {
    var fbid = document.getElementById("hdnfbid").value;
    //alert(fbid);
    FB.api('/'+fbid, function(response) {
          //alert(response.name);
          document.getElementById("fbcheckedin").innerHTML = response.name + " will get credit for this sale";
          turnonfbname();
          
          
        
        document.getElementById("hdnfbname").value = response.name;
        });
    }
  }

  function turnonfbname()
  {
    document.getElementById("fbcheckin").style.display = 'none';
    document.getElementById("fbcheckedin").style.display = 'block';
    document.getElementById("fbOptions").style.display = 'block';
  }



/*    if (document.getElementById("hdPostSteam").value == '1') //ok to stream story
    {
        document.getElementById("fbcheckedinStreamSettingsOn").style.display = 'block';
    }
    else
    {
        document.getElementById("fbcheckedinStreamSettingsOff").style.display = 'block';    
    }
  FB.getLoginStatus(function(response) {
  if (response.session) {
    // logged in and connected user, someone you know
    alert('exists');
    FB.api('/me', function(response) {
          //alert(response.name);
          document.getElementById("fbcheckedin").innerHTML = response.name + " will get credit for this sale";
        });
    document.getElementById("hdnfbid").value = FB.getSession().uid;
      document.getElementById("fbcheckin").style.display = 'none';
      document.getElementById("fbcheckedin").style.display = 'block';      
  } else {
    // no user session available, someone you dont know
    alert('not exists');
    if (document.getElementById("hdnfbid").value != '0')
    {
    var fbid = document.getElementById("hdnfbid").value;
    alert(fbid);
    FB.api('/'+fbid, function(response) {
          alert(response.name);
          document.getElementById("fbcheckedin").innerHTML = response.name + " will get credit for this sale";
        });
    }
  }
});*/

  /*
  FB.login(function(response) {
  if (response.session) {  
    if (response.perms) {
      // user is logged in and granted some permissions.
      // perms is a comma separated list of granted permissions
      //window.location = "<%= ConfigurationSettings.AppSettings["DefaultPage"] %>";
      //alert('1');      
      document.getElementById("hdnfbid").value = FB.getSession().uid;
      document.getElementById("fbcheckin").style.display = 'none';
      document.getElementById("fbcheckedin").style.display = 'block';
      FB.api('/me', function(response) {
          //alert(response.name);
          document.getElementById("fbcheckedin").innerHTML = response.name + " will get credit for this sale";
        });
    } else {
      // user is logged in, but did not grant any permissions
      alert('Dont have permission');
    }
  } else {
    // user is not logged in
    alert('You are not logged in');
  }
}, {perms:'email'});
*/

}

        
    </script>

    </form>
    

</div> 
</center>

</body>
</html>
