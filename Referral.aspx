<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Referral.aspx.cs" Inherits="Eventomatic.Referral" MasterPageFile="~/Site.Master"%>

<%@ MasterType VirtualPath="~/Site.Master" %>



<%@ Register src="Addons/SendMoney.ascx" tagname="SendMoney" tagprefix="uc1" %>


<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
<script language=javascript>
function viewtxout()
{
document.getElementById('spnTransactions').style.display = "block";
}
</script>


<igcalc:UltraWebCalcManager ID=webcalc runat=server></igcalc:UltraWebCalcManager>
<asp:Panel ID=pnlSettings runat=server>
<table width=100%> 
    <tr>
        <td>
            <table width=100%>
                <tr>
                    <td align=left><h3>Referral</h3></td>        
                    <td align=right>
                    <div id="ci3aLf" style="z-index:100;position:absolute"></div><div id="sc3aLf" style="display:inline"></div><div id="sd3aLf" style="display:none"></div><script type="text/javascript">var se3aLf=document.createElement("script");se3aLf.type="text/javascript";var se3aLfs=(location.protocol.indexOf("https")==0?"https":"http")+"://image.providesupport.com/js/lornestar/safe-standard.js?ps_h=3aLf&ps_t="+new Date().getTime();setTimeout("se3aLf.src=se3aLfs;document.getElementById('sd3aLf').appendChild(se3aLf)",1)</script><noscript><div style="display:inline"><a href="http://www.providesupport.com?messenger=lornestar">Live Help Desk</a></div></noscript>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
<tr>
<td>
<table>
<tr>
    <td colspan=2>
    Refer a friend, and you can receive 50% of the Groupstore profits for 1 year.
    </td>
</tr>
    <tr valign=top>
        <td>
        
        
    <table width=100%>    
    <tr>
        <td colspan=2>What Paypal email would you like to receive money you made from the Referral Program?</td>
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
        <td colspan=2 align=center>
        <asp:Button ID="btnSave" runat="server" 
                        Text="Save Changes" onclick="btnSave_Click" />
        </td>
    </tr>
            </table>
        </asp:Panel>
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
    <td>
        <table width=100%>
            <tr>
                <td><!--Learn more about the Referral Program.--></td>
            </tr>
            <tr>
                <td>
                <asp:Panel ID=pnladdtab runat=server style="background-color:#EDEFF4; width:400px;" Visible=false>
                                    Refer friends from your Facebook Profile.<br />
                                     
                                        <fb:add-profile-tab type="off-facebook"></fb:add-profile-tab>         
                                 </asp:Panel>    
                </td>
            </tr>            
            <tr>
                <td>
                Refer friends by email. Type in their email address below, and we will email them the instructions for you.
                <br />
                <asp:TextBox ID=txtreferemail runat=server MaxLength=200 Width=200px></asp:TextBox>
                <br />
                <asp:Button ID=btnreferemail runat=server OnClick=btnreferemail_Click text="Refer Friend"/>
                <br />
                <asp:Label ID=lblreferemail runat=server ForeColor=Blue Visible=false>Your referral email has been sent.</asp:Label>
                </td>
            </tr>                                    
        </table>
    </td>
    </tr>    
</table>

</td>
</tr>
<tr>
    <td><hr /></td>
</tr>
<tr>
    <td align=left>Amount owed to you: <asp:Label ID=lblowed runat=server>$0.00</asp:Label>
    <br />
    <asp:Button ID=btnpaynow runat=server Text="Get Money now"  OnClick="btnpaynow_Click"/>
    </td>
</tr>
<tr>
    <td>
        <igtbl:UltraWebGrid ID="UltraWebGrid2" runat="server">
            <Bands>
