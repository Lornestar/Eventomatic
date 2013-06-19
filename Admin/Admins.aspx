<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admins.aspx.cs" Inherits="Eventomatic.Admin.Admins" MasterPageFile="~/Site.Master"%>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
<script language="Javascript" src="Scripts.js"></script>
<table width=100%>
    <tr>
        <td width="0%"><h3>Admins</h3></td>        
        <td width="100%"><hr /></td>
    </tr>
    <tr>
        <td colspan=2 align="center">
            <table width=100%>
                <tr valign="middle">
                    <td align=right>
                        Current App Users<br />
                        <asp:ListBox ID="lbUsersList" runat="server" Rows=10 Width=200></asp:ListBox>
                    </td>
                    <td align=center>                        
                        <asp:Button ID=btnAdd runat=server onclick="btnAdd_Click" Text=">" />
                        <br />
                        <asp:Button ID=btnRemove runat=server onclick="btnRemove_Click" Text="<" />
                                                
                    </td>
                    <td align=left>Administrators of App:<br />                    
                        <asp:Label ID=lblAdmins runat=server></asp:Label><br />
                        <asp:ListBox ID="lbAdmins" runat="server" Rows=10 Width=200>                            
                        </asp:ListBox>
                    </td>            
                </tr>
                <tr>
                    <td align="center" colspan="3"><asp:Label ID=lblError runat=server ForeColor=Red Visible=false>You cannot remove yourself from the list</asp:Label></td>
                </tr>
            </table>
        </td>     
    </tr>    
</table>
</asp:Content>