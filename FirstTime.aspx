<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstTime.aspx.cs" Inherits="Eventomatic.FirstTime" MasterPageFile="~/Site.Master"%>
<%@ MasterType VirtualPath="~/Site.Master" %>

<%@ Register src="Addons/Add_Tab.ascx" tagname="Add_Tab" tagprefix="uc1" %>

<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">

<link rel="stylesheet" href="http://static.ak.facebook.com/css/fb_connect.css" type="text/css" />
<igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" >
<asp:HiddenField ID=hdstep Value=1 runat=server />
<asp:HiddenField ID=hdeid Value=0 runat=server /> <!--If they chose suggested then use hdeid -->
<asp:HiddenField ID=hdcustomevent Value=0 runat=server /> <!--0 means there are some events/ 1 means they have no events so have to enter custom event-->
<asp:HiddenField ID=hdsuggest Value=0 runat=server /> <!--0 means no suggestion/ 1 means have suggestion-->
<asp:HiddenField ID=hdpayment Value=1 runat=server /><!--1 means paypal/ 2 means cheque / 3 means decide later-->
<asp:HiddenField ID=hdEventstagechosen Value=0 runat=server /> <!--0 means Suggestion/ 1 means from ultragrid / 2 means custom-->
  
<link href="Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />  
    <link href="Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" />      
<script language=javascript>document.getElementById('spnNavbar').style.display = 'none';
</script>
<script language=javascript>
function initpage(){
document.getElementById('ctl00_hdCanvasSize').value = "950";
if (document.getElementById('ctl00_body_hdstep').value == "1"){//Step 1 Groupname
    document.getElementById('btnBack').style.display = 'none';    
}
else if (document.getElementById('ctl00_body_hdstep').value == "2"){ //Step 2 Choose Event    
    document.getElementById('btnBack').style.display = 'block';    
    if (document.getElementById('ctl00_body_hdcustomevent').value == "0")//There is events
    {
        if (document.getElementById('ctl00_body_hdsuggest').value == "1") //There is a suggestion
        {
            if (document.getElementById('spansuggest') != null)
            {document.getElementById('spansuggest').style.display = 'block';}
            if (document.getElementById('spaneventlist') != null)
            {document.getElementById('spaneventlist').style.display = 'none';}
        }
        else//There is no suggestion
        {
            document.getElementById('spansuggest').style.display = 'none';
            document.getElementById('spaneventlist').style.display = 'block';
        }
        document.getElementById('spanenterevent').style.display = 'none';
    }    
    else//There are no events , must type in custom one
    {
        document.getElementById('spanenterevent').style.display = 'block';
    }
}
else 
{
    document.getElementById('btnBack').style.display = 'block';
}
}

function backtab(){
var myUltraWebTab = igtab_getTabById('ctl00_body_UltraWebTab1');
if (myUltraWebTab.getSelectedIndex() == 1)
{
    myUltraWebTab.setSelectedIndex(0);
    document.getElementById('btnBack').style.display = 'none';
    document.getElementById('ctl00_body_hdstep').value == "1"
}
if (myUltraWebTab.getSelectedIndex() == 2)
{
    myUltraWebTab.setSelectedIndex(1);
    document.getElementById('btnNextStep').style.display = 'block';
    document.getElementById('btnSellEvent').style.display = 'none'; 
    document.getElementById('ctl00_body_hdstep').value == "2"
}
}

function Choose_eid(eid)
{
    //alert(document.getElementById('ctl00_body_hdeid').value);
    //igtbl_selectRow("ctl00_body_UltraWebTab1_ctl01_UltraWebGrid1", "ctl00_body_UltraWebTab1_ctl01_UltraWebGrid1r_"+Row, true);
    document.getElementById('ctl00_body_hdeid').value = eid;    
}

