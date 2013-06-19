<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Activities.ascx.cs" Inherits="Eventomatic.Addons.Activities" %>
<asp:HiddenField ID=hdResource_Key runat=server Value='0' />
<asp:HiddenField ID=hdLogAmount runat=server Value='0' />

<script language=javascript>

function init()
{               
    for (i=4;(obj=document.getElementById('ctl00_body_Activities1_GridView1_ctl0' + i + '_pnlhideall')) != null;i++)
    {                
        obj.style.display='none';        
    }
    for (i=10;(obj=document.getElementById('ctl00_body_Activities1_GridView1_ctl' + i + '_pnlhideall')) != null;i++)
    {                
        obj.style.display='none';        
    }
}

function changeview()
{
__doPostBack('Changeview','');
    /*for (i=4;(obj=document.getElementById('ctl00_body_Activities1_GridView1_ctl0' + i + '_pnlhideall')) != null;i++)
        {                
            if (obj.style.display=='none')
            {
                obj.style.display='block';
            }
            else
            {
                obj.style.display='none';
            }
        }
    for (i=10;(obj=document.getElementById('ctl00_body_Activities1_GridView1_ctl' + i + '_pnlhideall')) != null;i++)
        {                
            if (obj.style.display=='none')
            {
                obj.style.display='block';
            }
            else
            {
                obj.style.display='none';
            }
        }
*/
}
</script>

<table >
    <tr>
        <td><h3>Groupstore Log</h3></td>
        <td style="text-align:right;"><a href=# onclick='changeview(); return false;'><asp:Label ID=lblamount runat=server CssClass="Activities_Seeall">see all(AMOUNT)</asp:Label></a></td>
    </tr>

<tr>
    <td colspan=2>
<asp:GridView ID="GridView1" runat="server" ShowHeader=false AutoGenerateColumns=false BorderStyle="None" RowStyle-BorderStyle=None RowStyle-Wrap=true
         GridLines=None OnRowDataBound="GridView1_RowDataBound" AlternatingRowStyle-CssClass="StorePage_Gridview_bottomBorder" RowStyle-CssClass="Gridview_bottomBorder"  >
         <RowStyle Wrap="True" BorderStyle="None" VerticalAlign=Top></RowStyle>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                    <asp:Label id=lblfbid runat=server Text='<%# Bind("fbid") %>' Visible=false/>
                    <asp:Panel ID=pnlhideall runat=server>
                    <table style="border-bottom:solid 1px gray; width:100%; text-align:left;">
                        <tr valign=top >
                            <td style="width:32px;"><asp:Label ID=lblProfilepic runat=server Text=''></asp:Label></td>
                            <td>
                            <span class='Activities'>
                            <asp:Label ID=lblfbname runat=server Text=''></asp:Label>                            
                            <asp:Label ID=lblActivitytext runat=server Text='<%# Bind("Activity_Text") %>'></asp:Label> <!--  added <fb:name capitalize='true' uid='588350732'></fb:name> as an administrator-->
                            <asp:Label ID=lblfbid_added runat=server Text='<%# Bind("fbid_added") %>' Visible=false></asp:Label>
                            <asp:Label ID=lbltype runat=server Text='<%# Bind("Activity") %>' Visible=false></asp:Label>
                            <asp:Label ID=lblevent runat=server Text='<%# Bind("event_name") %>' Visible=false></asp:Label>
                            <asp:Label ID=lbltx_out_key runat=server Text='<%# Bind("tx_out_key") %>' Visible=false></asp:Label>
                            <asp:Label ID=lblPaypal_Email runat=server Text='<%# Bind("Paypal_Email") %>' Visible=false></asp:Label>
                            <asp:Label ID=lblAmount_Sent runat=server Text='<%# Bind("Amount_Sent") %>' Visible=false></asp:Label>
                            <asp:Label ID=lblCurrency_Sent runat=server Text='<%# Bind("Currency_Sent") %>' Visible=false></asp:Label>
                            </span>
                            <br />
                            <span class="Activities_Date">
                            <asp:Label ID=lbldate runat=server Text='<%# Bind("Date_Occured") %>'></asp:Label>
                            </span>        
                            </td>
                        </tr>                        
                    </table>                                                                              
                    </asp:Panel>
                    </ItemTemplate>
                   </asp:TemplateField>                   
            </Columns>
        </asp:GridView>        
        </td>
</tr>
</table>

<script language=javascript>
//init();
</script>