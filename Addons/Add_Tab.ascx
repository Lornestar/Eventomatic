<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Add_Tab.ascx.cs" Inherits="Eventomatic.Addons.Add_Tab" %>

<asp:HiddenField ID=Add_Tab_fbid runat=server Value=0 />
<div id="AddTab" class="QuestionPopup">

</div>
<div id="AddTab2" class="QuestionPopup2">
<table width=100%>
    <tr>
        <td >
        <center>

<table style=" font-size:small; width:450px;">
    <tr>
        <td class="QuestionPopupHeader"><table width=100%>
            <tr>
                <td align=left>Sell from Facebook Profile</td>
                <td align=right><a href="#" onclick="closepopup()"><img src="/images/QuestionsBoxClose.jpg" style="border:0;" /></a></td>
            </tr>
        </table></td>        
    </tr>
    <tr>
        <td>
            These are the events you can sell now from your Facebook Profile:
        </td>
    </tr>
    <tr>
        <td style="text-align:center;">
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"  
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true 
                  Width="400px" CssClass="ticket_selector" ShowHeader=false ShowFooter=false>
<RowStyle Wrap="True" BorderStyle="None"></RowStyle>
                <Columns>                    
                <asp:TemplateField>
                    <ItemTemplate>
                    <asp:Label ID="lblEvent_Key" runat="server" Text='<%# Bind("Event_Key") %>' Visible=false></asp:Label>
                    <asp:Image ID=imgEvent runat=server Height=40px />                    
                    <asp:Label ID="lblEvent" runat="server" Text='<%# Bind("Event_Name") %>'></asp:Label>                        
                    </ItemTemplate>
                    </asp:TemplateField>                                                            
                    </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td style="text-align:right; font-weight:bold;">
        <br />
            Click this button to begin selling from your profile:
            <br /><fb:add-profile-tab type="off-facebook"></fb:add-profile-tab>
        </td>
    </tr>
    <tr>
        <td class="QuestionPopupFooter"></td>
    </tr>
</table>
</center>
</td>
    </tr>
</table>

</div>

<script language=javascript>
    function popup_Add_Tab(){        
        document.getElementById('ctl00_hdCanvasSize').value = "950";
        CheckCanvas(false);
         var back = document.getElementById("AddTab");
         back.style.display='block';
          var topop = document.getElementById("AddTab2");
          topop.style.display='block';
          }
          
     function closepopup_Add_Tab(){
        var back = document.getElementById("AddTab");
         back.style.display='none';
          var topop = document.getElementById("AddTab2");
          topop.style.display='none';
          document.getElementById('ctl00_hdCanvasSize').value = "0";
        CheckCanvas(false);
      }
</script>