function changesuggest(suggest)
{
    if (suggest == "1")//see other suggestions
    {        
        document.getElementById('spaneventlist').style.display = 'none';
        document.getElementById('ctl00_body_hdEventstagechosen').value = '0';
    }
    else
    {        
        document.getElementById('spaneventlist').style.display = 'block';
        document.getElementById('ctl00_body_hdEventstagechosen').value = '1';
    }
}

function changeentercustom(suggest)
{
    if (suggest == "1")//enter custom event info
    {        
        document.getElementById('spanenterevent').style.display = 'block';
        document.getElementById('spansuggest').style.display = 'none';
        document.getElementById('spaneventlist').style.display = 'none';
        
        document.getElementById('ctl00_body_hdEventstagechosen').value = '2';
    }
    else if (suggest =="0")
    {        
        document.getElementById('spanenterevent').style.display = 'none';
        document.getElementById('spansuggest').style.display = 'block';
        document.getElementById('spaneventlist').style.display = 'block';
        
        document.getElementById('ctl00_body_hdEventstagechosen').value = '1';
    }
    else if (suggest =="2") //Comes from Suggest only
    {        
        document.getElementById('spanenterevent').style.display = 'block';        
        
        document.getElementById('ctl00_body_hdEventstagechosen').value = '2';
    }
    else if (suggest =="3")//Comes from Suggest only
    {        
        document.getElementById('spanenterevent').style.display = 'none';        
        
        document.getElementById('ctl00_body_hdEventstagechosen').value = '0';
    }
}

function btnnext()
{
var myUltraWebTab = igtab_getTabById('ctl00_body_UltraWebTab1');
if (myUltraWebTab.getSelectedIndex() == 0)
{
    __doPostBack('ctl00$body$btnaddgroup','');
}
else if (myUltraWebTab.getSelectedIndex() == 1)
{
    document.getElementById('btnNextStep').style.display = 'none';
    document.getElementById('btnSellEvent').style.display = 'block'; 
    myUltraWebTab.setSelectedIndex(2);
    document.getElementById('ctl00_body_hdstep').value == "3"
}
else if (myUltraWebTab.getSelectedIndex() == 2)
{
    var saveit = true;
    if (document.getElementById('ctl00_body_hdpayment').value == 1)
    {
        if (emailcheck(document.getElementById('ctl00_body_UltraWebTab1_ctl02_txtPaypal').value) == false)
        {
            saveit=false;
            document.getElementById('Paypalerror').style.display = 'block';
            document.getElementById('Paypalerror').innerHTML = 'An email address is required';
        }
        else if (document.getElementById('ctl00_body_UltraWebTab1_ctl02_txtPaypal').value != document.getElementById('ctl00_body_UltraWebTab1_ctl02_txtPaypalConfirm').value)
        {
            saveit=false;
            document.getElementById('Paypalerror').style.display = 'block';
            document.getElementById('Paypalerror').innerHTML = 'Paypal emails do not match';
        }
    }

    if (saveit)
    {
        //Sell event
        __doPostBack('SaveAll','');    
    }    
}
}

