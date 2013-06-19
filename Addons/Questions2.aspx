<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Questions2.aspx.cs" Inherits="Eventomatic.Addons.Questions2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>Tickets</title>
    <script src="../Scripts.js" language="javascript" type="text/javascript"></script>    
    <link href="../Eventomatic.css" rel="stylesheet" type="text/css" />
</head>
<body onload="AdjustRadWidow();">
    <form id="Form2" method="post" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>     
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat=server>
    <script type="text/javascript">        

        function CloseWindow()
        {                
           GetRadWindow().close('0');
        }
    </script>    
<div style="width: 400px; height:350px; text-align:center;">
    <table style="display:block; font-size:small; text-align:left;">
    <tr>
        <td>
            <table width=100% cellpadding=5 cellspacing=10>
                <tr>
                    <td class="QuestionSections">What is the question you would like to ask?
                    <br />                    
                    <telerik:RadTextBox ID="txtQuestionAsk" runat="server" Width="400px" EmptyMessage="What meal do you prefer?"/>                    
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
                        <telerik:RadTextBox ID=txtAnswersMultipleChoice runat=server Width="250px" TextMode=MultiLine Height=70px/>
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
            </table>
        
        </td>
    </tr>
    <tr>
        <td class="QuestionPopupFooter"> <asp:Button ID=btnSaveTicket runat=server 
                Text="Update Ticket"  OnClick="btnSaveTicket_Click"  /></td>
    </tr>
</table>   
       </div>
       </telerik:RadAjaxPanel>    
       <script language=javascript>
           function ChangeAnswertype(id) {
               if (id.value == '1') {
                   document.getElementById("AnswersMultipleChoice").style.display = 'block';
               }
               else {
                   document.getElementById("AnswersMultipleChoice").style.display = 'none';
               }
           }

           function showDropDownAnswers() {
               document.getElementById("AnswersMultipleChoice").style.display = 'block';
           }
       </script>
    </form>
</body>
</html>