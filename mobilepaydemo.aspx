<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mobilepaydemo.aspx.cs" Inherits="Eventomatic.mobilepaydemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:HiddenField ID="hdredirecturl" runat=server Value="0" />
    
    <script language=javascript>
        alert("This is a demo with Fake Money.  Complete the transaction with the following PayPal account: username = demo@thegroupstore.com & password = groupstore");
        location.href = document.getElementById("hdredirecturl").value;
    </script>
    </div>
    </form>
</body>
</html>
