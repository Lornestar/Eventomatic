<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tickets.ascx.cs" Inherits="Eventomatic.Addons.Tickets" %>

    
<link href="Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />  
<asp:HiddenField ID=Event_Key runat=server Value=0 />
<asp:HiddenField ID=Resource_Key runat=server Value=0 />
<asp:HiddenField ID=Currentfbuser runat=server Value=0 />

<table>    
    <tr>
        <td>
      
        
        
       
            <igtbl:UltraWebGrid ID="UltraWebGridT1" runat="server">
                <bands>
                    <igtbl:UltraGridBand>
                        <addnewrow view="NotSet" visible="NotSet">
                        </addnewrow>
                        <Columns>                               
                            <igtbl:UltraGridColumn BaseColumnName="Ticket_Key" DataType="System.Int32" 
                                Hidden="True">
                                <Header Caption="Ticket_Key">
                                    <RowLayoutColumnInfo OriginX="1" />
                                </Header>
                                <Footer>
                                    <RowLayoutColumnInfo OriginX="1" />
                                </Footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="Ticket_Description" Width="150px">
                                <Header Caption="Description">
                                    <RowLayoutColumnInfo OriginX="2" />
                                </Header>
                                <Footer>
                                    <RowLayoutColumnInfo OriginX="2" />
                                </Footer>
                                <CellStyle Wrap=true></CellStyle>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="Sale_Begins" DataType="System.DateTime" 
                                Format="MMM d yyyy<br/>h:mmtt" Width="100px">
                                <Header Caption="Sale Begins">
                                    <RowLayoutColumnInfo OriginX="3" />
                                </Header>
                                <Footer>
                                    <RowLayoutColumnInfo OriginX="3" />
                                </Footer>
                                <CellStyle Wrap=true></CellStyle>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="Sale_Ends" DataType="System.DateTime" 
                                Format="MMM d yyyy h:mmtt"  Width="100px">
                                <Header Caption="Sale Ends">
                                    <RowLayoutColumnInfo OriginX="4" />
                                </Header>
                                <Footer>
                                    <RowLayoutColumnInfo OriginX="4" />
                                </Footer>
                                <CellStyle Wrap=true></CellStyle>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="PriceRounded" DataType="System.Decimal"  Width="60px"
                                Format="$ ###,###,##0.00" >
                                <Header Caption="Price">
                                    <RowLayoutColumnInfo OriginX="5" />
                                </Header>
                                <Footer>
                                    <RowLayoutColumnInfo OriginX="5" />
                                </Footer>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="Capacity"  Width=60px>
                                <Header Caption="Capacity">
                                    <RowLayoutColumnInfo OriginX="6" />
                                </Header>
                                <Footer>
                                    <RowLayoutColumnInfo OriginX="6" />
                                </Footer>
                            </igtbl:UltraGridColumn>           
                                    
                           <igtbl:UltraGridColumn Width=85px> 
                                                      
<Header>
<RowLayoutColumnInfo OriginX="7"></RowLayoutColumnInfo>
</Header>

