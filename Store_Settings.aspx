<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Store_Settings.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Eventomatic.Store_Settings" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="Addons/Upload.ascx" tagname="Upload" tagprefix="uc1" %>

<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">

<table>
    <tr>
        <td width="0%"><h3>Store Settings</h3></td>
        <td width="100%"><hr /></td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="100%">
                        <tr>
                            <td>
                            Store Header:
                            </td>
                            <td><asp:TextBox ID="txtTitle" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>  
                        <tr valign=top>
                            <td>
                            Store Description:
                            </td>
                            <td><asp:TextBox ID="txtDescription" Rows="3" TextMode="MultiLine" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            Store Contact Info:
                            </td>
                            <td><asp:TextBox ID="txtContact" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>                        
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center"><asp:Button ID="btnSave" runat="server" 
                Text="Save Changes" onclick="btnSave_Click" /></td>
    </tr>
    <tr>
        <td width="0%"><h3>Group Image</h3></td>        
        <td width="100%"><hr /></td>        
    </tr>    
    <tr>
        <td colspan=2 ><center>
        <table>
            <tr>
                <td>-Max 500 Kb<br />
                            -Must be jpg format</td>
                            <td>
                            <uc1:Upload ID="Upload1" runat="server" />
&nbsp;</td>
            </tr>
        </table>
        </center>
        </td>
    </tr>
</table>
</asp:Content>
