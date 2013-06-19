var toppanel, bottomTab;
var pfjson = "";
Pollingvar = "";

Ext.setup({
    icon: 'Images/groupstore_Order_Logo2.png',
    tabletStartupScreen: 'Images/groupstore_Order_Logo2.png',
    phoneStartupScreen: 'Images/groupstore_Order_Logo2.png',
    glossOnIcon: false,
    onReady: function () {


        toppanel = new Ext.Panel({
            fullscreen: true,
            ui: 'dark',
            sortable: true,
            layout: 'card',
            items: [
                {
                    id: 'toppanelroot',
                    layout: 'card',
                    fullscreen: true
                }
            ]
        });

        if (document.getElementById("hdAlreadyPaid").value == "True") {
            showAlreadyPaid();
        }
        else {
            fbloginredirect = "http://www.facebook.com/dialog/oauth?client_id=" + document.getElementById("hdfbAppid").value + "&redirect_uri=" + document.getElementById("hdStore_URL").value + "PayForward2.aspx&display=touch&scope=email";
            pfjson = document.getElementById("hdStore_URL").value + "PayForward_Ajax.aspx";
            showwelcome();
            toppanel.getComponent('toppanelroot').setActiveItem(Page_DirectPayment, { type: 'fade', direction: 'left' });
            txtopay = document.getElementById("hdtxkey").value;
            Page_DirectPayment.getDockedComponent(0).getComponent('btnpaymentcancel').hide();
            Page_DirectPayment.setActiveItem(PaymentChoose);
            resetpaymentfields();
            isMobilePay = true;
        }
    }
});


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
        Ext.Msg.alert('Welcome ' + document.getElementById("hdnfbfirstname").value , 'You are trying the Groupstore Demo Store.  All payments in this store are done with Fake Money.<br/><br/><b>Go to theGroupstore.com</b> to open your own Live Store.', Ext.emptyFn);
    }
}


function toggleSellingPayment() {
    toppanel.getComponent('toppanelroot').update('Since your Payment is now complete you may now close this screen.  Thank you for using Groupstore.');
}

function showAlreadyPaid() {
    toppanel.getComponent('toppanelroot').update('This Transaction has already been completed. You must initiate a new transaction to make a Payment.');
}