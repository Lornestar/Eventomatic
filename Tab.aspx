<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tab.aspx.cs" Inherits="Eventomatic.Tab" %>



    <form id="form1" runat="server">
    
    <div>
<link href="http://www.thegroupstore.com/Eventomatic_Stores.css" media="screen" rel="stylesheet" type="text/css" />
<script language=javascript>
function btnbuyfrom(gvid)
{
    gvid += 2;    
    grid1id =  'GridView1_ctl0' + gvid;
    lblbuy = document.getElementById(grid1id+'_lblBuy');
    pnltickets = document.getElementById(grid1id+'_pnltickets');
    lblbtnnext = document.getElementById(grid1id+'_lblNextbutton');
    lbldescriptionfull = document.getElementById(grid1id+'_pnlEvent_Descriptionfull');
    
    pnltickets.setStyle({display: 'block'});
    lblbtnnext.setStyle({display: 'block'});
    lblbuy.setStyle({display: 'none'});
    lbldescriptionfull.setStyle({display: 'block'});
    
}

function btnnext_Fundraiser(gvid)
{
    gvid += 2;
    if ((confirmQuantitySelected(gvid)) || (confirmDonateAmount(gvid))){
        var rdbtnCC = document.getElementById('GridView1_ctl0' + gvid + '_chkCC');
        rdbtnCC.setChecked('true');
        gettxkey(gvid);                
    }
    else if (confirmDonateAmount(gvid) == false)
        {
            new Dialog().showMessage('Error', 'Please enter the amount of money you wish to pay.');
        }
        else{
            new Dialog().showMessage('Error', 'Please select the Ticket Quantity you wish to purchase.');
        }
}

function btnnext(gvid)
{
    //alert('test');
    //it starts at ct102
    gvid += 2;
    
    grid1id =  'GridView1_ctl0' + gvid;
    pnltickets = document.getElementById(grid1id+'_pnltickets');
    pnlquestions = document.getElementById(grid1id+'_pnlquestions');
    pnlpayment = document.getElementById(grid1id+'_pnlpayment');
    pnlpayment = document.getElementById(grid1id+'_pnlpayment');
    objbtnback = document.getElementById(grid1id+'_lblBackbutton');
    
    //grid2id = 'GridView1_ctl03_pnltickets';
    
    if ((pnltickets.getStyle("display") == "") || (pnltickets.getStyle("display") == "block")) //step1 from tickets to questions
    {
        if ((confirmQuantitySelected(gvid)) || (confirmDonateAmount(gvid))){
            pnltickets.setStyle({display: 'none'});
            pnlquestions.setStyle({display: 'block'});
            pnlpayment.setStyle({display: 'none'});
            //new Dialog().showMessage('Error', btnback);
            objbtnback.setStyle({display: 'block'});            
            checkiffree(gvid);
            }
        else if (confirmDonateAmount(gvid) == false)
        {
            new Dialog().showMessage('Error', 'Please enter the amount of money you wish to pay.');
        }
        else{
            new Dialog().showMessage('Error', 'Please select the Ticket Quantity you wish to purchase.');
        }
    }
    else if(pnlquestions.getStyle("display") == "block") //step2 from questions to payment
    { //step2 to payments
        //new Dialog().showMessage('Error', 'to payments');        
        if (confirmFieldsEntered(gvid)){
            pnltickets.setStyle({display: 'none'});
            pnlquestions.setStyle({display: 'none'});
            pnlpayment.setStyle({display: 'block'});
        }
        else{
            new Dialog().showMessage('Error', 'Please enter the required fields.');
        }
    }
    else if((pnlpayment.getStyle("display") == "block")) //step3 send to payment processing
    {
        var rdbtnCC = document.getElementById('GridView1_ctl0' + gvid + '_chkCC');
        var rdbtnPaypal = document.getElementById('GridView1_ctl0' + gvid + '_chkPaypal');    
        if ((rdbtnCC.getChecked() == true) || (rdbtnPaypal.getChecked() == true)){
            gettxkey(gvid);            
        }
        else{
            new Dialog().showMessage('Error', 'Please select payment method.');
        }
    }    
    //new Dialog().showMessage('Dialog', tempint);    
    //document.getElementById('spntickets').style.display = 'none';
    //document.getElementById('spnquestions').style.display = 'block';
    //new Dialog().showMessage('Dialog', pnltickets.getStyle('display'));
}

