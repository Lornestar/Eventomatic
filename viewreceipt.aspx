<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewreceipt.aspx.cs" Inherits="Eventomatic.viewreceipt" MasterPageFile="Snappay_Promo.Master" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID=content1 ContentPlaceHolderID=body runat=server>
<center>
<table>
            <tr>
                <td colspan=2>
                <center>
                    <asp:Panel runat=server ID=pnlpartone Height=100px>
                        <table>
                            <tr>
                                <td class="First_Time_Big_Questions">
                                <center>
                                    Please enter in your Receipt Number?
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID=txtreceiptkey runat=server EmptyMessage="ABCD1234" Width=100px/>
                                    <telerik:RadButton ID=btnsubmit runat=server Text="View Receipt" 
                                        onclick="btnsubmit_Click"></telerik:RadButton>
                                        <br />
                                        <asp:Label runat=server ForeColor=Blue Visible=false ID=lblerror>Receipt Number doesn't exist, please try again</asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    </center>
                </td>
            </tr>            
            <tr>
                <td colspan=2>
                <center>
                    <asp:Panel ID=pnlparttwo runat=server Visible=false>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID=lblthereceipt runat=server>
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    </center>
                </td>
            </tr>
        </table>
        </center>
    </asp:Content>