<igtbl:UltraGridBand>
    <Columns>
        <igtbl:UltraGridColumn BaseColumnName="Resource_Key" Hidden="True">
            <Header Caption="Event_Key">
            </Header>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="FBCreator" Width="100px" >
            <FooterStyle BackColor="White" BorderColor=White/>
            <CellStyle Wrap="True">
            </CellStyle>
            <Header Caption="Referred">
                <RowLayoutColumnInfo OriginX="1" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="1" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Group_Name" Width="150px">            
        <CellStyle Wrap=true></CellStyle>
        <FooterStyle BackColor="White"  BorderColor=White/>
            <Header Caption="Groupstore">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Revenue" Width=100px>
            <FooterStyle BackColor="White"  BorderColor=White/>
            <Header Caption="Revenue Generated">
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="3" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Share" Width=100px>
            <CellStyle HorizontalAlign=Right Wrap=true></CellStyle>
            <Header Caption="Your Share">
                <RowLayoutColumnInfo OriginX="4" />
            </Header>
            <FooterStyle Wrap=true />
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Paid_Out" Key="Paid_Out"
        width=100px>
        <CellStyle Wrap=true HorizontalAlign=Right></CellStyle>
            <Header Caption="Amount Paid to you">
                <RowLayoutColumnInfo OriginX="5" />
            </Header>
            <FooterStyle Wrap=true />            
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn width=100px >
            <CellStyle Wrap=true HorizontalAlign=Right></CellStyle>
            <Header Caption="Amount Owing">
                <RowLayoutColumnInfo OriginX="6" />
            </Header>
            <FooterStyle Wrap=true />            
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="FBid_Name" Hidden=true>
            <Header Caption="FBid_Name">
                <RowLayoutColumnInfo OriginX="7" />
            </Header>            
        </igtbl:UltraGridColumn>        
    </Columns>
<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
</Bands>

<DisplayLayout Name="UltraWebGrid2" AllowColSizingDefault="Free" 
                AutoGenerateColumns="False" RowSelectorsDefault="No" 
                StationaryMarginsOutlookGroupBy="True" Version="4.00" 
                ViewType="Hierarchical" ColFootersVisibleDefault="Yes">
    <FrameStyle BorderColor="#999999" BorderStyle="None" BorderWidth="3px" 
        Cursor="Default">
    </FrameStyle>
    <RowAlternateStyleDefault BackColor="#F0D8FF">
        <BorderDetails ColorLeft="233, 233, 247" ColorTop="233, 233, 247" />
    </RowAlternateStyleDefault>
    <HeaderStyleDefault BackColor="#410067" BorderColor="Black" BorderStyle="Solid" 
        ForeColor="White">
        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
            WidthTop="1px" />
    </HeaderStyleDefault>
    <RowStyleDefault BackColor="#FFFFFF" BorderColor="Gray" BorderStyle="None" 
        BorderWidth="1px">
        <Padding Left="3px" />
        <BorderDetails ColorLeft="199, 213, 232" ColorTop="199, 213, 232" />
    </RowStyleDefault>
    <SelectedRowStyleDefault BackColor="#0A75F0" ForeColor="White">
    </SelectedRowStyleDefault>
<ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted"></ActivationObject>
    <FilterOptionsDefault>
        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
            BorderWidth="1px" CustomRules="overflow:auto;" 
            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
            <Padding Left="2px" />
        </FilterDropDownStyle>
        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
        </FilterHighlightRowStyle>
        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" 
            BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" 
            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
            <Padding Left="2px" />
        </FilterOperandDropDownStyle>
    </FilterOptionsDefault>
</DisplayLayout>
            </igtbl:UltraWebGrid>        
    </td>
</tr>
<tr>
    <td>
        <span id=spnTransactions style="display:none;">
        <h3>Transactions Out</h3><br />
        <igtbl:UltraWebGrid ID="UltraWebGrid3" runat="server">
            <Bands>
