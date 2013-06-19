<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Eventomatic.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html  >
<head id="Head1" runat="server">
<title>ASP.NET AJAX Web Services: Web Service Sample Page</title>

 <script type="text/javascript"  src="http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js">   
</script>  

   
  <script type="text/javascript">

      $(document).ready(function () {

          $("#sayHelloButton").click(function (event) {
              $.ajax({
                  type: "POST",
                  url: "http://localhost:49450/dummyWebsevice.asmx/sayHello",                  
                  success: function (msg) {
                      AjaxSucceeded(msg);
                  },
                  error: AjaxFailed

              });
          });

      });
      function AjaxSucceeded(result) {
          alert(result.d);
      }

      function AjaxFailed(result) {
          alert(result.status + ' ' + result.statusText);
      }  


  </script>  
</head>
<body>
    <form id="form1" runat="server">
     <h1> Calling ASP.NET AJAX Web Services with jQuery </h1>
     Enter your name: 
        <input id="name" />
        <br />
        <input id="sayHelloButton" value="Say Hello"
               type="button"  />

    </form>
</body>
</html>
