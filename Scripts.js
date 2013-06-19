function AddListbox()
{
    var source = document.getElementById('ctl00_body_ddlFriendsList');
    var target = document.getElementById('ctl00_body_lbAdmins');
    //var count =  source.length;
    //for (var i = count - 1; i >= 0; i--)
    //{
        var opt = document.createElement("Option");
        var i = source.selectedIndex;
        opt.text = source[i].text;
        opt.value= source[i].value;
        
        
        target.options.add(opt);
        source.remove(i);
        //alert(item);
        /*if(item.selected)
        {
            //source.remove(i);
            target.add(item);
        }*/
    //}
}
function strLeft(str, n){
	if (n <= 0)
	    return "";
	else if (n > String(str).length)
	    return str;
	else
	    return String(str).substring(0,n);
}
function strRight(str, n){
    if (n <= 0)
       return "";
    else if (n > String(str).length)
       return str;
    else {
       var iLen = String(str).length;
       return String(str).substring(iLen, iLen - n);
    }
}

function emailcheck(str) { //checks email addresses

		var at="@"
		var dot="."
		var lat=str.indexOf(at)
		var lstr=str.length
		var ldot=str.indexOf(dot)
		if (str.indexOf(at)==-1){
		   
		   return false
		}

		if (str.indexOf(at)==-1 || str.indexOf(at)==0 || str.indexOf(at)==lstr){
		   
		   return false
		}

		if (str.indexOf(dot)==-1 || str.indexOf(dot)==0 || str.indexOf(dot)==lstr){
		   
		    return false
		}

		 if (str.indexOf(at,(lat+1))!=-1){
		   
		    return false
		 }

		 if (str.substring(lat-1,lat)==dot || str.substring(lat+1,lat+2)==dot){
		   
		    return false
		 }

		 if (str.indexOf(dot,(lat+2))==-1){
		   
		    return false
		 }
		
		 if (str.indexOf(" ")!=-1){
		   
		    return false
		 }

 		 return true					
	}
	
	function Callimportfb(){                     
        document.getElementById('ctl00_hdCanvasSize').value = "950";        
        __doPostBack('ImportfbEvent','');     
       
    }
    
    function popup4(){            
         var back = document.getElementById("ImportFB");
         back.style.display='block';
          var topop = document.getElementById("ImportFB2");
          topop.style.display='block';                              
          
          
          }
   function closepopup4(){
        var back = document.getElementById("ImportFB");
         back.style.display='none';
          var topop = document.getElementById("ImportFB2");
          topop.style.display='none';
          document.getElementById('ctl00_hdCanvasSize').value = "0";
          CheckCanvas(true);
      }      
      
      function GetRadWindow()
        {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        

        function AdjustRadWidow()
        {
            var oWindow = GetRadWindow();
            setTimeout(function () 
            {
                oWindow.autoSize(true);
                if ($telerik.isChrome || $telerik.isSafari) 
                {
                    ChromeSafariFix(oWindow);
                }
                else 
                {
                    var bounds = oWindow.getWindowBounds();
                    var x = bounds.x;
                    oWindow.moveTo(x, 50);
                }

            }, 310);  
        }

        //fix for Chrome/Safari due to absolute positioned popup not counted as part of the content page layout
        function ChromeSafariFix(oWindow)
        {
            var iframe = oWindow.get_contentFrame();
            var body = iframe.contentWindow.document.body;

            setTimeout(function ()
            {
                var height = body.scrollHeight;
                var width = body.scrollWidth;

                var iframeBounds = $telerik.getBounds(iframe);
                var heightDelta = height - iframeBounds.height;
                var widthDelta = width - iframeBounds.width;

                if (heightDelta > 0) oWindow.set_height(oWindow.get_height() + heightDelta);
                if (widthDelta > 0) oWindow.set_width(oWindow.get_width() + widthDelta);
                oWindow.center();
                var bounds = oWindow.getWindowBounds();
                var x = bounds.x;
                oWindow.moveTo(x,50);

            }, 310);
        }
      
      //<![CDATA[
        function openWin(fbid,resourcekey,type,rooturl) {            
            var strrooturl = "&rooturl=1";
            if (rooturl == "True") {
                strrooturl = "";
            }
            var oWnd = radopen("addons/FacebookEvents.aspx?fbid=" + fbid + "&resourcekey=" + resourcekey + "&type=" + type + strrooturl, "RadWindow1");
        }

        function OnClientClose(oWnd, args) {
            
            //get the transferred arguments                        
            var arg = args.get_argument();
            if (arg) 
            {
                
                var eid = arg.eid;
                var url = arg.url;
                var prod = arg.prod;
                var resourcekey = arg.resourcekey;
                
                if (eid) {
                    __doPostBack('FacebookEvents', eid);
                }
                else if (url) {
                
                    if (prod) {
                        top.location.href = 'http://www.thegroupstore.com/order_form2.aspx?eid=' + url + '&storeid=' + resourcekey + '&edit=true';
                    }
                    else {
                        top.location.href = 'http://localhost:50495/order_form2.aspx?eid=' + url + '&storeid=' + resourcekey + '&edit=true';
                    }
                    
                }
                //$get("order").innerHTML = "You chose eid <strong>" + eid + "</strong>";
            }
        }
      
      //Tickets open
        function openWinTickets(tixid,eventkey)
        {
            var oWnd = radopen("addons/Tickets2.aspx?tixid=" + tixid + "&eventkey="+eventkey, "Tickets2");
        }

        function OnClientCloseTickets(sender, args)
        {            
            //get the transferred arguments
            //var arg = args.get_argument();
            var arg = args.get_argument();
            if (arg)
            {
                __doPostBack('ReloadTickets','');                        
            }
        }

        //Questions open
        function openWinQuestions(tixid, eventkey) {
            var oWnd = radopen("addons/Questions2.aspx?tixid=" + tixid + "&eventkey=" + eventkey, "Questions2");
        }

        function OnClientCloseQuestions(sender, args) {
            //get the transferred arguments
            //var arg = args.get_argument();
            var arg = args.get_argument();
            if (arg) {
                __doPostBack('ReloadQuestions', '');
            }
        }

        function openWinfbGuestList(eventkey,viewtype) {            
            var oWnd = radopen("addons/fbGuestList.aspx?eid=" + eventkey + "&viewtype=" + viewtype, "Guest List");
        }

        function OnClientClosefbGuestList(sender, args) {                        
            var arg = args.get_argument();
            
            if (arg) {
                location.href = arg.fbid;
            }
        }

        function openWinBackground(eventkey) {
            var oWnd = radopen("addons/BackgroundImage2.aspx?&eventkey=" + eventkey, "Background2");
        }

        function OnClientCloseBackground(sender, args) {            
            var arg = args.get_argument();
            if (arg) {
                __doPostBack('ReloadBackground', '');
            }
        }

        function openWinSelltix(eventkey) {
            var oWnd = radopen("mobile.aspx?event=" + eventkey + "&frompc=true","Selltix");
        }

        function openWinShareApp() {
            var oWnd = radopen("mobileshare.aspx?tx=app", "ShareApp");
        }

        function fileUploaded(sender, args) {
            $find('RadAjaxManager1').ajaxRequest();
            $telerik.$(".invalid").html("");
            sender.deleteFileInputAt(0);
        }

        function validationFailed(sender, args) {
            $telerik.$(".invalid")
                .html("Invalid File. Must be of type 'jpg' and under 500k");
            sender.deleteFileInputAt(0);
        }

        function OpenNewfbevent(theurl) {
            window.open(theurl, 'Groupstore Event', '');
        }

        //Resend eTickets open
        function openWinResendTickets(txkey) {
            var oWnd = radopen("addons/ResendTickets.aspx?txkey=" + txkey , "ResendTickets");
        }

        function OnClientCloseResendTickets(sender, args) {
        }

        //Confirmation Social open
        function openWinConfirmationSocial(eventkey) {
            var oWnd = radopen("addons/Confirmation_Social.aspx?eventkey=" + eventkey, "Confirmation_Social");                        
        }

        function OnClientCloseConfirmationSocial(sender, args) {
            document.getElementById("tzcdFrame").style.display = 'none';
        }




        function getScreenCenterY() {
            var y = 0;

            y = getScrollOffset() + (getInnerHeight() / 2);

            return (y);
        }

        function getScreenCenterX() {
            return (document.body.clientWidth / 2);
        }

        function getInnerHeight() {
            var y;
            if (self.innerHeight) // all except Explorer
            {
                y = self.innerHeight;
            }
            else if (document.documentElement &&
document.documentElement.clientHeight)
            // Explorer 6 Strict Mode
            {
                y = document.documentElement.clientHeight;
            }
            else if (document.body) // other Explorers
            {
                y = document.body.clientHeight;
            }
            return (y);
        }

        function getScrollOffset() {
            var y;
            if (self.pageYOffset) // all except Explorer
            {
                y = self.pageYOffset;
            }
            else if (document.documentElement &&
document.documentElement.scrollTop)
            // Explorer 6 Strict
            {
                y = document.documentElement.scrollTop;
            }
            else if (document.body) // all other Explorers
            {
                y = document.body.scrollTop;
            }
            return (y);
        }