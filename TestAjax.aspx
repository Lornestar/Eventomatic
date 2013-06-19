<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestAjax.aspx.cs" Inherits="Eventomatic.TestAjax" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Facebook Events</title>
    <script src="../Scripts.js" language="javascript" type="text/javascript"></script>
    <link href="../Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />  
</head>
<body onload="AdjustRadWidow();">
    <form id="Form2" method="post" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">   
<script type="text/javascript">
    function pageLoad(sender, eventArgs) {
        if (!eventArgs.get_isPartialLoad()) {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("InitialPageLoad");
        }
    }      
</script>   
</telerik:RadCodeBlock>   
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
   <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
           <UpdatedControls>
               <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
           </UpdatedControls>
       </telerik:AjaxSetting>
   </AjaxSettings>
</telerik:RadAjaxManager>
<asp:Panel ID="Panel1" runat="server">
   <asp:Panel ID="Panel2" Visible="false" runat="server">
       My content:
   </asp:Panel>
</asp:Panel>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Height="75px" Width="75px" Transparency="50">
   <img alt="Loading..." src="loading.gif" style="border:0;" />
</telerik:RadAjaxLoadingPanel> 

    </form>
</body>
</html>