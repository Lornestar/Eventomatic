<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Paypal_Confirmation.ascx.cs" Inherits="Eventomatic.Addons.Paypal_Confirmation" %>
<asp:HiddenField ID=hdemail runat=server Value="" />
<asp:Panel ID=pnlisconfirmed runat=server Visible=false>
<asp:Label ID=lblisconfirmed runat=server ForeColor=Blue>Email address has been confirmed</asp:Label>
</asp:Panel>
<asp:Panel ID=pnlnotconfirmed runat=server>
<hr />
<table>
    <tr>
        <td style="color:Blue; text-align:center;">        
        **GroupStore cannot send you money until you confirm your PayPal email address**<br />
        To confirm your PayPal email address enter in the amount of money Groupstore
        sent on <asp:Label ID=lbldatesent runat=server></asp:Label> EST:
        </td>        
    </tr>
    <tr>
        <td style="text-align:center;">
        <igtxt:WebCurrencyEdit ID="Amount_Sent" runat="server" DataMode="Decimal" 
                            HorizontalAlign="Left" Width="100px" ValueText="0.00" >
                            <ClientSideEvents ValueChange="PriceValueChanged" />
                        </igtxt:WebCurrencyEdit>                         
         <asp:Button ID=btnenteramount runat=server text="Enter Amount" 
                onclick="btnenteramount_Click" />
                <asp:Label ID=lblincorrect runat=server ForeColor=Red Visible=false>Incorrect Amount, please try again.</asp:Label>
                </td>
    </tr>
</table>
</asp:Panel>