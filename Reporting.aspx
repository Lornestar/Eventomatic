<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reporting.aspx.cs" Inherits="Eventomatic.Reporting" MasterPageFile="~/Site.Master" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
    <table width=100%>
    <tr>
        <td width="0%"><h3>Reporting</h3></td>        
        <td width="100%"><hr /></td>
    </tr>
    <tr>
        <td colspan=2 align="center">
<table>
    <tr>
        <td align=center><table style="border-color:Black; border-style:solid; border-width:thin;background-color:#EEEEEE">
                <tr><td style="height: 42px">From Date:
                    </td>
                    <td style="height: 42px">
                        <igsch:WebDateChooser ID="StartDate" runat="server" Value="" Width="200px">
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
                    <td style="height: 42px">To Date:</td>
                    <td style="height: 42px">
                        <igsch:WebDateChooser ID="EndDate" runat="server" Value="" Width="200px">
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
                    <td style="height: 42px"><asp:Button ID=btndate runat=server Text="Filter" OnClick="btndate_Click" /></td>
                </tr>
                <tr>
                    <td colspan=5 align=left>
                        <table><tr>
                            <td><asp:Panel ID=pnlGroupsFilter runat=server><asp:DropDownList ID=ddlGroups runat=server Visible=false></asp:DropDownList></asp:Panel></td>
                            <td><asp:Panel ID=pnlEventsFilter runat=server><asp:DropDownList ID=ddlEvents runat=server Visible=false></asp:DropDownList></asp:Panel></td>
                        </tr></table></td>
                </tr>
            </table></td>        
    </tr>
    <tr>
        <td>
            <igtab:UltraWebTab ID="UltraWebTab1" runat="server" Width=100% SelectedTab="0" AutoPostBack=true>
                <Tabs>
                    <igtab:Tab Text="Overall Stats" >
                        <ContentTemplate>
                        <div class="Reporting_Normal">
                        <asp:Panel ID=pnlHost runat=server>
                        <table>
                            <tr>
                                <td>Currently running events = <asp:Label ID=lblCurrentEvents runat=server></asp:Label>
                                <br />
                                    Total events created = <asp:Label ID=lblTotalEvents runat=server></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                            <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server">
            <Bands>
<igtbl:UltraGridBand ColFootersVisible="Yes">
    <Columns>
        <igtbl:UltraGridColumn BaseColumnName="Event_Key" Hidden="True">
            <Header Caption="Event_Key">
            </Header>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Event_Name">
            <Header Caption="Event">
                <RowLayoutColumnInfo OriginX="1" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="1" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Tickets_Progress" Width="160px" >            
            <Header Caption="Tickets Sold">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <Footer Total=Text>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Revenue" Format="$ ###,###,##0.00" >
            <Header Caption="Revenue">
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <Footer Total="Sum">
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

