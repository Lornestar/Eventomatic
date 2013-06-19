var txtopay = 0;
var isMobilePay = false;
var globalamount = "0";
var expresscheckout_url = "";
var istxcomplete = "false";
var checktxinterval;

var Page_DirectPayment = new Ext.Panel({

    title: 'Payment',
    //html: 'Share Groupstore.',    
    //cls: 'card card1',    
    id: 'Page_Payment',
    layout: 'card',
    dockedItems: [
        {
            id: 'paymentstoolbar',
            dock: 'top',
            xtype: 'toolbar',
            ui: 'light',
            align: 'stretch',
            items: [
                {
                    id: 'btnpaymentcancel',
                    text: 'Cancel',
                    ui: 'back',
                    handler: function () {
                        if (Page_DirectPayment.getActiveItem().id == "PaymentChoose") {
                            toggleSellingPayment();
                        }
                        else {
                            Page_DirectPayment.setActiveItem(PaymentChoose, { type: 'slide', direction: 'left' });
                            if (isMobilePay) {
                                this.hide();
                            }
                        }
                        Page_DirectPayment.getDockedComponent(0).getComponent('btnScanCard').hide();
                        stopcheckfortxcomplete();
                    }
                },
                { xtype: 'spacer' },
                {
                    id: 'pnlcurrentamount',
                    xtype: 'panel'
                    //html: '<span style="color:white;">' + document.getElementById("hdcurrentamount").value + '</span>'
                },
                { xtype: 'spacer' },
                {
                    xtype: 'button',
                    id: 'btnScanCard',
                    text: 'Scan Card',
                    hidden: true,
                    handler: function () {
                        btnScanCard();
                    }
                }
                ]
        }],
    afterLayout: function (layout) {
        this.setScrollable(false);        
    }
});

var PaymentChoose = new Ext.Panel({
    id: 'PaymentChoose',
    cls: 'card card1',
    items: [
        {
            html: '<span style=" font-size:medium;">Choose payment method</span>'
            
        },
        {
            xtype: 'button',
            id: 'btnPayCC',
            text: 'Credit Card',
            handler: function () {
                btnGetExpressCheckouturl();
                stopcheckfortxcomplete();
            }
        },
        {
        },
        {
            xtype: 'button',
            id: 'btnPayCCWPP',
            text: 'Credit Card Fast',
            handler: function () {       //use directpayment         
                if (document.getElementById("hddodirectpayment").value == "True") {
                    Page_DirectPayment.setActiveItem(PaymentCC, { type: 'slide', direction: 'right' });
                    if (isMobilePay) {
                        Page_DirectPayment.getDockedComponent(0).getComponent('btnpaymentcancel').show();
                    }

                    if (document.getElementById("hdisnative").value == "True") {
                        Page_DirectPayment.getDockedComponent(0).getComponent('btnScanCard').show();

                    }
                    stopcheckfortxcomplete();
                }
                else {
                    paywithpaypal();
                }
            }
        },
        {
        },
        {
            xtype: 'button',
            id: 'btnPayPP',
            text: 'PayPal',
            handler: function () {
                paywithpaypal();
            }
        }
    ]
});

    function paywithpaypal() {
        var txkey = txtopay;
        if (document.getElementById("hdos").value == "android") {
            //var thenewurl = document.getElementById("hdStore_URL").value + "mobilepay.aspx?tx=" + txkey + "&paywithpp=true&isnative=true";
            //window.demo.clickOnGotoNewURL(thenewurl);                    
            window.demo.clickOnPayPal(txkey, globalamount.replace("$ ", ""), document.getElementById("hdppemail").value, document.getElementById("hdResourceCurrency").value, document.getElementById("hdStoreName").value);
        }
        else { //open paywithpaypal on ios
            //location.href = document.getElementById("hdStore_URL").value + "mobilepay.aspx?tx=" + txkey + "&paywithpp=true";                    
            //alert('ios');
            var islive = true;
            if (document.getElementById("hdisDemo").value == "True") { //is demo
                islive = "false";
            }
            else {
                islive = "true";
            }
            //NativeBridge.call("runcardscan", [], function (ccinfo) {
            checkfortxcomplete();
            NativeBridge.call("clickOnPayPal", [txkey, globalamount.replace("$ ", ""), document.getElementById("hdppemail").value, document.getElementById("hdResourceCurrency").value, document.getElementById("hdStoreName").value, islive], function (strresult) {
                //when return
                //alert(strresult);
                alert("done call");
            });

        }
}

function checkisdodirectpayment() {
    if (document.getElementById("hddodirectpayment").value == "True") {

    }
    else { //hide Pay with Credit Card WPP
        PaymentChoose.getComponent('btnPayCCWPP').setText('PayPal');
        PaymentChoose.getComponent('btnPayPP').hide();        
    }
}

