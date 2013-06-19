<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResendTickets.aspx.cs" Inherits="Eventomatic.Addons.ResendTickets" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tickets</title>
    <script src="../Scripts.js" language="javascript" type="text/javascript"></script>    
    <link href="../Eventomatic.css" rel="stylesheet" type="text/css" />
</head>
<body onload="AdjustRadWidow();">
    <form id="Form2" method="post" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>     
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat=server LoadingPanelID="RadAjaxLoadingPanel1">
    <script type="text/javascript">

        function CloseWindow() {
            GetRadWindow().close('0');
        }
    </script>    
<div style="width: 400px; height:170px; text-align:center;">
    <table style="display:block; font-size:small; text-align:left;">
    <tr>
        <td>
            <table width=100% cellpadding=5 cellspacing=10>
            <tr>
                <td>A copy of the e-ticket will be resent to <asp:Label ID=lblresendemail runat=server/> </td>
            </tr>
            <tr>
                    <td class="QuestionSections">                                                
                                Enter below another email address that you would like to send a copy of this e-ticket:<br />
                        <telerik:RadTextBox ID=txtccemail runat=server Width="300px" EmptyMessage="your@email.com"/>
                    </td>
                </tr>                
            </table>
        
        </td>
    </tr>
    <tr>
        <td class="QuestionPopupFooter"><asp:Button ID=btnSaveTicket runat=server 
                Text="Resend eTicket"  OnClick="btnSaveTicket_Click"  /></td>
    </tr>
</table>   
       </div>
       </telerik:RadAjaxPanel>    
    </form>
</body>

</html>