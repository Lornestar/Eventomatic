var toppanel, bottomTab;
var pfjson = "";
Pollingvar = "";


Ext.setup({
    icon: 'Images/groupstore_Order_Logo2.png',    
    glossOnIcon: false,
    onReady: function () {


        toppanel = new Ext.TabPanel({
            fullscreen: true,
            ui: 'dark',
            sortable: true,
            layout: 'card',
            tabBar: {
                id: 'toppaneltabbar',
                dock: 'bottom',
                ui: 'light',
                scroll: {
                    direction: 'verical',
                    useIndicators: false
                },
                layout: {
                    pack: 'center'
                }
            },
            defaults: {
                scroll: 'vertical'
            },
            items: [
            {
                id: 'Sell_Page',
                xtype: 'Sell_Page'
            },
            {
                id: 'Page_Report',
                xtype: 'Page_Report'

            },
            {
                id: 'Page_Settings',
                xtype: 'Page_Settings'
            },
            {
                xtype: 'Share_Page'
            },
            {
                xtype: 'Help_Page'
            }
            ],
            /*
            initComponent: function () {
            if (Ext.is.Phone && Ext.Viewport.getOrientation() == 'portrait') {
            //toppanel.getComponent('toppaneltabbar').hide();
            alert('here');
            }
            else {
            //toppanel.getComponent('toppaneltabbar').show();
            alert('not');
            }
            }*/

            

        });

        fbloginredirect = "http://www.facebook.com/dialog/oauth?client_id=" + document.getElementById("hdfbAppid").value + "&redirect_uri=" + document.getElementById("hdStore_URL").value + "PayForward2.aspx&display=touch&scope=email";
        pfjson = document.getElementById("hdStore_URL").value + "PayForward_Ajax.aspx";
        runIntervals();
        Pollingvar = setInterval(runIntervals, 5000);
        showwelcome();
        toppanel.getComponent('Sell_Page').getComponent('Sell_Page2').setActiveItem(Page_Sell_root);
        resetamountbox();

        if (document.getElementById("hdos").value != "browser") {
        }
    }

});

function runIntervals() {
    if (toppanel.getActiveItem().id == "Page_Report") {
        callreportlist(0);
    }
    if (toppanel.getActiveItem().id == "Sell_Page") {
        callreportlist(2);                
    }    
}

function showwelcome() {
    if (document.getElementById("hdshowwelcome").value == '1') {
        //admin popup
        Ext.Msg.alert('Welcome ' + document.getElementById("hdnfbfirstname").value, 'You are now an admin of this Store.  You have access to make changes to store settings, add/remove store sellers, and accept payments for the store...Happy Selling.', Ext.emptyFn);
    }
    else if (document.getElementById("hdshowwelcome").value == '2') {
        //seller popup
        Ext.Msg.alert('Welcome ' + document.getElementById("hdnfbfirstname").value, 'You are now an admin of this Store.  You have access to make add store sellers, and accept payments for the store...Happy Selling.', Ext.emptyFn);
    }
    else if (document.getElementById("hdshowwelcome").value == '3') {
        //demo popup
        Ext.Msg.alert('Welcome ' + document.getElementById("hdnfbfirstname").value, 'You are trying the Snappay Demo Store.  All payments in this store are done with Fake Money.<br/><br/><b>Go to snap-pay.com</b> to open your own Live Store.', Ext.emptyFn);
    }
}

function toggleSellingPayment() {
    if (toppanel.getComponent('Sell_Page').getComponent('Sell_Page2').getActiveItem().id == "Page_Sell_root") {
        toppanel.getComponent('Sell_Page').getComponent('Sell_Page2').setActiveItem(Page_DirectPayment, { type: 'fade', direction: 'left' });
        resetpaymentfields();
        Page_DirectPayment.setActiveItem(PaymentChoose);
        Page_DirectPayment.getDockedComponent(0).getComponent('btnScanCard').hide();
        checkisdodirectpayment();
    }
    else {
        toppanel.getComponent('Sell_Page').getComponent('Sell_Page2').setActiveItem(Page_Sell_root, { type: 'fade', direction: 'left' });
    }
}

function initNative() {
    if (document.getElementById("hdos").value == "android") {
        if (document.getElementById("hdisDemo").value == "True") { //is demo
            window.demo.initNative(false);
        }
        else {
            window.demo.initNative(true);
        }
    }
    else {
        if (document.getElementById("hdisDemo").value == "True") { //is demo
            NativeBridge.call("initNative", ["false"], function (strresult) {
            });
        }
        else {
            NativeBridge.call("initNative", ["true"], function (strresult) {
            });
        }
    }
}