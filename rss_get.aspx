<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rss_get.aspx.cs" Inherits="Eventomatic.rss_get" MasterPageFile="~/Site.Master" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
<table>
    <tr>
        <td><h1>Get RSS</h1></td>        
    </tr>
    <tr>
        <td>Below is the code you need to insert into your website to receive your custom event listing:</td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID=txtrssoutput runat=server TextMode=MultiLine Width=500px Height=200px CssClass="Rssgetbox"></asp:TextBox>
        </td>
    </tr>
</table>

</asp:Content>