<Footer>
<RowLayoutColumnInfo OriginX="7"></RowLayoutColumnInfo>
</Footer>
                                                      
                           </igtbl:UltraGridColumn>      
                     <igtbl:UltraGridColumn Hidden=true> 
                                                      
                        <Header>
                        <RowLayoutColumnInfo OriginX="8"></RowLayoutColumnInfo>
                        </Header>

                        <Footer>
                        <RowLayoutColumnInfo OriginX="8"></RowLayoutColumnInfo>
                        </Footer>
                                                      
                           </igtbl:UltraGridColumn>                              
                    <igtbl:UltraGridColumn Hidden=true> 
                                                      
                        <Header>
                        <RowLayoutColumnInfo OriginX="9"></RowLayoutColumnInfo>
                        </Header>

                        <Footer>
                        <RowLayoutColumnInfo OriginX="9"></RowLayoutColumnInfo>
                        </Footer>
                                                      
                           </igtbl:UltraGridColumn>      
                           <igtbl:UltraGridColumn Hidden=true BaseColumnName="isdonation"> 
                                                      
                        <Header>
                        <RowLayoutColumnInfo OriginX="10"></RowLayoutColumnInfo>
                        </Header>
                        <Footer>
                        <RowLayoutColumnInfo OriginX="10"></RowLayoutColumnInfo>
                        </Footer>                                                      
                           </igtbl:UltraGridColumn>      
                        </Columns>
                    </igtbl:UltraGridBand>                    
                </bands>
                <displaylayout allowaddnewdefault="Yes" 
                    bordercollapsedefault="Separate" colheadersvisibledefault="Yes" 
                    name="ctl00xUltraWebGridT1" rowheightdefault="20px" rowselectorsdefault="No" 
                    stationarymargins="Header" stationarymarginsoutlookgroupby="True" 
                    tablelayout="Fixed" version="4.00" autogeneratecolumns="False" CellClickActionDefault="RowSelect"                      
                    >
                    <framestyle backcolor="Window" bordercolor="InactiveCaption" 
                        borderwidth="0px" font-names="Microsoft Sans Serif" font-size="10.25pt" 
                         >
                    </framestyle>
                    <RowAlternateStyleDefault>
                        <Padding Top="0px" />
                    </RowAlternateStyleDefault>
                    <pager minimumpagesfordisplay="2">
                        <PagerStyle BackColor="LightGray" BorderWidth="1px">
                        <borderdetails colorleft="White" colortop="White" widthleft="1px" 
                            widthtop="1px" />
                        </PagerStyle>
                    </pager>
                    <editcellstyledefault borderwidth="0px">
                    </editcellstyledefault>
                    <footerstyledefault backcolor="LightGray" borderwidth="1px">
                        <borderdetails colorleft="White" colortop="White" widthleft="1px" 
                            widthtop="1px" />
                    </footerstyledefault>
                    <headerstyledefault backcolor="White" 
                        horizontalalign="Left" BorderColor="Black" BorderStyle="Solid" 
                        Font-Bold="True" Font-Underline="False">
                        <borderdetails widthleft="0px" 
                            widthtop="0px" ColorBottom="Black" StyleBottom="Solid" WidthBottom="1px" 
                            WidthRight="0px" />
                    </headerstyledefault>
                    <rowstyledefault backcolor="Window" bordercolor="Silver" borderwidth="1px" 
                        font-names="Arial" font-size="10pt" VerticalAlign="Top">
                        <borderdetails colorleft="Window" colortop="Window" />
                        <padding left="3px" Top="0px" Bottom="10px" />
                    </rowstyledefault>
                    <groupbyrowstyledefault backcolor="Control" bordercolor="Window">
                    </groupbyrowstyledefault>
                    <groupbybox hidden="True">
                        <boxstyle backcolor="ActiveBorder" bordercolor="Window">
                        </boxstyle>
                    </groupbybox>
                    <addnewbox>
                        <boxstyle backcolor="Window" bordercolor="InactiveCaption" borderstyle="none" 
                            borderwidth="1px">
                            <borderdetails colorleft="White" colortop="White" widthleft="1px" 
                                widthtop="1px" />
                        </boxstyle>
                    </addnewbox>
                    <activationobject bordercolor="" borderwidth="" BorderStyle=None>
                    </activationobject>
                    <filteroptionsdefault>
                        <filterdropdownstyle backcolor="White" bordercolor="Silver" 
                            borderwidth="1px" customrules="overflow:auto;" 
                            font-names="Verdana,Arial,Helvetica,sans-serif" font-size="11px" >
                            <padding left="2px" />
                        </filterdropdownstyle>
                        <filterhighlightrowstyle backcolor="#151C55" forecolor="White">
                        </filterhighlightrowstyle>
                        <filteroperanddropdownstyle backcolor="White" bordercolor="Silver" 
                            borderstyle="None" borderwidth="1px" customrules="overflow:auto;" 
                            font-names="Verdana,Arial,Helvetica,sans-serif" font-size="11px">
                            <padding left="2px" />
                        </filteroperanddropdownstyle>
                    </filteroptionsdefault>
                </displaylayout>
            </igtbl:UltraWebGrid>
        
        
        
        
        </td>
    </tr>
    <tr>
        <td><a href=#>        
        <asp:Label ID=lblAddNew runat=server Text="Add Ticket"  
        CssClass="QuestionsButton" OnClick="javascript:popup3(0,0,0,0);"></asp:Label>  </a>
    </td>        
    </tr>
</table>


<div id="AddT" class="QuestionPopup">