<DisplayLayout Name="UltraWebGrid" AllowColSizingDefault="Free" 
                AutoGenerateColumns="False" RowSelectorsDefault="No" 
                StationaryMarginsOutlookGroupBy="True" Version="4.00" ViewType="Hierarchical">
    <FrameStyle BorderColor="#999999" BorderStyle="None" BorderWidth="3px" 
        Cursor="Default">
    </FrameStyle>
    <RowAlternateStyleDefault BackColor="#E9E9F7">
        <BorderDetails ColorLeft="233, 233, 247" ColorTop="233, 233, 247" />
    </RowAlternateStyleDefault>
    <HeaderStyleDefault BackColor="#2B64A7" BorderColor="Black" BorderStyle="Solid" 
        ForeColor="White">
        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
            WidthTop="1px" />
    </HeaderStyleDefault>
    <RowStyleDefault BackColor="#C7D5E8" BorderColor="Gray" BorderStyle="Solid" 
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
            </igtbl:UltraWebGrid>
            </td>
                            </tr>
                        </table>
   </asp:Panel>   
   
            <igtbl:UltraWebGrid ID="UltraWebGrid2" runat="server" 
                                oninitializelayout="UltraWebGrid2_InitializeLayout">
                <bands>
                    <igtbl:UltraGridBand ColFootersVisible="Yes">
                        <Columns>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Resource_Key" Width=25px>
                                <header caption="Key">
                                </header>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Group_Name" Width=100px>
                                <CellStyle Wrap=true></CellStyle>
                                <header caption="Group Name">
                                    <rowlayoutcolumninfo originx="1" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="1" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Current_Events" 
                                Width=50px FooterTotal="Sum">
                                <header caption="Current">
                                    <rowlayoutcolumninfo originx="2" />
                                </header>
                                <footer Total="Sum">
                                    <rowlayoutcolumninfo originx="2" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="AmountEvents_Date" 
                                Width=50px FooterTotal="Sum" Key="AmountEvents_Date">
                                <header caption="ByDate">
                                    <rowlayoutcolumninfo originx="3" />
                                </header>
                                <footer Total="Sum" >
                                    <rowlayoutcolumninfo originx="3" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Total_Events" 
                                Width=50px FooterTotal="Sum">
                                <header caption="Total">
                                    <rowlayoutcolumninfo originx="4" />
                                </header>
                                <footer Total="Sum">
                                    <rowlayoutcolumninfo originx="4" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="Tix_Sold" Width=50px FooterTotal="Sum">
                                <header caption="Tix Sold">
                                    <rowlayoutcolumninfo originx="5" />
                                </header>
                                <footer Total=Text>
                                    <rowlayoutcolumninfo originx="5" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="Tix_Sold_Total" Width=50px 
                                FooterTotal="Sum">
                                <header caption="Tix Total">
                                    <rowlayoutcolumninfo originx="6" />
                                </header>
                                <footer Total=Text>
                                    <rowlayoutcolumninfo originx="6" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="Revenue_Date" DataType="System.Decimal" 
                                Format=" $###,###,##0.00" Width=75px FooterTotal="Sum" Key="Revenue_Date">
                                <header caption="Rev">
                                    <rowlayoutcolumninfo originx="7" />
                                </header>
                                <footer Total="Sum" >
                                    <rowlayoutcolumninfo originx="7" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="Revenue_Total" DataType="System.Decimal" 
                                Format=" $###,###,##0.00" Width=75px FooterTotal="Sum">
                                <header caption="Rev Total">
                                    <rowlayoutcolumninfo originx="8" />
                                </header>
                                <footer Total="Sum" >
                                    <rowlayoutcolumninfo originx="8" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="" DataType="System.Decimal" 
                                Format=" $###,###,##0.00" Width=75px
                                >
                                <header caption="Rev/Evt">
                                    <rowlayoutcolumninfo originx="9" />
                                </header>
                                <footer  Total=Formula  Formula="SUM([Revenue_Date])/SUM([AmountEvents_Date])"> 
                                    <rowlayoutcolumninfo originx="9" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="" DataType="System.Decimal" 
                                Format=" $###,###,##0.00" Width=75px FooterTotal="Sum">
                                <header caption="Rev/Tix">
                                    <rowlayoutcolumninfo originx="10" />
                                </header>
                                <footer Total=Formula  Formula="SUM([Revenue_Date])/SUM([Tix_Sold])">
                                    <rowlayoutcolumninfo originx="10" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="TicketsSold_Date" Key="Tix_Sold"   Hidden=true>
                                <header caption="TixSold">
                                    <rowlayoutcolumninfo originx="11" />
                                </header>
                                <footer >
                                    <rowlayoutcolumninfo originx="11" />
                                </footer>
                            </igtbl:UltraGridColumn>
                        </Columns>
                        <addnewrow view="NotSet" visible="NotSet">
                        </addnewrow>
                    </igtbl:UltraGridBand>
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
        <igtbl:UltraGridColumn BaseColumnName="Tickets_Progress" Width="160px" >            
            <Header Caption="Tickets Sold">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Revenue" Format="$ ###,###,##0.00">
            <Header Caption="Revenue">
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="3" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="eid" Hidden="True">
            <Header Caption="eid">
                <RowLayoutColumnInfo OriginX="5" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="5" />
            </Footer>
        </igtbl:UltraGridColumn>
    </Columns>
