<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayCC.aspx.cs" Inherits="Eventomatic.PayCC" %>

    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Groupstore Payment</title>
    <link href="Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" /> 
    <link href="Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />  
    <link REL="SHORTCUT ICON" HREF="http://www.thegroupstore.com/favicon.ico"/>
    <script src="/Addons/meteora/meteora.js" type="text/javascript"></script>
    <script src="/Addons/DateTools.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts.js"></script>
    <script src="Addons/CountdownTime.js" type="text/javascript"></script>
</head>
<body onload="initpage()" style="background: rgb(0, 0, 0) no-repeat fixed center top; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;">
    <form id="form1" runat="server">
    <div>        
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
var BkImg = document.getElementById("hdBackgroundImage");
if (BkImg.value != 0)
    {
        document.body.style.backgroundImage = 'url('+BkImg.value+')';
    }
document.getElementById("btnCC").href = "javascript:btnCCrun();";
start_counter();
}

function btnCCrun(){
document.getElementById("Warning").style.display = "block";
document.getElementById("Warning2").style.display = "block";
setTimeout("__doPostBack('btnCC','');", 1000);
}

function GoBack()
{
window.history.go(-1);
}
</script>
    <asp:HiddenField ID=hdServiceFee Value="0" runat=server/>
    <asp:HiddenField ID=hdOverallTotal Value="0" runat=server />
    <asp:HiddenField ID=hdBackgroundImage Value="../Images/BackgroundImages/100.jpg" runat=server />
    <asp:HiddenField ID=hdLastTicketDate Value="0" runat=server />
    <asp:HiddenField ID=hdCurrentDate Value="0" runat=server />
    <asp:HiddenField ID=hdEvent_Key Value="0" runat=server />
    <asp:HiddenField ID=hdTx_Key Value="0" runat=server />
    <table width=100%>
        <tr>
            <td >
            <center>
