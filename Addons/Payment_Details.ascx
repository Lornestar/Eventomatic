<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Payment_Details.ascx.cs" Inherits="Eventomatic.Addons.Payment_Details" %>

<asp:HiddenField ID=Tx_Key runat=server Value=0 />
<table>
    <tr> 
        <td><asp:Label ID=lblBuyertext runat=server>Buyer Name:</asp:Label></td>
        <td><asp:Label ID=lblName runat=server></asp:Label></td>
    </tr>
    <tr>
        <td>Buyer Email:</td>
        <td><asp:Label ID=lblEmail runat=server></asp:Label></td>
    </tr>
    <tr>
        <td>Transaction Amount:</td>
        <td><asp:Label ID=lblAmount runat=server></asp:Label></td>
    </tr>
    <tr>
        <td>Transaction Date:</td>
        <td><asp:Label ID=lblDate runat=server></asp:Label></td>
    </tr>
</table>
