<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="Eventomatic.Store" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml" >
<head runat="server">
    <title>Store</title>
    <link href="Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" /> 
</head>
<body>
<form id="form1" runat="server" >
<asp:HiddenField ID=hdnfbid runat=server Value=0 />
<div id="fb-root"></div>
    
<script src="http://connect.facebook.net/en_US/all.js"></script>
<script>
  //FB.init({appId: "e56b8159f39e86e8a858cea345f5b09c", status: true, cookie: true,
   //          xfbml: true});
   FB.init({appId: "cb4d81e23b90fbc40d88dc3bf02db2e9", status: true, cookie: true,
             xfbml: true});
  
  FB.Event.subscribe('auth.sessionChange', function(response) {
    if (response.session) {
      // A user has logged in, and a new cookie has been saved
      //alert('logged in');      
    } else {
      // The user has logged out, and the cookie has been cleared
      //alert('logged out');
    }
  });
</script>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Thumbnail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <script type="text/javascript">
        function fileUploaded(sender, args) {
            $find('RadAjaxManager1').ajaxRequest();
            $telerik.$(".invalid").html("");
            sender.deleteFileInputAt(0);
        }

        function validationFailed(sender, args) {
            $telerik.$(".invalid")
                .html("Invalid File. Must be of type 'jpg' and under 500k");
            sender.deleteFileInputAt(0);
        }
    </script>


