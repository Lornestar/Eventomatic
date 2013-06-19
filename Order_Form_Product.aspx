<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order_Form_Product.aspx.cs" Inherits="Eventomatic.Order_Form_Product" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ MasterType VirtualPath="~/Site.Master" %>
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
    <title>Order Form Product</title>
</head>
<body onload="ManipulateGrid(0);" style="background: rgb(255, 255, 255) no-repeat fixed center top; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;">
    <form id="form1" runat="server">
    
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
<asp:HiddenField id=hdCalendarType Value="0" runat=server />
<asp:HiddenField ID=hdEarliestTime Value="0" runat=server />
<asp:HiddenField id=hdLatestTime Value="0" runat=server />
<asp:HiddenField id=hdTicket_Key Value="0" runat=server />
<asp:HiddenField id=hdTicketPrice Value="0" runat=server />    
<asp:HiddenField id=hdGridRowsCount Value="0" runat=server />    
<center>
<div class="Order_Form_1">
    
        <div class="Order_Form_2">
            <div class="Order_Form_3">
<table width=100%>
<tr>
        <td align=center class="EntireStoreBox">
        
    <table width=100%>
    <tr>
        <td align=right colspan=2 >
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
        <td colspan=2 class="Store_Header"><asp:Label ID="lblEvent_Name" runat="server" Text="lblEvent_Name"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan=2>


        </td>
    </tr>
    <tr valign=top>
        <td>
        <table width=100%>            
        <tr valign=top>
        <td align=center>
                <asp:Panel ID=pnlSelectdate runat=server Visible=true>
        <table>
            <tr>
                <td colspan=2>
                
                <table>
                    <tr>
                <td align=center><asp:Label ID=lblcheckin runat=server>Check-in Date:</asp:Label></td>
                <td align=center><asp:Label ID=lblcheckout runat=server>Check-out Date:</asp:Label></td>
                
            </tr>
            <tr>
                <td>
                <table>
                    <tr>
                        <td>
                        <igsch:WebDateChooser ID="Checkin" runat="server" Value="" Width="200px">
                                    <DropButton ImageUrl1="./Images/WebDateChooser/datePickerImage.gif">
                                    </DropButton>
                                    <CalendarLayout NextMonthImageUrl="./Images/WebDateChooser/ig_cal_blueN0.gif" 
                                        PrevMonthImageUrl="./Images/WebDateChooser/ig_cal_blueP0.gif" ShowMonthDropDown="False" 
                                        ShowYearDropDown="False" TitleFormat="Month">
                                        <CalendarStyle BackColor="#E0EEFF" BorderColor="SteelBlue" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" 
                                            Font-Underline="False">
                                        </CalendarStyle>
                                        <DayHeaderStyle BackColor="#7AA7E0" Font-Bold="True" Font-Size="8pt" 
                                            ForeColor="#606090" Height="1pt">
                                        <BorderDetails ColorBottom="LightSteelBlue" StyleBottom="Solid" 
                                            WidthBottom="1px" />
                                        </DayHeaderStyle>
                                        <DayStyle BackColor="#7AA7E0" />
                                        <NextPrevStyle BackgroundImage="../images/ig_cal_blue1.gif" />
                                        <OtherMonthDayStyle ForeColor="SlateGray" />
                                        <SelectedDayStyle BackColor="SteelBlue" />
                                        <TitleStyle BackColor="#7AA7E0" BackgroundImage="ig_cal_blue1.gif" 
                                            Font-Bold="True" Font-Size="10pt" ForeColor="#505080" Height="18pt" />
                                        <TodayDayStyle BackColor="#97B0F0" />
                                        <FooterStyle BackgroundImage="ig_cal_blue2.gif" Font-Size="8pt" 
                                            ForeColor="#505080" Height="16pt">
                                        <BorderDetails ColorTop="LightSteelBlue" StyleTop="Solid" WidthTop="1px" />
                                        </FooterStyle>
                                    </CalendarLayout>
                                </igsch:WebDateChooser>
                        </td>
                        <td>
                        
                        </td>
                    </tr>                    
                    <tr>
                        <td>
                        <igtxt:WebDateTimeEdit visible=false ID="Checkin_Time" runat="server" EditModeFormat="t" 
                                    DisplayModeFormat="t" Fields="2009-5-1-7-0-0-0" Nullable="False" Width="100">
                                </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                </table>                
                                
                </td>
                
                <td>
                <table>
                    <tr>
                        <td>
                        <igsch:WebDateChooser ID="Checkout" runat="server" Value="" Width="200px">
                                    <DropButton ImageUrl1="./Images/WebDateChooser/datePickerImage.gif">
                                    </DropButton>
                                    <CalendarLayout NextMonthImageUrl="./Images/WebDateChooser/ig_cal_blueN0.gif" 
                                        PrevMonthImageUrl="./Images/WebDateChooser/ig_cal_blueP0.gif" ShowMonthDropDown="False" 
                                        ShowYearDropDown="False" TitleFormat="Month">
                                        <CalendarStyle BackColor="#E0EEFF" BorderColor="SteelBlue" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" 
                                            Font-Underline="False">
                                        </CalendarStyle>
                                        <DayHeaderStyle BackColor="#7AA7E0" Font-Bold="True" Font-Size="8pt" 
                                            ForeColor="#606090" Height="1pt">
                                        <BorderDetails ColorBottom="LightSteelBlue" StyleBottom="Solid" 
                                            WidthBottom="1px" />
                                        </DayHeaderStyle>
                                        <DayStyle BackColor="#7AA7E0" />
                                        <NextPrevStyle BackgroundImage="../images/ig_cal_blue1.gif" />
                                        <OtherMonthDayStyle ForeColor="SlateGray" />
                                        <SelectedDayStyle BackColor="SteelBlue" />
                                        <TitleStyle BackColor="#7AA7E0" BackgroundImage="ig_cal_blue1.gif" 
                                            Font-Bold="True" Font-Size="10pt" ForeColor="#505080" Height="18pt" />
                                        <TodayDayStyle BackColor="#97B0F0" />
                                        <FooterStyle BackgroundImage="ig_cal_blue2.gif" Font-Size="8pt" 
                                            ForeColor="#505080" Height="16pt">
                                        <BorderDetails ColorTop="LightSteelBlue" StyleTop="Solid" WidthTop="1px" />
                                        </FooterStyle>
                                    </CalendarLayout>
                                </igsch:WebDateChooser>
                        </td>
                        <td>
                        
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <igtxt:WebDateTimeEdit visible=false ID="Checkout_Time" runat="server" EditModeFormat="t"
                                    DisplayModeFormat="t" Fields="2009-5-1-9-0-0-0" Nullable="False" Width="100">
                                </igtxt:WebDateTimeEdit>
                        </td>
                    </tr>
                </table>                
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                <asp:Panel ID=pnlAdults runat=server Visible=false>
                <asp:Label ID=lblAdults runat=server>Adults:</asp:Label> 
                    <asp:DropDownList ID=ddlAdults runat=server AutoPostBack=true 
                        onselectedindexchanged="ddlAdults_SelectedIndexChanged">
                        </asp:DropDownList>
                        </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan=2 align=right><span style="float:right;">
                    <asp:LinkButton  ID="btnAvailability" runat="server" Text="Check Availability"
                    CssClass="QuestionsButton" ForeColor=White onclick="btnAvailability_Click"></asp:LinkButton>
                    </span>
                </td>
            </tr>
                    
                </table>
                
                </td>
            </tr>
            
            
            
            
            
            <tr>
    <td colspan=2>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" 
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true 
                  Width="700px" CssClass="ticket_selector">
