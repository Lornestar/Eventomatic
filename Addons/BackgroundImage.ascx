<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BackgroundImage.ascx.cs" Inherits="Eventomatic.Addons.BackgroundImage" %>
<link href="Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />  
<asp:HiddenField ID=Event_Key runat=server Value=0 />
<asp:HiddenField ID=hdnSelectedImageid runat=server Value="ctl00_body_UltraWebTab1__ctl0_BackgroundImage1_Repeater1_ctl00_img1" />                                                           
<asp:HiddenField ID=hdnSelectedImagesrc runat=server Value="../Images/BackgroundImages/00.jpg" />
<asp:Panel ID=pnlbckselected runat=server>
<asp:Image ID=ImgBackground runat=server ImageUrl="../Images/BackgroundImages/00.jpg" Height=100px BorderStyle=Solid BorderWidth=1px/>
</asp:Panel>
<br />
<a href=#>        
        <asp:Label ID=lblAddNew runat=server Text="Change Background"  
        CssClass="QuestionsButton" OnClick="javascript:popup2();"></asp:Label>
        </a>
<div id="AddBk" class="QuestionPopup">

</div>
<div id="AddBk2" class="QuestionPopup2">
<table width=100%>
    <tr>
        <td class="QuestionPopupHeader"><table width=100%>
            <tr>
                <td align=left>Background Image</td>
                <td align=right><a href="#" onclick="closepopup2()"><img src="/images/QuestionsBoxClose.jpg" style="border:0;" /></a></td>
                
            </tr>
        </table></td>        
    </tr>
    <tr>
        <td>
            <table width=100% cellpadding=5 cellspacing=10>
                <tr>
                    <td class="QuestionSections">
                    Choose a background image <br />             
                    <div>
                    <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                     <a href="#" onclick="" class="BackgroundImages"><asp:Image onclick="selectimage(this)" ID="img1" runat="server" ImageUrl='<%#Eval("Key")%>' Height=100px /></a>
                    </ItemTemplate>
                    </asp:Repeater>
                    </div>                         
                    </td>
                </tr>                             
            </table>
        
        </td>
    </tr>
    <tr>
        <td class="QuestionPopupFooter">
            <asp:Button ID=btnSaveQuestion runat=server Text="Update Background" onclick="btnSaveQuestion_Click"  /></td>
    </tr>
</table>   

<script language=javascript>
    function popup2(){
         var back = document.getElementById("AddBk");
         back.style.display='block';
          var topop = document.getElementById("AddBk2");
          topop.style.display='block';
          
          var hdnBkImage = document.getElementById("ctl00_body_UltraWebTab1__ctl0_BackgroundImage1_hdnSelectedImageid");                    
          document.getElementById(hdnBkImage.value).className = "BackgroundImages_Selected";          
          }
    function selectimage(id){                                
        var hdnBkImage = document.getElementById("ctl00_body_UltraWebTab1__ctl0_BackgroundImage1_hdnSelectedImageid");
        var hdnBkImagesrc = document.getElementById("ctl00_body_UltraWebTab1__ctl0_BackgroundImage1_hdnSelectedImagesrc");
        if (hdnBkImage.value != 0)
        {                
            document.getElementById(hdnBkImage.value).className = "";
        }
        hdnBkImage.value = id.id;
        hdnBkImagesrc.value = id.src;
        id.className = "BackgroundImages_Selected";
    }    
    function closepopup2(){
        var back = document.getElementById("AddBk");
         back.style.display='none';
          var topop = document.getElementById("AddBk2");
          topop.style.display='none';
      }
</script>
</div>