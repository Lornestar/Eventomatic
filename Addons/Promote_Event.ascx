<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Promote_Event.ascx.cs" Inherits="Eventomatic.Addons.Promote_Event" %>
<asp:HiddenField ID="hdfbid" runat="server" Value='0' />
<script language=javascript>
function doLoadCombo(){    
		__doPostBack('LoadCombo','');
	}    
</script>
<table >
    <tr>
        <td><h3>Promote Facebook Event</h3></td>    
    </tr>
<tr>
    <td>
        <table>                                        
                    <tr>
                        <td>
                        <telerik:RadListView ID="RadListView1" runat="server" Width=300
                        ItemPlaceholderID="ListViewContainer" >
                        <LayoutTemplate>                
                            <asp:PlaceHolder runat="server" id="ListViewContainer" />
                        </LayoutTemplate>            
                        <ItemTemplate>    
                            <table>
                                <tr>
                                    <td>
                                    <telerik:RadBinaryImage ID=radimage runat=server Height=20px/>
                                    <asp:HyperLink ID=hypevent runat=server></asp:HyperLink> </td>
                                    <td><asp:Button ID=btnRemove runat=server Text=Remove OnClick=btnRemove_Click /> </td>
                                    <td><asp:label ID=lbleventid runat=server Text='<%#Eval("Eventid")%>' Visible=false></asp:label></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        </telerik:RadListView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <asp:Label ID=lblpromoteevent runat=server><a href="javascript:doLoadCombo()" visible=false>Promote a Facebook Event</a></asp:Label>
                        <telerik:RadComboBox ID="RadComboBox1" runat="server" Width="350px"
                Height="300px" EmptyMessage="Select an event to Promote on your groupstore" OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged"
                AutoPostBack="true" HighlightTemplatedItems="true" >
                <ItemTemplate>
                                <table style="width: 275px" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="width: 177px;">
                                            <%# DataBinder.Eval(Container, "Text")%>
                                        </td>
                                        <td style="width: 60px;">
                                            <%# DataBinder.Eval(Container.DataItem, "start_time") %>
                                        </td>                                        
                                    </tr>
                                </table>
                            </ItemTemplate>
                </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
    </td>
</tr>
</table>
