<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Questions.ascx.cs" Inherits="Eventomatic.Addons.Questions" %>


<link href="Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />  
<asp:HiddenField ID=Event_Key runat=server Value=0 />
<table>    
    <tr>
        <td>
        <!--
<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true ShowHeader=false
                 AlternatingRowStyle-CssClass="Gridview_bottomBorder" RowStyle-CssClass="Gridview_bottomBorder"  Width="100%" >
<RowStyle Wrap="True" BorderStyle="None" VerticalAlign=Top></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Question_Text" ReadOnly="True" Visible=false />
                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID=Question_Text Text='<%# Bind("Question_Text") %>' runat=server CssClass="Each_Question"></asp:Label><br />
                            <asp:TextBox ID=txtAnswer runat=server Width=480></asp:TextBox>                            
                        </ItemTemplate>                        
                    </asp:TemplateField>
                    <asp:TemplateField  >
                        <ItemTemplate>
                            <asp:Label ID=Mandatory Text='<%# Bind("Mandatory") %>' runat=server Visible=false></asp:Label>
                            <asp:Label ID=Question_Key Text='<%# Bind("Question_Key") %>' runat=server Visible=false></asp:Label>
                        </ItemTemplate>                        
                    </asp:TemplateField>                    
                    
                </Columns>
            </asp:GridView>-->            
        
        
        
       
            <igtbl:UltraWebGrid ID="UltraWebGridQ1" runat="server">
                <bands>
                    <igtbl:UltraGridBand>
                        <addnewrow view="NotSet" visible="NotSet">
                        </addnewrow>
                        <Columns>                               
                        <igtbl:UltraGridColumn BaseColumnName="Question_Key" DataType="System.Int32" 
                                        Hidden="True">
                                        <Header Caption="Question_Key">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Header>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Footer>
                                    </igtbl:UltraGridColumn>           
                                    <igtbl:TemplatedColumn  Width=500px Key="Col1">                            
                                    <CellTemplate>
                                     
                             <asp:Label ID=lblMandatory runat=server ForeColor=Red Text="*" Visible=false></asp:Label>
                             <asp:Label ID=Question_Text Text='<%# DataBinder.Eval(Container.DataItem,"The_Question") %>' runat=server CssClass="Each_Question"></asp:Label>:<br />
                             <asp:TextBox ID=txtAnswer runat=server Width=480></asp:TextBox>
                             <asp:DropDownList ID=ddlAnswer runat=server Visible=false></asp:DropDownList>                             
                             <br />&nbsp;
                             </CellTemplate>
                             <CellStyle Height=45px></CellStyle>

<Header>
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Header>

<Footer>
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Footer>
                            </igtbl:TemplatedColumn>
                           <igtbl:UltraGridColumn Width=85px> 
                                                      
<Header>
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Header>

<Footer>
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Footer>
                                                      
                           </igtbl:UltraGridColumn>                                    
                        </Columns>
                    </igtbl:UltraGridBand>                    
                </bands>
                <displaylayout allowaddnewdefault="Yes" 
                    bordercollapsedefault="Separate" colheadersvisibledefault="No" 
                    name="ctl00xUltraWebGrid1" rowheightdefault="20px" rowselectorsdefault="No" 
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
                    <headerstyledefault backcolor="LightGray" 
                        horizontalalign="Left">
                        <borderdetails colorleft="White" colortop="White" widthleft="1px" 
                            widthtop="1px" />
                    </headerstyledefault>
                    <rowstyledefault backcolor="Window" bordercolor="Silver" borderwidth="1px" 
                        font-names="Arial" font-size="10pt">
                        <borderdetails colorleft="Window" colortop="Window" />
                        <padding left="3px" Top="0px" />
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
        <asp:Label ID=lblAddNew runat=server Text="Add Question"  
        CssClass="QuestionsButton" OnClick="javascript:popup(0,0,0,0);"></asp:Label>  </a>
    </td>        
    </tr>
    <tr>
        <td style="font-size:smaller;"><asp:Label ID=lblPleaseNote runat=server>*Please note that First name & Last name are already being asked for.</asp:Label></td>
    </tr>
</table>


<div id="AddQ" class="QuestionPopup">

</div>
<div id="AddQ2" class="QuestionPopup2">
<table width=100%>
    <tr>
        <td class="QuestionPopupHeader"><table width=100%>
            <tr>
                <td align=left>Add Question</td>
                <td align=right><a href="#" onclick="closepopup()"><img src="/images/QuestionsBoxClose.jpg" style="border:0;" /></a></td>
            </tr>
        </table></td>        
    </tr>
    <tr>
        <td>
            <table width=100% cellpadding=5 cellspacing=10>
                <tr>
                    <td class="QuestionSections">What is the question you would like to ask?
                    <br />
                    <asp:TextBox ID=txtQuestionAsk TextMode=MultiLine runat=server Width="400px" onfocus="TextboxwithSuggestionFocus(this)" CssClass="TextboxwithSuggestion" ></asp:TextBox>                    
                    <input type=hidden id=hdntxtQuestionAsk value=0 />
                    </td>
                </tr>
                <tr>
                    <td class="QuestionSections">
                        
                                How would you like the question to be answered?<br />
                        With a <asp:DropDownList ID=ddlAnswertype runat=server onchange="ChangeAnswertype(this)">
                            <asp:ListItem Selected=True Value=0>Text Box</asp:ListItem>
                            <asp:ListItem Value=1>Drop Down</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                        <div id=AnswersMultipleChoice style="display:none;">
                        Answer Choices (each choice on a separate line)<br />
                        <asp:TextBox ID=txtAnswersMultipleChoice runat=server Width=250px TextMode=MultiLine onfocus="TextboxwithSuggestionFocus(this)" CssClass="TextboxwithSuggestion"></asp:TextBox>
                        <input type=hidden id=hdntxtAnswersMultipleChoice value=0 />
                        </div>
                        
                    </td>
                </tr>
                <tr>
                    <td class="QuestionSections">
                        Is it mandatory the question be answered?<br />                        
                        No<asp:radiobutton id="radio1" Checked=true GroupName=rdMandatory runat="server" />
                        Yes<asp:radiobutton id="radio2" GroupName=rdMandatory runat="server" />
                    </td>
                </tr>
                <tr>
                    <td><asp:HiddenField ID=hdnQuestion_Key runat=server Value=0 />
                    </td>
                </tr>
            </table>
        
        </td>
    </tr>
    <tr>
        <td class="QuestionPopupFooter"><asp:Button ID=btnSaveQuestion runat=server 
                Text="Update Question" onclick="btnSaveQuestion_Click" /></td>
    </tr>
