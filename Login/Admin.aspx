<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Eventomatic.Login.Admin" MasterPageFile="~/Login/Site_Login.Master"%>

<%@ MasterType VirtualPath="~/Login/Site_Login.Master" %>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">

<script type="text/javascript">
    function onRequestStart(sender, args) {
        if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
            args.set_enableAjax(false);
        }
    }
    </script>

    <div >
    The most important metric:
    <table>
        <tr>
            <td>Money Amount passed through Snappay -&nbsp;</td>            
            <td style="color:Blue; font-weight:bolder;">$<asp:Label ID=lbltxcompleted runat=server></asp:Label></td>
        </tr>
        <tr>
            <td>New users signed up -&nbsp;</td>            
            <td style="color:Blue; font-weight:bolder;"><asp:Label ID=lblusersignup runat=server></asp:Label></td>
        </tr>
        <tr>
            <td>New users signed up & made a transaction -&nbsp;</td>            
            <td style="color:Blue; font-weight:bolder;"><asp:Label ID=lblusersdidtx runat=server></asp:Label></td>
        </tr>
    </table>
    </div>

<telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Vista" MultiPageID="RadMultiPage1"
                SelectedIndex="0" Align="Justify" ReorderTabsOnSelect="true" Width="900px" >
                <Tabs>
                    <telerik:RadTab Text="Users">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Stores" >
                    </telerik:RadTab>
                    <telerik:RadTab Text="Transactions">
                    </telerik:RadTab>                                        
                </Tabs>
            </telerik:RadTabStrip>
            <div style="float:right;">
            <asp:Button runat=server Text="Export to Excel" ID="btnexportexcel"  OnClick="btnexportexcel_click" />
            </div>
            

            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="pageView"
                Width="900px" style="background-color:White;">
                <telerik:RadPageView ID="RadPageView1" runat="server" BorderWidth=1>
                
                 <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" Width="95%" Visible=false>
        <ExportSettings HideStructureColumns="true" />
        <MasterTableView Width="100%" CommandItemDisplay="Top">
            
        </MasterTableView>
    </telerik:RadGrid>

            <br />
                <telerik:RadListView ID="RadListView1" runat="server" Width=100% ItemPlaceholderID="ListViewContainer">                                
                <LayoutTemplate>
                <asp:Panel ID="HierarchyPanel" runat="server" CssClass="wrapper">
                    <table id="products" class="products" >
                            <tr>
                                <td>
                                    User
                                </td>
                                <td style="width:180px;">
                                    Last Online
                                </td>
                                <td style="width:180px;">
                                    Signed up
                                </td>
                                <td>
                                    # of Stores
                                </td>
                                <td>
                                    email
                                </td>
                                <td>
                                    ip address
                                </td>
                            </tr>
                        <tr id="ListViewContainer" runat="server">
                        </tr>
                    </table>
                </asp:Panel>
            </LayoutTemplate>
                    <ItemTemplate>
                        
                            <tr style="border-bottom:1px solid black;">
                                
                                <td style="width:200px;">
                                    <asp:Label ID=lbllinkfb runat=server ><%#Eval("fblink") %> "></asp:Label>
                                    
                                      <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" ImageUrl=<%#Eval("picurl") %>
                                        AutoAdjustImageControlSize="false" Width="40px" Height="40px" />
                                        <%# Eval("Full_Name") %>
                                        </a>
                                </td>
                                <td>
                                    <%# Eval("Last_Change")%>
                                </td>
                                <td >
                                    <%# Eval("Signed_Up")%>
                                </td>
                                <td><%# Eval("Groups")%></td>
                                <td>
                                <%# Eval("Email")%>
                                </td>
                                <td >
                                  <a href='http://www.ipchecking.com/?ip=<%# Eval("ip_address")%>&check=lookup' target=_blank><%# Eval("IP_Address")%></a>
                                </td>
                            </tr>
                        
                    </ItemTemplate>
                </telerik:RadListView>

                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView2" runat="server" BorderWidth=1>
                <telerik:RadListView ID="RadListView2" runat="server" Width=100% ItemPlaceholderID="ListViewContainer" >                
                <LayoutTemplate>
                <asp:Panel ID="HierarchyPanel" runat="server" CssClass="wrapper">
                    <table id="products" class="products" >
                            <tr>
                                <td>
                                    Store Name
                                </td>
                                <td style="width:180px;">
                                    Creator
                                </td>
                                <td style="width:180px;">
                                    Store Opened
                                </td>                                
                            </tr>
                        <tr id="ListViewContainer" runat="server">
                        </tr>
                    </table>
                </asp:Panel>
            </LayoutTemplate>
                    <ItemTemplate>
                        
                            <tr style="border-bottom:1px solid black;">
                                <td>
                                    <%# Eval("Group_Name")%>
                                </td>
                                <td style="width:200px;">
                                    <asp:Label ID=lbllinkfb runat=server ><%#Eval("fblink") %> "></asp:Label>
                                    
                                      <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" ImageUrl=<%#Eval("picurl") %>
                                        AutoAdjustImageControlSize="false" Width="40px" Height="40px" />
                                        <%# Eval("Full_Name") %>
                                        </a>
                                </td>
                                
                                <td >
                                    <%# Eval("Signed_Up")%>
                                </td>                                
                            </tr>
                        
                    </ItemTemplate>
                </telerik:RadListView>

                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView3" runat="server" BorderWidth=1>
                
                <telerik:RadListView ID="RadListView3" runat="server" Width=100% ItemPlaceholderID="ListViewContainer"  >                
                <LayoutTemplate>
                <asp:Panel ID="HierarchyPanel" runat="server" CssClass="wrapper">
                    <table id="products" class="products" >
                            <tr>
                                <td>
                                    Seller
                                </td>
                                <td >
                                    Amount
                                </td>
                                <td>
                                    Currency
                                </td>
                                <td>
                                    Init Date
                                </td>
                                <td >
                                    Status
                                </td>
                                <td>
                                    IP address
                                </td>                                
                            </tr>
                        <tr id="ListViewContainer" runat="server">
                        </tr>
                    </table>
                </asp:Panel>
            </LayoutTemplate>
                    <ItemTemplate>
                        
                            <tr style="border-bottom:1px solid black;">                                
                                <td style="width:200px;">
                                    <asp:Label ID=lbllinkfb runat=server ><%#Eval("fblink") %> "></asp:Label>
                                    
                                      <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" ImageUrl=<%#Eval("picurl") %>
                                        AutoAdjustImageControlSize="false" Width="40px" Height="40px" />
                                        <%# Eval("Full_Name") %>
                                        </a>
                                </td>
                                <td>
                                    <%# Eval("amount")%>
                                </td>       
                                <td>
                                    <%# Eval("Currency")%>
                                </td>                                
                                <td>
                                    <%# Eval("Init_Date")%>
                                </td>
                                <td >
                                    <%# Eval("lbltx_status")%>
                                </td>                                
                                <td >
                                  <a href='http://www.ipchecking.com/?ip=<%# Eval("ip_address")%>&check=lookup' target=_blank><%# Eval("ip_address")%></a>
                                </td>
                            </tr>
                        
                    </ItemTemplate>
                </telerik:RadListView>


                </telerik:RadPageView>
            </telerik:RadMultiPage>


</asp:Content>