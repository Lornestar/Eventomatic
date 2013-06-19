<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackgroundImage2.aspx.cs" Inherits="Eventomatic.Addons.BackgroundImage2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tickets</title>
    <script src="../Scripts.js" language="javascript" type="text/javascript"></script>    
    <link href="../Eventomatic.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .myClass:hover
        {
            background-color: #a1da29 !important;
        }
    </style>
</head>
<body onload="AdjustRadWidow();">
    <form id="Form2" method="post" runat="server">    
    <asp:HiddenField ID=hdnSelectedImageid runat=server Value="" />                                                           
    <asp:HiddenField ID=hdnSelectedImagesrc runat=server Value="" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>     
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat=server>
    <script type="text/javascript">

        function CloseWindow() {
            GetRadWindow().close('0');
        }
        function containerMouseover(sender) {
            sender.getElementsByTagName("div")[0].style.display = "";
        }
        function containerMouseout(sender) {
            sender.getElementsByTagName("div")[0].style.display = "none";
        }

        function selectimage(id) {
            var hdnBkImage = document.getElementById("hdnSelectedImageid");
            var hdnBkImagesrc = document.getElementById("hdnSelectedImagesrc");
            if (hdnBkImage.value != 0) {
                document.getElementById(hdnBkImage.value).className = "";
            }
            hdnBkImage.value = id.id;
            hdnBkImagesrc.value = id.src;
            id.className = "BackgroundImages_Selected";
        }    
    </script>        
        <div style="width: 525px; text-align:center;">
   <table>
    <tr>
        <td>
        <table width=100% cellpadding=5 cellspacing=10>
                <tr>
                    <td class="QuestionSections">
                    <telerik:RadListView ID="RadListView1" runat="server" 
                                        ItemPlaceholderID="ListViewContainer">
                                        <LayoutTemplate>                
                                            <asp:PlaceHolder runat="server" id="ListViewContainer" />
                                        </LayoutTemplate>            
                                        <ItemTemplate>                                
                                         <fieldset style=" height:94px; float: left; padding: 2px 2px 2px 2px;
                        background: #eeeeee" class="myClass">                
                                            <telerik:RadBinaryImage CssClass="myClass" runat="server" ID="RadBinaryImage1" ImageUrl=<%#Eval("Backgroundurl") %>
                             Width="150px" onclick="selectimage(this)" />                                                                  
                            </fieldset>
                                        </ItemTemplate>
                                        </telerik:RadListView>
                    </td>
                </tr>                             
            </table>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Button ID=btnSaveQuestion runat=server Text="Update Background" onclick="btnSaveQuestion_Click"/></td>
        </td>
    </tr>
   </table>


       </div>
       </telerik:RadAjaxPanel>    
    </form>
</body>

</html>