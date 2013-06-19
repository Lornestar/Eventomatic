<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeaderBoard.ascx.cs" Inherits="Eventomatic.Addons.LeaderBoard" %>
<asp:HiddenField ID=hdEvent_Key runat=server Value='0' />
<asp:HiddenField ID=hdonlytop runat=server Value='0' />
<asp:Label ID=lblnoleaders runat=server Visible=false Text="No Leaders yet"></asp:Label>
<asp:Label ID=lblWinnergets runat=server></asp:Label>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    OnRowDataBound="GridView1_RowDataBound" ShowFooter="false"
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true 
                  Width="300px" CssClass="ticket_selector" >
<RowStyle Wrap="True" BorderStyle="None"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="fbid" HeaderText="fbid" 
                        ReadOnly="True" Visible=false/>                       
                    <asp:TemplateField HeaderText="Seller Ranking" ItemStyle-VerticalAlign=Top>
                        <ItemTemplate>                                               
                        <asp:Image ID=imgSeller runat=server Width=30px/>                        
                        <asp:Label ID=lblranking runat=server></asp:Label>
                        <asp:HyperLink ID=hypSeller runat=server Text='<%# Bind("Full_Name") %>' NavigateUrl="http://www.facebook.com/profile.php?id=0"/>
                        <asp:Label ID=lblfbid runat=server Visible=false Text='<%# Bind("fbid") %>'></asp:Label>
                        
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sold" FooterStyle-CssClass="TicketPurchase_TotalFooter" ItemStyle-Width=100px>
                        <ItemTemplate>
                            <asp:Label ID="lbltotalamount" runat="server" Width="100px" Text='<%# Bind("Amount_Sold") %>' visible=false>0</asp:Label>
                            <asp:Label ID="lblTotalPercent" runat="server" Width="100px" >0%</asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalOverall" runat="server" Width="100px" >100%</asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>

<FooterStyle BackColor="White"></FooterStyle>

<HeaderStyle CssClass="TicketPurchase_Header" HorizontalAlign="Left"></HeaderStyle>
            </asp:GridView>