<table width=100%>
    <tr>
        <td align=center>
    
    <table class="EntireStoreBox">
        <tr>
            <td colspan=2>
            <table style="background-color:#18243d" width=100%>
                <tr>
                    <td align=left><asp:Image ID="imgGroup" runat=server Height="100px" />
                    <telerik:RadBinaryImage runat="server" Height="100px" ResizeMode="Fit"
            ID="Thumbnail"  AlternateText="Thumbnail" CssClass="binary-image" Visible=false/>            
            <span class="invalid"></span>
        <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" MaxFileInputsCount="1" OnClientFileUploaded="fileUploaded"
            OnFileUploaded="AsyncUpload1_FileUploaded" AllowedFileExtensions="jpeg,jpg" OnClientValidationFailed="validationFailed"
             Visible=false MaxFileSize="524288" >
            <Localization Select="Change Picture"/>
        </telerik:RadAsyncUpload>
                    </td>
                    <td style="text-align:center;">          
                    <table style="height:100%; width:100%;">                        
                        <tr valign=middle>
                            <td><asp:Label ID=lblGroupName runat=server CssClass="StorePage_GroupName"></asp:Label></td>        
                        </tr>
                    </table>
                    </td>
                    <td align=right style="vertical-align:bottom;">
                        <table>
                            <tr>                                 
                                 <td><div class="GroupStoreLogo3"></div></td>       
                            </tr>
                        </table>                    
                    </td>                    
                </tr>
            </table>            
            <asp:Panel ID=pnledit runat=server>
            <div style="text-align:right;">
            <asp:Panel ID=pnlloggedin runat=server Visible=false>
            <table width=100%>
                <tr>
                    <td>
                    
                    <table >
                        <tr>
                            <td><asp:Image ID=imgfbuser runat=server Height="30px" /> </td>
                            <td><asp:Label ID=lblfbuser runat=server>Logged in as </asp:Label>
                            <br />
                            <asp:Label ID=lblfbuserstoreadmin runat=server></asp:Label>
                            </td>
                        </tr>                        
                    </table>                    
                                        
                    </td>
                    <td style="float:right;">
                    <asp:Panel ID=pnleditcontrols runat=server Visible=false>
                    <table style="text-align:center;">
                        <tr>
                            <td colspan=2 >Currently in Edit mode</td>
                        </tr>
                        <tr>
                            <td><asp:Button ID="btnsave" runat=server Text="Save Changes" 
                                    onclick="btnsave_Click" /></td>
                            <td><asp:Button ID="btncancel" runat=server Text="Cancel" 
                                    onclick="btncancel_Click" /></td>
                        </tr>
                    </table>
                    </asp:Panel>
                    </td>
                </tr>
            </table>
            </asp:Panel>
            <asp:Panel ID=pnlloggedout runat=server Visible=false>                    
            You must be logged into Facebook to make changes
             <fb:login-button></fb:login-button>             
            </asp:Panel>
            </div>
            </asp:Panel>
            </td>
        </tr>        
        <tr valign=top>
            <td>
            
            <asp:Panel ID=pnlInfo runat=server>
            <table width=350px >
                <tr valign=top>
                    <td align=center style="background-color:#ebecf0;">
                    <table>
                        <tr>
                            <td class="StorePage_TextHeader"><asp:Label ID=lblStoreTitle runat=server ></asp:Label>
                            <telerik:RadTextBox Width="300px" ID="txtStoreTitle" runat="server" TextMode=SingleLine
                            EmptyMessage="Store Header" Visible=false>
                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="StorePage_TextDescription"><br /><asp:Label ID=lblStoreDescription runat=server></asp:Label>

                            <telerik:RadTextBox Width="300px" ID="txtStoreDescription" runat="server" TextMode="MultiLine"
                            EmptyMessage="Store Description" Visible=false Height=200px Wrap=true>
                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;"><br /><asp:Label ID=lblContact runat=server class="Fine_Print"></asp:Label>
                            <telerik:RadTextBox Width="250px" ID="txtContact" runat="server" TextMode=SingleLine
                            EmptyMessage="Contact info" Visible=false>
                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
                    </asp:Panel>                    
                    </td>
                    <td align=center width=100%>
                    <table>
                        <tr valign=top>
                            <td>
                            <telerik:RadAjaxPanel ID="pnlOthergroups" runat="server" Visible=false CssClass="OtherStores" >
                            <div style="text-align:center;">Display events from other groupstores in your network</div>                                                        
                            <table>
                                <tr valign=top>
                                    <td>Other groupstores
                                    <telerik:RadListBox ID="lbOthergroupstores" runat="server" Width="200px" Height="100px"
            SelectionMode="Multiple" AllowTransfer="true" TransferToID="lbCurrentDisplaying" AutoPostBackOnTransfer="true"
            AllowReorder="false" EnableDragAndDrop="true" OnTransferred="lbOthergroupstores_Transferred" >
            <Items>
                <telerik:RadListBoxItem Text="Argentina" />
                <telerik:RadListBoxItem Text="Australia" />
                <telerik:RadListBoxItem Text="Brazil" />
                <telerik:RadListBoxItem Text="Canada" />
                <telerik:RadListBoxItem Text="Chile" />
                <telerik:RadListBoxItem Text="China" />
                <telerik:RadListBoxItem Text="Egypt" />
                <telerik:RadListBoxItem Text="England" />
                <telerik:RadListBoxItem Text="France" />
                <telerik:RadListBoxItem Text="Germany" />
                <telerik:RadListBoxItem Text="India" />
                <telerik:RadListBoxItem Text="Indonesia" />
                <telerik:RadListBoxItem Text="Kenya" />
                <telerik:RadListBoxItem Text="Mexico" />
                <telerik:RadListBoxItem Text="New Zealand" />
                <telerik:RadListBoxItem Text="South Africa" />
            </Items>
        </telerik:RadListBox>
                                    </td>
                                    <td>Currently displaying
                                    <telerik:RadListBox ID="lbCurrentDisplaying" runat="server" Width="200px" Height="100px"
            SelectionMode="Multiple" AllowReorder="false" AutoPostBackOnReorder="true" EnableDragAndDrop="true"
            OnTransferred="lbCurrentDisplaying_Transferred"            >
            <Items>
                <telerik:RadListBoxItem Text="USA"/>
            </Items>
        </telerik:RadListBox>
                                    </td>
                                </tr>
                            </table>
                            </telerik:RadAjaxPanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <asp:Panel ID=pnlGroupName runat=server>
                            <table width=400px>
            <telerik:RadListView ID="RadListView1" runat="server"
             ItemPlaceholderID="ListViewContainer" OnItemDataBound="RadListView1_ItemDataBound">
                        <LayoutTemplate>                
                            <asp:PlaceHolder runat="server" id="ListViewContainer" />
                        </LayoutTemplate>            
                        <ItemTemplate>                                                                
                                <tr valign=top >
                                    <td style="border-bottom:1px solid #e1e1e1;">
                                        <table style="height:100%; width:100%;">
                                            <tr valign=top>
                                                <td style="width:50px;">
                                                <asp:Panel ID=pnldate runat=server>
                            <div class="CoolDate">-<asp:Label ID=lblDayofWeek runat=server></asp:Label>-<br /><asp:Label ID=lblDay runat=server></asp:Label><br /><asp:Label ID=lblMonth runat=server></asp:Label></div>                            
                            </asp:Panel>
                                    </td>
                                    <td align=left><asp:HyperLink ID=hypEvent_Name runat=server Text='<%#Eval("Event_Name")%>'  NavigateUrl='~/Order_Form.aspx?Event_Key=' CssClass="StorePage_EventTitle"></asp:HyperLink>
                            <br />
                            <asp:HyperLink ID=hypEvent_Description runat=server Text='<%#Eval("description")%>' CssClass="StorePage_EventText"></asp:HyperLink>
                            <asp:Label ID=lblEvent_Key runat=server Visible=false Text='<%#Eval("Event_Key")%>' ></asp:Label>
                            <asp:Label ID=lblEvent_Begins runat=server Visible=false Text='<%#Eval("Event_Begins")%>' ></asp:Label>                       
                            <asp:label ID=lbleid runat=server Text='<%#Eval("eid")%>' Visible=false></asp:label>
                            <asp:label ID=lbleventtype runat=server Text='<%#Eval("event_type")%>' Visible=false></asp:label>                                                                                    
                                    </td>
                                    <td  style="text-align:center;">
                                    <asp:Label runat=server ID=lblimagelink></asp:Label>
                                    <telerik:RadBinaryImage ID=ImgEvent runat=server Height=70px/>                          
                                    </a>
                                    <br />              
                                    <asp:HyperLink ID=hypbuynow runat=server CssClass="Store_BuyNow_Button" Text="Buy Now"/>
                                    </td>
                                            </tr>
                                        </table>
                                    </td>
                                    
                                </tr>
                        </ItemTemplate>
                        </telerik:RadListView>        
                        </table>                    
         </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    
            <asp:Label ID=lblheader runat=server Visible=false></asp:Label>           
                    </td>
                </tr>
                <tr>
             <td colspan=2>
                    <table width=100%>
                        <tr>
                            <td align=left></td>                    
                    <td align=right ></td>
                        </tr>
                    </table>
                    </td>                    
                </tr>
                       
        <tr>
        <td align="right" colspan=2 class="Fine_Print">Copyright Groupstore 2010</td>
    </tr>
            </table>
            
            
            </td>
        </tr>
             
        
    </table>
    
    </form>
    <script type="text/javascript">
        var GoSquared = {};
        GoSquared.acct = "GSN-105322-Y";
        (function (w) {
            function gs() {
                w._gstc_lt = +(new Date); var d = document;
                var g = d.createElement("script"); g.type = "text/javascript"; g.async = true; g.src = "//d1l6p2sc9645hc.cloudfront.net/tracker.js";
                var s = d.getElementsByTagName("script")[0]; s.parentNode.insertBefore(g, s);
            }
            w.addEventListener ? w.addEventListener("load", gs, false) : w.attachEvent("onload", gs);
        })(window);
</script>
</body>
</html>