function changepayment(selection)
{
    document.getElementById('ctl00_body_hdpayment').value = selection;
    if (selection == "1")
    { 
        document.getElementById('spanpaypal').style.display = 'block';
        document.getElementById('spancheque').style.display = 'none';        
        document.getElementById('spandecidelater').style.display = 'none';
    }
    if (selection == "2")
    { 
        document.getElementById('spanpaypal').style.display = 'none';
        document.getElementById('spancheque').style.display = 'block';        
        document.getElementById('spandecidelater').style.display = 'none';
    }
    if (selection == "3")
    { 
        document.getElementById('spanpaypal').style.display = 'none';
        document.getElementById('spancheque').style.display = 'none';        
        document.getElementById('spandecidelater').style.display = 'block';
    }
}
function becomeseller(){
    document.getElementById('addbutton').style.display = 'block';
}
//Choose_eid(23);
</script>
<table width=100%>
    <tr>
        <td >
        <table width=100%>
            <tr>
                <td align=left>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td><h3>Welcome to </h3></td>
                            <td><img width="100px" src="Images/groupstore_Order_Logo2.png"/></td>
                                    </tr>
                                </table>
                            </td>                            
                        </tr>                        
                    </table>        
                </td>
                <td style="text-align:center;">
                <a href="http://promo.thegroupstore.com/Demo.aspx" target="_blank" style="color:White; width:150px;" class="Button_Stickout">&nbsp;Try the Demo&nbsp;</a>
                </td>
                <td align=right style="text-align:right;">
                    <table width=100%>
                        <tr>
                            <td align=right style="text-align:right;">
                            <div id="ciUiRc" style="z-index:100;position:absolute;"></div>
                            <div id="scUiRc" style="display:inline;"></div>
                            <div id="sdUiRc" style="display:none;"></div>
                            <script type="text/javascript">
                            var seUiRc=document.createElement("script");
                            seUiRc.type="text/javascript";
                            var seUiRcs=(location.protocol.indexOf("https")==0?"https":"http")+"://image.providesupport.com/js/lornestar/safe-standard.js?ps_h=UiRc&ps_t="+new Date().getTime();
                            setTimeout("seUiRc.src=seUiRcs;document.getElementById('sdUiRc').appendChild(seUiRc)",1)
                            </script><noscript>
                            <div style="display:inline;">
                            <a href="http://www.providesupport.com?messenger=lornestar">Live Help Desk</a></div></noscript>
                            </td>
                        </tr>
                        <tr>
                            <td align=right style="text-align:right;">
                            Want help getting started? Call us! 1-888-828-4496
                            </td>
                        </tr>
                    </table>                    
                </td>
            </tr>
        </table>        
        </td>
     </tr>     
    <tr>
        <td align=center width=100%>
       <asp:Panel ID=pnlOpenstore runat=server>
<table width=100% >
    
     <tr>
     <td>
        <igtab:UltraWebTab ID="UltraWebTab1" runat="server" Width=100% BorderStyle=Solid BorderWidth=1px
               >
                <Tabs>
                    <igtab:Tab Text="Step 1 (Group Name) <img src='Images/arrow_forward.gif' />" >
                        <ContentTemplate>
                        <br />
                        <table width=100% class="First_Time_Content">
                            <tr>
                                <td style="text-align:center;">
                                    <span class="First_Time_Big_Questions">What is the name of your Group?</span><br />
                                    <asp:TextBox ID=txtNewGroup runat=server Width=300px></asp:TextBox>
                                    <br />
                                    <div style="font-size:smaller;">*This will become the name of your new Groupstore</div>                                        
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <!-- Hidden Grid Holds Groups & pages admin of -->
                                    <igtbl:UltraWebGrid ID="UltraWebGrid2" runat="server" Height="200px" Width="325px" Visible=false>
                         
<DisplayLayout Name="UltraWebGrid2" AutoGenerateColumns="True" AllowColSizingDefault="Free" 
                                AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes" 
                                AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" 
                                BorderCollapseDefault="Separate" RowHeightDefault="20px" 
                                RowSelectorsDefault="No" SelectTypeRowDefault="Extended" 
                                StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" 
                                TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" >
    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif" 
        Font-Size="8.25pt" Height="200px" Width="325px">
    </FrameStyle>
    <Pager MinimumPagesForDisplay="2">
        <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
            WidthTop="1px" />
        </PagerStyle>
    </Pager>
    <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
    </EditCellStyleDefault>
    <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
            WidthTop="1px" />
    </FooterStyleDefault>
    <HeaderStyleDefault BackColor="LightGray" BorderStyle="Solid" 
        HorizontalAlign="Left">
        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
            WidthTop="1px" />
    </HeaderStyleDefault>
    <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="None" 
        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
        <Padding Left="3px" />
        <BorderDetails ColorLeft="Window" ColorTop="Window" />
    </RowStyleDefault>
    <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
    </GroupByRowStyleDefault>
    <GroupByBox>
        <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
        </BoxStyle>
    </GroupByBox>
    <AddNewBox Hidden="False">
        <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" 
            BorderWidth="1px">
            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                WidthTop="1px" />
        </BoxStyle>
    </AddNewBox>
