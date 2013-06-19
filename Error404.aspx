<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error404.aspx.cs" Inherits="Eventomatic.Error404" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Groupstore Error</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Error Page 404<br />
    you are looking for page <asp:Label ID=lblpath runat=server></asp:Label>
    <br />
    <asp:Label ID=lblpath2 runat=server></asp:Label>
    </div>
    </form>
</body>
</html>