function resetpaymentfields() {
    PaymentCC.getComponent('paymentccnumber').reset();
    PaymentCC.getComponent('paymentccMonth').reset();
    PaymentCC.getComponent('paymentccYear').reset();
    PaymentCC.getComponent('paymentccCSC').reset();    
    PaymentCC.getComponent('paymentccName').reset();
    PaymentCC.getComponent('paymentemail').reset();
    
    if (document.getElementById("hdisDemo").value == "True") { //is demo
        PaymentCC.getComponent('paymentccnumber').setValue('4757654115438898');
        PaymentCC.getComponent('paymentccMonth').setValue('05');
        PaymentCC.getComponent('paymentccYear').setValue('13');
        PaymentCC.getComponent('paymentccCSC').setValue('000');
        PaymentCC.getComponent('paymentccName').setValue('Joe Smith');
    }
    Page_DirectPayment.getDockedComponent(0).getComponent('pnlcurrentamount').update('<span style="color:white;">' + globalamount + '</span>');
}

var PaymentCC = new Ext.Panel({
    id: 'thePaymentCC',
    xtype: 'fieldset',
    items: [
                {
                    id: 'paymentfieldsetmsg',
                    html: 'Enter Credit Card info'
                },
                {
                    xtype: 'textfield',
                    name: 'ccnumber',
                    placeHolder: '0000000000000000',
                    label: 'Card #',
                    id: 'paymentccnumber',
                    maxLength: 40,
                    required: true,
                    disableKeyFilter: true,
                    validateOnBlur: false,
                    validateOnChange: false,
                    validationEvent: false,
                    maskRe: /[A-Za-z0-9]/,
                    regex: /[A-Za-z0-9]/,
                    listeners: {
                        'keyup': function (elem, evnt) {
                            /* var currentval = this.getValue();
                            if ((currentval.length == 16) && (currentval.indexOf('-') == -1)) {
                            this.setValue(currentval.substr(0, 4) + '-' + currentval.substr(4, 4) + '-' + currentval.substr(8, 4) + '-' + currentval.substr(12));
                            }
                            else if (currentval.length == 9) {
                            this.setValue(currentval.substr(0, 8) + '-' + currentval.substr(8));
                            }
                            else if (currentval.length == 5) {
                            this.setValue(currentval.substr(0, 4) + '-' + currentval.substr(4));
                            }*/
                            //this.setValue(this.getValue());
                        }
                    }
                },
                {
                    xtype: 'textfield',
                    name: 'ccmonth',
                    placeHolder: '08',
                    label: 'Month',
                    id: 'paymentccMonth',
                    maxLength: 2,
                    required: true
                },
                {
                    xtype: 'textfield',
                    name: 'ccyear',
                    placeHolder: '13',
                    label: 'Year',
                    id: 'paymentccYear',
                    maxLength: 4,
                    required: true
                },
                     {
                    xtype: 'textfield',
                    name: 'ccCSC',
                    placeHolder: '000',
                    label: 'CSC',
                    id: 'paymentccCSC',
                    maxLength: 4,
                    required: true
                },

    { xtype: 'textfield',
    placeHolder: 'Joe Smith',
    maxLength: 60,
    name: 'ccname',
    id: 'paymentccName',
    label: 'Name',
    required: false
    },
                    {xtype: 'textfield',
                    placeHolder: '416-888-5555',
                    maxLength: 12,
                    name: 'paymentemail',
                    id: 'paymentemail',
                    label: 'Phone',
                    required: true
                },
                    {
                        xtype: 'button',
                        id: 'btnPaymentCC',
                        text: 'Pay',
                        handler: function () {
                            btnPaymentCC();
                        }
                    }
                    , {
                        html: "<center><img src='../images/Paypal2.png'/></center>"                     
                        }
                ]
});


function sendtosignature() {
    toppanel.setActiveItem(Payment_Signature, { type: 'slide', direction: 'right' });
    //toppanel.getComponent('Sell_Page').getComponent('Sell_Page2').setActiveItem(Payment_Signature, { type: 'slide', direction: 'right' });
    if (toppanel.getComponent('toppaneltabbar')) {
        toppanel.getComponent('toppaneltabbar').hide();
    }    
}

function btnPaymentCC() {
    Ext.getBody().mask("<div class='demos-loading'>Processing Card</div>");    
    var localpfjson = "/payforward_ajax.aspx";
    Ext.util.JSONP.request({
        url: localpfjson,
        callbackKey: 'callback',
        params: {
            type: "PaymentCC",
            txkey: txtopay,
            ccnumber: PaymentCC.getComponent('paymentccnumber').getValue(),
            ccexpmonth: PaymentCC.getComponent('paymentccMonth').getValue(),
            ccexpyear: PaymentCC.getComponent('paymentccYear').getValue(),
            ccCSC: PaymentCC.getComponent('paymentccCSC').getValue(),
            ccname: PaymentCC.getComponent('paymentccName').getValue(),
            ccemail: PaymentCC.getComponent('paymentemail').getValue()
        },
        callback: function (result) {
            //alert(result.PaymentComplete);
            if (result.PaymentComplete) {
                if (result.PaymentComplete == "true") {//payment went through
                    //Ext.Msg.alert('', 'Your Transaction was Successful.', Ext.emptyFn);
                    //toggleSellingPayment();                    
                    //refreshkeypad();
                    sendtosignature();
                }
                else {//payment did not go through                    
                    //Ext.Msg.alert('', 'Your Transaction did not succeed. Please Try again.', Ext.emptyFn);
                    Ext.Msg.alert('', result.PaymentComplete, Ext.emptyFn);
                }
            }
            else {
                alert("An error occured.");
            }
            Ext.getBody().unmask();
        }
    });
}

