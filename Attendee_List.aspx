<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Attendee_List.aspx.cs" Inherits="Eventomatic.Attendee_List" %>


<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
<script language="javascript">
    function doGotTicket(txkey){
    
		__doPostBack('doGotTicket',txkey);
	 
    }
    function doGotTicketRemove(txkey){
    
		__doPostBack('doGotTicketRemove',txkey);
	 
    }
    </script>
    <igcalc:UltraWebCalcManager ID=webcalc runat=server></igcalc:UltraWebCalcManager>
    <table>
    <tr>
    <td >
            <table width="100%"> 
                <tr>
                    <td style="text-align:center; font-weight:bolder; font-size:x-large;">                    
                            <asp:LinkButton  ID="btnExcel" runat="server" Text="Export to Excel"
                                        CssClass="ExportExcelButton" onclick="btnExcel_Click"></asp:LinkButton>
                                    <asp:Label ID=lblEventName runat=server></asp:Label>
                     </td>
                </tr>                  
                <tr>
                    <td>
                    <asp:LinkButton  ID="btnRemoveDemo" runat="server" Text="Clear Demo Tix" Font-Size=Smaller
                                        CssClass="ExportExcelButton" onclick="btnRemoveDemo_Click" Visible=false></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td  style="float:left;">                                        
                                          
                        <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter1" 
                            runat="server" DownloadName="AttendeeList.xls" ExportMode=InBrowser>
                        </igtblexp:UltraWebGridExcelExporter>
                    </td>
                </tr>
                <tr>
                    <td align=center>
                    <table width=100%>
                        <tr>                        
                        <td align=center>
                        Tickets Sold: <asp:Label ID=lbltotalsold runat=server>0</asp:Label>
                        </td>
                        <td align=center>
                        Revenue: <asp:Label ID=lblyouget runat=server>0</asp:Label>
                        </td>
                        <td align=center><asp:Panel ID=pnlSellers runat=server Visible=false>
                        Filter Sellers<asp:DropDownList ID=ddlSellers runat=server Width=80px></asp:DropDownList>
                        </asp:Panel>
                        </td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                <td>
<igtab:UltraWebTab ID="UltraWebTab1" runat="server" 
Width=100% ontabclick="UltraWebTab1_TabClick" AutoPostBack=true>
                <Tabs>
                    <igtab:Tab Text="Guest List" >
                        <ContentTemplate>
                        
                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server">
                            <Bands>
<igtbl:UltraGridBand>
    <Columns>
        <igtbl:UltraGridColumn BaseColumnName="GuestList_Full_Name">
        <CellStyle Wrap="True">
            </CellStyle>
            <Header Caption="Buyer">
            </Header>            
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn >
            <Header Caption="Seller">
                <RowLayoutColumnInfo OriginX="1" />
            </Header>
            <CellStyle TextOverflow=Ellipsis Wrap=true>
            </CellStyle>
            <Footer>
                <RowLayoutColumnInfo OriginX="1" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Email" Width=100px Hidden=true>
            <Header Caption="Email">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
            </Header>
<CellStyle Wrap="True">
            </CellStyle>
<Footer>
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Ticket_Description" Width="150px" >
            <Header Caption="Ticket Type">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <CellStyle Wrap="True">
            </CellStyle>
            <Footer>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Quantity" DataType="System.Int32" Width="55px">
            <Header Caption="Quantity">
<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
            </Header>
<CellStyle Wrap="True">
            </CellStyle>
<Footer>
<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
</Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Confirmation_Date"  Width="75px"
            DataType="System.DateTime" Format="dd/MM/yyyy hh:mm tt">
            <CellStyle Wrap="True">
            </CellStyle>
            <Header Caption="Date">
<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
            </Header>

