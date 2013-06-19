<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventNewsFeed.ascx.cs" Inherits="Eventomatic.Addons.EventNewsFeed" %>
<asp:HiddenField ID=hdEvent_Key runat=server Value='0' />
<asp:HiddenField ID=hdonlytop runat=server Value='0' />
<link href="../Eventomatic_Stores.css" rel="stylesheet" type="text/css" />
<asp:Label ID=lblnoevents runat=server Visible=false Text="<br/>Nothing in the newsfeed for this event"></asp:Label>
<asp:GridView ID="GridView1" runat="server" ShowHeader=false AutoGenerateColumns=false BorderStyle="None" RowStyle-BorderStyle=None RowStyle-Wrap=true
         GridLines=None OnRowDataBound="GridView1_RowDataBound" AlternatingRowStyle-CssClass="StorePage_Gridview_bottomBorder" RowStyle-CssClass="Gridview_bottomBorder">
         <RowStyle Wrap="True" BorderStyle="None" VerticalAlign=Top></RowStyle>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>                    
                    <table style="width:100%; text-align:left;">
                        <tr valign=top >
                            <td style="width:32px;">
                            <asp:Image ID=imgSeller runat=server Width=30px/>                            
                            <asp:Label ID=lblfbid runat=server Visible=false Text='<%# Bind("fbid") %>'></asp:Label>
                            </td>
                            <td>
                            <span class='Activities'>
                            <asp:HyperLink ID=hypSeller runat=server Text='<%# Bind("Full_Name") %>' NavigateUrl="http://www.facebook.com/profile.php?id=0"/>
                            <asp:Label ID=lblActivitytext runat=server Text='<%# Bind("Activity_Text") %>'></asp:Label> <!--  added <fb:name capitalize='true' uid='588350732'></fb:name> as an administrator-->
                            </span>
                            <br />
                            <span class="Activities_Date">
                            <asp:Label ID=lbldate runat=server Text='<%# Bind("Confirmation_Date") %>'></asp:Label>
                            </span>        
                            </td>
                        </tr>                        
                    </table>                                                                              
                    </ItemTemplate>
                   </asp:TemplateField>                   
            </Columns>
        </asp:GridView>        