<ActivationObject BorderColor="" BorderWidth=""></ActivationObject>
    <FixedCellStyleDefault BorderStyle="None">
    </FixedCellStyleDefault>
    <FixedHeaderStyleDefault BorderStyle="None">
    </FixedHeaderStyleDefault>
    <FilterOptionsDefault>
        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
            BorderWidth="1px" CustomRules="overflow:auto;" 
            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px" 
            Width="200px">
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
                        </igtbl:UltraWebGrid>
                                </td>
                            </tr>
                            <!--<tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td><span class="First_Time_Big_Questions">Do you want any of your friends to admin your Groupstore?</span></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr valign="middle">
                                                        <td align=right>
                                                            Your Facebook Friends List<br />
                                                            <asp:ListBox ID="lbFriendsList" runat="server" Rows=10 Width=200></asp:ListBox>
                                                        </td>
                                                        <td align=center>                        
                                                            <asp:Button ID=btnAdd runat=server onclick="btnAdd_Click" Text=">" />
                                                            <br />
                                                            <asp:Button ID=btnRemove runat=server onclick="btnRemove_Click" Text="<" />
                                                                                    
                                                        </td>
                                                        <td align=left>                   
                                                            <asp:Label ID=lblGroupName runat=server></asp:Label><br />
                                                            <asp:ListBox ID="lbAdmins" runat="server" Rows=10 Width=200>                            
                                                            </asp:ListBox>
                                                        </td>            
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="3"><asp:Label ID=lblError runat=server ForeColor=Red Visible=false>You cannot remove yourself from the list</asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                
                            </tr>-->
                        </table>                                                                                                         
                        </ContentTemplate>
                    </igtab:Tab>
                </Tabs>
                <Tabs>
                    <igtab:Tab Text="Step 2 (The Event) <img src='Images/arrow_forward.gif' />" >
                        <ContentTemplate>
                        <asp:Panel ID=pnlOnlyOneEvent runat=server Visible=false>
                        <table width=100% class="First_Time_Content">
                            <tr>
                                <td align=center>
                                    <span class="First_Time_Big_Questions">Is this the event you want to sell?</span><br />
                                </td>
                             </tr>
                             <tr>
                                <td align=center>
                                    <table>
                                        <tr valign=top>
                                            <td><asp:Image ID=imgSuggestOnlyEvent runat=server /></td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td><asp:Label ID=lblSuggestOnlyEvent runat=server></asp:Label></td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID=lblSuggestOnlyEventtime runat=server></asp:Label></td>
                                                    </tr>
                                                </table>                                            
                                        </tr>
                                    </table>
                                    
                                    </td>
                             </tr>
                             <tr>
                                <td align=center>
                                    <table cellpadding=10px>
                                        <tr>
                                            <td align=left><input type='radio' name='Suggest' value='Yes' onclick="changeentercustom(3)" checked=checked/>Yes, Sell It</td>                                     
                                        <td align=left><input type='radio' name='Suggest' value='No' onclick="changeentercustom(2)" />No, let me sell an event not on facebook</td>
                                        </tr>
                                    </table>                                
                                    </td>
                             </tr>
                         </table>
                        </asp:Panel>
                        <asp:Panel ID=pnlHasEvent runat=server Visible=true>
                        <span id=spansuggest>
                        <table width=100% class="First_Time_Content">
                            <tr>
                                <td align=center>
                                    <span class="First_Time_Big_Questions">Is this the event you want to sell?</span><br />
                                </td>
                             </tr>
                             <tr>
                                <td align=center>
                                    <table>
                                        <tr valign=top>
                                            <td><asp:Image ID=imgSuggestedEvent runat=server ImageUrl="~/Images/Event_NoImage.jpg"/></td>                                        
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td><asp:Label ID=lblSuggestedEvent runat=server></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID=lblSuggestedEventtime runat=server></asp:Label></td>
                                                    </tr>
                                                </table>
                                                                                       
                                        </tr>
                                    </table>
                                    </td>
                             </tr>
                             <tr>
                                <td align=center >
                                    <table cellpadding=10px>
                                        <tr>
                                            <td align=left><input type='radio' name='Suggest' value='Yes' onclick="changesuggest(1)" checked=checked/>Yes, Sell It</td>                                    
                                        <td align=left><input type='radio' name='Suggest' value='No' onclick="changesuggest(0)" />No let me see more events</td>
                                        </tr>
                                    </table>                                
                                    </td>
                             </tr>
                         </table>
                        </span>
                        <span id=spaneventlist>
                        <table width=100% class="First_Time_Content">
                            <tr>
                                <td align=center>
                                    <span class="First_Time_Big_Questions">Which Event do you want to sell?</span><br />
                                    <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server">
        <Bands>
            <igtbl:UltraGridBand>
                <Columns>
                    <igtbl:UltraGridColumn BaseColumnName="creator" Hidden="True">
                        <Header Caption="Creator">
                        </Header>
                    </igtbl:UltraGridColumn>
                    <igtbl:UltraGridColumn Width=30px >
                        <CellStyle HorizontalAlign=Left></CellStyle>
                        <Header>
                            <RowLayoutColumnInfo OriginX="1" />
                        </Header>
                        <Footer>
                            <RowLayoutColumnInfo OriginX="1" />
                        </Footer>
                    </igtbl:UltraGridColumn>
                    <igtbl:UltraGridColumn BaseColumnName="name" Key="EventID" Width="300px">                    
                        <Header Caption="Event">
                            <RowLayoutColumnInfo OriginX="2" />
                        </Header>
                        <Footer>
                            <RowLayoutColumnInfo OriginX="2" />
                        </Footer>
                        <CellStyle Wrap="true"></CellStyle>
                    </igtbl:UltraGridColumn>
                    <igtbl:UltraGridColumn BaseColumnName="start_time" Width="150px">
                        <Header Caption="Start Time">
                            <RowLayoutColumnInfo OriginX="3" />
                        </Header>
                        <Footer>
                            <RowLayoutColumnInfo OriginX="3" />
                        </Footer>
                    </igtbl:UltraGridColumn>                    
                    <igtbl:UltraGridColumn BaseColumnName="eid" Hidden="True">
                        <Header Caption="eid">
                            <RowLayoutColumnInfo OriginX="4" />
                        </Header>
                        <Footer>
                            <RowLayoutColumnInfo OriginX="4" />
                        </Footer>
                    </igtbl:UltraGridColumn>
                </Columns>
                <RowEditTemplate>
                    <br>
                        <p align="center">
                            <input id="igtbl_reOkBtn" onclick="igtbl_gRowEditButtonClick(event);" 
                                style="width:50px;" type="button" value="OK">
                                &nbsp;
                                <input id="igtbl_reCancelBtn" onclick="igtbl_gRowEditButtonClick(event);" 
                                    style="width:50px;" type="button" value="Cancel">
                                </input>
                            </input>
                        </p>
                    </br>
                </RowEditTemplate>
                <RowTemplateStyle BackColor="Window" BorderColor="Window" BorderStyle="Ridge">
                    <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" 
                        WidthTop="3px" />
                </RowTemplateStyle>
                <AddNewRow View="NotSet" Visible="NotSet">
                </AddNewRow>
            </igtbl:UltraGridBand>
        </Bands>
        <DisplayLayout Name="UltraWebGrid1" AutoGenerateColumns="False" RowSelectorsDefault="No"  
                                StationaryMarginsOutlookGroupBy="True" Version="4.00"  CellClickActionDefault="RowSelect"
                                AllowSortingDefault="Yes" ViewType="Hierarchical" TableLayout="Fixed" 
                                AllowUpdateDefault="Yes" HeaderClickActionDefault="SortSingle" 
                                SelectTypeCellDefault="Single" SelectTypeRowDefault="Single" ColHeadersVisibleDefault="No">
    <FrameStyle BorderColor="#999999" BorderStyle="None" BorderWidth="3px" 
        Cursor="Default">
    </FrameStyle>
    <RowAlternateStyleDefault BackColor="#F0D8FF">
        <BorderDetails ColorLeft="233, 233, 247" ColorTop="233, 233, 247" />
    </RowAlternateStyleDefault>
    <HeaderStyleDefault Height=0px BackColor="#410067" BorderColor="Black" BorderStyle="Solid" 
        ForeColor="White">
        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
            WidthTop="1px" />
    </HeaderStyleDefault>
    <RowStyleDefault BackColor="#FFFFFF" BorderColor="Gray" BorderStyle="None" 
        BorderWidth="1px">
        <Padding Left="3px" />
        <BorderDetails ColorLeft="199, 213, 232" ColorTop="199, 213, 232" />
    </RowStyleDefault>    
<ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted"></ActivationObject>
    <FixedCellStyleDefault Wrap="True">
    </FixedCellStyleDefault>
    <FixedHeaderStyleDefault HorizontalAlign="Center">
    </FixedHeaderStyleDefault>
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
    </igtbl:UltraWebGrid>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align=right><div style="font-size:smaller;"><a href="javascript:changeentercustom(1);">*I want to sell an event not in the list</a></div></td>
                            </tr>
                        </table>
                        </span>
                        </asp:Panel>
                        <!--***********************Enter Event Info****************************************-->
                        <span id=spanenterevent>
                            <table width=100% class="First_Time_Content">
                            <tr>
                                <td align=right><asp:Panel ID=pnlisinlist runat=server Visible=true><div style="font-size:smaller;"><a href="javascript:changeentercustom(0);">*I want to sell an event that is in the list</a></div></asp:Panel></td>
                            </tr>
                            <tr>
                                <td align=center>
                                    <span class="First_Time_Big_Questions">Enter your Event information.</span><br />                                    
                                </td>
                            </tr>
                            <tr>
                                <td align=center>
                                <table >
    
                        <tr>
                            <td >Event name:</td>
                            <td><asp:TextBox ID="txtEventName" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>                        
                        <tr>
                            <td>Event Begins:</td>
                            <td>
                                <table>
                                    <tr>
                                        <td><igsch:WebDateChooser ID="StartDate" runat="server" Value="" Width="200px">
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
                                </igsch:WebDateChooser></td>
                                        <td><igtxt:WebDateTimeEdit ID="StartTime" runat="server" EditModeFormat="t" 
                                    DisplayModeFormat="t" Fields="2009-5-1-20-0-0-0" Nullable="False" Width="100">
                                </igtxt:WebDateTimeEdit></td>
                                    </tr>
                                </table>
                                
                                
                                </td>
                        </tr>
                        <tr>
                            <td>Event Ends:</td>
                            <td><table>
                                <tr>
                                    <td><igsch:WebDateChooser ID="EndDate" runat="server" Value="" Width="200px">
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
                                </igsch:WebDateChooser></td>
                                    <td><igtxt:WebDateTimeEdit ID="EndTime" runat="server" EditModeFormat="t" 
                                    DisplayModeFormat="t" Fields="2009-5-1-1-0-0-0" Nullable="False" Width="100">
                                </igtxt:WebDateTimeEdit></td>
                                </tr>
                            </table>
                             
                                
                            </td>
                        </tr>
                        <tr>
                            <td>Ticket Price:</td>
                            <td>
                                <igtxt:WebCurrencyEdit ID="WebCurrencyEdit1" runat="server" DataMode="Decimal" 
                                    HorizontalAlign="Left" Width="100px" ValueText="20" >
                                    <ClientSideEvents ValueChange="PriceValueChanged" />
                                </igtxt:WebCurrencyEdit>                         
                            </td>
                        </tr>                        
                    </table>
                                
                                </td>
                            </tr>
                        </table>
                        </span>
                        
                        </ContentTemplate>
                    </igtab:Tab>
                </Tabs>
                <Tabs>
                    <igtab:Tab Text="Step 3 (Collect Money)" >
                        <ContentTemplate>
                        <table width=100% class="First_Time_Content">
                            <tr>
                                <td align=center>
                                    <span class="First_Time_Big_Questions">How do you want to receive money from ticket sales?</span><br />
                                </td>
                            </tr>
                            <tr>
                                <td align=center>
                                    <table cellpadding=10px>
                                        <tr>
                                            <td align=left><asp:RadioButton ID=rdPayment GroupName='Payment' Checked=true onclick="changepayment('1')" runat=server/>By Paypal</td>                                         
                                            <!--<td align=left><asp:RadioButton ID=rdCheque GroupName='Payment' onclick="changepayment('2')" runat=server />Cheque by mail</td>-->
                                            <td align=left><asp:RadioButton ID=rdLater GroupName='Payment' onclick="changepayment('3')" runat=server />Arrange Paypal later, Start selling now</td>
                                        </tr>
                                    </table>                                
                                    </td>
                             </tr>
                             <tr>
                                <td align=center>
                                    <!-- Paypal info-->
                                    <span id=spanpaypal>
                                        <table>            
                                                <tr>
                                            <td><span class="Mandatory_Star">*</span>PayPal Email:</td>
                                            <td><asp:TextBox ID="txtPaypal" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><span class="Mandatory_Star">*</span>Confirm PayPal Email:</td>
                                            <td><asp:TextBox ID="txtPaypalConfirm" runat="server"></asp:TextBox>                                            
                                            

                                            </td>
                                        </tr>    
                                        <tr>
                                            <td>Desired Currency:</td>
                                            <td><asp:DropDownList ID="ddlCurrency" runat="server">
                                                    <asp:ListItem value="CAD">CAD</asp:ListItem>
                                                    <asp:ListItem value="USD">USD</asp:ListItem>
                                                    <asp:ListItem value="EUR">EUR</asp:ListItem>
                                                    <asp:ListItem value="GBP">GBP</asp:ListItem>
                                                    <asp:ListItem value="ILS">ILS</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>                                        
                                        <tr>
                                            <td colspan=2 align=center><span id=Paypalerror style="color:Red; display:none;"></span></td>
                                        </tr>
                                                </table>
                                    </span>
                                    <!-- Cheque mail info-->
                                    <span id=spancheque style="display:none;">
                                        <table>
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
                                                        <td>Country:</td>
                                                        <td><asp:DropDownList ID=ddlCountry runat=server AutoPostBack=true 
                                                                onselectedindexchanged="ddlCountry_SelectedIndexChanged">
                                                                    <asp:ListItem Value="United States">United States</asp:ListItem>
                                                                    <asp:ListItem Value="Canada">Canada</asp:ListItem>
                                                                </asp:DropDownList></td>
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
                                    <td class="Fine_Print" colspan=3><br /><span class="Mandatory_Star">*</span> Please fill in every field.</td>
                                </tr>                                                    
                                            </table>
                                    </span>
                                    <!-- Decide later-->
                                    <span id=spandecidelater style="display:none;">
                                    Begin selling now,<br /> and let us know later how you want to receive payments.
                                    </span>
                                </td>
                             </tr>
                        </table>
                        </ContentTemplate>
                    </igtab:Tab>
                </Tabs>
        <SelectedTabStyle CssClass="First_Time_SelectedTab" BorderStyle="None">
                </SelectedTabStyle>                
                <DefaultTabStyle CssClass="First_Time_UnSelectedTab" BorderStyle="None"></DefaultTabStyle>
        </igtab:UltraWebTab>
        </td>
    </tr>    
