function makeHttpRequest(storeid)
      {
        //===============================
        // Define http_request       
        var httpRequest;
        try 
        {
          httpRequest = new XMLHttpRequest();  // Mozilla, Safari, etc
        }
        catch(trymicrosoft)
        {
          try
          {
            httpRequest = new ActiveXObject("Msxml2.XMLHTTP");
          }
          catch(oldermicrosoft)
          {
            try
            {
              httpRequest = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch(failed)
            {
              httpRequest = false;
            }
          }
        }
        if(!httpRequest)
        {
          alert('Your browser does not support Ajax.');
          return false;
        }
       //===============================
        // Action http_request
        httpRequest.onreadystatechange = function()
        {
          if(httpRequest.readyState == 4)
            if(httpRequest.status == 200)                
                document.getElementById('groupstoreoutput').innerHTML = httpRequest.responseText;
              //alert(httpRequest.responseText);
            else
              alert('Request Error: '+httpRequest.status);
        }
        httpRequest.open('GET','http://www.thegroupstore.com/rss.aspx?storeid='+storeid,true);
        httpRequest.send(null);       
      }
 //makeHttpRequest();
 
 function getquery(){
      // get the current URL
 var url = window.location.toString();
 //get the parameters
 url.match(/\?(.+)$/);
 var params = RegExp.$1;
 // split up the query string and store in an
 // associative array
 var params = params.split("&");
 var queryStringList = {};
 
 for(var i=0;i<params.length;i++)
 {
     var tmp = params[i].split("=");
     queryStringList[tmp[0]] = unescape(tmp[1]);
 }
 
 // print all querystring in key value pairs
 for(var i in queryStringList)
     //document.write(i+" = "+queryStringList[i]+"<br/>");
     makeHttpRequest(queryStringList[i]);
     }
     getquery();
     
     