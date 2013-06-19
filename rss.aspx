<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rss.aspx.cs" Inherits="Eventomatic.rss" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>rss</title>
    <link href="Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="GridView1" runat="server" ShowHeader=false AutoGenerateColumns=false BorderStyle="None" RowStyle-BorderStyle=None RowStyle-Wrap=true
         GridLines=None OnRowDataBound="GridView1_RowDataBound" Width=400px AlternatingRowStyle-CssClass="StorePage_Gridview_bottomBorder" RowStyle-CssClass="Gridview_bottomBorder"  >
         <RowStyle Wrap="True" BorderStyle="None" VerticalAlign=Top></RowStyle>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        
                        <table  style="height:100%; width:100%;">
                            <tr valign=top >
                                <td style="border-bottom:1px solid #e1e1e1;">
                                    <table style="height:100%; width:100%;">
                                        <tr valign=top>
                                            <td style="width:50px;">
                        <div class="CoolDate">-<asp:Label ID=lblDayofWeek runat=server></asp:Label>-<br /><asp:Label ID=lblDay runat=server></asp:Label><br /><asp:Label ID=lblMonth runat=server></asp:Label></div>                            
                                </td>
                                <td align=left><asp:HyperLink ID=hypEvent_Name runat=server Text='<%# Bind("Event_Name") %>'  NavigateUrl='~/Order_Form.aspx?Event_Key=' CssClass="StorePage_EventTitle"></asp:HyperLink>
                        <br />
                        <asp:HyperLink ID=hypEvent_Description runat=server Text='<%# Bind("Description") %>' CssClass="StorePage_EventText"></asp:HyperLink>
                        <asp:Label ID=lblEvent_Key runat=server Visible=false Text='<%# Bind("Event_Key") %>' ></asp:Label>
                        <asp:Label ID=lblEvent_Begins runat=server Visible=false Text='<%# Bind("Event_Begins") %>' ></asp:Label>                       
                                </td>
                                <td align=right style="width:0px">
                                    <asp:Image ID=ImgEvent runat=server Height="70px" />
                                </td>
                                        </tr>
                                    </table>
                                </td>
                                
                            </tr>
                        </table>                                               
                        
                    </ItemTemplate>
                   </asp:TemplateField>                   
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