<RowStyle Wrap="True" BorderStyle="None"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Ticket_Description" HeaderText="Description" 
                        ReadOnly="True" ItemStyle-Width="300px"/>
                    <asp:BoundField DataField="Quantity_Remaining" HeaderText="Availability" 
                        ReadOnly="True" />   
                    <asp:TemplateField HeaderText="Sale Ends" Visible=false>
                        <ItemTemplate>
                        <asp:Label id="lblEnds" runat="Server" text='<%# Bind("Sale_Ends") %>' Width=100px></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                 
                    <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("PriceRounded","{0:c}") %>'></asp:Label>
                        <asp:Label ID="lblTicketKey" runat="server" Text='<%# Bind("Ticket_Key") %>' Visible=false></asp:Label>
                        <asp:Label id=lblDollarSign runat=server visible=false>$</asp:Label>
                        <asp:TextBox id="txtDonate" Maxlength=6 runat="server" onKeyUp="ManipulateGrid(0);" visible=false width=45px text="0.00"></asp:TextBox>
                        <asp:Label ID=lblremaining runat=server Text='<%# Bind("Quantity_Remaining") %>' Visible=false></asp:Label>
                        <asp:Label ID=lblstarttime runat=server Visible=false Text='<%# Bind("Start_Time") %>'></asp:Label>
                        <asp:Label ID=lblendtime runat=server Visible=false Text='<%# Bind("End_Time") %>'></asp:Label>
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
        <td align=right colspan=2>
        <table>
            <tr>
                <td>
                
                <span style="float:right;">
                    <asp:LinkButton  ID="btnAddCart" runat="server" Text="Book Now" Visible=false
                    CssClass="OrderFormButtons" ForeColor=White onclick="btnAddCart_Click"></asp:LinkButton>
                    </span>
                </td>
                <td>
                <span style="float:left; display:none;">
                    <asp:LinkButton  ID="btnCheckCart" runat="server" Text="Check Cart" Visible=false
                    CssClass="OrderFormButtons" ForeColor=White onclick="btnCheckCart_Click"></asp:LinkButton>
                    </span>
                </td>
            </tr>
            <tr>
                <td colspan=2>
                <asp:Label ID=lblerror runat=server ForeColor=Red></asp:Label>
                </td>
            </tr>
        </table>
        
        </td>
    </tr>
        </table>
        
        </asp:Panel>
                
          </td>  </tr>
        </table>        
        </td>
        
                <td align=right>
                <asp:Image ID=imgEvent runat=server ImageUrl="Images/Events/no_image.jpg" Width=200px />
                </td>
    </tr>
    <tr>
        <td align=center colspan=2>
        
        </td>
    </tr>
    <tr>
        <td colspan=2>
            <asp:Label ID="lblComments" runat="server"></asp:Label>
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
