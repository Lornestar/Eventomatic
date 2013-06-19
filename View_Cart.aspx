<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Cart.aspx.cs" Inherits="Eventomatic.View_Cart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" />    
    <link href="Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />    
    <link REL="SHORTCUT ICON" HREF="http://www.thegroupstore.com/favicon.ico"/>
    <script src="/Addons/meteora/meteora.js" type="text/javascript"></script>
    <script src="/Addons/DateTools.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts.js"></script>
    <script src="Addons/CountdownTime.js" type="text/javascript"></script>
    <script src="Order_Form.js" type="text/javascript"></script>
    <title>View Cart</title>
</head>
<body style="background: rgb(255, 255, 255) no-repeat fixed center top; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;">
<script language=javascript>
function clickedPaypal(){
    document.getElementById("btnPaypal").style.display = "block";
    document.getElementById("btnCC").style.display = "none";             
    //document.getElementById("GuestList").style.display = "block";
}

function clickedCC(){
    document.getElementById("btnCC").style.display = "block";      
    document.getElementById("btnPaypal").style.display = "none";    
    document.getElementById("Span2").style.display = "block";      
    //document.getElementById("GuestList").style.display = "block";
}

function doRemove2(ticketid){
    var answer = confirm("Are you sure you want to Remove the Ticket?")
	if (answer){
		__doPostBack('DoRemove2',ticketid);
	}
	else{
	//
	}    
    }    

</script>
    <form id="form1" runat="server">
    <asp:HiddenField ID=hdSFP Value=0.0499 runat=server />
    <asp:HiddenField ID=hdSFC Value=0.0 runat=server />    
    <asp:HiddenField ID=hdSFM Value=2.5 runat=server />    
    <asp:HiddenField ID=hdServiceFee Value="0" runat=server/>
    <asp:HiddenField ID=hdOverallTotal Value="0" runat=server />
    <asp:HiddenField ID=hdtx_key Value="0" runat=server />
    
    <center>
<div class="Order_Form_1">
    
        <div class="Order_Form_2">
            <div class="Order_Form_3">
<table width=100%>
<tr>
        <td align=center class="EntireStoreBox">
        
    <table width=100%>
    <tr>
        <td align=right >
        <asp:Label ID=lblstorelink runat=server></asp:Label>
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
        <td class="Store_Header">View Cart
        </td>
    </tr>
    <tr>
        <td align=center>
        <asp:Label ID=lbltxkey Visible=false runat=server></asp:Label>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" 
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true 
                  Width="500px" CssClass="ticket_selector">
<RowStyle Wrap="True" BorderStyle="None"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Ticket_Description" HeaderText="Description" 
                        ReadOnly="True" ItemStyle-Width="300px"/>
                    <asp:TemplateField HeaderText="Price" Visible=false>
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price","{0:c}") %>' Visible=false></asp:Label>
                        <asp:Label ID="lblTicketKey" runat="server" Text='<%# Bind("Ticket_Key") %>' Visible=false></asp:Label>
                        <asp:Label ID="lblTicketPurchasedKey" runat="server" Text='<%# Bind("Tickets_Purchased_Key") %>' Visible=false></asp:Label>
                        <asp:Label id=lblDollarSign runat=server visible=false>$</asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlQuantity" Enabled=false runat="server" onchange="ManipulateGrid(0);">
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
                            <asp:Label id=lblQuantity runat=server visible=false Text='<%# Bind("Quantity") %>'>1</asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" FooterStyle-CssClass="TicketPurchase_TotalFooter" >
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" Width="80" Text='<%# Bind("PriceTotal","{0:c}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalOverall" runat="server" Width="100" >$ 0.00</asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblRemove" runat="server" Width=60px></asp:Label>                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

<FooterStyle BackColor="White"></FooterStyle>

<HeaderStyle CssClass="TicketPurchase_Header" HorizontalAlign="Left"></HeaderStyle>
            </asp:GridView>            
        </td>
    </tr>
    <tr>
        <td align=center>
        <asp:Panel ID=pnlnopay runat=server Visible=false>
        There is nothing in the cart
        </asp:Panel>
        <asp:Panel ID=pnlpay runat=server>        
        <table>
            <tr>
                <td colspan=2><span id=Terms><asp:CheckBox ID="chkTerms" runat="server" />I have read and accept the <a href=Ticket_Buyer_TOS.htm target="_blank">Terms of Service</a>, and understand the Service Fee is not refundable.
                    </span></td>
            </tr>
            <tr>
                <td colspan=2 align=center><asp:Label ID=lblError runat=server Visible=false ForeColor=Red></asp:Label>
                </td>
            </tr>
            <tr valign=bottom>
            <td >
            
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
             </td>
             <td>
             <span id=Span1> <asp:LinkButton  ID="btnPaypal" runat="server" Text=""
                                        CssClass="BuynowButton" onclick="btnPaypal_Click"></asp:LinkButton></span>
                                        
                               <span id=Span2 style="display:none;"> <asp:LinkButton  ID="btnCC" runat="server" Text="Checkout"
                                        CssClass="OrderFormButtons" onclick="btnCC_Click"></asp:LinkButton></span>
             </td>
        </tr>
        </table>                                            
        </asp:Panel>
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
    
    
    
    
    </form>
</body>
</html>