<Footer>
<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
</Footer>
        </igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Width="100px" BaseColumnName="TixNum">
            <Header Caption="Num">
                <RowLayoutColumnInfo OriginX="5" />
            </Header>
            <CellStyle Wrap="True">
            </CellStyle>
            <Footer>
                <RowLayoutColumnInfo OriginX="5" />
            </Footer>
        </igtbl:UltraGridColumn>
    
        <igtbl:UltraGridColumn Width="75px" Hidden=true>
            <Header Caption="Physical Tix">
                <RowLayoutColumnInfo OriginX="6" />
            </Header>
            <CellStyle Wrap="True">
            </CellStyle>
            <Footer>
                <RowLayoutColumnInfo OriginX="6" />
            </Footer>
        </igtbl:UltraGridColumn>
     
        <igtbl:UltraGridColumn Width="75px">
            <Header Caption="Transaction">
                <RowLayoutColumnInfo OriginX="7" />
            </Header>
            <CellStyle Wrap="True">
            </CellStyle>
            <Footer>
                <RowLayoutColumnInfo OriginX="7" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Tx_Key" DataType="System.Int32" 
            Hidden="True">
            <Header Caption="Tx_Key">
                <RowLayoutColumnInfo OriginX="8" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="8" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Got_Tickets" DataType="System.Int32" 
            Hidden="True">
            <Header Caption="Got_Tickets">
                <RowLayoutColumnInfo OriginX="9" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="9" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Got_Tickets_Name"  
            Hidden="True">
            <Header Caption="Got_Tickets_Name">
                <RowLayoutColumnInfo OriginX="10" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="10" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="fbid_Seller" DataType="System.Int64" 
            Hidden="True">
            <Header Caption="fbid_Seller">
                <RowLayoutColumnInfo OriginX="11" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="11" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="fbid_Buyer" DataType="System.Int64" 
            Hidden="True">
            <Header Caption="fbid_Buyer">
                <RowLayoutColumnInfo OriginX="12" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="12" />
            </Footer>
        </igtbl:UltraGridColumn>
<igtbl:UltraGridColumn BaseColumnName="fbid_Seller_Name" Hidden="True">
            <Header Caption="fbid_Seller">
                <RowLayoutColumnInfo OriginX="13" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="13" />
            </Footer>
        </igtbl:UltraGridColumn>        
    </Columns>
<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>

<igtbl:UltraGridBand>
    <Columns>
        <igtbl:UltraGridColumn BaseColumnName="Question_Answered_Key" Hidden="True">
            <Header Caption="Question_Answered_Key">
            </Header>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="The_Answer">
            <Header Caption="Answers">
                <RowLayoutColumnInfo OriginX="1" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="1" />
            </Footer>
        </igtbl:UltraGridColumn>        
        </Columns>

<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
        </igtbl:UltraGridBand>
</Bands>
<DisplayLayout Name="UltraWebGrid1" AutoGenerateColumns="False" RowSelectorsDefault="No" 
                                StationaryMarginsOutlookGroupBy="True" Version="4.00" 
                                AllowSortingDefault="Yes" ViewType="Hierarchical" TableLayout="Fixed" 
                                AllowUpdateDefault="Yes" HeaderClickActionDefault="SortSingle" 
                                SelectTypeCellDefault="Single" SelectTypeRowDefault="Single">
    <FrameStyle BorderColor="#999999" BorderStyle="None" BorderWidth="3px" 
        Cursor="Default">
    </FrameStyle>
    <RowAlternateStyleDefault BackColor="#F0D8FF">
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
            <br />
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
                        </ContentTemplate>
                </igtab:Tab>
                <igtab:Tab Text="Transactions" >
                        <ContentTemplate>
<!--*************************************************************************************-->                        
                        <igtbl:UltraWebGrid ID="UltraWebGrid3" runat="server">
                            <Bands>
<igtbl:UltraGridBand>
    <Columns>
        <igtbl:UltraGridColumn BaseColumnName="GuestList_Full_Name">
        <CellStyle Wrap=true>
            </CellStyle>
            <FooterStyle BackColor="White" BorderColor=White/>
            <Header Caption="Buyer Name">
            </Header>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn >
            <Header Caption="Seller Name">
                <RowLayoutColumnInfo OriginX="1" />
            </Header>
            <FooterStyle BackColor="White" BorderColor=White/>
            <CellStyle Wrap=true>
            </CellStyle>
            <Footer>
                <RowLayoutColumnInfo OriginX="1" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Confirmation_Date"  Width="75px"
            DataType="System.DateTime" Format="dd/MM/yyyy hh:mm tt">
            <CellStyle Wrap="True">
            </CellStyle>
            <FooterStyle BackColor="White" BorderColor=White/>
            <Header Caption="Date">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
            </Header>

<Footer>
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Amount" Width=100px hidden=true
            DataType="System.Decimal" Format="$ ###,###,##0.00" Key="Paid">
            <Header Caption="Paid">
<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
            </Header>
<CellStyle Wrap="True">
            </CellStyle>
            <FooterStyle BackColor="White" BorderColor=White/>
