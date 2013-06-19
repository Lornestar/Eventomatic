<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Eventomatic.Settings" MasterPageFile="~/Site.Master"%>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register src="Addons/Paypal_Confirmation.ascx" tagname="Paypal_Confirmation" tagprefix="uc2" %>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
<asp:Panel ID=pnlSettings runat=server>
<table width=100%> 
<tr>
        <td>
            <table width=100%>
                <tr>
                    <td align=left><h3>General Settings</h3></td>        
                    <td align=right>
                    <div id="Div4" style="z-index:100;position:absolute"></div><div id="Div5" style="display:inline"></div><div id="Div6" style="display:none"></div><script type="text/javascript">var se3aLf=document.createElement("script");se3aLf.type="text/javascript";var se3aLfs=(location.protocol.indexOf("https")==0?"https":"http")+"://image.providesupport.com/js/lornestar/safe-standard.js?ps_h=3aLf&ps_t="+new Date().getTime();setTimeout("se3aLf.src=se3aLfs;document.getElementById('sd3aLf').appendChild(se3aLf)",1)</script><noscript><div style="display:inline"><a href="http://www.providesupport.com?messenger=lornestar">Live Help Desk</a></div></noscript>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                    <telerik:RadComboBox ID="RadComboBox1" AllowCustomText=false runat="server" Width="250px" Label="Network"
                Height="200px" EmptyMessage="Select the Network your group belongs to" OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged"
                AutoPostBack="true">
            </telerik:RadComboBox>                    
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table width=100%>
                <tr>
                    <td align=left><h3>Paypal Settings</h3></td>                            
                </tr>
            </table>
        </td>
    </tr>
<tr>
<td>
<table>
    <tr valign=top>
        <td>
        
        
    <table width=100%>    
    <tr>
        <td colspan=2>What Paypal email would you like to receive your money?</td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Panel ID=pnlModifyTrial runat=server Visible=false>
            <table>
                <tr>
                    <td colspan=2>Which version of this application would you like to be running?</td>
                </tr>
                <tr>
                    <td align=center><asp:RadioButton runat=server ID=chkLive Text="Live" 
                            GroupName=Version AutoPostBack=true oncheckedchanged="chkLive_CheckedChanged" /></td>
                    <td align=center>
                    <asp:RadioButton runat=server ID=chkTrial Text="Trial" GroupName=Version 
                            AutoPostBack=true oncheckedchanged="chkTrial_CheckedChanged"/></td>
                </tr>
            </table>   
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan=2 align=center>
        <asp:Label ID=lblTrialNote runat=server ForeColor=Red>
            Only Admins should be able to use the Trial Mode.
            </asp:Label>
        <asp:Panel ID=pnlLive runat=server>
            <table>
            
            <tr>
        <td>PayPal Email:</td>
        <td><asp:TextBox ID="txtPaypal" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator id="RequiredFieldValidator1"  
      ControlToValidate="txtPaypal"
      Text="*" 
      runat="server" Enabled=false/></td>
    </tr>
    <tr>
        <td>Confirm PayPal Email:</td>
        <td><asp:TextBox ID="txtPaypalConfirm" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator id="RequiredFieldValidator2"  
      ControlToValidate="txtPaypalConfirm"
      Text="*" 
      runat="server" Enabled=false/>
        

        </td>
    </tr>    
    <tr>
        <td>Desired Currency:</td>
        <td><asp:DropDownList ID="ddlCurrency" runat="server">
                <asp:ListItem value="CAD">CAD</asp:ListItem>
                <asp:ListItem value="USD">USD</asp:ListItem>
                <asp:ListItem value="EUR">EUR</asp:ListItem>
                <asp:ListItem value="GBP">GBP</asp:ListItem>
                <asp:ListItem value="ILS">ILS</asp:ListItem>
            </asp:DropDownList></td>
    </tr>    
    <tr>
        <td style="font-size:smaller;" colspan=2>
        *Please note - Your PayPal account must be a Verified Premiere or Verified Business Account. <a href="http://promo.thegroupstore.com/faqs.aspx" target="_blank">Click here for details</a>
        </td>
    </tr>
            </table>
        </asp:Panel>
        </td>
    </tr>
    <tr height=50px valign=bottom>
                <td align=center><asp:Button ID="btnSave" runat="server" 
                        Text="Save Email" onclick="btnSave_Click" /></td>
            </tr>
    <tr>
        <td colspan="2" align="center">
            </td>
    </tr>
    
    
    
    <tr>
        <td colspan="2" align="center"><asp:Label ID=lblError runat=server ForeColor=Red Visible=false></asp:Label>
        <asp:CompareValidator id="Compare1" 
                    ControlToValidate="txtPaypal" 
                    ControlToCompare="txtPaypalConfirm" 
                    Type="String"
                    EnableClientScript="false" 
                    Text="Paypal Emails do not match." 
                    runat="server" Enabled=false/></td>
    </tr>   
    
