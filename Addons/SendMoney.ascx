<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendMoney.ascx.cs" Inherits="Eventomatic.Addons.SendMoney" %>

<asp:HiddenField ID=hdamount runat=server Value="0" />
<asp:HiddenField ID=hdresource_key runat=server Value="0" />
<asp:HiddenField ID=hdfbid runat=server Value="0" />
<asp:Panel ID=pnlsendmoney runat=server>
<table>
    <tr>
        <td align=left><h3>Send Money</h3></td>
        <td align=right>
            <div id="ci3aLf" style="z-index:100;position:absolute"></div><div id="sc3aLf" style="display:inline"></div><div id="sd3aLf" style="display:none"></div><script type="text/javascript">var se3aLf=document.createElement("script");se3aLf.type="text/javascript";var se3aLfs=(location.protocol.indexOf("https")==0?"https":"http")+"://image.providesupport.com/js/lornestar/safe-standard.js?ps_h=3aLf&ps_t="+new Date().getTime();setTimeout("se3aLf.src=se3aLfs;document.getElementById('sd3aLf').appendChild(se3aLf)",1)</script><noscript><div style="display:inline"><a href="http://www.providesupport.com?messenger=lornestar">Live Help Desk</a></div></noscript>
        </td>
    </tr>
    <tr>
        <td>
            To:
        </td>
        <td>
            <asp:Label ID=lbltoemail runat=server></asp:Label>
        </td>
    </tr>
    <tr>        
        <td colspan=2>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns=false BorderStyle="None" RowStyle-BorderStyle=None RowStyle-Wrap=true
         GridLines=None OnRowDataBound="GridView1_RowDataBound" Width=350px ShowHeader=true>
         <RowStyle Wrap="True" BorderStyle="None" VerticalAlign=Top></RowStyle>
         <HeaderStyle HorizontalAlign=Left />
            <Columns>
                <asp:BoundField DataField="Currency" HeaderText="Currency"/>
                <asp:BoundField DataField="Owed" HeaderText="Owed" />
                <asp:BoundField DataField="Fee" HeaderText="Fee" />
                <asp:BoundField DataField="Amount" HeaderText="Total Amount to Send" ItemStyle-HorizontalAlign=Right HeaderStyle-HorizontalAlign=Right ItemStyle-Font-Bold=true />                
            </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan=2 align=right>
        <asp:LinkButton  ID="lbSendMoney" runat="server" Text="Send Now"
        CssClass="OrderFormButtons" OnClientClick="btnSendMoney()"></asp:LinkButton>
           
        </td>
    </tr>
    <tr>
        <td colspan=2>
            <a href="https://cms.paypal.com/ca/cgi-bin/?cmd=_render-content&content_ID=developer/howto_api_masspay" target=_blank>Why is there a Paypal Transaction Fee of 2% max$1?</a>
        </td>
    </tr>
</table>
</asp:Panel>

<asp:Panel ID=pnlresult runat=server Visible=false>
<table>
    <tr>
        <td>The following amount has been sent to <asp:Label ID=lblpaypalemail runat=server></asp:Label></td>
    </tr>
    <tr>
        <td>
      <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns=false BorderStyle="None" RowStyle-BorderStyle=None RowStyle-Wrap=true
         GridLines=None OnRowDataBound="GridView1_RowDataBound" Width=350px ShowHeader=true>
         <RowStyle Wrap="True" BorderStyle="None" VerticalAlign=Top></RowStyle>
         <HeaderStyle HorizontalAlign=Left />
            <Columns>
                <asp:BoundField DataField="Amount" HeaderText="Amount to Send" ItemStyle-HorizontalAlign=Right HeaderStyle-HorizontalAlign=Right />                
                <asp:BoundField DataField="Result" HeaderText="Result" ItemStyle-HorizontalAlign=Right HeaderStyle-HorizontalAlign=Right/>
            </Columns>
            </asp:GridView>  
        </td>
    </tr>
    <tr>
        <td>Thank you for using Groupstore.</td>
    </tr>
</table>
</asp:Panel>

<script language="Javascript">
function btnSendMoney(){
document.getElementById("Warning").style.display = "block";
document.getElementById("Warning2").style.display = "block";
__doPostBack('btnSendMoney','');
}
</script>

<div id="Warning" class="QuestionPopup">

</div>
<div id="Warning2" class="QuestionPopup2">
<center>
    <table >
        <tr>
            <td align=center>
                <img src="/images/page_loader.gif" /> 
We are now Sending the money.

            </td>
        </tr>
    </table>
</center>
</div>