<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Service_Fees.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Eventomatic.Admin.Service_Fees" %>


<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
<table>
    <tr>
        <td>
                    
            <igtab:UltraWebTab ID="UltraWebTab1" runat="server" BorderColor="#949878" 
                BorderStyle="Solid" BorderWidth="1px" ThreeDEffect="False" 
                AutoPostBack=true OnTabClick=WebTab_Click >
                <Tabs>
                <igtab:Tab Text="Groups"><ContentTemplate>
<igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server">
                <bands>
                    <igtbl:UltraGridBand>
                        <Columns>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Resource_Key" Width=50px>
                                <header caption="Key">
                                </header>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Group_Name" Width=75px>
                                <CellStyle Wrap="True">
                                </CellStyle>
                                <header caption="Group Name">
                                    <rowlayoutcolumninfo originx="1" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="1" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Current_Events">
                                <header caption="Current Events">
                                    <rowlayoutcolumninfo originx="2" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="2" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Past_Events">
                                <header caption="Past Events">
                                    <rowlayoutcolumninfo originx="3" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="3" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="Yes" 
                                BaseColumnName="Service_Fee_Percentage" DataType="System.Decimal" 
                                Format=" ###,###,##0.00">
                                <header caption="Service Fee %">
                                    <rowlayoutcolumninfo originx="4" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="4" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Service_Fee_Cents" 
                                DataType="System.Decimal" Format="$ ###,###,##0.00">
                                <header caption="Service Fee $">
                                    <rowlayoutcolumninfo originx="5" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="5" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Service_Fee_Max" 
                                DataType="System.Decimal" Format="$ ###,###,##0.00">
                                <header caption="Service Fee Max">
                                    <rowlayoutcolumninfo originx="6" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="6" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Creator">
                                <header caption="Creator">
                                    <rowlayoutcolumninfo originx="7" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="7" />
                                </footer>
                            </igtbl:UltraGridColumn>
                        </Columns>
                        <addnewrow view="NotSet" visible="NotSet">
                        </addnewrow>
                    </igtbl:UltraGridBand>
                </bands>
                <displaylayout autogeneratecolumns="False" cellclickactiondefault="Edit" 
                    name="UltraWebGrid1" selecttyperowdefault="Single" 
                    stationarymarginsoutlookgroupby="True" version="4.00">
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
                        <padding left="3px" />
                        <borderdetails colorleft="199, 213, 232" colortop="199, 213, 232" />
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
                
                </ContentTemplate>
                </igtab:Tab>
                <igtab:Tab Text="Events">
    <ContentTemplate>
    
    <igtbl:UltraWebGrid ID="UltraWebGrid2" runat="server">
                <bands>
                    <igtbl:UltraGridBand>
                        <Columns>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Event_Key" Width=50px>
                                <header caption="Key">
                                </header>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Event_Name" Width=100px>
                            <CellStyle Wrap="True">
                                </CellStyle>
                                <header caption="Event Name">
                                    <rowlayoutcolumninfo originx="1" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="1" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Group_Name" Width=75px>
                            <CellStyle Wrap="True">
                                </CellStyle>
                                <header caption="Group Name">
                                    <rowlayoutcolumninfo originx="2" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="2" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Current_Previous">
                                <header caption="Status">
                                    <rowlayoutcolumninfo originx="3" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="3" />
                                </footer>
                            </igtbl:UltraGridColumn>                            
                            <igtbl:UltraGridColumn AllowUpdate="Yes" 
                                BaseColumnName="Service_Fee_Percentage" DataType="System.Decimal" 
                                Format=" ###,###,##0.00">
                                <header caption="Service Fee %">
                                    <rowlayoutcolumninfo originx="4" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="4" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Service_Fee_Cents" 
                                DataType="System.Decimal" Format="$ ###,###,##0.00">
                                <header caption="Service Fee $">
                                    <rowlayoutcolumninfo originx="5" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="5" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Service_Fee_Max" 
                                DataType="System.Decimal" Format="$ ###,###,##0.00">
                                <header caption="Service Fee Max">
                                    <rowlayoutcolumninfo originx="6" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="6" />
                                </footer>
                            </igtbl:UltraGridColumn>
                        </Columns>
                        <addnewrow view="NotSet" visible="NotSet">
                        </addnewrow>
                    </igtbl:UltraGridBand>
                </bands>
                <displaylayout autogeneratecolumns="False" cellclickactiondefault="Edit" 
                    name="UltraWebGrid1" selecttyperowdefault="Single" 
                    stationarymarginsoutlookgroupby="True" version="4.00">
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
                        <padding left="3px" />
                        <borderdetails colorleft="199, 213, 232" colortop="199, 213, 232" />
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
    </ContentTemplate>
    </igtab:Tab>
                
                <igtab:Tab Text="Users">
    <ContentTemplate>
    
    <igtbl:UltraWebGrid ID="UltraWebGrid3" runat="server">
                <bands>
                    <igtbl:UltraGridBand>
                        <Columns>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="FBid">
                                <header caption="FBid">
                                </header>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Full_Name">
                                <header caption="User Name">
                                    <rowlayoutcolumninfo originx="1" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="1" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Signed_Up" 
                                DataType="System.DateTime" Format="dd/MM/yyyy">
                                <header caption="Signed Up">
                                    <rowlayoutcolumninfo originx="2" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="2" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Last_Change" 
                                DataType="System.DateTime" Format="dd/MM/yyyy">
                                <header caption="Last Change">
                                    <rowlayoutcolumninfo originx="3" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="3" />
                                </footer>
                            </igtbl:UltraGridColumn>                            
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Admin">
                                <header caption="Admin">
                                    <rowlayoutcolumninfo originx="4" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="4" />
                                </footer>
                            </igtbl:UltraGridColumn>  
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Groups">
                                <header caption="Amount Groups">
                                    <rowlayoutcolumninfo originx="5" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="5" />
                                </footer>
                            </igtbl:UltraGridColumn>  
                        </Columns>
                        <addnewrow view="NotSet" visible="NotSet">
                        </addnewrow>
                    </igtbl:UltraGridBand>
                </bands>
                <displaylayout autogeneratecolumns="False" 
                    name="UltraWebGrid1" selecttyperowdefault="Single" 
                    stationarymarginsoutlookgroupby="True" version="4.00">
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
                        <padding left="3px" />
                        <borderdetails colorleft="199, 213, 232" colortop="199, 213, 232" />
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
    </ContentTemplate>
    </igtab:Tab>
    
    <igtab:Tab Text="Error Logs">
    <ContentTemplate>
    <asp:LinkButton  ID="btnExcel" runat="server" Text="Export to Excel"
                                        CssClass="ExportExcelButton" onclick="btnExcel_Click"></asp:LinkButton>
    <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter1" 
                            runat="server" DownloadName="ErrorList.xls" ExportMode=InBrowser>
                        </igtblexp:UltraWebGridExcelExporter><br />
    <igtbl:UltraWebGrid ID="UltraWebGrid4" runat="server">
                <bands>
                    <igtbl:UltraGridBand>
                        <Columns>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="EventTime">
                                <header caption="Time">
                                </header>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="RequestURL" 
                                Width="100px">
                                <CellStyle Wrap="True">
                                </CellStyle>
                                <header caption="Path">
                                    <RowLayoutColumnInfo OriginX="1" />
                                </header>
                                <Footer>
                                    <RowLayoutColumnInfo OriginX="1" />
                                </Footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Details" Width="400px">
                                <CellStyle Wrap="True">
                                </CellStyle>
                                <header caption="Details">
                                    <RowLayoutColumnInfo OriginX="2" />
                                </header>
                                <Footer>
                                    <RowLayoutColumnInfo OriginX="2" />
                                </Footer>
                            </igtbl:UltraGridColumn>
                            
                        </Columns>
                        <addnewrow view="NotSet" visible="NotSet">
                        </addnewrow>
                    </igtbl:UltraGridBand>
                </bands>
                <displaylayout autogeneratecolumns="False" 
                    name="UltraWebGrid1" selecttyperowdefault="Single" 
                    stationarymarginsoutlookgroupby="True" version="4.00" 
                    AllowSortingDefault="Yes" CellClickActionDefault="Edit" 
                    HeaderClickActionDefault="SortMulti" AllowUpdateDefault="Yes">
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
                        <padding left="3px" />
                        <borderdetails colorleft="199, 213, 232" colortop="199, 213, 232" />
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
    </ContentTemplate>
    </igtab:Tab>
    <igtab:Tab Text="Restore">
    <ContentTemplate>
    
    <igtbl:UltraWebGrid ID="UltraWebGrid5" runat="server">
                <bands>
                    <igtbl:UltraGridBand>
                        <Columns>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Event_Key">
                                <header caption="Event Key">
                                </header>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Event_Name">
                                <header caption="Event Name">
                                    <rowlayoutcolumninfo originx="1" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="1" />
                                </footer>
                            </igtbl:UltraGridColumn>
                            
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Group_Name">
                                <header caption="Group Name">
                                    <rowlayoutcolumninfo originx="2" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="2" />
                                </footer>
                            </igtbl:UltraGridColumn>       
                            <igtbl:UltraGridColumn AllowUpdate="No">
            <Header Caption="Action" >
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="3" />
            </Footer>
        </igtbl:UltraGridColumn>                     
                        </Columns>
                        <addnewrow view="NotSet" visible="NotSet">
                        </addnewrow>
                    </igtbl:UltraGridBand>
                </bands>
                <displaylayout autogeneratecolumns="False" 
                    name="UltraWebGrid1" selecttyperowdefault="Single" 
                    stationarymarginsoutlookgroupby="True" version="4.00" 
                    AllowSortingDefault="Yes" CellClickActionDefault="Edit" 
                    HeaderClickActionDefault="SortMulti" AllowUpdateDefault="Yes">
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
                        <padding left="3px" />
                        <borderdetails colorleft="199, 213, 232" colortop="199, 213, 232" />
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
    </ContentTemplate>
    </igtab:Tab>
              </Tabs>  
                <RoundedImage FillStyle="LeftMergedWithCenter" HoverImage="[ig_tab_winXP2.gif]" 
                    LeftSideWidth="7" NormalImage="[ig_tab_winXP3.gif]" RightSideWidth="6" 
                    SelectedImage="[ig_tab_winXP1.gif]" ShiftOfImages="2" />
                <SelectedTabStyle>
                    <Padding Bottom="2px" />
                </SelectedTabStyle>
                <DefaultTabStyle BackColor="#FEFCFD" Font-Names="Microsoft Sans Serif" 
                    Font-Size="8pt" ForeColor="Black" Height="22px">
                    <Padding Top="2px" />
                </DefaultTabStyle>
            </igtab:UltraWebTab>
                    
            
                    
        </td>
    </tr>
    <tr>
        <td align=center><asp:Button runat=server ID=btnUpdate Text="Save Changes" 
                onclick="btnUpdate_Click" /></td>
    </tr>
    <tr>
        <td align=center><asp:Label ID=lblresult runat=server visible=false></asp:Label></td>
    </tr>
</table>
<script language="javascript">
    function doRestore(eventid){
    var answer = confirm("Are you sure you want to Restore Event to Store?")
	if (answer){
		__doPostBack('DoRestore',eventid);
	}
	else{
	//
	}    
    }
    </script>
</asp:Content>