/*
Author: Robert Hashemian
http://www.hashemian.com/

You can use this code in any manner so long as the author's
name, Web address and this disclaimer is kept intact.
********************************************************
Usage Sample:

<script language="JavaScript">
TargetDate = "12/31/2020 5:00 AM";
BackColor = "palegreen";
ForeColor = "navy";
CountActive = true;
CountStepper = -1;
LeadingZero = true;
DisplayFormat = "%%D%% Days, %%H%% Hours, %%M%% Minutes, %%S%% Seconds.";
FinishMessage = "It is finally here!";
</script>
<script language="JavaScript" src="http://scripts.hashemian.com/js/countdown.js"></script>


function calcage(secs, num1, num2) {
  s = ((Math.floor(secs/num1))%num2).toString();
  if (LeadingZero && s.length < 2)
    s = "0" + s;
  return "<b>" + s + "</b>";
}

function CountBack(secs) {
  if (secs < 0) {
    document.getElementById("cntdwn").innerHTML = FinishMessage;
    return;
  }
  DisplayStr = DisplayFormat.replace(/%%D%%/g, calcage(secs,86400,100000));
  DisplayStr = DisplayStr.replace(/%%H%%/g, calcage(secs,3600,24));
  DisplayStr = DisplayStr.replace(/%%M%%/g, calcage(secs,60,60));
  DisplayStr = DisplayStr.replace(/%%S%%/g, calcage(secs,1,60));

  document.getElementById("cntdwn").innerHTML = DisplayStr;
  if (CountActive)
    setTimeout("CountBack(" + (secs+CountStepper) + ")", SetTimeOutPeriod);
}

function putspan(backcolor, forecolor) {
 document.write("<span id='cntdwn' style='background-color:" + backcolor + 
                "; color:" + forecolor + "'></span>");
}

if (typeof(BackColor)=="undefined")
  BackColor = "white";
if (typeof(ForeColor)=="undefined")
  ForeColor= "black";
if ( document.getElementById("hdLastTicketDate").value =="0")
  {TargetDate = "12/31/2020 5:00 AM";}
else{
    TargetDate = document.getElementById("hdLastTicketDate").value;
}
if ( document.getElementById("hdCurrentDate").value =="0")
  {CurrentDate = "12/31/2020 5:00 AM";}
else{
    CurrentDate = document.getElementById("hdCurrentDate").value;
}

if (typeof(DisplayFormat)=="undefined")
  DisplayFormat = "%%D%% Days, %%H%% Hours, %%M%% Minutes, %%S%% Seconds.";
if (typeof(CountActive)=="undefined")
  CountActive = true;
if (typeof(FinishMessage)=="undefined")
  FinishMessage = "";
if (typeof(CountStepper)!="number")
  CountStepper = -1;
if (typeof(LeadingZero)=="undefined")
  LeadingZero = true;


CountStepper = Math.ceil(CountStepper);
if (CountStepper == 0)
  CountActive = false;
var SetTimeOutPeriod = (Math.abs(CountStepper)-1)*1000 + 990;
putspan(BackColor, ForeColor);
var dthen = new Date(TargetDate);
var dnow = new Date();
if(CountStepper>0)
  ddiff = new Date(dnow-dthen);
else
  ddiff = new Date(dthen-dnow);
gsecs = Math.floor(ddiff.valueOf()/1000);
CountBack(gsecs);*/

// ****  Time Zone Count Down Javascript  **** //
/*
Visit http://rainbow.arch.scriptmania.com/scripts/
 for this script and many more
*/

////////// CONFIGURE THE COUNTDOWN SCRIPT HERE //////////////////


/*var month = TargetDate.charAt(3)+TargetDate.charAt(4);     //  '*' for next month, '0' for this month or 1 through 12 for the month 
var day = TargetDate.charAt(0)+TargetDate.charAt(1);       //  Offset for day of month day or + day  
var hour = TargetDate.charAt(11)+TargetDate.charAt(12);        //  0 through 23 for the hours of the day
var minutes = TargetDate.charAt(14)+TargetDate.charAt(15);        //  0 through 23 for the hours of the day
var tz = -4;         //  Offset for your timezone in hours from UTC*/
var lab = 'tzcd';    //  The id of the page entry where the timezone countdown is to show
 
//07/06/2010 12:00:00 AM
//alert(TargetDate+'/'+CurrentDate);

function start_counter() {displayTZCountDown(setTZCountDown(),lab);}

    // **    The start function can be changed if required   **
//window.onload = start;

////////// DO NOT EDIT PAST THIS LINE //////////////////

function setTZCountDown() 
{

if ( document.getElementById("hdLastTicketDate").value =="0")
  {TargetDate = "12/31/2020 05:00 AM";}
else{
    TargetDate = document.getElementById("hdLastTicketDate").value;
}
if ( document.getElementById("hdCurrentDate").value =="0")
  {CurrentDate = "12/31/2020 5:00 AM";}
else{
    CurrentDate = document.getElementById("hdCurrentDate").value;
}
var toDate = new Date(TargetDate);
/*if (month == '*')toDate.setMonth(toDate.getMonth() + 1);
else if (month > 0) 
{ 
if (month <= toDate.getMonth())toDate.setYear(toDate.getYear() + 1);
toDate.setMonth(month-1);
}
if (day.substr(0,1) == '+') 
{var day1 = parseInt(day.substr(1));
toDate.setDate(toDate.getDate()+day1);
} 
else{toDate.setDate(day);
}
toDate.setHours(hour);
toDate.setMinutes(minutes-(tz*60));
toDate.setSeconds(0);*/
var fromDate = new Date(CurrentDate);
//alert(toDate+'/'+fromDate);
//fromDate.setMinutes(fromDate.getMinutes() + fromDate.getTimezoneOffset());


var diffDate = new Date(0);
diffDate.setMilliseconds(toDate - fromDate);
return Math.floor(diffDate.valueOf()/1000);

}
function displayTZCountDown(countdown,tzcd) 
{

if (countdown == 0) 
{
    //Will need to change this
    //document.getElementById(tzcd).innerHTML = "Sorry, you are too late."; 
    window.location.href = window.location.href;
}
else {
    var secs = countdown % 60; 
    if (secs < 10) secs = '0'+secs;
    var countdown1 = (countdown - secs) / 60;
    var mins = countdown1 % 60; 
    if (mins < 10) mins = '0'+mins;
    countdown1 = (countdown1 - mins) / 60;
    var hours = countdown1 % 24;
    var days = (countdown1 - hours) / 24;
    //alert(document.getElementById('tzcdFrame').style.display);
    if ((countdown < 7200) && (countdown > -1))
    {
       
        if (document.getElementById('tzcdFrame').style.display != 'block')
        {document.getElementById('tzcdFrame').style.display = 'block';}
        
        document.getElementById('tzcd_text').innerHTML = 'Time Left to complete your purchase';
        document.getElementById('tzcd').innerHTML = hours+ 'h : ' +mins+ 'm : '+secs+'s';
    }
    else if ((countdown < 0) && (document.getElementById('tzcdFrame').style.display != 'none'))
    {
        document.getElementById('tzcdFrame').style.display != 'none';
    }
    setTimeout('displayTZCountDown('+(countdown-1)+',\''+tzcd+'\');',999);
}
}