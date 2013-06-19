<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Eventomatic.Login.Settings" MasterPageFile="~/Login/Site_Login.Master" %>

<%@ MasterType VirtualPath="~/Login/Site_Login.Master" %>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">   
    <script type="text/javascript">
        function pageLoad(sender, eventArgs) {
            if (!eventArgs.get_isPartialLoad()) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("InitialPageLoad");
            }
        }      
</script>   
</telerik:RadCodeBlock>   
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="pnlwholepage" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="pnlgeneral" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>                                                
            <telerik:AjaxSetting AjaxControlID="btnChangePayPal">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="pnlwholepage" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlPlan">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="pnlwholepage" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>         
            <telerik:AjaxSetting AjaxControlID="btnrefer1">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="pnlwholepage" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnrefer2">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="pnlwholepage" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnrefer3">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="pnlwholepage" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">  
</telerik:RadAjaxLoadingPanel> 

<asp:Panel ID=pnlwholepage runat=server>
<table width=100%>     
<tr>
        <td>
            <table width=100%>
                <tr>
                    <td align=left><h3>General Settings</h3></td>                            
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID=pnlgeneral runat=server>
            <table>
        <tr>
                            <td>
                                 <telerik:RadTextBox ID=txtStoreName2 runat=server Label="Business Name" EmptyMessage="Jewelery Makers Inc" Width=300></telerik:RadTextBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <telerik:RadComboBox ID=ddlCurrency runat=server Label="Desired Currency">
                                <Items>
                                    <telerik:RadComboBoxItem Value="CAD" Text="CAD" Selected=true/>
                                    <telerik:RadComboBoxItem Value="USD" Text="USD" />                                    
                                </Items>
                            </telerik:RadComboBox>
                            </td>
                        </tr>                        
                        <tr >
                            <td style="vertical-align:top;">
                                <telerik:RadTextBox ID=txtReceipt runat=server Label="e-Receipt" EmptyMessage="Thank you for your purchase.  We will accept refunds up to 14 days after purchase.  Please call 1-800-555-555 for questions." TextMode=MultiLine Width=300 MaxLength=500 Rows=3></telerik:RadTextBox>                            
                                <br />
                               <span style=" font-size:smaller;">*Your customers will receive their e-receipt immediately after a purchase.</span>
                            </td>
                        </tr>
                        <tr valign=bottom>
                            <td align=center><asp:Button ID="btnSave" runat="server" Text="Save General Settings" onclick="btnSave_Click" />
                            <br /><asp:Label ID=lblSaved runat=server ForeColor=Blue Visible=false>Changes Saved</asp:Label>
                            </td>
                        </tr>                        
                    </table>
                </asp:Panel>
        </td>
    </tr>    
    <tr>
        <td style="height:20px;">&nbsp;</td>
    </tr>
    <tr>
        <td >
            <table width=100%>
                <tr>
                    <td align=left><h3>Change Password</h3></td>                            
                </tr>                
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td><telerik:RadTextBox ID=txtpwdchange runat=server EmptyMessage="Enter New Password"></telerik:RadTextBox>
                    <telerik:RadButton ID=btnpwdchange runat=server OnClick=btnpwdchange_Click Text="Change"></telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Label ID=lblpwdchangefinish runat=server ForeColor=Blue Visible=false>Password has been changed</asp:Label>
                     </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height:20px;">&nbsp;</td>
    </tr>
    <tr>
        <td >
            <table width=100%>
                <tr>
                    <td align=left><h3>Payment Settings</h3></td>                            
                </tr>                
            </table>
        </td>
    </tr>
<tr>
<td>
<table>
    <tr>
                            <td>
                            <asp:Panel runat=server Visible=false>
                            <telerik:RadComboBox ID=ddlPlan runat=server Label="Choose Plan" AutoPostBack=true 
                            onselectedindexchanged="ddlPlan_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="Pay as you Go" Selected=true/>
                                    <telerik:RadComboBoxItem Value="1" Text="High Volume" />                                    
                                </Items>
                            </telerik:RadComboBox>
                            <span style="font-size:smaller;"><a href="http://www.snap-pay.com/site/pricing.aspx" target="_blank">See Plan Details</a></span>
                            </asp:Panel>
                            </td>
                        </tr>    
    <tr valign=top>
        <td>
        
        
    <table width=100%>    
        
                <tr>                    
                    <td>
                        <asp:Label ID=lblcurrentaccount runat=server>current@paypal.com</asp:Label><asp:Button runat=server ID=btnChangePayPal Text="Change PayPal Account" OnClick=btnChangePayPal_Click/>
                    </td>                    
                </tr>
                <tr>
                    <td >
                        <div style="font-size:smaller;">
                        <asp:Label ID=lbnotverified runat=server ForeColor=Blue Visible=false>
                            **Warning - Your PayPal account is current not verified.  This means that customer must create a new PayPal account or have an existing PayPal account when they pay you.
                            <br />
                            <a href="https://www.paypal.com/cgi-bin/webscr?cmd=p/acc/seal-CA-unconfirmed-outside">Click here to get your PayPal account verified</a>
                        </asp:Label>
                        <br />
                        <asp:Label ID=lblWPPLink runat=server Visible=false>
                                    <div style="color:Blue;">Warning: Your PayPal account does not have Website Payments Pro Activated.</div>
                                    Website Payments Pro (WPP) is optional, and is only required to ensure a faster checkout experience.<br />
                                    Without WPP you will need to collect your customers home billing address to complete the transaction.
                                    <br /><a href="##" target=_blank>Click here to activate Website Payments Pro</a>                                    

                                </asp:Label>
                            </div>
                    </td>
                </tr>
    </table>
</td>
    
    </tr>    
    <tr>
        <td style="height:20px;">&nbsp;</td>
    </tr>
    <tr>
        <td >
            <table width=100%>
                <tr>
                    <td align=left><h3>Refer Friends</h3></td>                            
                </tr>                
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td><asp:Label runat=server ID=lblReferStatus ForeColor=Blue>Snappay is available to you free for life</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Referral 1 - <telerik:RadTextBox ID=txtrefer1 runat=server EmptyMessage="Referral email"></telerik:RadTextBox><telerik:RadButton ID=btnrefer1 runat=server OnClick=btnrefer1_Click Text="Send Referral"></telerik:RadButton>
                        <br />Referral 2 - <telerik:RadTextBox ID=txtrefer2 runat=server EmptyMessage="Referral email"></telerik:RadTextBox><telerik:RadButton ID=btnrefer2 runat=server OnClick=btnrefer2_Click Text="Send Referral"></telerik:RadButton>
                        <br />Referral 3 - <telerik:RadTextBox ID=txtrefer3 runat=server EmptyMessage="Referral email"></telerik:RadTextBox><telerik:RadButton ID=btnrefer3 runat=server OnClick=btnrefer3_Click Text="Send Referral"></telerik:RadButton>
                        <asp:Label ID=lblerror runat=server ForeColor=Red Visible=false>Please enter a valid email</asp:Label>
                     </td>
                </tr>
            </table>
        </td>
    </tr>   
</table>
</td>
</tr>


</table>
</asp:Panel>
</asp:Content>