</div>
<div id="AddT2" class="QuestionPopup2">
<table width=100%>
    <tr>
        <td class="QuestionPopupHeader"><table width=100%>
            <tr>
                <td align=left>Add Ticket</td>
                <td align=right><a href="#" onclick="closepopup3()"><img src="/images/QuestionsBoxClose.jpg" style="border:0;" /></a></td>
            </tr>
        </table></td>        
    </tr>
    <tr>
        <td>
            <table width=100% cellpadding=5 cellspacing=10>
            <tr>
                    <td class="QuestionSections">                        
                                What do you want to call these tickets?<br />
                        <asp:TextBox ID=txtdescription TextMode=SingleLine runat=server Width="300px" onfocus="TextboxwithSuggestionFocus2(this)" CssClass="TextboxwithSuggestion" ></asp:TextBox>                        
                        <input type=hidden id=hdDescription value=0 />
                    </td>
                </tr>
                <tr>
                    <td class="QuestionSections">
                       How much do these tickets cost?<br />                       
                        <igtxt:WebCurrencyEdit ID="WebCurrencyEdit1" runat="server" DataMode="Decimal" 
                            HorizontalAlign="Left" Width="100px" ValueText="20" >
                            <ClientSideEvents ValueChange="PriceValueChanged" />
                        </igtxt:WebCurrencyEdit>                         
                        <br />
                        Donation Box?<asp:CheckBox runat=server ID=chkdonation onClick="DonationCheck()"/><br />
                        <span style="font-size:smaller;">(Check donation box if you want the buyer to type in the price)</span>
                    </td>
                </tr>
                <tr>
                    <td class="QuestionSections">
                       What is the maximum amount of tickets available?<br />
                                               
                        <igtxt:WebMaskEdit ID="WebMaskEdit1" runat="server" InputMask="####" 
                            Width="100px" RealText="100">
                        </igtxt:WebMaskEdit>
                        <input type=hidden id=hdCapacity value=0 />                                               
                    </td>
                </tr>
                <tr>
                    <td class="QuestionSections">During what period of time do you want to sell these tickets?
                        <br />    
                        <br />                    
                                <table>
                                    <tr>
                                        <td>Begin Selling</td>
                                        <td><igsch:WebDateChooser ID="Begin_Selling" runat="server" Value="" Width="200px">
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
                                        <td><igtxt:WebDateTimeEdit ID="Begin_Selling_Time" runat="server" EditModeFormat="t" 
                                    DisplayModeFormat="t" Fields="2009-5-1-20-0-0-0" Nullable="False" Width="100">
                                </igtxt:WebDateTimeEdit></td>
                                    </tr>
                                </table>                              
                         <table>
                                <tr>
                                    <td>Finish Sell</td>
                                    <td><igsch:WebDateChooser ID="Selling_Deadline" runat="server" Value="" Width="200px">
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
                                    <td><igtxt:WebDateTimeEdit ID="Selling_Deadline_Time" runat="server" EditModeFormat="t" 
                                    DisplayModeFormat="t" Fields="2009-5-1-1-0-0-0" Nullable="False" Width="100">
                                </igtxt:WebDateTimeEdit></td>
                                </tr>
                            </table>                   
                    <input type=hidden id=hdBeginSell value=0 />
                    <input type=hidden id=hdFinishSell value=0 />
                    </td>
                </tr>
                <tr>
                    <td class="QuestionSections">
                    
                    <table>
                        <tr>
                            <td colspan=3>Which friends do you want to sell these tickets?</td>
                        </tr>
                        <tr valign="middle">
                        <td align=right>
                            Your Facebook Friends List<br />
                            <asp:ListBox ID="lbFriendsList_Temp" runat="server" style="display:none;"></asp:ListBox>
                            <asp:ListBox ID="lbFriendsList" runat="server" Rows=10 Width=200></asp:ListBox>
                        </td>
                        <td align=center>                                                    
                            <input onclick="Javascript:MoveItem('ctl00_body_UltraWebTab1__ctl1_Tickets1_lbFriendsList', 'ctl00_body_UltraWebTab1__ctl1_Tickets1_lbAdmins',true);" type="button" value=">" />
                            <br />
                             <input onclick="Javascript:MoveItem('ctl00_body_UltraWebTab1__ctl1_Tickets1_lbAdmins', 'ctl00_body_UltraWebTab1__ctl1_Tickets1_lbFriendsList',false);" type="button" value="<" />
                                
                            <!--    <asp:Button ID=btnAdd runat=server onclick="btnAdd_Click" Text=">" />
                                <br />
                                <asp:Button ID=btnRemove runat=server onclick="btnRemove_Click" Text="<" />                    
                                -->
                        </td>
                        <td align=left>Ticket Sellers                   
                            <asp:Label ID=lblGroupName runat=server></asp:Label><br />
                            <asp:ListBox ID="lbAdmins" runat="server" Rows=10 Width=200>                            
                            </asp:ListBox>
                        </td>            
                    </tr>
                    </table>
                    <asp:HiddenField ID=hdsellers runat=server Value="" />
                    <asp:HiddenField ID=hdsellersnames runat=server Value="" />
                    </td>
                </tr>
                <tr>
                    <td><asp:HiddenField ID=hdnTicket_Key runat=server Value=0 />
                    <asp:HiddenField ID=hdnPrice runat=server Value=0 />
                    </td>
                </tr>
                
            </table>
        
        </td>
    </tr>
    <tr>
        <td class="QuestionPopupFooter"><asp:Button ID=btnSaveTicket runat=server 
                Text="Update Ticket"  OnClick="btnSaveTicket_Click"  /></td>
    </tr>
