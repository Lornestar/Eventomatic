<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="fbloggedin.ascx.cs" Inherits="Eventomatic.Addons.fbloggedin" %>

<table  >
                        <tr>
                            <td><asp:Image ID=imgfbuser runat=server Height="30px" /> </td>
                            <td><asp:Label ID=lblfbuser runat=server>Logged in as </asp:Label>
                            <br />
                            <asp:Label ID=lblmessage runat=server></asp:Label>
                            </td>
                        </tr>                        
                    </table>                    
