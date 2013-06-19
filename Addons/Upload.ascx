<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Upload.ascx.cs" Inherits="Eventomatic.Addons.Upload" %>
<asp:HiddenField ID=Event_Key runat=server Value=0 />
<asp:HiddenField ID=Resource_Key runat=server Value=0 />
<asp:HiddenField ID=Current_Resource runat=server Value=0 />
<table><tr><td>
                            <table>
                                    <tr>
                                        <td>
                                            <INPUT id="fileUpload" type="file" Runat="server" NAME="fileUpload">
                                        </td>
                                        <td><asp:Button ID="btnImage" runat=server Text="Upload Image" 
                                                onclick="btnImage_Click" /></td>
                                        
                                    </tr>
                                    <tr>
                                        <td colspan=2><asp:Label ID=lblCurrentImage runat=server>No Event Image has been uploaded. A Standard image will be used.</asp:Label>
                                        <asp:Label ID=lblImageError runat=server Visible=false></asp:Label>
                                        </td>
                                    </tr>
                                </table>                                
                                </td>
                                <td >
                                <asp:Panel ID=pnlImg runat=server visible=false CssClass="UploadImageArea">
                                <table ><tr>
                                <td><asp:Image ID=ImgEvent runat=server  Width=100px/></td>
                                    <tr>
                                        <td><center><asp:Button ID=btnDeleteImage runat=server Text="Delete" 
                                                onclick="btnDeleteImage_Click"/></center></td>
                                    </tr>
                                 </table>
                                 </asp:Panel>
                                 </td>
                                </tr></table>