var buttonheight = 50;

ToolbarText = Ext.extend(Ext.Component, {
    cls: 'toolbar-text x-toolbar-title',
    flex: 1
});
Ext.reg('toolbartext', ToolbarText);

var Page_Sell = Ext.extend(Ext.Panel, {
    title: 'Sell',
    id: 'Page_Sell',
    iconCls: 'sell',
    items: [
        {
            id: 'Sell_Page2',
            layout: 'card',
            height: 800
        }
    ]    
});

var Page_Sell_root = new Ext.Panel({
    id: 'Page_Sell_root',
    layout: {
        align: 'stretch'
    },
    items: [
        {
            id: 'theamountbox',
            layout: 'card',
            height: 80
        },
        {
            id: 'thekeypad',
            layout: 'card',
            height: 200
        },
            {
                id: 'paymentupdateshell',
                layout: 'hbox',
                //style: 'width: 90%;border:1px solid black;',
                items: [
                    {
                        id: 'thepaymentupdate',
                        html: 'No Activity'
                        //flex: 9
                    }
                    /*
                    {
                        flex: 1,
                        html: '<a href="#" onClick="switchkeypad()"><div style="border:1px solid black;float:right;padding-right:10px;padding-left:10px;">Toggle KeyPad</div></a>'
                    }*/
                    ]
            }

    ],
    dockedItems: [
        {
            id: 'sellingtoolbar',
            dock: 'top',
            xtype: 'toolbar',
            ui: 'light',
            align: 'stretch',
            items: [                
                {
                    id: 'btnpayhere',
                    text: 'Pay Here',
                    handler: function () {
                        btnPayhere();
                    }
                },
                { xtype: 'spacer' },
                {
                    id: 'btnback',
                    text: 'Cancel',
                    ui: 'back',
                    hidden: true,
                    handler: function () {
                        btnCancelPayForward();
                    }
                },
                {
                    id: 'btnpayforward',
                    text: 'Pay Forward',
                    ui: 'forward',
                    handler: function () {
                        btnPayForward();
                    }
                },
                {
                    id: 'btnsendsms',
                    text: 'Send SMS',
                    ui: 'confirm',
                    hidden: true,
                    handler: function () {
                        btnsendsms();
                    }
                }
            ]
        }
    ]
});


var amountPanel = new Ext.Panel({
    html: 'Slides can be used in tandem with <code>direction: "up/down/left/right"</code>.'    
});

var smsPanel = new Ext.Panel({
    html: 'SMS Panel'    
});

/*
{            xtype: 'textfield',
            value: '$ 0.00',
            name: 'amountboxname'
        },
    {
        xtype: 'textareafield',
        name: 'note',
        maxRows: 2,
        maxLength: 60,
        placeHolder: 'Add Note'
    },*/

    //alert(document.getElementsByName("amountboxname23"));
    
//amountbox.setValue('testing');

Ext.reg('Sell_Page', Page_Sell);



//******************************Normal js functions*********************************************
function gettxtamount() {
    return document.getElementsByName("amountboxname")[0];
}

function getsmsbox() {
    return document.getElementsByName("smsbox")[0];
}

function keypadpress(addamount) {
    if (Page_Sell_root.getComponent('theamountbox').getActiveItem().id == "theamountboxcomponent") //money
    {
        keypadpressmoney(addamount);
    }
    else //phone #
    {
        keypadpressphone(addamount);
    }
}

function keypadpressmoney(addamount) {
    var txtamount = gettxtamount();
    var currentplace = parseInt(document.getElementById("hdkeypadplace").value);
    var currentnum = txtamount.value.replace("$ ", "").replace(".", "");
    var currentlength = currentnum.length;

    document.getElementById("hdkeypadplace").value = currentplace + 1; ;

    var thenewtxtamount = "";
    if (currentplace >= 3) //need to add decimal
    {
        thenewtxtamount = currentnum + addamount;
    }
    else if (currentplace == 0) {
        thenewtxtamount = currentnum.substring(0, currentlength - 1) + addamount;
    }
    else if (currentplace == 1) {
        thenewtxtamount = currentnum.substr(0, 1) + currentnum.substr(currentlength - 1, 1) + addamount;
    }
    else if (currentplace == 2) {//036
        thenewtxtamount = currentnum.substr(1, 2) + addamount;
    }
    thenewtxtamount = addthedecimal(thenewtxtamount);
    thenewtxtamount = addthesymbol(thenewtxtamount);
    txtamount.value = thenewtxtamount;
}