</table>
</td>
    
    </tr>       
</table>
</td>
</tr>
<tr>
    <td>
        <uc2:Paypal_Confirmation ID="Paypal_Confirmation1" runat="server" />
    </td>
</tr>

<tr>
<td>
<asp:Panel ID=pnlRequestPermission runat=server >
<h3>Mobile Sales Force</h3>
                
<table>
    <tr>
        <td>
        <asp:Panel ID=pnlRequestPermission_Thepitch runat=server>
        Click below to enable Mobile Sales.  To enable Mobile Sales, you must give Groupstore Permission to do the following actions in PayPal on your behalf:
        <br />Perform Mobile_Checkout 
        <br />Direct_Payment 
        <br /><br />
        <asp:Button ID=btnRequestPermission runat=server Text="Enable Mobile Sales" 
                onclick="btnRequestPermission_Click" />
        <br /><span style="font-size:smaller;">Mobile Sales allows people to buy their tickets on their smartphones.  It is an optomized Checkout experience for iPhone, Android & Blackberry phones.</span>
        </asp:Panel>
        </td>
    </tr>    
    <tr>
        <td>
        <asp:Panel ID=pnlRequestPermission_AlreadyHave runat=server Visible=false>
        Mobile Sales is currently enabled.

        <asp:Button ID=btnCancelPerm runat=server Text="Disable Mobile Sales" 
                onclick="btnCancelPerm_Click" />
        </asp:Panel>
        </td>
    </tr>
</table>
</asp:Panel>

</td>
</tr>
<tr>
        <td align=left><h3>Group Store Administrators</h3></td>        
        <td align=right>
        <div id="Div1" style="z-index:100;position:absolute"></div><div id="Div2" style="display:inline"></div><div id="Div3" style="display:none"></div><script type="text/javascript">var se3aLf=document.createElement("script");se3aLf.type="text/javascript";var se3aLfs=(location.protocol.indexOf("https")==0?"https":"http")+"://image.providesupport.com/js/lornestar/safe-standard.js?ps_h=3aLf&ps_t="+new Date().getTime();setTimeout("se3aLf.src=se3aLfs;document.getElementById('sd3aLf').appendChild(se3aLf)",1)</script><noscript><div style="display:inline"><a href="http://www.providesupport.com?messenger=lornestar">Live Help Desk</a></div></noscript>
        </td>
    </tr>
    <tr>
        <td colspan=2 align="center">
            <table width=100%>
                <tr>
                    <td align=center colspan=3>
                        Add Administrators from your friends list to your Current Group Store,
                        <br />
                        or Remove Administrators from your Current Group Store.
                    </td>
                </tr>
                <tr valign="middle">
                    <td align=right>
                        Your Facebook Friends List<br />
                        <asp:ListBox ID="lbFriendsList" runat="server" Rows=10 Width=200></asp:ListBox>
                    </td>
                    <td align=center>                        
                        <asp:Button ID=btnAdd runat=server onclick="btnAdd_Click" Text=">" />
                        <br />
                        <asp:Button ID=btnRemove runat=server onclick="btnRemove_Click" Text="<" />
                                                
                    </td>
                    <td align=left>                   
                        <asp:Label ID=lblGroupName runat=server></asp:Label><br />
                        <asp:ListBox ID="lbAdmins" runat="server" Rows=10 Width=200>                            
                        </asp:ListBox>
                    </td>            
                </tr>
                <tr>
                    <td align="center" colspan="3"><asp:Label ID=Label1 runat=server ForeColor=Red Visible=false>You cannot remove yourself from the list</asp:Label></td>
                </tr>
            </table>
        </td>     
    </tr>
</table>
</asp:Panel>


</asp:Content>