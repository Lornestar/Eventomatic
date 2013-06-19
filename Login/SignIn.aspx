<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="Eventomatic.Login.SignIn" MasterPageFile="~/Login/Site_Login.Master"%>

<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
<div style="vertical-align:middle; height:150px; width:100%;">

<center>
<span id='signin' >
<table>        
    <tr>
        <td><center> <div style="font-size:large;">Please Sign In</div> </center></td>
    </tr>
    <tr>
        <td>
            <telerik:RadTextBox ID=txtemail runat=server Label="Email" Font-Size=Large Width=300/>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadTextBox ID=txtpwd runat=server Label="Password" TextMode=Password Font-Size=Large Width=300/>
        </td>
    </tr>
    <tr>
        <td><center>
            <telerik:RadButton ID=btnsignin runat=server Text="Sign In" OnClick=btnsignin_Click Width=200/>
            <br />
            <asp:Label ID=lblerror runat=server ForeColor=Red Visible=false>Incorrect Email & Password</asp:Label>
            </center>
        </td>
    </tr>
    <tr>
        <td style="height:30px;">
            <a href=# onclick='Toggleforgot()'>Forgot Password</a>
        </td>
    </tr>
</table>
</span>
</center>

<center>
<span id='forgotpwd' style="display:none;">
<table>
    <tr>
        <td>
            <telerik:RadTextBox ID=txtforgotpwd runat=server Width=300 EmptyMessage="Email Address" Label="Forgot Password"/>
        </td>
    </tr>
    <tr>
        <td><center>
            <telerik:RadButton ID=btnforgotpwd runat=server Text="Sign In" OnClick=btnforgotpwd_Click Width=200/>
            <br />
            <asp:Label ID=lblforgotpwdsent runat=server ForeColor=Blue Visible=false>Password has been emailed to you</asp:Label>
            </center>
        </td>
    </tr>
    <tr>
        <td style="height:30px;">
            <a href=# onclick='Toggleforgot()'>Login</a>
        </td>
    </tr>
</table>
</span>
</center>

</div>

</telerik:RadAjaxPanel>

<script language=javascript>
    
    function Toggleforgot() {

        if (document.getElementById("forgotpwd").style.display == 'none') {

            document.getElementById("forgotpwd").style.display = 'block';
            document.getElementById("signin").style.display = 'none';
        }
        else {
            document.getElementById("forgotpwd").style.display = 'none';
            document.getElementById("signin").style.display = 'block';
        }
    }

</script>
</asp:Content>