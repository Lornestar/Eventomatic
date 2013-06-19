<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seller.aspx.cs" Inherits="Eventomatic.Seller" MasterPageFile="~/Site.Master"%>


<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
<table width="100%">
    <tr>
        <td >
            <table width="100%">
                <tr valign=top>
                    <td>
                        <table>
                            <tr>
                               <td>Welcome <asp:Label ID="lblUsername" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td >
                                <asp:Panel ID=pnladdtab runat=server style="background-color:#EDEFF4; width:400px;" Visible=false>
                                    You are currently not selling events from your Facebook Profile.<br />
                                     
                                        <fb:add-profile-tab type="off-facebook"></fb:add-profile-tab>         
                                </asp:Panel>     
                                </td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="lblYouareSeller" runat="server">Below are Results of your personal Groupstore Sales<br />(eg. From your Facebook profile)</asp:Label></td>
                            </tr>                            
                        </table>
                    </td>                    
                </tr>
            </table>
        </td>        
        <td align=right>
                       <!-- <asp:LinkButton  ID="btnExcel" runat="server" Text="Import fb Event"
                                        CssClass="ImportFBEventsButton" OnClientClick="__doPostBack('ImportfbEvent','')">
                                        
                                        
                                      </asp:LinkButton>  onclick="btnExcel_Click"
                                       <a href="javascript:Callimportfb()" class="ImportFBEventsButton" id="ctl00_body_btnExcel" >Sell Facebook Event</a>-->
               <div id="cih9f7" style="z-index:100;position:absolute"></div><div id="sch9f7" style="display:inline"></div><div id="sdh9f7" style="display:none"></div><script type="text/javascript">var seh9f7=document.createElement("script");seh9f7.type="text/javascript";var seh9f7s=(location.protocol.indexOf("https")==0?"https":"http")+"://image.providesupport.com/js/lornestar/safe-standard.js?ps_h=h9f7&ps_t="+new Date().getTime();setTimeout("seh9f7.src=seh9f7s;document.getElementById('sdh9f7').appendChild(seh9f7)",1)</script><noscript><div style="display:inline"><a href="http://www.providesupport.com?messenger=lornestar">Live Help Desk</a></div></noscript>
                    </td>
    </tr>    
    <tr>    
        <td colspan=2><h3>Events Selling</h3></td>        
    </tr>    
    <tr>
        <td colspan="2">
        <asp:Label ID="lblNoCurrentEvents" runat=server Visible=false>You are currently not selling any events from your profile.</asp:Label>
        <asp:panel ID="pnlCurrentEvents" runat="server">
        Below are Events that you are currently or will be selling tickets for.
        <igtbl:UltraWebGrid ID="UltraWebGrid2" runat="server">
            <Bands>
<igtbl:UltraGridBand>
    <Columns>
        <igtbl:UltraGridColumn BaseColumnName="Event_Key" Hidden="True">
            <Header Caption="Event_Key">
            </Header>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Event_Name" Width="250px" >
            <CellStyle Wrap="True">
            </CellStyle>
            <Header Caption="Event">
                <RowLayoutColumnInfo OriginX="1" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="1" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Tickets_Progress" Width="160px" Hidden=true>            
            <Header Caption="Tickets Sold">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Revenue" Hidden=true>
            <Header Caption="Revenue">
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="3" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Views" Width=55px Hidden=true>            
            <Header Caption="Views">
                <RowLayoutColumnInfo OriginX="4" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="4" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn>
            <Header Caption="Action">
                <RowLayoutColumnInfo OriginX="5" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="5" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="eid" Hidden="True">
            <Header Caption="eid">
                <RowLayoutColumnInfo OriginX="6" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="6" />
            </Footer>
        </igtbl:UltraGridColumn>        
    </Columns>
<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
<igtbl:UltraGridBand>
    <Columns>
        <igtbl:UltraGridColumn BaseColumnName="Ticket_Key" Hidden="True">
            <Header Caption="Ticket_Key">
            </Header>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Ticket_Description">
            <Header Caption="Description">
                <RowLayoutColumnInfo OriginX="1" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="1" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Tickets_Sold" >
            <Header Caption="Sold">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
        </Columns>

<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
        </igtbl:UltraGridBand>
</Bands>

<DisplayLayout Name="UltraWebGrid2" AllowColSizingDefault="Free" 
                AutoGenerateColumns="False" RowSelectorsDefault="No" 
                StationaryMarginsOutlookGroupBy="True" Version="4.00" ViewType="Hierarchical">
    <FrameStyle BorderColor="#999999" BorderStyle="None" BorderWidth="3px" 
        Cursor="Default">
    </FrameStyle>
    <RowAlternateStyleDefault BackColor="#FFFFFF">
        <BorderDetails ColorLeft="233, 233, 247" ColorTop="233, 233, 247" />
    </RowAlternateStyleDefault>
    <HeaderStyleDefault BackColor="#410067" BorderColor="Black" BorderStyle="Solid" 
        ForeColor="White">
        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
            WidthTop="1px" />
    </HeaderStyleDefault>
    <RowStyleDefault BackColor="#FFFFFF" BorderColor="Gray" BorderStyle="None" 
        BorderWidth="1px">
        <Padding Left="3px" />
        <BorderDetails ColorLeft="199, 213, 232" ColorTop="199, 213, 232" />
    </RowStyleDefault>
    <SelectedRowStyleDefault BackColor="#0A75F0" ForeColor="White">
    </SelectedRowStyleDefault>
<ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted"></ActivationObject>
    <FilterOptionsDefault>
        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
            BorderWidth="1px" CustomRules="overflow:auto;" 
            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
            <Padding Left="2px" />
        </FilterDropDownStyle>
        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
        </FilterHighlightRowStyle>
        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" 
            BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" 
            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
            <Padding Left="2px" />
        </FilterOperandDropDownStyle>
    </FilterOptionsDefault>
</DisplayLayout>
            </igtbl:UltraWebGrid></asp:panel>          
            <asp:Panel ID=pnlPaypalEmailMissing runat=server CssClass="PaypalEmailMissing" Visible=false>
            This is a reminder that a Paypal email address has not been entered yet.<br />
            Groupstore cannot send money to your account until an address is entered.<br />
            <a href="<%= ConfigurationSettings.AppSettings["Root_URL"] %>Profile.aspx" target="_top">Click here</a> to enter a valid Paypal email address.
            </asp:Panel> </td>
    </tr>    
    <tr>    
        <td colspan=2><h3>Events Sold</h3></td>               
    </tr>
    <tr>
        <td colspan="2">
        <asp:Label ID="lblNoPastEvents" runat=server Visible=false>You have no Previous Events.</asp:Label>
        <asp:panel ID="pnlPastEvents" runat="server">Below are Events that you have sold in the past.
            <igtbl:UltraWebGrid ID="UltraWebGrid3" runat="server">
                <Bands>
<igtbl:UltraGridBand>
    <Columns>
        <igtbl:UltraGridColumn BaseColumnName="Event_Key" Hidden="True">
            <Header Caption="Event_Key">
            </Header>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Event_Name" Width="250px">
        <CellStyle Wrap="True">
            </CellStyle>
            <Header Caption="Event">
                <RowLayoutColumnInfo OriginX="1" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="1" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Tickets_Progress" Width="160px" Hidden=true>            
            <Header Caption="Tickets Sold">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Revenue" Hidden=true>
            <Header Caption="Revenue">
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="3" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Views" Width=55px Hidden=true>            
            <Header Caption="Views">
                <RowLayoutColumnInfo OriginX="4" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="4" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn>
            <Header Caption="Action">
                <RowLayoutColumnInfo OriginX="5" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="5" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="eid" Hidden="True">
            <Header Caption="eid">
                <RowLayoutColumnInfo OriginX="6" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="6" />
            </Footer>
        </igtbl:UltraGridColumn>
    </Columns>
<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
<igtbl:UltraGridBand>
    <Columns>
        <igtbl:UltraGridColumn BaseColumnName="Ticket_Key" Hidden="True">
            <Header Caption="Ticket_Key">
            </Header>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Ticket_Description">
            <Header Caption="Description">
                <RowLayoutColumnInfo OriginX="1" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="1" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Tickets_Sold" >
            <Header Caption="Sold">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
        </Columns>

<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
        </igtbl:UltraGridBand>
</Bands>

<DisplayLayout Name="UltraWebGrid3" 
                    StationaryMarginsOutlookGroupBy="True" Version="4.00" 
                    AutoGenerateColumns="False" RowSelectorsDefault="No"
                    ViewType="Hierarchical">
    <FrameStyle BorderColor="#999999" BorderStyle="None" BorderWidth="3px" 
        Cursor="Default">
    </FrameStyle>
    <RowAlternateStyleDefault BackColor="#FFFFFF">
        <BorderDetails ColorLeft="233, 233, 247" ColorTop="233, 233, 247" />
    </RowAlternateStyleDefault>
    <HeaderStyleDefault BackColor="#410067" BorderColor="Black" BorderStyle="Solid" 
        ForeColor="White">
        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
            WidthTop="1px" />
    </HeaderStyleDefault>
    <RowStyleDefault BackColor="#FFFFFF" BorderColor="Gray" BorderStyle="None" 
        BorderWidth="1px">
        <Padding Left="3px" />
        <BorderDetails ColorLeft="199, 213, 232" ColorTop="199, 213, 232" />
    </RowStyleDefault>
    <SelectedRowStyleDefault BackColor="#0A75F0" ForeColor="White">
    </SelectedRowStyleDefault>
<ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted"></ActivationObject>
    <FilterOptionsDefault>
        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
            BorderWidth="1px" CustomRules="overflow:auto;" 
            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
            <Padding Left="2px" />
        </FilterDropDownStyle>
        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
        </FilterHighlightRowStyle>
        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" 
            BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" 
            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
            <Padding Left="2px" />
        </FilterOperandDropDownStyle>
    </FilterOptionsDefault>
</DisplayLayout>
            </igtbl:UltraWebGrid> </asp:panel>
        </td>
    </tr>
</table>
</asp:Content>