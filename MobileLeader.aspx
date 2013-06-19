<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileLeader.aspx.cs" Inherits="Eventomatic.MobileLeader" %>

<%@ Register src="Addons/LeaderBoard.ascx" tagname="LeaderBoard" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mobile Leader</title>
    <script language=javascript src="Addons/gosquared.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0b1/jquery.mobile-1.0b1.min.css" />
	<script type="text/javascript" src="http://code.jquery.com/jquery-1.6.1.min.js"></script>
	<script type="text/javascript" src="http://code.jquery.com/mobile/1.0b1/jquery.mobile-1.0b1.min.js"></script>
</head>
<body>
<div id="mobileleader" data-role="page"> 
<div  data-role="content">
    <form id="form1" runat="server">
    <center>
    Leader board
    <br />    
    <uc1:LeaderBoard ID="LeaderBoard1" runat="server" />
    </center>
    </form>
    </div>
    </div>
</body>
</html>
