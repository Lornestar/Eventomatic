<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tabiframe.aspx.cs" Inherits="Eventomatic.Tabiframe" EnableSessionState="False"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:HiddenField ID=hdpageid runat=server />
    <table width=100%>
        <tr>
            <td>
            <a href="http://www.theGroupstore.com" target="_blank"> <img src=http://www.theGroupstore.com/Images/groupstore_Order_Logo2.png style=border-style:none;height:25px; /</a>
            </td>
            <td style=" float:right;">
                <table>
                                <tr>
                                    <td><a href="http://messenger.providesupport.com/messenger/lornestar.html" target="_blank">Live Support</a>
                                    </td>
                                    <td>|</td>
                                    <td><a href="http://www.facebook.com/apps/application.php?id=391377955486" target="_top">About us</a></td>                                    
                                </tr>
                            </table>                            
            </td>
        </tr>
    </table>

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
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
                                    <td align=left><asp:HyperLink ID=hypEvent_Name runat=server Text='<%#Eval("Event_Name")%>'  NavigateUrl='~/Order_Form.aspx?Event_Key=' CssClass="StorePage_EventTitle" Target="_blank"></asp:HyperLink>
                            <br />
                            <asp:HyperLink ID=hypEvent_Description runat=server Text='<%#Eval("description")%>' CssClass="StorePage_EventText" Target="_blank"></asp:HyperLink>
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
                                    <asp:HyperLink ID=hypbuynow runat=server CssClass="Store_BuyNow_Button" Text="Buy Now" Target="_blank"/>
                                    </td>
                                            </tr>
                                        </table>
                                    </td>
                                    
                                </tr>
                        </ItemTemplate>
                        </telerik:RadListView>   
    </div>
    </form>
</body>
</html>