<Footer>
<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
</Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Service_Fee" DataType="System.Decimal" Format="$ ###,###,##0.00" Key="Groupstore_Fee" Hidden=true>
            <Header Caption="Groupstore Fee">
                <RowLayoutColumnInfo OriginX="4" />
            </Header>
            <FooterStyle BackColor="White" BorderColor=White/>
            <CellStyle Wrap="True">
            </CellStyle>
            <Footer>
                <RowLayoutColumnInfo OriginX="4" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn  DataType="System.Decimal"  Key="Ticket_Price"
            Format="$ ###,###,##0.00"  BaseColumnName="Price">
            <Header Caption="Ticket Price">
<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
            </Header>
<CellStyle Wrap="True">
            </CellStyle>
            <FooterStyle BackColor="White" BorderColor=White/>
<Footer>
<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
</Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn DataType="System.Decimal"  Key="Paypal_Fee"
            Format="$ ###,###,##0.00"
             BaseColumnName="Paypal_Fee">
            <Header Caption="Paypal Fee">
<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
            </Header>
<CellStyle Wrap="True">
            </CellStyle>
            <FooterStyle BackColor="White" BorderColor=White/>
<Footer>
<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
</Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn DataType="System.Decimal" Format="$ ###,###,##0.00" 
             BaseColumnName="Revenue">
            <Header Caption="Revenue">
<RowLayoutColumnInfo OriginX="7"></RowLayoutColumnInfo>
            </Header>
<FooterStyle BackColor="White" BorderColor=White/>
<Footer Total=Sum Caption="Total = ">
<RowLayoutColumnInfo OriginX="7"></RowLayoutColumnInfo>
</Footer>
        </igtbl:UltraGridColumn>
       
        <igtbl:UltraGridColumn BaseColumnName="Tx_Key" DataType="System.Int32" 
            Hidden="True">
            <Header Caption="Tx_Key">
                <RowLayoutColumnInfo OriginX="8" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="8" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Currency" DataType="System.Decimal" Format="$ ###,###,##0.00" >
            <Header Caption="Currency">
                <RowLayoutColumnInfo OriginX="9" />
            </Header>
            <FooterStyle BackColor="White" BorderColor=White/>
            <Footer>
                <RowLayoutColumnInfo OriginX="9" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="fbid_Seller" DataType="System.Int64" 
            Hidden="True">
            <Header Caption="fbid_Seller">
                <RowLayoutColumnInfo OriginX="10" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="10" />
            </Footer>
        </igtbl:UltraGridColumn>        
<igtbl:UltraGridColumn BaseColumnName="fbid_Seller_Name" Hidden="True">
            <Header Caption="fbid_Seller">
                <RowLayoutColumnInfo OriginX="11" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="11" />
            </Footer>
        </igtbl:UltraGridColumn>
    </Columns>
<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
</Bands>
<DisplayLayout Name="UltraWebGrid3" AutoGenerateColumns="False" RowSelectorsDefault="No" 
                                StationaryMarginsOutlookGroupBy="True" Version="4.00" 
                                AllowSortingDefault="Yes" ViewType=Flat TableLayout="Fixed" 
                                AllowUpdateDefault="Yes" HeaderClickActionDefault="SortSingle" 
                                SelectTypeCellDefault="Single" SelectTypeRowDefault="Single"
                                ColFootersVisibleDefault="Yes">
    <FrameStyle BorderColor="#999999" BorderStyle="None" BorderWidth="3px" 
        Cursor="Default">
    </FrameStyle>
    <RowAlternateStyleDefault BackColor="#F0D8FF">
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
            <br />
        <igtbl:UltraWebGrid ID="UltraWebGrid4" runat="server" Height="200px" Width="325px" Visible=false>
                         
<DisplayLayout Name="UltraWebGrid4" AutoGenerateColumns="True" AllowColSizingDefault="Free" 
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
                        </ContentTemplate>
                </igtab:Tab>        
                </Tabs>
                <SelectedTabStyle CssClass="AttendeeList_SelectedTab" BorderStyle="None">
                </SelectedTabStyle>                
                <DefaultTabStyle CssClass="AttendeeList_UnSelectedTab" BorderStyle="None"></DefaultTabStyle>
                </igtab:UltraWebTab>
        </td>
    </tr>
</table>
</td>
</tr>
</table>

</asp:Content>