<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payout.aspx.cs" Inherits="Eventomatic.Admin.Payout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Payout</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server">
                <bands>
                    <igtbl:UltraGridBand>
                        <Columns>
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Resource_Key" Width=50px>
                                <header caption="Key">
                                </header>
                            </igtbl:UltraGridColumn>                            
                            
                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Group_Name" Width=200px>
                            <CellStyle Wrap="True">
                                </CellStyle>
                                <header caption="Group Name">
                                    <rowlayoutcolumninfo originx="1" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="1" />
                                </footer>
                            </igtbl:UltraGridColumn>                            
                            <igtbl:UltraGridColumn AllowUpdate="Yes" 
                                BaseColumnName="Amount_Owing" >
                                <header caption="Amount Owed">
                                    <rowlayoutcolumninfo originx="2" />
                                </header>
                                <footer>
                                    <rowlayoutcolumninfo originx="2" />
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
        <igtbl:UltraGridColumn BaseColumnName="Event_Name" Width="250px" >
            <FooterStyle BackColor="White" BorderColor=White/>
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
        <igtbl:UltraGridColumn BaseColumnName="Event_Ends" Width=80px>
            <FooterStyle BackColor="White"  BorderColor=White/>
            <Header Caption="Event Ends">
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="3" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Revenue" Key="Revenue"
        Width=100px>
            <CellStyle HorizontalAlign=Right Wrap=true></CellStyle>
            <Header Caption="Amount Collected">
                <RowLayoutColumnInfo OriginX="4" />
            </Header>
            <FooterStyle Wrap=true />
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Paid_Out" Key="Paid_Out"
        width=100px>
        <CellStyle Wrap=true HorizontalAlign=Right></CellStyle>
            <Header Caption="Amount Paid to you">
                <RowLayoutColumnInfo OriginX="5" />
            </Header>
            <FooterStyle Wrap=true />            
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn width=100px BaseColumnName="Amount_Owing">
            <CellStyle Wrap=true HorizontalAlign=Right></CellStyle>
            <Header Caption="Amount Owing">
                <RowLayoutColumnInfo OriginX="6" />
            </Header>
            <FooterStyle Wrap=true />            
        </igtbl:UltraGridColumn>
    </Columns>
<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
                </bands>
                <DisplayLayout Name="UltraWebGrid2" AllowColSizingDefault="Free" 
                AutoGenerateColumns="False" RowSelectorsDefault="No" 
                StationaryMarginsOutlookGroupBy="True" Version="4.00" ViewType="Hierarchical">

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
    
    </div>
    </form>
</body>
</html>
