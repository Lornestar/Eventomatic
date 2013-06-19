<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestGraph.aspx.cs" Inherits="Eventomatic.TestGraph" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
               fb multi selector<br />
          <fb:serverFbml style="width: 755px;">
    <script type="text/fbml">
      <fb:fbml>
          <fb:request-form
                    action="<url for post invite action, see wiki page for fb:request-form for details>"
                    method="POST"
                    invite="true"
                    type="XFBML"
                    content="This is a test invitation from XFBML test app
                 <fb:req-choice url='see wiki page for fb:req-choice for details'
                       label='Ignore the Connect test app!' />
              "
              >
 
                    <fb:multi-friend-selector
                    showborder="false"
                    actiontext="Invite your friends to use Connect.">
        </fb:request-form>
      </fb:fbml>
 
</body>
</html>
