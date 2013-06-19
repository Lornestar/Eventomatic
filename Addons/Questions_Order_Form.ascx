<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Questions_Order_Form.ascx.cs" Inherits="Eventomatic.Addons.Questions_Order_Form" %>

<link href="Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />  

<asp:HiddenField ID=Event_Key runat=server Value=0 />
<asp:HiddenField ID=Resource_Key runat=server Value=0 />

<asp:GridView ID="GridView1" runat="server" ShowHeader=false AutoGenerateColumns=false BorderStyle="None" RowStyle-BorderStyle=None RowStyle-Wrap=true
         GridLines=None Width=300px AlternatingRowStyle-CssClass="StorePage_Gridview_bottomBorder" RowStyle-CssClass="Gridview_bottomBorder"  
         OnRowDataBound="GridView1_RowDataBound">
         <RowStyle Wrap="True" BorderStyle="None" VerticalAlign=Top ></RowStyle>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table >
                            <tr>
                                <td style="text-align:left;" align=left>
                                <asp:Label ID=lblMandatory runat=server ForeColor=Red Text="*" Visible=false></asp:Label>
                                 <asp:Label ID=Question_Text Text='<%# DataBinder.Eval(Container.DataItem,"The_Question") %>' runat=server CssClass="Each_Question"></asp:Label>:<br />
                                 <asp:TextBox ID=txtAnswer runat=server Width=300></asp:TextBox>
                                 <asp:DropDownList ID=ddlAnswer runat=server Visible=false></asp:DropDownList>
                                 <asp:Label ID=lblQuestionKey runat=server Text='<%# DataBinder.Eval(Container.DataItem,"Question_Key") %>' Visible=false></asp:Label>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
</asp:GridView>