</table>   


</div>
<script type="text/javascript">
function popup3(GridRow){
        document.getElementById('ctl00_hdCanvasSize').value = "950";
        CheckCanvas(false);

         var back = document.getElementById("AddT");
         back.style.display='block';
          var topop = document.getElementById("AddT2");
          topop.style.display='block';
          if (GridRow == 0)//A new one
		  {
		    document.getElementById("ctl00_body_UltraWebTab1__ctl1_Tickets1_txtdescription").value = 'Early Bird Tickets';		    
		    //document.getElementById("igtxtctl00_body_UltraWebTab1__ctl1_Tickets1_WebCurrencyEdit1").value = '20';
		    //document.getElementById("igtxtctl00_body_UltraWebTab1__ctl1_Tickets1_WebMaskEdit1").value = '100';
		    var d = new Date();
		    d.setDate(d.getDate()-1);
		    var dat = igdrp_getComboById('ctl00_body_UltraWebTab1__ctl1_Tickets1_Begin_Selling');		    
		    dat.setValue(d);		    
		    
		    var dat2 = igdrp_getComboById('ctl00_body_UltraWebTab1__ctl1_Tickets1_Selling_Deadline');
		    var dat3 = igdrp_getComboById('ctl00_body_UltraWebTab1__ctl0_EndDate');
		    dat2.setValue(dat3.getValue());		    
		    document.getElementById("ctl00_body_UltraWebTab1__ctl1_Tickets1_hdnTicket_Key").value = '0';
		    document.getElementById("hdDescription").value = '0';
		    
		    ClearSellersList();
		    setDonationbox(-1);
		  }
		  else{//An existing question
		    document.getElementById("ctl00_body_UltraWebTab1__ctl1_Tickets1_hdnPrice").value = 0;
		  
		    GridRow = GridRow-1;
		    document.getElementById("ctl00_body_UltraWebTab1__ctl1_Tickets1_txtdescription").value = document.getElementById('ctl00xbodyxUltraWebTab1xxctl1xTickets1xUltraWebGridT1_rc_'+GridRow+'_1').innerHTML;
		    document.getElementById("hdDescription").value = '1';
		    
		    
		    var grid=igtbl_getGridById("ctl00_body_UltraWebTab1__ctl1_Tickets1_UltraWebGridT1");
		    //var row = grid.Rows.getRow(GridRow).getCell(1).getValue();
		    var row = grid.Rows.getRow(GridRow);
		    //alert(row);		    
		    document.getElementById("ctl00_body_UltraWebTab1__ctl1_Tickets1_hdnTicket_Key").value = row.getCell(0).getValue();
		    
		    var edit = igedit_getById("ctl00_body_UltraWebTab1__ctl1_Tickets1_WebMaskEdit1");
		    var tempstr = document.getElementById('ctl00xbodyxUltraWebTab1xxctl1xTickets1xUltraWebGridT1_rc_'+GridRow+'_5').innerHTML;		    		    
		    edit.setValue(tempstr.replace(/\D/g,''));
		    //alert(row.getCell(5).getValue());
		    //edit.setValue(row.getCell(5).getValue());
		    
		    tempstr = document.getElementById('ctl00xbodyxUltraWebTab1xxctl1xTickets1xUltraWebGridT1_rc_'+GridRow+'_4').innerHTML;
		    document.getElementById("igtxtctl00_body_UltraWebTab1__ctl1_Tickets1_WebCurrencyEdit1").value = filtergridinnerhtml(tempstr);

            //Upload Dates & Times
            var tempstrdate = row.getCell(2).getValue();                        
            var tempdate = new Date(strLeft(tempstrdate, tempstrdate.length-2)+' '+strRight(tempstrdate, 2));                                    
            var dat = igdrp_getComboById('ctl00_body_UltraWebTab1__ctl1_Tickets1_Begin_Selling');		    
		    dat.setValue(tempdate);
		    var tim = igedit_getById('ctl00_body_UltraWebTab1__ctl1_Tickets1_Begin_Selling_Time');
		    tim.setValue(tempdate);
		    
		    tempstrdate = row.getCell(3).getValue()
		    tempdate = new Date(strLeft(tempstrdate, tempstrdate.length-2)+' '+strRight(tempstrdate, 2));
		    var dat2 = igdrp_getComboById('ctl00_body_UltraWebTab1__ctl1_Tickets1_Selling_Deadline');
		    dat2.setValue(tempdate);
		    var tim2 = igedit_getById('ctl00_body_UltraWebTab1__ctl1_Tickets1_Selling_Deadline_Time');
		    tim2.setValue(tempdate);
		    
		    //Sellers list
		    ClearSellersList();
		    setAdminslist(GridRow);
		    
		    //Donation box
		    setDonationbox(GridRow);
		  }
          
          }        

