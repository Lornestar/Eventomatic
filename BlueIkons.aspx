<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlueIkons.aspx.cs" Inherits="Eventomatic.BlueIkons" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <script language=javascript>
        theurl = "http://www.facebook.com/dialog/oauth?client_id=267553856650771&redirect_uri=http://apps.facebook.com/BlueIkons/Default.aspx&scope=email,publish_stream";
        top.location.href = theurl;
    </script>

    </div>
    </form>
</body>
</html>
