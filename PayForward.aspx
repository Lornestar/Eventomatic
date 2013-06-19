<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayForward.aspx.cs" Inherits="Eventomatic.PayForward" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payforward</title>
    <script type="text/javascript" src="Addons/gosquared.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0b1/jquery.mobile-1.0b1.min.css" />
	<script type="text/javascript" src="http://code.jquery.com/jquery-1.6.1.min.js"></script>
	<script type="text/javascript" src="http://code.jquery.com/mobile/1.0b1/jquery.mobile-1.0b1.min.js"></script>
    <style type="text/css">
        .keypad
        {
            width:60px;
        }
    </style>
</head>
<body onload="initpage()">
    <form id="form1" runat="server">
    <asp:HiddenField ID=hdOverallTotal Value="0" runat=server />
<asp:HiddenField ID=hdGoToPayment Value="0" runat=server />
<asp:HiddenField ID=hdFirstName Value="" runat=server />    
<asp:HiddenField ID=hdLastName Value="" runat=server />
<asp:HiddenField ID=hdTrial Value="False" runat=server />
<asp:HiddenField ID=hdLastTicketDate Value="0" runat=server />
<asp:HiddenField ID=hdCurrentDate Value="0" runat=server />
<asp:HiddenField ID=hdspecificuser Value="False" runat=server />
<asp:HiddenField ID=hdgvoffset Value=0 runat=server />
<asp:HiddenField ID=hdcurrencysymbol Value=0 runat=server />
<asp:HiddenField ID=hdnfbid runat=server Value=0 />
<asp:HiddenField ID=hdnfbname runat=server Value=0 />
<asp:HiddenField ID="hdeventkey" runat=server Value=0 />
<asp:HiddenField ID="hdisdemopay" runat=server Value=0 />
<asp:HiddenField ID="hdgotourl" runat=server Value="" />
<asp:HiddenField ID="hdkeypadplace" runat=server Value="0" />
<asp:HiddenField ID="hdonsms" runat=server Value="0" />
<asp:HiddenField ID="hdipaddress" runat=server Value="0" />

    <div id="payforward" data-role="page"> 
	<div  data-role="header" style="height:35px;">                            
            <table width=100%>
            <tr>
                <td>
                <asp:LinkButton style="text-align:left;" id="btnContinue" Text="Pay Here" OnClick="btnContinue_Click" runat=server data-role="button" data-theme="b" data-icon="delete"/>
                </td>
                <td >
                </td>
                <td style="text-align:right;">
                <div id=pfback onclick=btnPayForward() data-theme="b"  data-icon="arrow-l" data-iconpos="left" data-role="button" style="display:none;">Back</div>
                <div id=pf onclick=btnPayForward() data-theme="b"  data-icon="arrow-r" data-iconpos="right" data-role="button">Pay Forward</div>
                <asp:LinkButton id="btnPayForward" class="Mobile_SellNow" OnClientClick="btnPayForward()" Text="Pay Forward"  runat=server data-role="none" data-theme="b"  data-icon="arrow-r" data-iconpos="right" Visible=false/>
                </td>
            </tr>
            </table>
    
    </div> 
	<div  data-role="content" style="height:440px;">
    
    
    <div id="fbname">
        <span id="fbcheckedin" style="display:none;"></span>                
    </div>
    
                    <div >
                        <asp:TextBox ID=txtamount runat=server style="width:280px; font-weight:bolder; font-size:larger;"></asp:TextBox>
                    </div>
                    <div style="width:300px;">
                        <a id="addnoteatag" href=# data-role="button"  data-icon="arrow-d" onclick="AddNote()">
                        <span class="ui-btn-text">Add a Note</span>
                        </a>        
                            <div id="thenote" style="display:none;width:300px;" >
                            <asp:TextBox ID=txtnote runat=server TextMode=MultiLine Height="100"></asp:TextBox>            
                            </div>        
                    </div>
                                
                <div id=sms style="background-color:#ECECEC; width:320px; height:0px; position:absolute; top:71px; left:10px; text-align:center; display:none;">
                    <div >
                        <asp:TextBox ID=txtsms runat=server style="width:280px; font-size:larger;" text="(###) ###-####"></asp:TextBox>
                    </div>
                    <div>
                        <div id=btncustomerphone onclick=btnsendsms() data-theme="b" style="width:280px;" data-iconpos="right" data-role="button">Enter Customer Phone</div>
                    </div>                    
                </div>                
    
    <div id=keypad style="text-align:center;">
        <div class="ui-grid-b">
	        <div class="ui-block-a">
                <div class="ui-bar ui-bar-d">
                    <a href="#" onclick="keypadpress(1)" class="keypad" data-role="button">1</a>
                </div>
            </div>
	        <div class="ui-block-b">
                <div class="ui-bar ui-bar-d">
                    <a href="#" onclick="keypadpress(2)" class="keypad" data-role="button">2</a>
                </div>
            </div>
	        <div class="ui-block-c">
                <div class="ui-bar ui-bar-d" >
                    <a href="#" onclick="keypadpress(3)" class="keypad" data-role="button">3</a>
                </div>
            </div>
            <div class="ui-block-a">
                <div class="ui-bar ui-bar-d">
                    <a href="#" onclick="keypadpress(4)" class="keypad" data-role="button">4</a>
                </div>
            </div>
	        <div class="ui-block-b">
                <div class="ui-bar ui-bar-d">
                    <a href="#" onclick="keypadpress(5)" class="keypad" data-role="button">5</a>
                </div>
            </div>
	        <div class="ui-block-c">
                <div class="ui-bar ui-bar-d">
                    <a href="#" onclick="keypadpress(6)" class="keypad" data-role="button">6</a>
                </div>
            </div>
            <div class="ui-block-a">
                <div class="ui-bar ui-bar-d">
                    <a href="#" onclick="keypadpress(7)" class="keypad" data-role="button">7</a>
                </div>
            </div>
	        <div class="ui-block-b">
                <div class="ui-bar ui-bar-d">
                    <a href="#" onclick="keypadpress(8)" class="keypad" data-role="button">8</a>
                </div>
            </div>
	        <div class="ui-block-c">
                <div class="ui-bar ui-bar-d">
                    <a href="#" onclick="keypadpress(9)" class="keypad" data-role="button">9</a>
                </div>
            </div>
            <div class="ui-block-a">
                <div class="ui-bar ui-bar-d" style="height:30px;">
                    
                </div>
            </div>
	        <div class="ui-block-b"  onclick="keypadpress(0)">
                <div class="ui-bar ui-bar-d">
                    <a href="#" onclick="keypadpress(0)" class="keypad" data-role="button">0</a>
                </div>
            </div>
	        <div class="ui-block-c">
                <div class="ui-bar ui-bar-d" >
                    <a href="#" onclick="refreshkeypad()" class="keypad" style="height:28px;" data-role="button" data-icon="refresh"  data-iconpos="top"></a>
                </div>
            </div>
        </div>        

    </div>
    </div>    
    </form>
    <div id="fb-root"></div>    
    <script src="http://connect.facebook.net/en_US/all.js"></script>
    <script language=javascript>

    function btnsendsms(){
        $.mobile.showPageLoadingMsg();        
        $.get("http://localhost:31578/PayForward_Ajax.aspx?",
          {
            type: "SetupTx",
            amount: document.getElementById("txtamount").value.replace("$ ","").replace(".",""),
            fbid: document.getElementById("hdnfbid").value,
            note: document.getElementById("txtnote").value,
            ipaddress: document.getElementById("hdipaddress").value,
            resource_key: "3"
          },          
             function(data) {            
                //alert(data.txkey);            
            },"json");            
        $.mobile.hidePageLoadingMsg();                
        alert("SMS Sent");
    }

    function btnPayForward(){
        if ($("#sms").css("display")=="none")
        {
            $("#sms").css("display","block");
            //alert($("#sms").css("display"));
        }
            //alert($("#sms").css("height"));
         if ($("#sms").css("height")=="0px")  //Opening Payforward
         {
         $("#sms").animate({            
            height: '130'
          });
          $("#pf").css("display","none");
          $("#pfback").css("display","block");
          document.getElementById("hdonsms").value = "1";
          document.getElementById("hdkeypadplace").value = "0";
          refreshkeypad();
         }
         else //Closing Payforward
         {
            $("#sms").animate({            
                    height: '0'            
                    }, 500,'linear', function() {
              $("#sms").css("display","none");
          } );
            $("#pfback").css("display","none");
            $("#pf").css("display","block");
            document.getElementById("hdonsms").value = "0";
            document.getElementById("hdkeypadplace").value = "0";
            $("#btncustomerphone").text("Enter customer Phone");
         }
         

    }

    function AddNote()
    {
        if ($("#thenote").css("display") == "none")
        {
            $("#thenote").slideDown('slow');
            $("#addnoteatag span.ui-icon").removeClass("ui-icon-arrow-d");
            $("#addnoteatag span.ui-icon").addClass("ui-icon-arrow-u");           
            document.getElementById("txtnote").focus();
        }        
        else
        {
            $("#thenote").slideUp('slow');
            $("#addnoteatag span.ui-icon").removeClass("ui-icon-arrow-u");
            $("#addnoteatag span.ui-icon").addClass("ui-icon-arrow-d");
        }
    }

    function keypadpress(addamount)
    {    
        if (document.getElementById("hdonsms").value == "0") //money
        {
            keypadpressmoney(addamount);
        }
        else //phone #
        {
            keypadpressphone(addamount);
        }       
    }    

    function keypadpressphone(addamount)
    {
        var currentplace =  parseInt(document.getElementById("hdkeypadplace").value);
        var currentnum = $("#txtsms").val().replace("(","").replace(")","").replace("-","").replace(/#/gi,"").replace(" ","");
        var currentlength = currentnum.length;
                
        var thenewtxtamount = jQuery.trim(currentnum);                
        if (currentlength < 11) //can still put in more numbers
        {
            thenewtxtamount += addamount;                        
        }        
        
        for (i=0;i<10-currentlength;i++)
        {            
            thenewtxtamount += "#";
        }                
        $("#txtsms").val(addthephone(thenewtxtamount));
        if (currentlength == 9)//reached end , now turn on button
        {
            $("#btncustomerphone").text("Send sms Payment");
            $("#btncustomerphone").height(35);
            $("#btncustomerphone").addClass("ui-icon-arrow-d");
        }
    }

    function addthephone(phone)
    {        
        var thephone = "(" + phone.substr(0,3) + ") " + phone.substr(3,3) + "-" + phone.substr(6,4);
        return thephone;
    }

    function keypadpressmoney(addamount)
    {
       var currentplace =  parseInt(document.getElementById("hdkeypadplace").value);
       var currentnum = $("#txtamount").val().replace("$ ","").replace(".","");
       var currentlength = currentnum.length;

       document.getElementById("hdkeypadplace").value = currentplace + 1;;       
       
       var thenewtxtamount = "";
       if (currentplace>=3) //need to add decimal
       {
        thenewtxtamount = currentnum + addamount;
       }
       else if (currentplace==0){
        thenewtxtamount = currentnum.substring(0,currentlength-1) + addamount;
       }
       else if (currentplace==1){
        thenewtxtamount = currentnum.substr(0,1) + currentnum.substr(currentlength-1,1) + addamount;
       }
       else if (currentplace==2){//036
        thenewtxtamount = currentnum.substr(1,2) + addamount;
       }       
        thenewtxtamount = addthedecimal(thenewtxtamount);
        thenewtxtamount = addthesymbol(thenewtxtamount);
        $("#txtamount").val(thenewtxtamount);
    }

    function refreshkeypad()
    {
        if (document.getElementById("hdonsms").value == "0") //money
        {
            $("#txtamount").val(addthesymbol("0.00"));
        }
        else //phone #
        {
            $("#txtsms").val("(###) ###-####");
        }       
        document.getElementById("hdkeypadplace").value = "0";        
    }

    function addthesymbol(amount)
    {
        return "$ " + amount;        
    }

    function addthedecimal(amount)
    {
        var currentlength = amount.length;
        return amount.substring(0,currentlength-2) + "." + amount.substring(currentlength-2,currentlength);
    }

        function initpage(){    
        
        
        refreshkeypad();

    $.mobile.defaultPageTransition = 'none';        
    
    FB.init({appId: "<%= ConfigurationSettings.AppSettings["APIKey"] %>", status: true, cookie: true,
             xfbml: true});
 
  if (FB.getSession())
  {
    alert('exists1');
    document.getElementById("hdnfbid").value = FB.getSession().uid;      
      turnonfbname();
      FB.api('/me', function(response) {
         // alert(response.name);
          document.getElementById("fbcheckedin").innerHTML = response.name + " is logged in";
          document.getElementById("hdnfbname").value = response.name;
          
        });
  }
  else
  {
    //alert('not exists1');
    if (document.getElementById("hdnfbid").value != '0')
    {
        var fbid = document.getElementById("hdnfbid").value;
        //alert(fbid);
        FB.api('/'+fbid, function(response) {
              //alert(response.name);
              document.getElementById("fbcheckedin").innerHTML = response.name + " is logged in";
              turnonfbname();
          
          
        
            document.getElementById("hdnfbname").value = response.name;
            });
    }
    else{ //not logged in
        alert('not logged in');        
        window.location = "PayForward_LoginFB.aspx";
    }
  }

  function turnonfbname()
  {
    //document.getElementById("fbcheckin").style.display = 'none';
    document.getElementById("fbcheckedin").style.display = 'block';
    //document.getElementById("fbOptions").style.display = 'block';
  }
}//end of initpage



    </script>
</body>
</html>