<div class="Order_Form_1">    
        <div class="Order_Form_2">
            <div class="Order_Form_3">
            <table width=100%  >
                <tr>
                    <td class="EntireStoreBox" align=center>
                        <asp:Panel ID=pnlError runat=server Visible=false >
        Sorry but there is no checkout to pay for.
    </asp:Panel>
    <asp:Panel ID=pnlCC runat=server>
                        <table>
                        <tr>
        <td align=right >
        
            <table style="background-color:White;" >
                <tr valign=middle>
                    <td><asp:Image ID=imgGroup runat=server Height="30px" ImageUrl="Images/Groups/no_image.jpg"/></td>
                    <td><asp:Label ID=lblGroupName runat=server></asp:Label></td>
                    <td><span class=SellAll>GroupStore</span></td>
                </tr>
            </table>
            
        </td>
    </tr>
    <tr>
        <td class="Store_Header"><asp:Label ID="lblEvent_Name" runat="server" Text="lblEvent_Name"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table width=100% style="background-color:White;">
                <tr valign=top>
        <td align=center >
        
            
    
    <table width=100% >
        <tr>
            <td class="Section_Header">Checkout Summary</td>
        </tr>
        <tr>
            <td>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" 
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true 
                  Width="500px" CssClass="ticket_selector">
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
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("PriceRounded","{0:c}") %>' Visible=true></asp:Label>
                        <asp:Label ID="lblTicketKey" runat="server" Text='<%# Bind("Ticket_Key") %>' Visible=false></asp:Label>
                        <asp:Label ID="lblDonationAmount" runat=server Text='<%# Bind("DonationAmount") %>' Visible=false></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>' Visible=true></asp:Label>
                            <asp:Label ID=lblServiceFee runat=server Visible=false>Service fee</asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign=Center />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" FooterStyle-CssClass="TicketPurchase_TotalFooter" >
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" Width="80" Text='<%# Bind("Tix_Amount") %>'></asp:Label>
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
                                        <td class="Section_Header">Credit Card Information</td>
                                    </tr>
                                    <tr>
                                        <td align=center>
                                            <table style="text-align:left;">
                                                <tr>
                                                        <td><asp:RequiredFieldValidator id="RequiredFieldValidator1"  
                                                        ControlToValidate="txtPayFirstName" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>First Name:</td>
                                                        <td><asp:TextBox ID=txtPayFirstName runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:RequiredFieldValidator id="RequiredFieldValidator2"  
                                                        ControlToValidate="txtPayLastName" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>Last Name:</td>
                                                        <td><asp:TextBox ID=txtPayLastName runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>Card Type:</td>
                                                        <td><asp:DropDownList ID=ddlType runat=server>                                                            
                                                            <asp:ListItem Value=Visa>Visa</asp:ListItem>
                                                            <asp:ListItem Value=MasterCard>MasterCard</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:RequiredFieldValidator id="RequiredFieldValidator3"  
                                                        ControlToValidate="txtPayCCNum" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>Credit Card Number:</td>
                                                        <td><igtxt:WebMaskEdit ID="txtPayCCNum" runat="server" InputMask="################" Width="200px"></igtxt:WebMaskEdit></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:RequiredFieldValidator id="RequiredFieldValidator4"  
                                                        ControlToValidate="txtPaymonth" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>Expiration Date:</td>
                                                        <td><table>
                                                                <tr>
                                                                    <td><igtxt:WebMaskEdit ID="txtPaymonth" runat="server" InputMask="##" Width="25px"></igtxt:WebMaskEdit></td>
                                                                    <td>/</td>
                                                                    <td><igtxt:WebMaskEdit ID="txtPayyear" runat="server" InputMask="##" Width="25px"></igtxt:WebMaskEdit></td>
                                                                    <td> <asp:RequiredFieldValidator id="RequiredFieldValidator5"  
                                                        ControlToValidate="txtPayCSC" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        CSC:<igtxt:WebMaskEdit ID="txtPayCSC" runat="server" InputMask="####" Width="40px"></igtxt:WebMaskEdit>
                                                        </td>
                                                        <td><div style="font-size:smaller;">3 digit # on <br />back of card</div></td>
                                                                </tr>
                                                            </table></td>
                                                    </tr>                                                    
                                                    <tr>
                                                    <td></td>
                                                        <td>Country:</td>
                                                        <td><asp:DropDownList ID=ddlCountry runat=server AutoPostBack=true 
                                                                onselectedindexchanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:RequiredFieldValidator id="RequiredFieldValidator6"  
                                                        ControlToValidate="txtPayBillingAddress1" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>Billing Address 1:</td>
                                                        <td><asp:TextBox ID=txtPayBillingAddress1 runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                    <td></td>
                                                        <td>Billing Address 2:</td>
                                                        <td><asp:TextBox ID=txtPayBillingAddress2 runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:RequiredFieldValidator id="RequiredFieldValidator7"  
                                                        ControlToValidate="txtPayCity" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>City:</td>
                                                        <td><asp:TextBox ID=txtPayCity runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                    <td><asp:RequiredFieldValidator id="RequiredFieldValidator10"  
                                                        ControlToValidate="txtPayStateProvince" Text="*" runat="server" CssClass="Mandatory_Star" Enabled=false/></td>
                                                        <td><asp:Label ID=lblStateProvince runat=server>Province</asp:Label>:</td>
                                                        <td><asp:DropDownList runat=server ID=ddlStateProvince></asp:DropDownList>
                                                        <asp:TextBox runat=server ID=txtPayStateProvince Width=200px Visible=false></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:RequiredFieldValidator id="RequiredFieldValidator8"  
                                                        ControlToValidate="txtPayAreaZipCode" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td><asp:Label ID=lblAreaZipCode runat=server>Postal Code</asp:Label>:</td>
                                                        <td><asp:TextBox ID=txtPayAreaZipCode runat=server Width=100px></asp:TextBox></td>
                                                    </tr>
                                                  
                                                    <tr>
                                                        <td><asp:RequiredFieldValidator id="RequiredFieldValidator9"  
                                                        ControlToValidate="txtPayEmail" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>Email:</td>
                                                        <td><asp:TextBox ID=txtPayEmail runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan=3 align=center>
                                                            <asp:Button runat=server ID=btnPurchase Text="Purchase" 
                                                                onclick="btnPurchase_Click" Visible=false />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                    <td class="Fine_Print" colspan=3><br /><span class="Mandatory_Star">*</span> Please fill in every field.</td>
                                </tr>                                                    
                                            </table>
                                        
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
                    <td align=center><asp:Label ID=lblError runat=server Visible=false></asp:Label>
                    <span id=lblErrorClient style="color:Red;"></span></td>
                </tr>
    <tr>
                    <td>
                    <table width=100% >
                        <tr>
                            <td style="width:120px;"><br /><div class="PaypalLogo"></div></td>
                    <td><center>
                        <asp:Panel ID=pnlbuttons runat=server Visible=true>
                        <table>
                            <tr>
                            <td><a href="javascript:GoBack();" id=btnBack class="OrderFormButtons">Back</a></td>
                                <td>     
                                 
                               <span id=Span2> <asp:LinkButton  ID="btnCC" runat="server" Text="Buy Now"
                                        CssClass="OrderFormButtons" onclick="btnCC_Click"></asp:LinkButton></span></td>
                            </tr>
                        </table>
                        </asp:Panel>
                        <asp:Label id=lblTimeup runat=server Visible=false Font-Bold=true></asp:Label>
                        </center>
                    </td>
                    <td align=right style="width:120px;"><a href="http://www.thegroupstore.com" target=_blank><div class="GroupStoreLogo"></div></a></td>
                        </tr>
                    </table>
                    </td>                    
                </tr>
                
                        </table>           
                   </asp:Panel>  
                    </td>
                </tr>
            
        </table>
        <table>
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
            </td>
        </tr>
        
    </table>
    
    </div>
    </form>
<div id="Warning" class="QuestionPopup">

</div>
<div id="Warning2" class="QuestionPopup2">
<center>
    <table >
        <tr>
            <td align=center>
                <img src="/images/page_loader.gif" /> 
We are now processing your purchase.<br />
<b>Please do not hit the back button, to prevent your card from being charged twice.</b>            
            </td>
        </tr>
    </table>
</center>
</div>

<div id="tzcdFrame" class="Countdown_Timer">
<table style="background-color:White;">
    <tr>
        <td align=center>Time Left to complete your purchase
        </td>
    </tr>
    <tr>
        <td align=center><div id="tzcd"></div>
        </td>
    </tr>
</table>
</div>


</body>
</html>
