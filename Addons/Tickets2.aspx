<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tickets2.aspx.cs" Inherits="Eventomatic.Addons.Tickets2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tickets</title>
    <script src="../Scripts.js" language="javascript" type="text/javascript"></script>    
    <link href="../Eventomatic.css" rel="stylesheet" type="text/css" />
</head>
<body onload="AdjustRadWidow();">
    <form id="Form2" method="post" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>     
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat=server>
    <script type="text/javascript">        

        function CloseWindow()
        {                
           GetRadWindow().close('0');
        }
    
function UpdateCalculator()
{
var txtticketprice = document.getElementById('TicketsCost_text');
var paypalfee = document.getElementById('paypalfee');
var total2 = document.getElementById('total2');

var boolgoodchar = checkForNumeric(txtticketprice);    
    if (boolgoodchar == false)
    {
        txtticketprice.value = "20.00";
    }

var tixprice = parseFloat(txtticketprice.value);
var gsfee = (tixprice * .029) + .3;
var ppfee = (tixprice * .029) + .3;
var ebritefee = (tixprice * .025) + .99;
if (gsfee > 6.95)
{
    gsfee = 6.95;
}
if (ebritefee > 9.95)
{
    ebritefee = 9.95;
}

if (tixprice == 0)
{
    gsfee = 0;
    ppfee = 0;
    ebritefee = 0;
}

var t1 = tixprice + Math.round(gsfee*100)/100;
var t2 = tixprice - Math.round(ppfee*100)/100;
var t3 = tixprice + Math.round(ebritefee*100)/100;


//tixprice2.innerHTML = '$'+tixprice.toFixed(2);
//tixprice3.innerHTML = '$'+tixprice.toFixed(2);
//tixprice4.innerHTML = '$'+tixprice.toFixed(2);


paypalfee.innerHTML = '-$'+ppfee.toFixed(2);
//paypalfee2.innerHTML = '-$'+ppfee.toFixed(2);
//servicefee2.innerHTML = '$'+ebritefee.toFixed(2);


total2.innerHTML = '$'+t2.toFixed(2);
//total3.innerHTML = '$'+t3.toFixed(2);
//total4.innerHTML = '$'+t2.toFixed(2);
}

function checkForNumeric(obj) {
	if( /[^0-9\.]|-{2,}/gi.test(obj.value) ) {
		alert("Must enter a positive numeric value");
		obj.focus();
		obj.select();
		return false;
	}
	return true;
}
</script>

<div style="width: 400px; height:400px; text-align:center;">
    <table style="display:block; font-size:small; text-align:left;">
    <tr>
        <td>
            <table width=100% cellpadding=5 cellspacing=10>
            <tr>
                    <td class="QuestionSections">                                                
                                What do you want to call these tickets?<br />
                        <telerik:RadTextBox ID=txtdescription runat=server Width="300px" EmptyMessage="General Admission"/>
                    </td>
                </tr>
                <tr>
                    <td class="QuestionSections">
                    <table width=100%>
                        <tr valign=top>
                            <td>
                                    How much do these tickets cost?<br />                       
                                 <telerik:RadNumericTextBox ShowSpinButtons="False" Value="20" Type=Number
                        ID="TicketsCost" runat="server" InvalidStyleDuration="100" Width=125px IncrementSettings-InterceptArrowKeys="true"
                        IncrementSettings-InterceptMouseWheel="true" onKeyUp="UpdateCalculator();">
                        <NumberFormat AllowRounding="True" KeepNotRoundedValue="False" DecimalDigits=2 />
                    </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <table style="font-size:smaller;">
                                    <tr>
                                        <td>
                                            PayPal fee
                                        </td>
                                        <td>
                                            Your revenue
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id=paypalfee>-$0.88</div>
                                        </td>
                                        <td>
                                            <div id=total2>$19.12</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan=2><a href="http://promo.thegroupstore.com/Fees.aspx" target=_blank>Click here</a> for more<br />details on fees</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>                       
                        
                        <br />
                        Donation Box?<asp:CheckBox runat=server ID=chkdonation AutoPostBack=true OnCheckedChanged="onchkdonation_Clicked"/><br />
                        <span style="font-size:smaller;">(Check donation box if you want the buyer to type in the price)</span>
                    </td>
                </tr>
                <tr>
                    <td class="QuestionSections">
                       What is the maximum amount of tickets available?<br />
                        
                         <telerik:RadNumericTextBox ShowSpinButtons="false" IncrementSettings-InterceptArrowKeys="true"
                IncrementSettings-InterceptMouseWheel="true" runat="server" Type=Number Value="100"
                ID="TicketsAvailable" Width="100px" >
                <NumberFormat AllowRounding="True" KeepNotRoundedValue="False" DecimalDigits=0/>
            </telerik:RadNumericTextBox>                                               
                    </td>
                </tr>
                <tr>
                    <td class="QuestionSections">During what period of time do you want to sell these tickets?
                        <br />    
                        <br />                    
                                <table>
                                    <tr>
                                        <td>Begin Selling</td>
                                        <td>
                                         <telerik:RadDatePicker ID="Begin_Selling" runat="server" Width=100px/>
                                        </td>
                                        <td>
                                         <telerik:RadTimePicker ID="Begin_Selling_Time" runat="server" Width=80px/>
                                        </td>
                                    </tr>
                                </table>                              
                         <table>
                                <tr>
                                    <td>Finish Selling</td>
                                    <td> 
                                    <telerik:RadDatePicker ID="Selling_Deadline"  runat="server" Width=100px
                                       />
                                    </td>
                                    <td>
                                    <telerik:RadTimePicker ID="Selling_Deadline_Time" runat="server" Width=80px/>
                                    </td>
                                </tr>
                            </table>                   
                    </td>
                </tr>                                
            </table>
        
        </td>
    </tr>
    <tr>
        <td class="QuestionPopupFooter"><asp:Button ID=btnSaveTicket runat=server 
                Text="Update Ticket"  OnClick="btnSaveTicket_Click"  /></td>
    </tr>
</table>   
       </div>
       </telerik:RadAjaxPanel>    
    </form>

    <script language=javascript>
        UpdateCalculator();
    </script>
</body>
</html>