</table>   


</div>


<script type="text/javascript">

function AddUltraGridNewRow()
{
    var cbo = igcmbo_getComboById('UltraWebGridQ1');
    var row = igtbl_addNew(cbo.grid.Id, 0);
    row.getCell(0).setValue(0);
    row.getCell(1).setValue("Name11");
    row.getCell(2).setValue(new Date());
}


        function popup(Question_Key,Question_Type,Question,Answer,Mandatory){        
        document.getElementById('ctl00_hdCanvasSize').value = "950";
        CheckCanvas(false);
         var back = document.getElementById("AddQ");
         back.style.display='block';
          var topop = document.getElementById("AddQ2");
          topop.style.display='block';
                 
		if (Question_Key == 0)//A new one
		{
		    document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_txtQuestionAsk").value = 'What meal do you prefer?';
		    document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_txtAnswersMultipleChoice").value = 'Beef'+ String.fromCharCode(13) + 'Chicken' + String.fromCharCode(13) + 'Fish';
		    document.getElementById("AnswersMultipleChoice").style.display = 'none';
		    document.getElementById("hdntxtQuestionAsk").value = '0';
		    document.getElementById("hdntxtAnswersMultipleChoice").value = '0';
		    document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_ddlAnswertype").value = '0';
		    document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_btnSaveQuestion").value = 'Add New Question';
		    document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_hdnQuestion_Key").value = '0';
		}
		else{//An existing question
		    document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_txtQuestionAsk").value = document.getElementById(Question).innerHTML;
		    document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_btnSaveQuestion").value = 'Update Question';
		    document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_hdnQuestion_Key").value = Question_Key;
		    
		    //Show boxes have been filled before
		    document.getElementById("hdntxtQuestionAsk").value = '1';
		    document.getElementById("hdntxtAnswersMultipleChoice").value = '1';
		    if (Mandatory == 1)//Mandatory
		    {
		        document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_radio1").checked = false;
		        document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_radio2").checked = true;
		    }
		    else 
		    {
		        document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_radio1").checked = true;
		        document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_radio2").checked = false;
		    }
		    
		    if (Question_Type == 0) //textbox
		    {
		        document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_ddlAnswertype").value = '0';
		        document.getElementById("AnswersMultipleChoice").style.display = 'none';
		    }
		    if (Question_Type == 1) //dropdown
		    {
		        document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_ddlAnswertype").value = '1';
		        document.getElementById("AnswersMultipleChoice").style.display = 'block';
		        document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_txtAnswersMultipleChoice").value = "";
		        var myDropDownList = document.getElementById(Answer);		        
                   for(var i = 0; i < myDropDownList.length; i++)
                   {
//                      alert(myDropDownList.options[i].value);
                      //how do I get the text of the drop down list?
                      if (myDropDownList.options[i].value != "Please Select ...")
                      {
                      document.getElementById("ctl00_body_UltraWebTab1__ctl2_Questions1_txtAnswersMultipleChoice").value += myDropDownList.options[i].value + String.fromCharCode(13);                      
                      }
                   }		        
		    }
		}
      }
      function closepopup(){
        var back = document.getElementById("AddQ");
         back.style.display='none';
          var topop = document.getElementById("AddQ2");
          topop.style.display='none';
          document.getElementById('ctl00_hdCanvasSize').value = "0";
        CheckCanvas(false);
      }
      
      function TextboxwithSuggestionFocus(id){                
        if ((document.getElementById("hdntxtQuestionAsk").value == '0') && (id.name == 'ctl00$body$UltraWebTab1$_ctl2$Questions1$txtQuestionAsk'))
        {
            id.value="";
            document.getElementById("hdntxtQuestionAsk").value = '1';
        }
        if ((document.getElementById("hdntxtAnswersMultipleChoice").value == '0') && (id.name == 'ctl00$body$UltraWebTab1$_ctl2$Questions1$txtAnswersMultipleChoice'))
        {
            id.value="";
            document.getElementById("hdntxtAnswersMultipleChoice").value = '1';
        }
      }
      
      function ChangeAnswertype(id){
        if (id.value == '1')
        {
            document.getElementById("AnswersMultipleChoice").style.display = 'block';
        }
        else{
            document.getElementById("AnswersMultipleChoice").style.display = 'none';
        }
      }
      
      function doRemove(eventid){
    var answer = confirm("Are you sure you want to Remove the Question?")
	if (answer){
		__doPostBack('DoRemove',eventid);
	}
	else{
	//
	}    
    }
      /*Meteora.uses('Meteora.Bubble');
		Meteora.onStart(
  function () {
    var bubble = new Bubble(
      'ctl00_body_txtQuestionAsk',
      'eg. What meal do you prefer?',
      {
        'showEvent':  'focus',
        'hideEvent':  'blur',
        'width':      200,
        'height':     30
      }
    );    
      }
      );*/
      
    </script>
            