function gettxkey(gvid)
{
    
    var gv = document.getElementById('GridView1_ctl0' + gvid + '_GridView2');            
    var EventKey = document.getElementById('GridView1_ctl0' + gvid + '_txtEventKey');
    var FirstName = document.getElementById('GridView1_ctl0' + gvid + '_txtList_FirstName');
    var LastName = document.getElementById('GridView1_ctl0' + gvid + '_txtList_LastName');
    var Email = document.getElementById('GridView1_ctl0' + gvid + '_txtList_Email');
    var tix = "";
    var paymenttype = "0";
    //alert(gv.rows.length);
    var countrows = 2;    
    
    for (i=2; (obj=document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl0' + i + '_txtTicketKey')) != null; i++)
    {
    //GridView1_ctl02_GridView2_ctl02_txtTicketKey 
        var Quantity = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl0' + i + '_ddlQuantity');        
        var txkey = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl0' + i + '_txtTicketKey');        
        var Donatetxt = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl0' + i + '_txtDonate');
        //new Dialog().showMessage('Dialog', Donatetxt);
        
        if (Quantity != null)
        {
            if (parseInt(Quantity.getValue()) > 0)
            {                
                if (tix != "")
                {
                    tix += ",";
                }
                tix += txkey.getValue() + "|" + Quantity.getValue();
            }            
        }
        else if (Donatetxt != null)
        {
            if (tix != "")
            {
                tix += ",";
            }
            tix += txkey.getValue() + "|" + Donatetxt.getValue();
        }
    }
    var servicefeerow = i - 1;
    var hdOverallTotal = document.getElementById('GridView1_ctl0' + gvid + '_hdOverallTotal');
    var hdServiceFee = document.getElementById('GridView1_ctl0' + gvid + '_hdServiceFee');
    //new Dialog().showMessage('Dialog', 'here');
    
    var rdbtnCC = document.getElementById('GridView1_ctl0' + gvid + '_chkCC');
    var rdbtnPaypal = document.getElementById('GridView1_ctl0' + gvid + '_chkPaypal');    
    if (rdbtnCC.getChecked() == true)
    {
        paymenttype = "0";
    }
    else if (rdbtnPaypal.getChecked() == true){
        
        paymenttype = "1";
    }    
    
    var params = {"event_key":EventKey.getValue(),"type":paymenttype,"tix":tix,"guestlist_firstname":FirstName.getValue(),"guestlist_lastname":LastName.getValue(),"guestlist_email":Email.getValue(),"totalamount":hdOverallTotal.getValue(),"servicefeeamount":hdServiceFee.getValue()};    
    var params2 = "event_key=" + EventKey.getValue() + "&type=" + paymenttype + "&tix=" + tix + "&guestlist_firstname=" + FirstName.getValue() + "&guestlist_lastname=" + LastName.getValue() + "&guestlist_email=" + Email.getValue() + "&totalamount=" + hdOverallTotal.getValue() + "&servicefeeamount=" + hdServiceFee.getValue();
    
    form = document.getElementById('form1'); 
    form.setAction("http://www.thegroupstore.com/tab2.aspx");            
    form.setTarget("_blank");
    form.setMethod("post");  
    
    input = document.createElement("input");
    input.setName("event_key");
    input.setValue(EventKey.getValue());
    input.setStyle({display: "none"});            
    
    input2 = document.createElement("input");
    input2.setName("type");
    input2.setValue(paymenttype);
    input2.setStyle({display: "none"});
    
    input3 = document.createElement("input");
    input3.setName("tix");
    input3.setValue(tix);
    input3.setStyle({display: "none"});
    
    input4 = document.createElement("input");
    input4.setName("guestlist_firstname");
    input4.setValue(FirstName.getValue());
    input4.setStyle({display: "none"});
    
    input5 = document.createElement("input");
    input5.setName("guestlist_lastname");
    input5.setValue(LastName.getValue());
    input5.setStyle({display: "none"});
    
    input6 = document.createElement("input");
    input6.setName("guestlist_email");
    input6.setValue(Email.getValue());
    input6.setStyle({display: "none"});
    
    input7 = document.createElement("input");
    input7.setName("totalamount");
    input7.setValue(hdOverallTotal.getValue());
    input7.setStyle({display: "none"});
    
    input8 = document.createElement("input");
    input8.setName("servicefeeamount");
    input8.setValue(hdServiceFee.getValue());
    input8.setStyle({display: "none"});
    
    form.appendChild(input);
    form.appendChild(input2);
    form.appendChild(input3);
    form.appendChild(input4);
    form.appendChild(input5);
    form.appendChild(input6);
    form.appendChild(input7);
    form.appendChild(input8);    
              
    form.submit();
    //do_ajax('actionDiv',params,params2,gvid);
    
}

function do_ajax(div,params,params2,gvid) {
    //new Dialog().showMessage('Dialog', params2);
	var ajax = new Ajax();
	ajax.responseType = Ajax.RAW;
	ajax.ondone = function(data) {		    
		//document.getElementById(div).setTextValue(data);
		var strtemp = data.split("|");
		//new Dialog().showMessage('Dialog', strtemp[0]);
		
		
		//document.getElementById('txttxkey').setTextValue(data.message);
		//new Dialog().showMessage('Dialog', document.getElementById('txttxkey').getValue());
		transfertopayment(strtemp[0],gvid);
		}    
		
	//var params={"action":'select',"option":val,"othertest":'anystring',"otherval":100}; //add parameters as comma separated "param":value	
	document.getElementById(div).setTextValue(params2);
	ajax.post('http://www.thegroupstore.com/tab2.aspx?'+params2,params);  //GET values sended with "val" and POST values sended with "params"
	
	} 

function transfertopayment(txkey,gvid){
    //new Dialog().showMessage('Dialog', txkey);
    var txkey2 = txkey.replace("tx_key=", "");        
    
    var rdbtnCC = document.getElementById('GridView1_ctl0' + gvid + '_chkCC');
    var rdbtnPaypal = document.getElementById('GridView1_ctl0' + gvid + '_chkPaypal');
    var txtfbid = document.getElementById('txtfbid');
    
    if (txkey2 == "0") //sold out
    {
        new Dialog().showMessage('Dialog', "Unfortunately the tickets you selected are sold out.");
    }   
    else{ //not sold out
        if (rdbtnCC.getChecked() == true)
        {//goto cc        
            /*form = document.createElement("formCC");
            form.setTarget("_blank");
            form.setMethod("get");
            form.setAction("https://www.thegroupstore.com/paycc.aspx");
            // in Safari and Firefox, you could just submit now, but IE insists the form is appended to the document
            // you need the id of an element to append the form to, any will do (we'll make sure the form isn't visible) but a div is probably safest
            form.setStyle({display: "none"});
            popup.appendChild(form);
            // call submit on the form
            
            input = document.createElement("input");
            input.setName("tx_key");
            input.setValue(txkey2);
            form.appendChild(input);
            form.submit();*/            
            var strurl = "https://www.thegroupstore.com/paycc.aspx?tx_key=" + txkey2+ "&fbid=" + txtfbid.getValue();            
            form = document.getElementById('form1'); 
            //form.setAction("https://www.thegroupstore.com/paycc.aspx");
            form.setAction(strurl);
            form.setTarget("_blank");
            form.setMethod("post");            
            /*input = document.createElement("input");
            input.setName("tx_key");
            input.setValue(txkey2);
            input.setStyle({display: "none"});            
            input2 = document.createElement("input");
            input2.setName("fbid");
            input2.setValue(txtfbid.getValue());
            input2.setStyle({display: "none"});
            
            form.appendChild(input);
            form.appendChild(input2);*/
            form.submit();
            new Dialog().showMessage('Dialog', strurl);
        }
        else if (rdbtnPaypal.getChecked() == true){//go to paypal
            /*form = document.createElement("formPaypal");
            form.setTarget("_blank");
            form.setMethod("get");
            form.setAction("https://www.thegroupstore.com/tab2.aspx");
            // in Safari and Firefox, you could just submit now, but IE insists the form is appended to the document
            // you need the id of an element to append the form to, any will do (we'll make sure the form isn't visible) but a div is probably safest
            form.setStyle({display: "none"});
            popup.appendChild(form);
            // call submit on the form
            input = document.createElement("input");
            input.setName("tx_key");
            input.setValue(txkey2);
            form.appendChild(input);
            form.submit();*/
            form = document.getElementById('form1'); 
            form.setAction("https://www.thegroupstore.com/tab2.aspx");
            form.setTarget("_blank");
            form.setMethod("post");            
            input = document.createElement("input");
            input.setName("tx_key");
            input.setValue(txkey2);
            input.setStyle({display: "none"});
            form.appendChild(input);
            form.submit();
        }
    }
}


function btnback(gvid)
{

gvid += 2;
    
    grid1id =  'GridView1_ctl0' + gvid;
    pnltickets = document.getElementById(grid1id+'_pnltickets');
    pnlquestions = document.getElementById(grid1id+'_pnlquestions');
    pnlpayment = document.getElementById(grid1id+'_pnlpayment');
    pnlpayment = document.getElementById(grid1id+'_pnlpayment');
    objbtnback = document.getElementById(grid1id+'_lblBackbutton');
    
    //grid2id = 'GridView1_ctl03_pnltickets';
    
    if(pnlquestions.getStyle("display") == "block") //step2 from questions to payment
    { //step2 to payments
        //new Dialog().showMessage('Error', 'to payments');        
        pnltickets.setStyle({display: 'block'});
        pnlquestions.setStyle({display: 'none'});
        pnlpayment.setStyle({display: 'none'});
        objbtnback.setStyle({display: 'none'});
    }
    else if((pnlpayment.getStyle("display") == "block")) //step3 send to payment processing
    {
        pnltickets.setStyle({display: 'none'});
        pnlquestions.setStyle({display: 'block'});
        pnlpayment.setStyle({display: 'none'});
    }
}

function checkiffree(gvid)  //then decides if should hide email or not
{
var hdOverallTotal = document.getElementById('GridView1_ctl0' + gvid + '_hdOverallTotal');            
if (parseFloat(hdOverallTotal.getValue()) != 0)
{
    //hide email question
    var txtEmail = document.getElementById('GridView1_ctl0' + gvid + '_txtList_Email');            
    var pnltxtEmail = document.getElementById('GridView1_ctl0' + gvid + '_pnltxtEmail');    
    txtEmail.setStyle({display: 'none'});
    pnltxtEmail.setStyle({display: 'none'});
}
}

function checkiffilledguestlist(gvid)
{
    
}

function confirmDonateAmount(gvid)
{
    var hdOverallTotal = document.getElementById('GridView1_ctl0' + gvid + '_hdOverallTotal');
    if (parseFloat(hdOverallTotal.getValue()) > 0)
    {
        return true;
    }
    else
    {
        return false;
    }
}

function confirmQuantitySelected(gvid){
    var gv = document.getElementById('GridView1_ctl0' + gvid + '_GridView2');    
    var selectedone = false;
    var Quantity = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl02_ddlQuantity');
    var i = 3;
    while (Quantity != null)
    {
        if (Quantity.getValue() != '0')
        {selectedone = true;}
        Quantity = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl0' + i + '_ddlQuantity');
        i += 1;
        
    }    
return selectedone
}

function confirmFieldsEntered(gvid){
    var selectedone = true;
    var FirstName = document.getElementById('GridView1_ctl0' + gvid + '_txtList_FirstName');
    var LastName = document.getElementById('GridView1_ctl0' + gvid + '_txtList_LastName');
    var Email = document.getElementById('GridView1_ctl0' + gvid + '_txtList_Email');
    var pnltxtEmail = document.getElementById('GridView1_ctl0' + gvid + '_pnltxtEmail');    
    
    if (FirstName.getValue() == "")
    {selectedone = false;}
    if (LastName.getValue() == "")
    {selectedone = false;}
    if ((Email.getValue() == "") && (pnltxtEmail.getStyle("display") == "block"))
    {selectedone = false;}

    return selectedone
}

function ManipulateGrid(gvid)
        {
        
            gvid += 2;
            var gv = document.getElementById('GridView1_ctl0' + gvid + '_GridView2');            
            
            //alert(gv.rows.length);
            var OverallTotal = 0;
            var QuantityTotal = 0;
            var ServiceFeeTotal = 0;
            var ServiceFeeRow = 0;
            var SFP = document.getElementById('GridView1_ctl0' + gvid + '_hdSFP');
            var SFC = document.getElementById('GridView1_ctl0' + gvid + '_hdSFC');
            var SFM = document.getElementById('GridView1_ctl0' + gvid + '_hdSFM');            
            var hdOverallTotal = document.getElementById('GridView1_ctl0' + gvid + '_hdOverallTotal');
            var hdServiceFee = document.getElementById('GridView1_ctl0' + gvid + '_hdServiceFee');
            
            var countrows = 2;
            var extradigit = "0";
            
            for (i=2;  (obj=document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl' + extradigit + i + '_txtTicketKey')) != null; i++)
            {              
                        
            countrows +=1;                
             var Price = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl' + extradigit  + i + '_lblPrice');
             var Quantity = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl' + extradigit  + i + '_ddlQuantity');
             var Total = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl' + extradigit  + i + '_lblTotal');             
             var Pricetxt = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl' + extradigit  + i + '_txtPrice');
            
             var Donatetxt = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl' + extradigit + i + '_txtDonate');
             
              if (Price != null && Quantity != null)
              {              
              //Price.setValue('testset');              
              var RowTotal = parseFloat(Pricetxt.getValue().replace("$", "")) * parseInt(Quantity.getValue());
                          
              //new Dialog().showMessage('Dialog', tempstr);  
              
              Total.setTextValue("$ " + RowTotal.toFixed(2));                            
              OverallTotal = OverallTotal + RowTotal;
              QuantityTotal = QuantityTotal + parseInt(Quantity.getValue());
              
              var ServiceFeeRow = 0;
              if (RowTotal != 0)
                {   
                
                ServiceFeeRow = ((RowTotal/100) * parseFloat(SFP.getValue())) + parseFloat(parseFloat(SFC.getValue()) * parseInt(Quantity.getValue()));                
                var maxamount = parseFloat(parseFloat(SFM.getValue())) * parseInt(Quantity.getValue());
                //new Dialog().showMessage('Dialog', ServiceFeeRow);  
                if (ServiceFeeRow > maxamount)
                    {
                        ServiceFeeRow = parseFloat(parseFloat(SFM.getValue())* parseInt(Quantity.getValue()));                        
                    }                                        
                ServiceFeeTotal += ServiceFeeRow;
                }                        
               
              }//if price != null
              if (Donatetxt != null)
              {
                if (checkForNumeric(Donatetxt))
                {
                    var RowTotal = parseFloat(Donatetxt.getValue());
                    
                    Total.setTextValue("$ " + RowTotal.toFixed(2));
                    OverallTotal = OverallTotal + RowTotal;
                    var ServiceFeeRow = 0;
                    //Service fee row
                    if (RowTotal != 0)
                    {                   
                    ServiceFeeRow = ((RowTotal/100) * parseFloat(SFP.getValue())) + parseFloat(parseFloat(SFC.getValue()) * 1);                
                    if (parseFloat(ServiceFeeRow) > parseFloat(parseFloat(SFM.getValue()) * 1))
                        {
                            ServiceFeeRow = parseFloat(parseFloat(SFM.getValue())* 1);
                        }                                        
                    ServiceFeeTotal += ServiceFeeRow;
                    }                
                 }//if its numeric
              }//if Donatetxt != null
            extradigit = "0";
             if (i > 8)
             {
                extradigit = "";
             }     
             
            }            
                        
            extradigit = "0";
            if (i > 10)
             {
                extradigit = "";                
             }            
            var servicefeerownum = i - 1;
            var servicefeerowtxt = "0" + servicefeerownum;
            if (i > 10)
            {
                servicefeerowtxt = servicefeerownum;
            }                        
            var OverallTotalText = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl' + extradigit + i + '_lblTotalOverall');                        
            var ServiceFeeTotalobj = document.getElementById('GridView1_ctl0' + gvid + '_GridView2_ctl' + servicefeerowtxt + '_lblTotal');
            //new Dialog().showMessage('Dialog', 'GridView1_ctl0' + gvid + '_GridView2_ctl' + servicefeerowtxt + '_lblTotal');
            //var OverallTotalText2 = document.getElementById("UltraWebTab1_ctl02_GridView3_ctl0"+ i + "_lblTotalOverall");
            OverallTotal += ServiceFeeTotal;
            OverallTotalText.setTextValue("$ " + OverallTotal.toFixed(2));
            ServiceFeeTotalobj.setTextValue("$ " + ServiceFeeTotal.toFixed(2));
            hdOverallTotal.setValue(OverallTotal.toFixed(2));
            hdServiceFee.setValue(ServiceFeeTotal.toFixed(2));
        }

function checkForNumeric(obj) {
	if( /[^0-9\.]|-{2,}/gi.test(obj.getValue()) ) {
		new Dialog().showMessage('Dialog', "Must enter a positive numeric value");
		obj.focus();
		obj.select();
		return false;
	}
	return true;
}

function ViewAdminsExternalLinks(gvid)
{
    gvid += 2;    
    grid1id =  'GridView1_ctl0' + gvid;
    lblexternalsite = document.getElementById(grid1id+'_lblexternalsite');
    if ((lblexternalsite.getStyle("display") == "") || (lblexternalsite.getStyle("display") == "none"))
    {
        lblexternalsite.setStyle({display: 'block'});
    }
    else
    {
        lblexternalsite.setStyle({display: 'none'});
    }
}

function ViewShowGoals(gvid)
{
    gvid += 2;    
    grid1id =  'GridView1_ctl0' + gvid;
    var EventKey = document.getElementById('GridView1_ctl0' + gvid + '_txtEventKey');
    var params = {"event_key":EventKey.getValue(),"type":0};    
    do_ajax3(params,gvid);
    
}

function do_ajax3(params,gvid) {
    //new Dialog().showMessage('Dialog', params2);
	var ajax = new Ajax();
	ajax.responseType = Ajax.RAW;
	ajax.ondone = function(data) {		    
		//document.getElementById(div).setTextValue(data);
		var strtemp = data.split("|");
		if (strtemp[0] == "AckShow")
		{
		    var pnlthermometer = document.getElementById('GridView1_ctl0' + gvid + '_pnlthermometer');		
            if ((pnlthermometer.getStyle("display") == "") || (pnlthermometer.getStyle("display") == "none"))
                {
                    pnlthermometer.setStyle({display: 'block'});
                }
                else
                {
                    pnlthermometer.setStyle({display: 'none'});
                }		    
		}		
		//new Dialog().showMessage('Dialog', strtemp[0]);    
		
		//document.getElementById('txttxkey').setTextValue(data.message);
		//new Dialog().showMessage('Dialog', document.getElementById('txttxkey').getValue());		
		}
		
		
	//var params={"action":'select',"option":val,"othertest":'anystring',"otherval":100}; //add parameters as comma separated "param":value	
	ajax.post('http://www.thegroupstore.com/tab3.aspx',params);  //GET values sended with "val" and POST values sended with "params"	
	}
	
function editFundraisingGoal(gvid)
{
    gvid += 2;
    var lblFundraisingGoal = document.getElementById('GridView1_ctl0' + gvid + '_lblFundraisingGoal');
    var txtFundraisingGoal = document.getElementById('GridView1_ctl0' + gvid + '_txtFundraisingGoal');
    var lbleditFundraisingGoal = document.getElementById('GridView1_ctl0' + gvid + '_lbleditFundraisingGoal');
    var lblsaveFundraisingGoal = document.getElementById('GridView1_ctl0' + gvid + '_lblsaveFundraisingGoal');

    lblFundraisingGoal.setStyle({display: 'none'});
    lbleditFundraisingGoal.setStyle({display: 'none'});
    txtFundraisingGoal.setStyle({display: 'block'});
    lblsaveFundraisingGoal.setStyle({display: 'block'});
}

function saveFundraisingGoal(gvid)
{
    gvid += 2;
    var lblFundraisingGoal = document.getElementById('GridView1_ctl0' + gvid + '_lblFundraisingGoal');
    var txtFundraisingGoal = document.getElementById('GridView1_ctl0' + gvid + '_txtFundraisingGoal');
    var lbleditFundraisingGoal = document.getElementById('GridView1_ctl0' + gvid + '_lbleditFundraisingGoal');
    var lblsaveFundraisingGoal = document.getElementById('GridView1_ctl0' + gvid + '_lblsaveFundraisingGoal');
    var imgthermometer = document.getElementById('GridView1_ctl0' + gvid + '_imgthermometer');
    var txtFundraised = document.getElementById('GridView1_ctl0' + gvid + '_txtFundraised');
    

    var EventKey = document.getElementById('GridView1_ctl0' + gvid + '_txtEventKey');    
    var params = {"event_key":EventKey.getValue(),"type":1,"goalamount":txtFundraisingGoal.getValue()};
    do_ajax3(params,gvid);

    lblFundraisingGoal.setStyle({display: 'block'});
    lbleditFundraisingGoal.setStyle({display: 'block'});
    txtFundraisingGoal.setStyle({display: 'none'});
    lblsaveFundraisingGoal.setStyle({display: 'none'});
    
    //update image
    lblFundraisingGoal.setTextValue("$ "+txtFundraisingGoal.getValue());        
    
    var strtempimg = imgthermometer.getSrc();    
    var strcurrentimg = strtempimg;
    var strcurrentimgsplit = strcurrentimg.split("=");
    if (strtempimg.indexOf('platform.ak.fbcdn') != -1)
    {
        strcurrentimg = strtempimg.split("src=");
        strcurrentimgsplit = strcurrentimg[1].split("%3D");
    }        
    var newmax = parseFloat(txtFundraisingGoal.getValue());
    var amountraised = txtFundraised.getValue();
    var newpercent = 0;
    if (newmax != 0)
        {newpercent =(parseFloat(amountraised)/parseFloat(newmax))* 100;}        
    var newsrc = strcurrentimgsplit[0] + "=" + newpercent.toFixed(0) + "&max=" + newmax.toFixed(0) + "&dollars=1";
    newsrc = newsrc.replace("%3A%2F%2F", "://");
    newsrc = newsrc.replace("%2F", "/");
    newsrc = newsrc.replace("%2F", "/");
    newsrc = newsrc.replace("%3F", "?");
    imgthermometer.setSrc(newsrc);
    
}

function ChecktxtFundraisingGoal(gvid)
{
    gvid += 2;
    var txtFundraisingGoal = document.getElementById('GridView1_ctl0' + gvid + '_txtFundraisingGoal');
    var boolgoodchar = checkForNumeric(txtFundraisingGoal);    
    if (boolgoodchar == false)
    {
        txtFundraisingGoal.setValue("0.00");
    }
}

function btncustommessage(gvid)
{
    gvid += 2;
    var lblEvent_Description = document.getElementById('GridView1_ctl0' + gvid + '_lblEvent_Description');
    var txtCustom_Message = document.getElementById('GridView1_ctl0' + gvid + '_txtCustom_Message');
    var lblCustom_Message = document.getElementById('GridView1_ctl0' + gvid + '_lblCustom_Message');
    var lblCustom_Message_Save = document.getElementById('GridView1_ctl0' + gvid + '_lblCustom_Message_Save');

    lblEvent_Description.setStyle({display: 'none'});
    lblCustom_Message.setStyle({display: 'none'});
    lblCustom_Message_Save.setStyle({display: 'block'});
    txtCustom_Message.setStyle({display: 'block'});
}

function btncustommessagesave(gvid)
{
    gvid += 2;
    var lblEvent_Description = document.getElementById('GridView1_ctl0' + gvid + '_lblEvent_Description');
    var txtCustom_Message = document.getElementById('GridView1_ctl0' + gvid + '_txtCustom_Message');
    var lblCustom_Message = document.getElementById('GridView1_ctl0' + gvid + '_lblCustom_Message');
    var lblCustom_Message_Save = document.getElementById('GridView1_ctl0' + gvid + '_lblCustom_Message_Save');
    var lblEvent_Descriptionfull = document.getElementById('GridView1_ctl0' + gvid + '_pnlEvent_Descriptionfull');
    
    var EventKey = document.getElementById('GridView1_ctl0' + gvid + '_txtEventKey');
    
    var params = {"event_key":EventKey.getValue(),"type":2,"txtCustom":txtCustom_Message.getValue()};
    do_ajax3(params,gvid);
    lblEvent_Description.setTextValue(txtCustom_Message.getValue().substring(0,75));
    lblEvent_Descriptionfull.setTextValue(txtCustom_Message.getValue());
    

    lblEvent_Description.setStyle({display: 'block'});
    lblCustom_Message.setStyle({display: 'block'});
    lblCustom_Message_Save.setStyle({display: 'none'});
    txtCustom_Message.setStyle({display: 'none'});
}

function seeallbutton(gvid)
{
    //gvid += 2;
    var lblSellersSeeAll = document.getElementById('GridView1_ctl0' + gvid + '_additionalsellers');
    if ((lblSellersSeeAll.getStyle("display") == "") || (lblSellersSeeAll.getStyle("display") == "none"))
    {
        lblSellersSeeAll.setStyle({display: 'block'});
    }
    else
    {
        lblSellersSeeAll.setStyle({display: 'none'});
    }
    //new Dialog().showMessage('Dialog',lblSellersSeeAll);    
}

function opendescription()
{
    new Dialog().showMessage('Dialog','here');
    gvid += 2;
    var lblEvent_Descriptionfull = document.getElementById('GridView1_ctl00_pnlEvent_Descriptionfull');
    lblEvent_Descriptionfull.setStyle({display: 'block'});   

}
</script>
<span style="display:none;">
<a href="#" onclick="do_ajax('actionDiv',2);return false;">Option 1 (AJAX)</a>
<div id="actionDiv">[[Nothing happening in this div yet.]]</div></span>
<input type=text id=txttxkey style="display:none;" />

<br />
<!-- 
    <a href="paycc.aspx?tx_key=5">test link</a>
    <br />
    <asp:Label id=lblquery runat=server></asp:Label>
    <br />
    
    <asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack=true>
                <asp:ListItem value="CAD">CAD</asp:ListItem>
                <asp:ListItem value="USD">USD</asp:ListItem>
            </asp:DropDownList>
            <br />
    <asp:Button ID="btnSave" runat="server" 
                Text="Save Changes" onclick="btnSave_Click" />
                <br />
                <div id=btnNextStep class="OrderFormButtons" onclick="btnnext()">Next Step</div>                
                <a href=# onclick="btnnext(); return false;">Test next</a>
    
<br />
-->
<style type="text/css">
.OrderFormButtons, a.OrderFormButtons:link 
 { background-image: url(http://www.thegroupstore.com/Images/button_bg2.png);
   background-color:Transparent;
	float: left;
	background-repeat: no-repeat;
	line-height: 28px;
	margin-left:3px;
	color:White;
	 width:100px;
	 text-align:center;
	 text-decoration:none;
	 font-weight:bold;	 
     }
.EventTitle
{    
    font-size:small;    
     font-weight:bold;
    }

.EventDate
{    
    }
.EventText
{
    color:Gray;
}
.BuyButton
{
    color:#410067;
    font-size:small;    
    font-weight:bold;
}
.BuyButton_hidden
{
    color:#410067;
    font-size:small;    
    font-weight:bold;
    display:none;
}

</style>
<center>
<asp:TextBox id=txtfbid text='0' runat=server style="display:none;"></asp:TextBox>
<table width=520px>    
    <tr>
        <td>
            <table width=100%>
                <tr valign=middle>
                    <td>
                        <table>
                            <tr valign=middle>
                                <td><h1><asp:Label id=lblFirstName runat=server></asp:Label>&nbsp;</h1></td>
                                <td><a href="http://www.theGroupstore.com"> <img src=http://www.theGroupstore.com/Images/groupstore_Order_Logo2.png style=border-style:none;height:25px; /</a></td>
                            </tr>                            
                        </table>                        
                    </td>
                    <td width=250px>
                    <div style="text-align:right;">
                    <table >
                        <tr valign='middle'>
                        <td align='right'>
                            <table>
                                <tr>
                                    <td><a href="http://messenger.providesupport.com/messenger/lornestar.html" target="_blank">Live Support</a>
                                    </td>
                                    <td>|</td>
                                    <td><a href="http://www.facebook.com/apps/application.php?id=391377955486">About us</a></td>
                                    <td>|</td>
                                    <td><asp:Label id=lblreferal runat=server></asp:Label></td>           
                                </tr>
                            </table>                            
                        </td>                            
                        </tr>                        
                    </table>
                    </div>
                    </td>
                </tr>
                <tr>
                                <td colspan=2 style="text-align:center;">
                                    <fb:visible-to-owner>
                                    <!--
                                    <a href="http://promo.thegroupstore.com/Customize_Tab.aspx">**Click here to learn how to customize your Groupstore**</a>
                                    -->
                                    </fb:visible-to-owner>
                                </td>
                            </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align=center>        
        <asp:Panel ID=pnlGroupName runat=server>
<asp:GridView ID="GridView1" runat="server" ShowHeader=false AutoGenerateColumns=false BorderStyle="None" RowStyle-BorderStyle=None RowStyle-Wrap=true
         GridLines=None OnRowDataBound="GridView1_RowDataBound" Width=520px AlternatingRowStyle-CssClass="StorePage_Gridview_bottomBorder">
         <RowStyle Wrap="True" BorderStyle="None" VerticalAlign=Top></RowStyle>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table  style="border-bottom:1px solid #e1e1e1;height:100%; width:100%;" id=eachevent>
                            <tr>
                                <td>
                                
                                
                        <table  style="height:100%; width:100%;" id=eacheventinfo>
                            <tr valign=top >
                                <td align=right style="width:0px">
                                    <table>
                                        <tr>
                                            <td><asp:Image ID=ImgEvent runat=server Height="70px" Width="70px"/></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label id=lblsharebtn runat=server></asp:Label></td>
                                        </tr>
                                    </table>                                    
                                </td>
                                <td >
                                    <table style="height:100%; width:250px;">
                                        <tr valign=top>                                            
                                <td align=left><asp:Label id=lblEvent_Name runat=server Text='<%# Bind("Event_Name") %>'   CssClass="EventTitle"></asp:Label>
                        <br />
                        <asp:Label ID=lblDate runat=server class="EventDate"></asp:Label>
                        <br />
                        <asp:Label ID=lblEvent_Description runat=server Text='<%# Bind("Description") %>' CssClass="EventText"></asp:Label>
                        <fb:visible-to-owner>
                        <asp:Label id=lblCustom_Message runat=server visible=false></asp:Label>
                        <asp:TextBox id=txtCustom_Message Text='<%# Bind("Descriptionfull") %>' rows="6" width=300px TextMode="multiline" runat=server style="display:none;" ></asp:TextBox>
                        <asp:Label id=lblCustom_Message_Save runat=server style="display:none;"></asp:Label>
                        </fb:visible-to-owner>
                        <asp:Label ID=lblEvent_Key runat=server Visible=false Text='<%# Bind("Event_Key") %>' ></asp:Label>
                        <asp:Label ID=lblEvent_Begins runat=server Visible=false Text='<%# Bind("Event_Begins") %>' ></asp:Label>                                               
                        <asp:TextBox id=txtEventKey runat=server Text='<%# Bind("Event_Key") %>' style="display:none;"></asp:TextBox>
                        <asp:HiddenField ID=hdSFP value='<%# Bind("Service_Fee_Percentage") %>' runat=server />
                        <asp:HiddenField ID=hdSFC value='<%# Bind("Service_Fee_Cents") %>' runat=server />
                        <asp:HiddenField ID=hdSFM value='<%# Bind("Service_Fee_Max") %>' runat=server />
                        <asp:HiddenField ID=hdeid value='<%# Bind("eid") %>' runat=server />
                        <asp:HiddenField ID=hdOverallTotal value=0 runat=server />
                        <asp:HiddenField ID=hdServiceFee value=0 runat=server />
                                </td>                                
                                        </tr>
                                    </table>
                                </td>                                
                                <td width=150px>
                                <table>
                                    <tr>
                                        <td><asp:Label id=lblBackbutton runat=server CssClass="BuyButton_hidden"></asp:Label></td>
                                        <td style="text-align:right;"><asp:Label ID=lblBuy runat=server CssClass="BuyButton"></asp:Label><asp:Label id=lblNextbutton runat=server CssClass="BuyButton_hidden"></asp:Label></td>
                                        <td><img src=http://www.theGroupstore.com/Images/arrow2.jpg style=border-style:none;height:10px; /></td>
                                    </tr>                                                                    
                                    <tr>
                                        <td colspan=3>
                                        </td>
                                    </tr>                            
                                    <tr>
                                        <td colspan=3 >
                                        <asp:panel id=pnlGroupsellers runat=server style="text-align:center;" visible=false>
                                        <table width=130px style="background-color:#F0D8FF;">
                                            <tr>
                                                <td colspan=2>
                                                   <asp:Label id=lblSellerStats runat=server></asp:Label> 
                                                   <hr style=" color:White;"/>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td>Top Group Sellers</td>
                                            <td style="text-align:right;"><asp:Label id=lblSellersSeeAll runat=server></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan=2>
                                                <asp:Label id=lblGroupSellers runat=server></asp:Label>
                                                </td>
                                            </tr>                                                                                                                                            
                                        </table>                                                                                    
                                        </asp:panel>
                                        <fb:visible-to-owner>
                                        <asp:Panel id=pnlencourageseller runat=server visible=false>
                                            <table width=130px style="background-color:#F0D8FF;">
                                                <tr>
                                                    <td style="text-align:center;">
                                                    Encourage your friends to buy tickets from you by Sharing your groupstore.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:right;">
                                                        <asp:Label id=lblsharebtn2 runat=server></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        </fb:visible-to-owner>
                                        </td>
                                    </tr>
                                </table>
                                                                     
                                </td>
                            </tr>                            
                            <tr>
                                <td colspan=3>
                                <fb:visible-to-owner>            
                                <table>
                                    <tr>
                                        <td  style="background-color:#F0D8FF;"><!-- Encourage your friends to buy tickets from you by Sharing your groupstore.--></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label id=lblAdminlinks runat=server visible=false></asp:Label></td>
                                    </tr>          
                                    <tr>
                                        <td>                                        
                                            <asp:Label id=lblexternalsite runat=server style="display:none;" visible=false>External Site URL - <a href='THEURL' target=_blank>THEURL</a></asp:Label>                                        
                                        </td>
                                    </tr>                                                              
                                </table>                                
                                </fb:visible-to-owner>
                                </td>
                            </tr>
                        </table>   
                        </td>
                        </tr>
                        <tr>
                        <td>
                        <table id=paymentprocess>
                            <tr>
                                <td>
                                <asp:Panel id=pnltickets runat=server style='display:none;'>
                                    <table>                                        
                                        <tr>
                                            <td>
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound" ShowFooter="true" 
                 BorderStyle="None" RowStyle-BorderStyle=None GridLines=None  RowStyle-Wrap=true 
                  Width="500px" CssClass="ticket_selector">
<RowStyle Wrap="True" BorderStyle="None"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="Ticket_Description" HeaderText="Description" 
                        ReadOnly="True" ItemStyle-Width="200px"/>
                    <asp:BoundField DataField="Quantity_Remaining" HeaderText="Remaining" 
                        ReadOnly="True" Visible=false/>   
                    <asp:TemplateField HeaderText="Sale Ends">
                        <ItemTemplate>
                        <asp:Label id="lblEnds" runat="Server" text='<%# Bind("Sale_Ends") %>' Width=100px></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                 
                    <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("PriceRounded","{0:c}") %>'></asp:Label>
                        <asp:TextBox id=txtPrice runat=server Text='<%# Bind("PriceRounded","{0:c}") %>' style="display:none;"></asp:TextBox>                        
                        <asp:Label ID="lblTicketKey" runat="server" Text='<%# Bind("Ticket_Key") %>' Visible=false></asp:Label>              
                        <asp:TextBox ID="txtTicketKey" runat="server" Text='<%# Bind("Ticket_Key") %>' style="display:none;"></asp:TextBox>          
                        <asp:Label ID="lblEventKey" runat="server" Text='<%# Bind("Event_Key") %>' visible=false></asp:Label>                        
                        <asp:Label id=lblDollarSign runat=server visible=false>$</asp:Label>
                        <asp:TextBox id="txtDonate" Maxlength=6 runat="server" visible=false width=45px text="0.00"></asp:TextBox>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlQuantity" runat="server" >
                                <asp:ListItem value="0" selected="True">0</asp:ListItem>
                                <asp:ListItem value="1">1</asp:ListItem>
                                <asp:ListItem value="2">2</asp:ListItem>   
                                <asp:ListItem value="3">3</asp:ListItem>
                                <asp:ListItem value="4">4</asp:ListItem>
                                <asp:ListItem value="5">5</asp:ListItem>
                                <asp:ListItem value="6">6</asp:ListItem>
                                <asp:ListItem value="7">7</asp:ListItem>
                                <asp:ListItem value="8">8</asp:ListItem>
                                <asp:ListItem value="9">9</asp:ListItem>
                                <asp:ListItem value="10">10</asp:ListItem>                             
                            </asp:DropDownList>
                            <asp:Label ID=lblServiceFee runat=server Visible=false>Service fee</asp:Label>
                            <asp:Label id=lblDonate runat=server visible=false>1</asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" FooterStyle-CssClass="TicketPurchase_TotalFooter" >
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" Width="60" >$ 0.00</asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalOverall" runat="server" Width="60">$ 0.00</asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>

<FooterStyle BackColor="White"></FooterStyle>

<HeaderStyle CssClass="TicketPurchase_Header" HorizontalAlign="Left"></HeaderStyle>
            </asp:GridView>
                                            
                                            </td>
                                        </tr>
                                    </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <!--<asp:Label ID=lblspnquestions runat=server  Text='<span id=spnquestions_<%# Bind("Event_Key") %> style=display:none; >' ></asp:Label>-->
                                <asp:Panel id=pnlquestions runat=server style='display:none;'>
                                <table Width="510px">
                                                    
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td colspan=2>What name do you want showing on the guest list?</td>
                                                                </tr>
                                                                <tr>
                                                        <td><span class="Mandatory_Star">*</span> First Name:</td>
                                                        <td><asp:TextBox ID=txtList_FirstName runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span class="Mandatory_Star">*</span> Last Name:</td>
                                                        <td><asp:TextBox ID=txtList_LastName runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Panel id=pnltxtEmail runat=server><span class="Mandatory_Star">*</span> Email:</asp:Panel></td>
                                                        <td><asp:TextBox ID=txtList_Email runat=server Width=200px></asp:TextBox></td>
                                                    </tr>
                                                            </table>
                                                        </td>
                                                    </tr>                                                    
                                                </table>
                                </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <asp:Panel id=pnlpayment runat=server style='display:none;'>
                                <table Width="510px">
                                    <tr>
                                                    <td ><b>How would you like to pay?</b></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ul>
                                                            <li>PayPal Account<asp:RadioButton Checked=false id=chkPaypal GroupName="PaymentMethod" value="Paypal" runat=server onClick="clickedPaypal()"/>
                                                            </li>
                                                            <li>Credit Card <asp:RadioButton Checked=false id=chkCC GroupName="PaymentMethod" value="CC" runat=server onClick="clickedCC()"/>
                                                            </li>
                                                        </ul>
                                                    </td> 
                                                </tr>
                                </table>
                                </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        </td>
                            </tr>
                            <tr>
                                <td>                                
                                <table>
                                    <tr valign=top>
                                        <td>
                                        <asp:Panel id=pnlEvent_Descriptionfull runat=server style="display:none;">
                                        <asp:Label ID=lblEvent_Descriptionfull runat=server Text='<%# Bind("Descriptionfull") %>' CssClass="EventText" ></asp:Label>
                                        <br />
                                        <table width=100%>
                                        <tr>
                                            <td width=100px></td>
                                            <td style="text-align:right;"><asp:Label id="lblfbcomments" visible=false runat="server"></asp:Label>
                                            
                                            </td>
                                            </tr>
                                        </table>                            
                                        </asp:Panel>
                                        </td>
                                        <td><!--Thermometer-->
                                <asp:Panel id=pnlthermometer runat=server>
                                    <table>
                                        <tr valign=top>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>Fundraising Goal</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label id=lblFundraisingGoal runat=server></asp:Label>
                                                                    <fb:visible-to-owner>
                                                                    <asp:TextBox id=txtFundraisingGoal width=45px maxlength=6 runat=server style="display:none;"></asp:TextBox>                                                                
                                                                    </fb:visible-to-owner>
                                                                </td>
                                                                <td>
                                                                    <fb:visible-to-owner>
                                                                    <asp:Label id=lbleditFundraisingGoal runat=server ></asp:Label>
                                                                    <asp:Label id=lblsaveFundraisingGoal runat=server style="display:none;"></asp:Label>
                                                                    </fb:visible-to-owner>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                                                                                                                                        
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Money Raised</td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label id=lblFundraised runat=server></asp:Label>
                                                        <asp:TextBox id=txtFundraised runat=server style="display:none;"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <asp:Image runat=server id=imgthermometer height=142px></asp:Image>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                </td>
                                    </tr>
                                </table>
                                
                                </td>                                
                            </tr>                            
                        <tr>
                            <td style="text-align:right;">                            
                            
                            </td>
                        </tr>                        
                        </table>
                    </ItemTemplate>
                   </asp:TemplateField>                   
            </Columns>
        </asp:GridView>
        </asp:Panel>        
        </td>
    </tr>
    <tr>
        <td><asp:Label ID=lblheader runat=server Visible=false></asp:Label></td>
    </tr>
    <tr>
        <td style="text-align:right;">
                          
        <!-- Begin Official PayPal Seal -->
        <a href="https://www.paypal.com/us/verified/pal=lorne%40lornestar%2ecom" target="_blank">
        <img src="https://www.paypal.com/en_US/i/icon/verification_seal.gif" order="0" alt="Official PayPal Seal" height=70px>
        </A>
        <!-- End Official PayPal Seal -->
        &nbsp;
        </td>
    </tr>
</table>
</center>
</div>
</form>

