<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FBEventsImport.ascx.cs" Inherits="Eventomatic.Addons.FBEventsImport" %>
<link href="Eventomatic.css" media="screen" rel="stylesheet" type="text/css" /> 


<asp:HiddenField ID=hdnfbid runat=server Value=0 />
<asp:LinkButton  ID="btnExcel" runat="server" Text="Import Fb Event"
                                        CssClass="ExportExcelButton" onclick="btnExcel_Click">
                                      </asp:LinkButton>                                                                     
<div id="ImportFB" class="QuestionPopup">

</div>
<div id="ImportFB2" class="QuestionPopup2">
<center>
<table>
    <tr>
        <td class="QuestionPopupHeader"><table width=100%>
            <tr>
                <td align=left>Background Image</td>
                <td align=right><a href="#" onclick="closepopup3()"><img src="/images/QuestionsBoxClose.jpg" style="border:0;" /></a></td>
                
            </tr>
        </table></td>        
    </tr>
    <tr>
        <td colspan="2">
        <asp:Label ID="lblNoImport" runat=server Visible=false>There are currently no Facebook Events that you are administrating.</asp:Label>
        <asp:Panel ID=pnlImport runat=server >
        Below are Facebook Events that you are an administrator for.  Click "Sell Event" to begin selling the Facebook Event.    
            <br />    
    <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server">
        <Bands>
            <igtbl:UltraGridBand>
                <Columns>
                    <igtbl:UltraGridColumn BaseColumnName="creator" Hidden="True">
                        <Header Caption="Creator">
                        </Header>
                    </igtbl:UltraGridColumn>
                    <igtbl:UltraGridColumn BaseColumnName="name" Key="EventID">
                        <Header Caption="Event">
                            <RowLayoutColumnInfo OriginX="1" />
                        </Header>
                        <Footer>
                            <RowLayoutColumnInfo OriginX="1" />
                        </Footer>
                    </igtbl:UltraGridColumn>
                    <igtbl:UltraGridColumn BaseColumnName="start_time">
                        <Header Caption="Start Time">
                            <RowLayoutColumnInfo OriginX="2" />
                        </Header>
                        <Footer>
                            <RowLayoutColumnInfo OriginX="2" />
                        </Footer>
                    </igtbl:UltraGridColumn>
                    <igtbl:UltraGridColumn>
                        <Header>
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
        <DisplayLayout AutoGenerateColumns="False" Name="UltraWebGrid1" 
            RowSelectorsDefault="No" StationaryMarginsOutlookGroupBy="True" Version="4.00">
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
            <ActivationObject BorderColor="Black" BorderStyle="Dotted" BorderWidth="">
            </ActivationObject>
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
    </asp:Panel>
    </td>
    </tr>
    <tr>
        <td class="QuestionPopupFooter"></td>
    </tr>
</table>
</center>
<script language=javascript>
    /*function popup3(){
        
        CheckCanvas(false);
         var back = document.getElementById("ImportFB");
         back.style.display='block';
          var topop = document.getElementById("ImportFB2");
          topop.style.display='block';                              
          }
   function closepopup3(){
        var back = document.getElementById("ImportFB");
         back.style.display='none';
          var topop = document.getElementById("ImportFB2");
          topop.style.display='none';
      }*/
</script>
</div>