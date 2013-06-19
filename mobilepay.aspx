<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mobilepay.aspx.cs" Inherits="Eventomatic.mobilepay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Groupstore Payment</title>
    <link href="../Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID=hdServiceFee Value="0" runat=server/>
    <asp:HiddenField ID=hdOverallTotal Value="0" runat=server />
    <asp:HiddenField ID=hdBackgroundImage Value="../Images/BackgroundImages/100.jpg" runat=server />
    <asp:HiddenField ID=hdLastTicketDate Value="0" runat=server />
    <asp:HiddenField ID=hdCurrentDate Value="0" runat=server />
    <asp:HiddenField ID=hdEvent_Key Value="0" runat=server />
    <asp:HiddenField ID=hdTx_Key Value="0" runat=server />

    <table width=100%  >
                <tr>
                    <td>
                    <center>
                    <table style="border:1px solid black; width:500px;">
                    <tr>
        <td class="Store_Header"><asp:Label ID="lblEvent_Name" runat="server" Text="lblEvent_Name">        
        </asp:Label>        
        </td>
        </tr>
        <tr>
            <td>
        <asp:Panel ID=pnlError runat=server Visible=false >
        Sorry but there is no checkout to pay for.
    </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
            <center>
            <asp:Panel ID=pnlcc runat=server>
            <table width=100% >
            <tr>
            <td>
            <table width=100%>
                <tr>
                    <td>Total Amount</td>
                    <td style="float:right;">
                    <asp:Label runat=server ID=lblTotalAmount/>
                    </td>
                </tr>
            </table>
            </td>
        </tr>        
        <tr>
            <td>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" 
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true Visible=false
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
                                        <td class="Section_Header">
                                        <table width=100%>
                                            <tr>
                                                <td>Pay with Credit Card</td>
                                                <td style="text-align:right;">Powered by PayPal</td>
                                            </tr>
                                        </table> 
                                        </td>
                                    </tr>                                                    
                                    <tr>
                                        <td align=center>
                                            <table style="text-align:left;">
                                                <tr>
                                                        <td>
                                                        <span class="Mandatory_Star">*</span>
                                                        <asp:RequiredFieldValidator id="RequiredFieldValidator1"  
                                                        ControlToValidate="txtPayName" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>Name:</td>
                                                        <td><asp:TextBox ID=txtPayName runat=server Width=200px></asp:TextBox></td>
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
                                                        <td><span class="Mandatory_Star">*</span>
                                                        <asp:RequiredFieldValidator id="RequiredFieldValidator3"  
                                                        ControlToValidate="txtPayCCNum" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>Credit Card #:</td>
                                                        <td><asp:TextBox ID="txtPayCCNum" runat="server"  MaxLength="16" Width="200px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span class="Mandatory_Star">*</span>
                                                        <asp:RequiredFieldValidator id="RequiredFieldValidator4"  
                                                        ControlToValidate="txtPaymonth" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>Expiration Date:</td>
                                                        <td style="text-align:left;">
                                                        <table style="float:left;">
                                                                <tr>
                                                                    <td><asp:TextBox ID="txtPaymonth" runat="server" MaxLength="2" style="width:25px;" Width="25px" /></td>
                                                                    <td>/</td>
                                                                    <td><asp:TextBox ID="txtPayyear" runat="server" MaxLength="2" style="width:25px;" Width="25px" /></td>                                                                    
                                                                </tr>
                                                            </table></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span class="Mandatory_Star">*</span>
                                                        <asp:RequiredFieldValidator id="RequiredFieldValidator11"  
                                                        ControlToValidate="txtPayCSC" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>Security Code:</td>
                                                        <td><asp:TextBox ID="txtPayCSC" runat="server" MaxLength="4" Width="40px" /></td>
                                                    </tr>                                                    
                                                    <tr>
                                                    <td></td>
                                                        <td>Country:</td>
                                                        <td><asp:DropDownList ID=ddlCountry runat=server AutoPostBack=true 
                                                                onselectedindexchanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span class="Mandatory_Star">*</span>
                                                        <asp:RequiredFieldValidator id="RequiredFieldValidator6"  
                                                        ControlToValidate="txtPayBillingAddress1" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>Billing Address:</td>
                                                        <td><asp:TextBox ID=txtPayBillingAddress1 runat=server Width=200px></asp:TextBox>
                                                        <br />
                                                        <asp:TextBox ID=txtPayBillingAddress2 runat=server Width=200px Visible=false></asp:TextBox>
                                                        </td>
                                                    </tr>                                                    
                                                    <tr>
                                                        <td><span class="Mandatory_Star">*</span>
                                                        <asp:RequiredFieldValidator id="RequiredFieldValidator7"  
                                                        ControlToValidate="txtPayCity" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td>City:</td>
                                                        <td><asp:TextBox ID=txtPayCity runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                    <td><span class="Mandatory_Star">*</span>
                                                    <asp:RequiredFieldValidator id="RequiredFieldValidator10"  
                                                        ControlToValidate="txtPayStateProvince" Text="*" runat="server" CssClass="Mandatory_Star" Enabled=false/></td>
                                                        <td><asp:Label ID=lblStateProvince runat=server>Province</asp:Label>:</td>
                                                        <td><asp:DropDownList runat=server ID=ddlStateProvince></asp:DropDownList>
                                                        <asp:TextBox runat=server ID=txtPayStateProvince Width=200px Visible=false></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        <span class="Mandatory_Star">*</span>
                                                        <asp:RequiredFieldValidator id="RequiredFieldValidator8"  
                                                        ControlToValidate="txtPayAreaZipCode" Text="*" runat="server" CssClass="Mandatory_Star"/>
                                                        </td><td><asp:Label ID=lblAreaZipCode runat=server>Postal Code</asp:Label>:</td>
                                                        <td><asp:TextBox ID=txtPayAreaZipCode runat=server Width=100px></asp:TextBox></td>
                                                    </tr>
                                                  
                                                    <tr>
                                                        <td><span class="Mandatory_Star">*</span>
                                                        <asp:RequiredFieldValidator id="RequiredFieldValidator9"  
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
                                                        <td align=center colspan=3><asp:Label ID=lblError runat=server Visible=false></asp:Label>
                    <span id=lblErrorClient style="color:Red;"></span></td>
                                                    </tr>
                                                    <tr>
                                    <td class="Fine_Print" colspan=3><br /><span class="Mandatory_Star">*</span> Please fill in every field.</td>
                                </tr>                                                    
                                <tr>
                                    <td>     
                                 
                               <span id=Span2> <asp:LinkButton  ID="btnCC" runat="server" Text="Buy Now"
                                        CssClass="OrderFormButtons" onclick="btnCC_Click"></asp:LinkButton></span></td>
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
                                                
            </asp:Panel>
            <asp:Panel ID=pnlnotcompatable runat=server Visible=false>
            In order to checkout with a mobile phone you must be using an iPhone, Android or a Blackberry with OS 6. You can also complete this checkout on a computer.
            </asp:Panel>
        </center>
        </td>
        </tr>
        </table>
        </center>
    </td>
    </tr>
    </table>
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
