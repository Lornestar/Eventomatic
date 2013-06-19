<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Group_Members.aspx.cs" Inherits="Eventomatic.Group_Members" MasterPageFile="~/Site.Master" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
<script language="Javascript" src="Scripts.js"></script>
<script language=javascript>
function ConfirmRemoval()
{
    var answer = confirm("Are you sure you want to Remove the Group?")
	if (answer){
		__doPostBack('btnremovegroup','');
	}
	else{
	//
	}
    
}
</script>
<table width=100%>
    <tr>
        <td align=left><h3>Group Store Administrators</h3></td>        
        <td align=right>
        <div id="ci3aLf" style="z-index:100;position:absolute"></div><div id="sc3aLf" style="display:inline"></div><div id="sd3aLf" style="display:none"></div><script type="text/javascript">var se3aLf=document.createElement("script");se3aLf.type="text/javascript";var se3aLfs=(location.protocol.indexOf("https")==0?"https":"http")+"://image.providesupport.com/js/lornestar/safe-standard.js?ps_h=3aLf&ps_t="+new Date().getTime();setTimeout("se3aLf.src=se3aLfs;document.getElementById('sd3aLf').appendChild(se3aLf)",1)</script><noscript><div style="display:inline"><a href="http://www.providesupport.com?messenger=lornestar">Live Help Desk</a></div></noscript>
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
                    <td align="center" colspan="3"><asp:Label ID=lblError runat=server ForeColor=Red Visible=false>You cannot remove yourself from the list</asp:Label></td>
                </tr>
            </table>
        </td>     
    </tr>
    <tr>
        <td><!--
        <table width=500px>
            <tr>
                <td><fb:add-profile-tab /></td>
            </tr>
            <tr>
                <td>fb multi selector</td>
            </tr>
            <tr>
                <td>
                <fb:serverFbml >
    <script type="text/fbml">
      <fb:fbml>
          
 
                    <fb:multi-friend-input width="350px" border_color="#8496ba" name=ChosenAdmins />
        
      </fb:fbml>
 
    </script>
  </fb:serverFbml>
                </td>
            </tr>
        </table>        -->
        </td>
    </tr>    
    <!-- 
    <tr>
        <td width="0%"><h3>Your Group Stores</h3></td>        
        <td width="100%"><hr /></td>
    </tr>
    <tr>
        <td colspan=2 align=center>
            <table cellpadding=10>
                <tr>
                    <td>
                        Below are Group Stores that you are an administrator of:<br />
                        <asp:ListBox runat=server ID=lbYourGroups Width=200></asp:ListBox>
                        <br />
                        <asp:Button ID=btnremovegroup runat=server text="Delete Selected Group" 
                            OnClientClick="ConfirmRemoval()"></asp:Button>    
                    </td>                    
                </tr>
                <tr>
                    <td colspan=3><asp:Label ID=lblErrorNoName runat=server ForeColor=Red Visible=false></asp:Label></td>
                </tr>
            </table>
        </td>        
    </tr>
    
    <tr>
        <td width="0%"><h3>Create New Group Store</h3></td>        
        <td width="100%"><hr /></td>
    </tr>
    <tr>
        <td colspan=2 align=center>
            <table cellpadding=10>                
                <tr>
                    <td>
                        New Group Store Name: <asp:TextBox ID=txtNewGroup runat=server></asp:TextBox>
                        
                           <asp:Button ID=btnaddgroup runat=server Text="Create New Group" 
                            onclick="btnaddgroup_Click"/>
                    </td>
                </tr>
                <tr>
                    <td colspan=3><asp:Label ID=lblErrorNoName2 runat=server ForeColor=Red Visible=false></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>-->
</table>

</asp:Content>