function filtergridinnerhtml(tempstr){
var tempdec = tempstr.replace(/\D/g,'');
//alert(parseFloat(tempdec));
return parseFloat(tempdec)/100;
}
  
function PriceValueChanged(){
document.getElementById("ctl00_body_UltraWebTab1__ctl1_Tickets1_hdnPrice").value = 1;
}  
  
function closepopup3(){
        var back = document.getElementById("AddT");
         back.style.display='none';
          var topop = document.getElementById("AddT2");
          topop.style.display='none';
          document.getElementById('ctl00_hdCanvasSize').value = "0";
        CheckCanvas(false);
      }   
      
function TextboxwithSuggestionFocus2(id){                        
        if ((document.getElementById("hdDescription").value == '0') && (id.name == 'ctl00$body$UltraWebTab1$_ctl1$Tickets1$txtdescription'))
        {
            id.value="";
            document.getElementById("hdDescription").value = '1';
        }        
      }  
      
function doRemove2(ticketid){
    var answer = confirm("Are you sure you want to Remove the Ticket?")
	if (answer){
		__doPostBack('DoRemove2',ticketid);
	}
	else{
	//
	}    
    }    
    
function MoveItem(ctrlSource, ctrlTarget,AddAdmin) {
        var Source = document.getElementById(ctrlSource);
        var Target = document.getElementById(ctrlTarget);

        if ((Source != null) && (Target != null)) {
            while ( Source.options.selectedIndex >= 0 ) {
                var newOption = new Option(); // Create a new instance of ListItem
                newOption.text = Source.options[Source.options.selectedIndex].text;
                newOption.value = Source.options[Source.options.selectedIndex].value;
                
                if (GetItemIndex(Target,newOption.value) == -1)
                {
                    if (AddAdmin)
                    {
                        Target.options[Target.length] = newOption; //Append the item in Target
                    }
                    
                    updatehdsellers();
                    
                }
                else if (AddAdmin == false)
                {
                    Source.remove(Source.options.selectedIndex);  //Remove the item from Source
                    updatehdsellers();                    
                }
                else{                    
                    break;
                }
            }
        }       
    }       
    
function selecttoplb(ctrlSource)
{
var Source = document.getElementById(ctrlSource);
if (Source.length > 1)
                    {
                        Source.selectedIndex = 0;
                    }
}

function updatehdsellers()
{
    var hdsellers = document.getElementById('ctl00_body_UltraWebTab1__ctl1_Tickets1_hdsellers');
                    var hdsellersnames = document.getElementById('ctl00_body_UltraWebTab1__ctl1_Tickets1_hdsellersnames');
                    hdsellers.value = "";                    
                    hdsellersnames.value = "";
                    var AdminsList = document.getElementById('ctl00_body_UltraWebTab1__ctl1_Tickets1_lbAdmins');
                    for (var i = 0; i < AdminsList.length; i++)
                    {
                        hdsellers.value += "|" + AdminsList.options[i].value;
                        hdsellersnames.value += "|" + AdminsList.options[i].text;
                    }
}

