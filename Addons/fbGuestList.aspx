<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fbGuestList.aspx.cs" Inherits="Eventomatic.Addons.fbGuestList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script src="../Scripts.js" language="javascript" type="text/javascript"></script>    
    <link href="../Eventomatic.css" rel="stylesheet" type="text/css" />
</head>
<body onload="Initpage();">
    <form id="Form2" method="post" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>     
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat=server LoadingPanelID="RadAjaxLoadingPanel1">
    <script type="text/javascript">

        function CloseWindow() {            
            GetRadWindow().close();
        }

        function Initpage() {
            var oWnd = GetRadWindow();
            oWnd.set_height(600);
            oWnd.set_width(320);
            var bounds = oWnd.getWindowBounds();
            var x = bounds.x;
            oWnd.moveTo(x, 50);
        }

        function returnToParent(fbid) {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get the city's name 
            oArg.fbid = "http://www.facebook.com/profile.php?id="+ fbid;

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();            
            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }
    </script>    
    <div style="width:250px; text-align:center; text-align:left; font-weight:bold; font-family:Tahoma;">
    <telerik:RadComboBox ID=RadCombo1 runat=server OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged" AutoPostBack=true>
        <Items>
            <telerik:RadComboBoxItem Text="Attending" Value="1"/>
            <telerik:RadComboBoxItem Text="Maybe Attending" Value="2"/>           
        </Items>
    </telerik:RadComboBox>
    <telerik:RadListView ID="RadListView1" runat="server" Width=100% AllowPaging="True" OnPageIndexChanged="RadListView1_PageIndexChanged"
        ItemPlaceholderID="ListViewContainer" >
        <LayoutTemplate>                
            <asp:PlaceHolder runat="server" id="ListViewContainer" />
            <table cellpadding="0" cellspacing="0" width="100%;" style="clear: both;">
                            <tr>
                                <td style="text-align:center;">
                                    <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="RadListView1"
                           PageSize="100">
                           <Fields>                           
                               <telerik:RadDataPagerButtonField FieldType="FirstPrev" />                               
                               <telerik:RadDataPagerButtonField FieldType="NextLast" />                                                                                             
                           </Fields>
                       </telerik:RadDataPager>
                                </td>
                            </tr>
                        </table>
        </LayoutTemplate>            
        <ItemTemplate>    
            <table style="border-bottom:solid 1px gray; width:100%; ">
                <tr valign=middle>
                    <td style="width:50px;">
                        <asp:Label ID=lbllinkfb runat=server Text=<%#Eval("fblink") %>></asp:Label>
                          <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" ImageUrl=<%#Eval("Pic_Url") %>
                            AutoAdjustImageControlSize="false" Width="40px" Height="40px" />
                            </a>
                    </td>
                    <td >
                    <asp:Label ID=Label1 runat=server Text=<%#Eval("fblink") %>></asp:Label>
                        <asp:Label ID=lblname runat=server Text=<%#Eval("Name") %>/>
                        </a>
                    </td>                    
                </tr>
            </table>
        </ItemTemplate>
        </telerik:RadListView>
    </div>
    </telerik:RadAjaxPanel>
    </form>
    
</body>
</html>