</table>
<table width=100%>
    <tr>
        <td align=center>
            <table>
                <tr>
                    <td><a href="javascript:backtab();" id=btnBack class="OrderFormButtons" style="display:none;">Back</a></td>
                    <td><a href="javascript:btnnext();" id=btnNextStep class="OrderFormButtons">Next Step</a>
                    <a href="javascript:btnnext();" id=btnSellEvent class="OrderFormButtons" style="display:none;">Sell Event</a>
                    <!--<asp:LinkButton  ID="btnaddgroup" runat="server" Text="Next Step"
                                        CssClass="OrderFormButtons" onclick="btnaddgroup_Click"></asp:LinkButton>-->
                                        </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align=center>            
                         <asp:Label ID=lblErrorNoName runat=server ForeColor=Red Visible=false></asp:Label>
        </td>
    </tr>
</table>
</asp:Panel> 
</td>
</tr>
    <tr> <!-- The question to be just a seller-->
    <td style="font-size:small;" width=100%>
    <table width=100%>
        <tr valign=bottom>
            <td><!--
            <table>
                <tr>
                <td align=left>
                    <div style="font-size:small;"><a href="#" onclick='becomeseller(); return false;'>Become a Seller of an Existing Groupstores</a></div>
                    <div id=addbutton style="display:none;">                     
                    Click below to begin selling events from your Facebook Profile.   
                    <br />
                    <fb:add-profile-tab type="off-facebook"></fb:add-profile-tab>
                </div>
                     
                </td>
                <td>|</td>
                </td>
                <td ><a href="<%= ConfigurationSettings.AppSettings["Root_URL"] %>Referral.aspx" target="_top">Referral Program                
                    </a></td>        
                </tr>
            </table>-->
            <uc1:Add_Tab ID="Add_Tab1" runat="server" />
            </td>
            <td style="text-align:right;">
             <!-- Begin Official PayPal Seal -->
                <a href="https://www.paypal.com/us/verified/pal=lorne%40lornestar%2ecom" target="_blank">
                <img src="https://www.paypal.com/en_US/i/icon/verification_seal.gif" order="0" alt="Official PayPal Seal" height=70px style="border:none;">
                </A>
                &nbsp;&nbsp;&nbsp;<!-- End Official PayPal Seal -->&nbsp;<!-- End Official PayPal Seal --></td>
        </tr>
    </table>
    
     </td>     
     </tr>
