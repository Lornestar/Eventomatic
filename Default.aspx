<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Eventomatic._Default" MasterPageFile="~/Site.Master"%>


<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="Addons/Activities.ascx" tagname="Activities" tagprefix="uc2" %>
<%@ Register src="Addons/Promote_Event.ascx" tagname="Promote" tagprefix="uc3" %>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat=server  LoadingPanelID="LoadingPanel1" >
<telerik:RadCodeBlock ID="RadCodeBlockDefault" runat="server">        
    </telerik:RadCodeBlock> 
    <div id="order" class="orderText">
            <!---->
        </div>
<asp:Panel ID=pnlDefault runat=server Visible=true>
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
                                <td><asp:Label ID="lblRevenue" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td >
                                <asp:Panel ID=pnltodos runat=server style="background-color:#F0D8FF;width:520px;">
                                <b>Suggested Next Steps:</b>
                                <asp:Panel ID=pnladdtab runat=server  Visible=false><hr />
                                    Sell your events from your Facebook Profile.<br />                                     
                                        <fb:add-profile-tab type="off-facebook"></fb:add-profile-tab>       
                                 </asp:Panel>    
                                 <asp:Panel ID=pnladdadmin runat=server Visible=false><hr />
                                    Give access to your friends to sell in this store - <a href="<%= ConfigurationSettings.AppSettings["Root_URL"] %>Settings.aspx" target="_top">Modify Store Settings</a>
                                 </asp:Panel>
                                 <asp:Panel ID=pnlbuydemo runat=server Visible=false>
                                    <hr />
                                    See what happens when a guest buys a ticket, Completely free - <asp:HyperLink ID="hypdidbuydemo" Text="Click here to try" runat="server" Target="_top"></asp:HyperLink>
                                 </asp:Panel>
                                 </asp:Panel>
                                </td>                                
                            </tr>
                            <tr>
                                <td>Your Groupstore: <asp:Label ID="lblStoreURL" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><asp:Panel ID=pnlCalendar runat=server Visible=false><a href=<%= ConfigurationSettings.AppSettings["Root_URL"] %>Calendar.aspx target=_top>View the Calendar</a></asp:Panel></td>
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
                                       <table>
                                        <tr>
                                            <td align=right>
                                            <div id="cih9f7" style="z-index:100;position:absolute"></div><div id="sch9f7" style="display:inline"></div><div id="sdh9f7" style="display:none"></div><script type="text/javascript">var seh9f7=document.createElement("script");seh9f7.type="text/javascript";var seh9f7s=(location.protocol.indexOf("https")==0?"https":"http")+"://image.providesupport.com/js/lornestar/safe-standard.js?ps_h=h9f7&ps_t="+new Date().getTime();setTimeout("seh9f7.src=seh9f7s;document.getElementById('sdh9f7').appendChild(seh9f7)",1)</script><noscript><div style="display:inline"><a href="http://www.providesupport.com?messenger=lornestar">Live Help Desk</a></div></noscript>
                                            </td>
                                        </tr>
                                       <!-- <tr>
                                            <td align=right>
                                            <a href="<%= ConfigurationSettings.AppSettings["Root_URL"] %>Referral.aspx" target="_top" ><div Class="Button_Stickout">Refer a Friend</div></a>
                                            </td>
                                        </tr>-->
                                        <tr>
                                            <td align=right style="font-size:small;"></td>
                                        </tr>
                                       </table>               
                    </td>
    </tr>    
    <tr>    
        <td colspan=2><uc2:Activities ID="Activities1" runat="server" />
        </td>        
    </tr>    
    <tr>    
        <td colspan=2><h3>Facebook Events Promoting</h3></td>        
    </tr>    
    <tr>    
        <td colspan=2>
        
        
    <asp:Panel runat=server ID=pnlEventsPromoting>
    <table>
    <telerik:RadListView ID="RadListView1" runat="server" Width=300
                        ItemPlaceholderID="ListViewContainer" OnItemDataBound="RadListView1_ItemDataBound">
                        <LayoutTemplate>                
                            <asp:PlaceHolder runat="server" id="ListViewContainer" />
                        </LayoutTemplate>            
                        <ItemTemplate>                                
                                <tr valign=top>
                                    <td>
                                    <telerik:RadBinaryImage ID=radimage runat=server Height=30px/></td>
                                    <td>
                                    <asp:HyperLink ID=hypevent runat=server Text='<%#Eval("Event_Name")%>'></asp:HyperLink> </td>
                                    <td><asp:label ID=lblremove runat=server></asp:label></td>
                                    <td><asp:label ID=lbleventid runat=server Text='<%#Eval("eid")%>' Visible=false></asp:label>
                                    <asp:label ID=lbleventkey runat=server Text='<%#Eval("Event_Key")%>' Visible=false></asp:label></td>
                                </tr>                            
                        </ItemTemplate>
                        </telerik:RadListView>                            
      </table>
      </asp:Panel>
    <asp:Label ID=lblPromoteEvent runat=server></asp:Label>    
        
        </td>        
    </tr>    
    <tr>    
        <td colspan=2><h3>Events Selling</h3></td>        
    </tr>    
    <tr>
        <td colspan="2">
        <asp:Label ID="lblNoCurrentEvents" runat=server Visible=false>You have no Current Events. <asp:label ID=lblSellanEvent runat=server/></asp:Label>
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
        <igtbl:UltraGridColumn BaseColumnName="Date_Begins" Width="55px" >            
            <Header Caption="Date">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Tickets_Progress" Width="105px" >            
            <Header Caption="Tickets Sold">
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="3" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Revenue" Width=80px >
        <CellStyle Wrap=true></CellStyle>
            <Header Caption="Revenue">
                <RowLayoutColumnInfo OriginX="4" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="4" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Views" Width=55px>            
            <Header Caption="Views">
                <RowLayoutColumnInfo OriginX="5" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="5" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn>
            <Header Caption="Action">
                <RowLayoutColumnInfo OriginX="6" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="6" />
            </Footer>
            <CellStyle BackColor=#F0D8FF></CellStyle>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="eid" Hidden="True">
            <Header Caption="eid">
                <RowLayoutColumnInfo OriginX="7" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="7" />
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
            <a href="<%= ConfigurationSettings.AppSettings["Root_URL"] %>Settings.aspx" target="_top">Click here</a> to enter a valid Paypal email address.</asp:Panel> </td>
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
        <igtbl:UltraGridColumn BaseColumnName="Date_Begins" Width="55px" >            
            <Header Caption="Date">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Tickets_Progress" Width="105px" >            
            <Header Caption="Tickets Sold">
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="3" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Revenue" Width=80px>
        <CellStyle Wrap=true></CellStyle>
            <Header Caption="Revenue">
                <RowLayoutColumnInfo OriginX="4" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="4" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Views" Width=55px>            
            <Header Caption="Views">
                <RowLayoutColumnInfo OriginX="5" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="5" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn>
            <Header Caption="Action">
                <RowLayoutColumnInfo OriginX="6" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="6" />
            </Footer>
            <CellStyle BackColor=#F0D8FF></CellStyle>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="eid" Hidden="True">
            <Header Caption="eid">
                <RowLayoutColumnInfo OriginX="7" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="7" />
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
<asp:Label ID=lbltest runat=server></asp:Label>
    <script language="javascript">
    function doRemove(eventid){
    var answer = confirm("Are you sure you want to Remove Event from Store?")
	if (answer){
		__doPostBack('DoRemove',eventid);
	}
	else{
	//
	}    
    }
    function DoRemovePromoted(eventid){
    var answer = confirm("Are you sure you want to stop promoting this event?")
	if (answer){
		__doPostBack('DoRemovePromoted',eventid);
	}
	else{
	//
	}    
    }
    </script>

</asp:Panel>

<asp:Panel ID=pnlfirsttime runat=server Visible=false>

<table width=100%>
    <tr>
        <td align=center width=100%>
    <table >
    <tr>
        <td >
        <table>
            <tr>
                <td><h3>Welcome to </h3></td>
                <td><img width="100px" src="Images/groupstore_Order_Logo2.png"/></td>
            </tr>
        </table>
        
        <br />
        What is the name of your Group?<br />
        <asp:TextBox ID=txtNewGroup runat=server Width=300px></asp:TextBox>
        <br />
        <div class=Fine_Print>*This will become the name of your new Groupstore</div>
                        
                           <asp:Button ID=btnaddgroup runat=server Text="Begin Selling" 
                            onclick="btnaddgroup_Click"/><br />
         <asp:Label ID=lblErrorNoName runat=server ForeColor=Red Visible=false></asp:Label>
                
        </td>
    </tr>
</table>
</td>
    </tr>
</table>
</asp:Panel>
</telerik:RadAjaxPanel>
</asp:Content>