function keypadpressphone(addamount) {
    var smsbox = getsmsbox();
    var currentplace = parseInt(document.getElementById("hdkeypadplace").value);
    var currentnum = smsbox.value.replace("(", "").replace(")", "").replace("-", "").replace(/#/gi, "").replace(" ", "");
    var currentlength = currentnum.length;

    var thenewtxtamount = currentnum.replace(/^\s+|\s+$/g, "");
    if (currentlength < 11) //can still put in more numbers
    {
        thenewtxtamount += addamount;
    }

    for (i = 0; i < 10 - currentlength; i++) {
        thenewtxtamount += "#";
    }
    smsbox.value = addthephone(thenewtxtamount);
    if (currentlength == 9)//reached end , now turn on button
    {
        getSellPage().getDockedComponent(0).getComponent('btnsendsms').show();        
    }
}

function addthesymbol(amount) {
    return "$ " + amount;
}

function addthedecimal(amount) {
    var currentlength = amount.length;
    return amount.substring(0, currentlength - 2) + "." + amount.substring(currentlength - 2, currentlength);
}

function addthephone(phone) {
    var thephone = "(" + phone.substr(0, 3) + ") " + phone.substr(3, 3) + "-" + phone.substr(6, 4);
    return thephone;
}

function refreshkeypad() {
    if (Page_Sell_root.getComponent('theamountbox').getActiveItem().id == "theamountboxcomponent") //money
    {
        var txtamount = gettxtamount();
        txtamount.value = addthesymbol("0.00");
    }
    else //phone #
    {
        var smsbox = getsmsbox();
        smsbox.value = "(###) ###-####";
        getSellPage().getDockedComponent(0).getComponent('btnsendsms').hide();
    }
    document.getElementById("hdkeypadplace").value = "0";
}

function btnPayhere() {
    if (checkifhaveamount()) {
        initiatetx(false);
    }
}

function btnCancelPayForward() {
    thecard = Page_Sell_root.getComponent('theamountbox');
    thenewcard = amountbox;
    thecard.setActiveItem(thenewcard, { type: 'slide', direction: 'right' });

    getSellPage().getDockedComponent(0).getComponent('btnback').hide();
    getSellPage().getDockedComponent(0).getComponent('btnpayforward').show();
    getSellPage().getDockedComponent(0).getComponent('btnsendsms').hide();        
}

function btnPayForward() {
    if (checkifhaveamount()) {
        thecard = Page_Sell_root.getComponent('theamountbox');
        thenewcard = newcard;
        thecard.setActiveItem(thenewcard, 'slide');

        getSellPage().getDockedComponent(0).getComponent('btnback').show();
        getSellPage().getDockedComponent(0).getComponent('btnpayforward').hide();
        refreshkeypad();
    }    
}

function btnsendsms() {
    initiatetx(true);

}

function initiatetx(boolpayforward) {
    Ext.getBody().mask("<div class='demos-loading'>Loading</div>");    
    var setuptype = "SetupTx";
    if (boolpayforward) {
        setuptype = "SetupForwardTx"
    }
    var thephonenumber = "";
    if (getsmsbox()) {
        thephonenumber = getsmsbox().value;
    }
    globalamount = gettxtamount().value;
    
    Ext.util.JSONP.request({
        url: pfjson,
        callbackKey: 'callback',
        params: {
            type: setuptype,
            amount: gettxtamount().value.replace("$ ", ""),
            fbid: document.getElementById("hdnfbid").value,
            note: document.getElementsByName("txtnote")[0].value,
            ipaddress: document.getElementById("hdipaddress").value,
            resource_key: document.getElementById("hdResource_Key").value,
            phonenumber: thephonenumber
        },
        callback: function (result) {
            if (result.txkey) {
                //check if is ordered from menu
                if (Page_Sell_root.getComponent('thekeypad').getActiveItem().id == "MerchantOrderbox") {
                    initiatetx_MenuItems(result.txkey,boolpayforward);
                }
                else {
                    initiatetx_completeit(boolpayforward, result.txkey);
                }

            }
            else {
                alert("An error occured.");
            }
            Ext.getBody().unmask();
        }
    });      

}

function initiatetx_MenuItems(txkey, boolpayforward) {
    var setuptype = "updateProductsPurchased";    
    Ext.util.JSONP.request({
        url: pfjson,
        callbackKey: 'callback',
        params: {
            type: setuptype,
            txkey: txkey,
            Ticket_Keys: MenuItemsencode()
        },
        callback: function (result) {
            if (result) {
                initiatetx_completeit(boolpayforward, txkey);
            }
            else {
                alert("An error occured.");
            }
        }
    })
}

function initiatetx_completeit(boolpayforward, txkey) {    
    if (boolpayforward) { //payforward
        //alert(result.txkey);
        btnCancelPayForward();
        refreshkeypad();
        //thecard = toppanel.getComponent('Sell_Page').getComponent('thepaymentupdate');
        //thecard.setActiveItem(PendingPayment, 'fade');

        startpendingpayment();
    }
    else { //pay here
        //alert('pay here');        
        txtopay = txkey;
        toggleSellingPayment();     
    }
}

function MenuItemsencode() {
    var strReturn = "";
    for (i = 0; i < MenuItemsSelectedjson.length - 1; i += 1) {        
        strReturn += "~"+ MenuItemsSelectedjson[i].data['Ticket_Key'] + "-";
        strReturn += MenuItemsSelectedjson[i].data['quantity'];
    }    
    return strReturn
}

function resetamountbox() {    
    thecard = Page_Sell_root.getComponent('theamountbox');
    thenewcard = amountbox;
    thecard.setActiveItem(thenewcard, 'slide');
    Page_Sell_root.getComponent('theamountbox').getComponent('theamountboxcomponent').getComponent('amountboxamount').getComponent('sellingtoolbartext').update(document.getElementById("hdnfbfirstname").value + ' - ' + document.getElementById("hdStoreName").value);
    Page_Sell_root.setScrollable(false);
    toppanel.getComponent('Sell_Page').setScrollable(false);


    Page_Sell_root.getComponent('thekeypad').setActiveItem(thekeypadbuttons);
    //toppanel.getComponent('Sell_Page').getComponent('thekeypad').setActiveItem(MerchantOrderbox);
}

function getSellPage() {
    return Page_Sell_root;
}

function keypaddelete() {
    if (Page_Sell_root.getComponent('theamountbox').getActiveItem().id == "theamountboxcomponent") //money
    {
        var txtamount = gettxtamount();
        var currentplace = parseInt(document.getElementById("hdkeypadplace").value);
        var currentnum = txtamount.value.replace("$ ", "").replace(".", "");
        var currentlength = currentnum.length;



        if ((currentlength > 0) && (currentnum != "000")) {
            document.getElementById("hdkeypadplace").value = currentplace - 1; ;
            var thenewtxtamount = "";
            if (currentlength < 4) {
                thenewtxtamount = "0" + currentnum.substring(0, currentlength - 1);
            }
            else {
                thenewtxtamount = currentnum.substring(0, currentlength - 1);
            }                       

            thenewtxtamount = addthedecimal(thenewtxtamount);
            thenewtxtamount = addthesymbol(thenewtxtamount);
            txtamount.value = thenewtxtamount;
        }                
    }
    else //phone #
    {
        var smsbox = getsmsbox();
        var currentplace = parseInt(document.getElementById("hdkeypadplace").value);
        var currentnum = smsbox.value.replace("(", "").replace(")", "").replace("-", "").replace(/#/gi, "").replace(" ", "");
        var currentlength = currentnum.length;

        var thenewtxtamount = currentnum.replace(/^\s+|\s+$/g, "");
        if (currentlength >  0) //can still put in more numbers
        {
            thenewtxtamount = thenewtxtamount.substring(0, currentlength - 1);
            currentlength -= 1;
        }

        for (i = 0; i < 10 - currentlength; i++) {
            thenewtxtamount += "#";
        }
        smsbox.value = addthephone(thenewtxtamount);
        getSellPage().getDockedComponent(0).getComponent('btnsendsms').hide();
    }
}

function checkifhaveamount() {
    //checks if there is an amount great than 0 in the amount box
    var txtamount = gettxtamount();
    var currentnum = txtamount.value.replace("$ ", "").replace(".", "");
    var boolreturn = true;

    if (currentnum == "000") {
        boolreturn = false;
        //alert("You must enter an amount to charge.");
        Ext.Msg.alert('','You must enter an amount to charge.', Ext.emptyFn);
    }    
    return boolreturn;
}

function switchkeypad() {
    if (Page_Sell_root.getComponent('thekeypad').getActiveItem().id == "MerchantOrderbox") {
        Page_Sell_root.getComponent('thekeypad').setActiveItem(thekeypadbuttons, { type: 'slide', direction: 'right' });
    }
    else {
        Page_Sell_root.getComponent('thekeypad').setActiveItem(MerchantOrderbox, { type: 'slide', direction: 'left' });
    }   
    
}