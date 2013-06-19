<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FaceMash.ascx.cs" Inherits="Eventomatic.Addons.FaceMash" %>

<telerik:RadListView ID="RadListView1" runat="server" Width=350
                                        ItemPlaceholderID="ListViewContainer">
                                        <LayoutTemplate>                
                                            <asp:PlaceHolder runat="server" id="ListViewContainer" />
                                        </LayoutTemplate>            
                                        <ItemTemplate>        
                                        <asp:Label ID=lbllinkfb runat=server Text=<%#Eval("fblink") %> />
                                            <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" ImageUrl=<%#Eval("Pic_Url") %>
                            AutoAdjustImageControlSize="false" Width="30px" Height="30px" />
                                            <telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="RadBinaryImage1" RelativeTo="Element"
                            Position="TopCenter" EnableShadow="true" CssClass="FaceMashToolTip"
                            Text=<%#Eval("Name") %>  EnableEmbeddedSkins=false>
                            </telerik:RadToolTip>
                            
 
                            </a>
                                        </ItemTemplate>
                                        </telerik:RadListView>