<igtbl:UltraGridBand>
    <Columns>
        <igtbl:UltraGridColumn BaseColumnName="Resource_Key" Hidden="True">
            <Header Caption="Resource_Key">
            </Header>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Group_Name" Width="150px" >
            <FooterStyle BackColor="White" BorderColor=White/>
            <CellStyle Wrap="True">
            </CellStyle>
            <Header Caption="Groupstore">
                <RowLayoutColumnInfo OriginX="1" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="1" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Email_Recipient">            
        <CellStyle Wrap="True">
            </CellStyle>
            <Header Caption="Paypal Email">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Amount_Total" >            
            <Header Caption="Amount">
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <Footer>
                <RowLayoutColumnInfo OriginX="3" />
            </Footer>
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="Paypal_Fee_Total" >            
            <Header Caption="Paypal Fee">
                <RowLayoutColumnInfo OriginX="4" />
            </Header>            
        </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="tx_date" DataType="System.DateTime" Format="dd/MM/yyyy hh:mm tt">
        <CellStyle Wrap=true ></CellStyle>
            <Header Caption="Date">
                <RowLayoutColumnInfo OriginX="5" />
            </Header>            
        </igtbl:UltraGridColumn>        
        <igtbl:UltraGridColumn BaseColumnName="FBid_Name">
        <CellStyle Wrap=true HorizontalAlign=Right></CellStyle>
            <Header Caption="Did Transaction">
                <RowLayoutColumnInfo OriginX="6" />
            </Header>            
        </igtbl:UltraGridColumn>        
        <igtbl:UltraGridColumn BaseColumnName="Currency" Hidden=true>
            <Header Caption="Currency">
                <RowLayoutColumnInfo OriginX="7" />
            </Header>            
        </igtbl:UltraGridColumn>        
        <igtbl:UltraGridColumn BaseColumnName="FBid" Hidden=true>
            <Header Caption="FBid">
                <RowLayoutColumnInfo OriginX="8" />
            </Header>            
        </igtbl:UltraGridColumn>        
    </Columns>
<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
</Bands>

<DisplayLayout Name="UltraWebGrid3" AllowColSizingDefault="Free" 
                AutoGenerateColumns="False" RowSelectorsDefault="No" 
                StationaryMarginsOutlookGroupBy="True" Version="4.00" 
                ViewType="Hierarchical" ColFootersVisibleDefault="No">
    <FrameStyle BorderColor="#999999" BorderStyle="None" BorderWidth="3px" 
        Cursor="Default">
    </FrameStyle>
    <RowAlternateStyleDefault BackColor="#F0D8FF">
        <BorderDetails ColorLeft="233, 233, 247" ColorTop="233, 233, 247" />
    </RowAlternateStyleDefault>
    <HeaderStyleDefault BackColor="#410067" BorderColor="Black" BorderStyle="Solid" 
        ForeColor="White">
        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
            WidthTop="1px" />
    </HeaderStyleDefault>
    <RowStyleDefault BackColor="#FFFFFF" BorderColor="Gray" BorderStyle="None" 
        BorderWidth="1px">
        <Padding Left="3px" />
        <BorderDetails ColorLeft="199, 213, 232" ColorTop="199, 213, 232" />
    </RowStyleDefault>
    <SelectedRowStyleDefault BackColor="#0A75F0" ForeColor="White">
    </SelectedRowStyleDefault>
<ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted"></ActivationObject>
    <FilterOptionsDefault>
        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
            BorderWidth="1px" CustomRules="overflow:auto;" 
            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
            <Padding Left="2px" />
        </FilterDropDownStyle>
        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
        </FilterHighlightRowStyle>
        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" 
            BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" 
            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
            <Padding Left="2px" />
        </FilterOperandDropDownStyle>
    </FilterOptionsDefault>
</DisplayLayout>
            </igtbl:UltraWebGrid>
        </span>
    </td>
</tr>
</table>
</asp:Panel>
<asp:Panel ID=pnlSendMoney runat=server Visible=false>
   <uc1:SendMoney ID="SendMoney1" runat="server" />
</asp:Panel>
</asp:Content>