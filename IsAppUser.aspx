﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IsAppUser.aspx.cs" Inherits="Eventomatic.IsAppUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <asp:HiddenField ID=hdredirect runat=server Value=0 />
<script language=javascript>
top.location.href = document.getElementById('hdredirect').value;
</script>    
    </div>
    </form>
</body>
</html>