</table>
<script language=javascript>
initpage();
</script>
</igmisc:WebAsyncRefreshPanel>

<!-- Google Code for signup Conversion Page -->
<script type="text/javascript">
/* <![CDATA[ */
var google_conversion_id = 1021700689;
var google_conversion_language = "en";
var google_conversion_format = "2";
var google_conversion_color = "ffffff";
var google_conversion_label = "xbWiCMvQ4AEQ0dSX5wM";
var google_conversion_value = 0;
/* ]]> */
</script>
<script type="text/javascript" src="http://www.googleadservices.com/pagead/conversion.js">
</script>
<noscript>
<div style="display:inline;">
<img height="1" width="1" style="border-style:none;" alt="" src="http://www.googleadservices.com/pagead/conversion/1021700689/?label=xbWiCMvQ4AEQ0dSX5wM&amp;guid=ON&amp;script=0"/>
</div>
</noscript>

<!-- Google Code for Signup2 Conversion Page -->
<script type="text/javascript">
/* <![CDATA[ */
var google_conversion_id = 1013909460;
var google_conversion_language = "en";
var google_conversion_format = "2";
var google_conversion_color = "ffffff";
var google_conversion_label = "2CRWCMyo4gEQ1I-84wM";
var google_conversion_value = 0;
/* ]]> */

</script>
<script type="text/javascript" src="http://www.googleadservices.com/pagead/conversion.js">
</script>
<noscript>
<div style="display:inline;">
<img height="1" width="1" style="border-style:none;" alt="" src="http://www.googleadservices.com/pagead/conversion/1013909460/?label=2CRWCMyo4gEQ1I-84wM&amp;guid=ON&amp;script=0"/>
</div>
</noscript>


</asp:Content>