function btnGetExpressCheckouturl() {
    var localpfjson = "/payforward_ajax.aspx";    
    Ext.util.JSONP.request({
        url: localpfjson,
        callbackKey: 'callback',
        params: {
            type: "ECurl",
            txkey: txtopay
        },
        callback: function (result) {
            //alert(result.PaymentComplete);
            if (result.ECurl) {
                expresscheckout_url = result.ECurl;
                //second send url to native
                if (document.getElementById("hdos").value != "ios") { //is android
                    window.demo.clickOnExpressCheckout(expresscheckout_url);
                }
                else { //is ios
                    NativeBridge.call("clickOnExpressCheckout", [expresscheckout_url], function (strresult) {
                        alert(strresult);
                    });
                }
            }
            else {
                alert("EC error occured. tx "+ txtopay);
            }
        }
    });
}


function btnScanCard() {
    if (document.getElementById("hdos").value == "ios") {
   
        //alert("ios");
        NativeBridge.call("runcardscan", [], function (ccinfo) {
            //insert callback actions here

            

            var mySplitResult = ccinfo.split("|");

            PaymentCC.getComponent('paymentccnumber').setValue(mySplitResult[0]);
            PaymentCC.getComponent('paymentccMonth').setValue(mySplitResult[1]);
            PaymentCC.getComponent('paymentccYear').setValue(mySplitResult[2]);
            PaymentCC.getComponent('paymentccCSC').setValue(mySplitResult[3]);
            //alert(ccinfo);
            

        });
        /*
        NativeBridge.call("runcardscan", ["do you see blue background ?"], function (response) {
            alert("Clicked something");
            if (response) {
                alert("Clicked Yes");
                //document.body.innerHTML += "<br/>You saw blue background, all is perfectly fine!<br/>";
            } else {
                alert("Clicked No");
                //document.body.innerHTML += "<br/>Are you sure ? Because you have to see blue!<br/>";
            }
            
        });  */     
    }
    else {
        //alert("android");
        window.demo.clickOnScanCard();
    }
    //
}


function getcardinfo(ccnum, expmonth, expyear, csc) {
    PaymentCC.getComponent('paymentccnumber').setValue(ccnum);
    PaymentCC.getComponent('paymentccMonth').setValue(expmonth);
    PaymentCC.getComponent('paymentccYear').setValue(expyear);
    PaymentCC.getComponent('paymentccCSC').setValue(csc);
}

function successtxmpl() {
    domasspay3();
    
    toggleSellingPayment();
    if (toppanel.getComponent('toppaneltabbar')) {
        refreshkeypad();
        resetamountbox();
        if (toppanel.getComponent("toppaneltabbar").getComponent(5)) {
            toppanel.getComponent("toppaneltabbar").getComponent(5).destroy();
        }
        try {
            toppanel.setActiveItem('Sell_Page');
        }
        catch (err) {

        }
    }
    Ext.Msg.alert('', 'Transaction was Successful.', Ext.emptyFn);       
}

function checkfortxcomplete() {    
    //istxcomplete = "false";
    //checktxinterval = setInterval(checkfortxcomplete2, 3000);
}

function stopcheckfortxcomplete() {
    //clearInterval(checktxinterval);
}

function checkfortxcomplete2() {
    var localpfjson = "/payforward_ajax.aspx";

    //alert('checkfortxcomplete2');

    Ext.Ajax.request({
        url: localpfjson,
        method: 'GET',
        params: {
            type: "Checktxstatus",
            txkey: txtopay
        },
        reader: {
            type: 'json',
            root: 'txstatus'
        },
        success: function (result) {
            if (result.responseText) {
                if (result.responseText.length > 2) {                    
                    if (result.responseText == "true") {                                                
                        successtxmpl();
                    }
                }
                else {

                }
            }
            else {
                alert("An error occured.");
            }
        }
    });
}

function domasspay3() {
    var localpfjson = "/payforward_ajax.aspx";
    Ext.Ajax.request({
        url: localpfjson,
        method: 'GET',
        params: {
            type: "MassPay3",
            txkey: txtopay
        },
        reader: {
            type: 'json',
            root: 'txstatus'
        },
        success: function (result) {
          /*  if (result.responseText) {
                if (result.responseText.length > 2) {
                    if (result.responseText == "true") {
                        successtxmpl();
                    }
                }
                else {

                }
            }
            else {
                alert("An error occured.");
            }
            */
        }
    });    
}