function setAdminslist(GridRow){
    var AdminsList = document.getElementById('ctl00_body_UltraWebTab1__ctl1_Tickets1_lbAdmins');
    var FriendsList = document.getElementById('ctl00_body_UltraWebTab1__ctl1_Tickets1_lbFriendsList');
    var fbids = document.getElementById('ctl00xbodyxUltraWebTab1xxctl1xTickets1xUltraWebGridT1_rc_' + GridRow +'_7').innerHTML;
    var fbnames = document.getElementById('ctl00xbodyxUltraWebTab1xxctl1xTickets1xUltraWebGridT1_rc_' + GridRow +'_8').innerHTML;
    //ctl00xbodyxUltraWebTab1xxctl1xTickets1xUltraWebGridT1_c_0_7
    fbids = fbids.replace("<nobr>","");
    fbids = fbids.replace("</nobr>","");
    fbids = fbids.replace("<NOBR>","");
    fbids = fbids.replace("</NOBR>","");
    fbids = fbids.replace("&nbsp;","");
    fbnames = fbnames.replace("<nobr>","");
    fbnames = fbnames.replace("</nobr>","");
    fbnames = fbnames.replace("<NOBR>","");
    fbnames = fbnames.replace("</NOBR>","");
    fbnames = fbnames.replace("&nbsp;","");
    var fbidsSplit = fbids.split("|");
    var fbnamesSplit = fbnames.split("|");
    for(i = 0; i < fbidsSplit.length; i++){	    
	    if ((fbidsSplit[i] != "") && (fbidsSplit[i] != "&nbsp;") && (fbidsSplit[i] != "<NOBR>") && (fbidsSplit[i] != "</NOBR>"))
	    {
	        //an actual id
	        var newOption = new Option(); // Create a new instance of ListItem
	        newOption.text = fbnamesSplit[i].replace("</NOBR>","");
            newOption.value = fbidsSplit[i].replace("</NOBR>","");
            AdminsList.options[AdminsList.length] = newOption; //Append the item in Target
            
            //remove from list
            //FriendsList.remove(GetItemIndex(FriendsList,fbidsSplit[i]));
	    }
    }    
    updatehdsellers();
}

function GetItemIndex(objListBox, strId)
{
  for (var i = 0; i < objListBox.length; i++)
  {
    var strCurrentValueId = objListBox.options[i].value;
    if (strId == strCurrentValueId)
    {
      return i;
    }
  }
  return -1;
}

function ClearSellersList()
{
    var AdminsList = document.getElementById('ctl00_body_UltraWebTab1__ctl1_Tickets1_lbAdmins');
    var rowamount = AdminsList.length;
    for (var i = 0; i < rowamount; i++)
    {
        AdminsList.remove(0);
    }
}

function DonationCheck()
{    
    var pricebox = document.getElementById('igtxtctl00_body_UltraWebTab1__ctl1_Tickets1_WebCurrencyEdit1');
    if (pricebox.disabled == false)
    {
        pricebox.disabled = true;
    }
    else
    {
        pricebox.disabled = false;
    }
}

function setDonationbox(GridRow)
{
    var donationbox = document.getElementById('ctl00_body_UltraWebTab1__ctl1_Tickets1_chkdonation');
    var pricebox = document.getElementById('igtxtctl00_body_UltraWebTab1__ctl1_Tickets1_WebCurrencyEdit1');    
    
    if (GridRow == -1) //new one
    {
        donationbox.checked = false;
        pricebox.disabled = false;
    }
    else
    {        
        var isdonation = document.getElementById('ctl00xbodyxUltraWebTab1xxctl1xTickets1xUltraWebGridT1_rc_' + GridRow +'_9').innerHTML;
        //alert(isdonation);
        if (isdonation.indexOf('True') !=-1)
        {
            //alert('true');
            donationbox.checked = true;
            pricebox.disabled = true;            
        }
        else
        {
            //alert('false');
            donationbox.checked = false;
            pricebox.disabled = false;
        }
    }    
}
</script>