<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
                </bands>
                <displaylayout autogeneratecolumns="False" cellclickactiondefault="Edit" 
                    name="UltraWebGrid2" selecttyperowdefault="Single" 
                    stationarymarginsoutlookgroupby="True" version="4.00" 
                    ViewType="Hierarchical">
                    <framestyle bordercolor="#999999" borderstyle="None" borderwidth="3px" 
                        cursor="Default">
                    </framestyle>
                    <rowalternatestyledefault backcolor="#E9E9F7">
                        <borderdetails colorleft="233, 233, 247" colortop="233, 233, 247" />
                    </rowalternatestyledefault>
                    <headerstyledefault backcolor="#2B64A7" bordercolor="Black" borderstyle="Solid" 
                        forecolor="White">
                        <borderdetails colorleft="White" colortop="White" widthleft="1px" 
                            widthtop="1px" />
                    </headerstyledefault>
                    <rowstyledefault backcolor="#C7D5E8" bordercolor="Gray" borderstyle="Solid" 
                        borderwidth="1px">
                        <borderdetails colorleft="199, 213, 232" colortop="199, 213, 232" />
                        <padding left="3px" />
                    </rowstyledefault>
                    <selectedrowstyledefault backcolor="#0A75F0" forecolor="White">
                    </selectedrowstyledefault>
                    <activationobject bordercolor="Black" borderstyle="Dotted" borderwidth="">
                    </activationobject>
                    <addnewrowdefault visible="Yes">
                    </addnewrowdefault>
                    <filteroptionsdefault>
                        <filterdropdownstyle backcolor="White" bordercolor="Silver" borderstyle="Solid" 
                            borderwidth="1px" customrules="overflow:auto;" 
                            font-names="Verdana,Arial,Helvetica,sans-serif" font-size="11px" width="200px">
                            <padding left="2px" />
                        </filterdropdownstyle>
                        <filterhighlightrowstyle backcolor="#151C55" forecolor="White">
                        </filterhighlightrowstyle>
                        <filteroperanddropdownstyle backcolor="White" bordercolor="Silver" 
                            borderstyle="Solid" borderwidth="1px" customrules="overflow:auto;" 
                            font-names="Verdana,Arial,Helvetica,sans-serif" font-size="11px">
                            <padding left="2px" />
                        </filteroperanddropdownstyle>
                    </filteroptionsdefault>
                </displaylayout>
            </igtbl:UltraWebGrid>  
            </div>
                        </ContentTemplate>                        
                    </igtab:Tab>
                    <igtab:Tab Text="Timeline">
                        <ContentTemplate>
                        <div class="Reporting_Normal">
                        <table>
                            <tr>
                                <td>Timeline Chart - Showing Ticket Sales</td>
                            </tr>
                            <tr>
                                <td>
                            <igchart:UltraChart ID="UltraChart1" runat="server" BackgroundImageFileName="" 
                                BorderColor="Black" BorderWidth="1px" ChartType="StackColumnChart" 
                                EmptyChartText="No Tickets Sold" 
                                Version="8.1" Width=650px   >
                                <DeploymentScenario FilePath="/Images/ChartImages" 
                                 ImageURL="/Images/ChartImages/Chart_#SEQNUM(100).png" Scenario="FileSystem" />
                                <TitleTop Visible="False">
                                </TitleTop>
                                <tooltips font-bold="False" font-italic="False" font-overline="False" 
                                    font-strikeout="False" font-underline="False" />
                                <TitleBottom Visible="False">
                                </TitleBottom>
                                <colormodel alphalevel="150" colorbegin="Pink" colorend="DarkRed" 
                                    modelstyle="CustomLinear">
                                </colormodel>
                                <effects>
                                    <effects>
                                        <igchartprop:GradientEffect />
                                    </effects>
                                </effects>
                                <axis>
                                    <pe elementtype="None" fill="Cornsilk" />
                                    <x linethickness="1" tickmarkinterval="0" tickmarkstyle="Smart" visible="True" 
                                        Extent="53"  >
                                        <Margin>
                                            <Near Value="0.48780487804878048"  />
                                        </Margin>
                                        <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                                            thickness="1" visible="True" />
                                        <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                                            thickness="1" visible="False" />
                                        <labels font="Verdana, 7pt" fontcolor="DarkRed" horizontalalign="Near" 
                                            itemformatstring="&lt;ITEM_LABEL&gt;" orientation=VerticalRightFacing
                                            verticalalign="Center">
                                            <serieslabels font="Verdana, 7pt" fontcolor="DarkRed" horizontalalign="Center" 
                                                orientation=VerticalRightFacing  verticalalign="Center">
                                                <layout  behavior="Auto" >
                                                </layout>
                                            </serieslabels>
                                            <layout behavior="Auto">
                                            </layout>
                                        </labels>
                                    </x>
                                    <y linethickness="1" tickmarkinterval="100" tickmarkstyle="Smart" 
                                        visible="True" Extent="18">
                                        <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                                            thickness="1" visible="True" />
                                        <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                                            thickness="1" visible="False" />
                                        <labels font="Verdana, 7pt" fontcolor="DimGray" horizontalalign="Far" 
                                            itemformatstring="&lt;DATA_VALUE:0&gt;" orientation="Horizontal" 
                                            verticalalign="Center">
                                            <serieslabels font="Verdana, 7pt" fontcolor="DimGray" formatstring="" 
                                                horizontalalign="Far" orientation="Horizontal" verticalalign="Center">
                                                <layout behavior="Auto">
                                                </layout>
                                            </serieslabels>
                                            <layout behavior="Auto">
                                            </layout>
                                        </labels>
                                    </y>
                                    <y2 linethickness="1" tickmarkinterval="100" tickmarkstyle="Smart" 
                                        visible="False">
                                        <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                                            thickness="1" visible="True" />
                                        <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                                            thickness="1" visible="False" />
                                        <labels font="Verdana, 7pt" fontcolor="Gray" horizontalalign="Near" 
                                            itemformatstring="&lt;DATA_VALUE:00.##&gt;" orientation="Horizontal" 
                                            verticalalign="Center" visible="False">
                                            <serieslabels font="Verdana, 7pt" fontcolor="Gray" formatstring="" 
                                                horizontalalign="Near"  orientation="Horizontal" verticalalign="Center">
                                                <layout behavior="Auto" >
                                                </layout>
                                            </serieslabels>
                                            <layout behavior="Auto">
                                            </layout>
                                        </labels>
                                    </y2>
                                    <x2 linethickness="1" tickmarkinterval="0" tickmarkstyle="Smart" 
                                        visible="False">
                                        <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                                            thickness="1" visible="True" />
                                        <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                                            thickness="1" visible="False" />
                                        <labels font="Verdana, 7pt" fontcolor="Gray" horizontalalign="Far" 
                                            itemformatstring="&lt;ITEM_LABEL&gt;" orientation="VerticalRightFacing" 
                                            verticalalign="Center" visible="False">
                                            <serieslabels font="Verdana, 7pt" fontcolor="Gray" horizontalalign="Far" 
                                                orientation="VerticalLeftFacing" verticalalign="Center">
                                                <layout behavior="Auto">
                                                </layout>
                                            </serieslabels>
                                            <layout behavior="Auto">
                                            </layout>
                                        </labels>
                                    </x2>
                                    <z linethickness="1" tickmarkinterval="0" tickmarkstyle="Smart" visible="False">
                                        <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                                            thickness="1" visible="True" />
                                        <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                                            thickness="1" visible="False" />
                                        <labels font="Verdana, 7pt" fontcolor="DimGray" horizontalalign="Near" 
                                            itemformatstring="" orientation="VerticalLeftFacing" verticalalign="Center" 
                                            visible="False">
                                            <serieslabels font="Verdana, 7pt" fontcolor="DimGray" horizontalalign="Near" 
                                                orientation="Horizontal" verticalalign="Center">
                                                <layout behavior="Auto">
                                                </layout>
                                            </serieslabels>
                                            <layout behavior="Auto">
                                            </layout>
                                        </labels>
                                    </z>
                                    <z2 linethickness="1" tickmarkinterval="0" tickmarkstyle="Smart" 
                                        visible="False">
                                        <majorgridlines alphalevel="255" color="Gainsboro" drawstyle="Dot" 
                                            thickness="1" visible="True" />
                                        <minorgridlines alphalevel="255" color="LightGray" drawstyle="Dot" 
                                            thickness="1" visible="False" />
                                        <labels font="Verdana, 7pt" fontcolor="Gray" horizontalalign="Near" 
                                            itemformatstring="" orientation="VerticalLeftFacing" verticalalign="Center" 
                                            visible="False">
                                            <serieslabels font="Verdana, 7pt" fontcolor="Gray" horizontalalign="Near" 
                                                orientation="Horizontal" verticalalign="Center">
                                                <layout behavior="Auto">
                                                </layout>
                                            </serieslabels>
                                            <layout behavior="Auto">
                                            </layout>
                                        </labels>
                                    </z2>
                                </axis>
                                <columnchart>                                    
                                    <charttext>
                                        <igchartprop:ChartTextAppearance charttextfont="Arial, 7pt" column="-2" 
                                            itemformatstring="&lt;DATA_VALUE:0&gt;" row="-2" visible="True" />
                                    </charttext>
                                </columnchart>
                                <legend location="Bottom" visible="True"></legend>
                            </igchart:UltraChart>
                            </td>
                            </tr>
                        </table>
                        </div>
                        </ContentTemplate>                        
                    </igtab:Tab>
                </Tabs>
                <SelectedTabStyle CssClass="Reporting_SelectedTab" BorderStyle="None">
                </SelectedTabStyle>                
                <DefaultTabStyle CssClass="Reporting_UnSelectedTab" BorderStyle="None"></DefaultTabStyle>
           </igtab:UltraWebTab>
        </td>
    </tr>
</table>
</td>
</tr>
</table>
<igcalc:UltraWebCalcManager 
                        ID="UltraWebCalcManager1" runat="server">
                    </igcalc:UltraWebCalcManager>
</asp:Content>