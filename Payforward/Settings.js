var settingsformvar = "";

var SellersListTemplate = new Ext.XTemplate(
                    '<table width="100%">',
                    '<tpl for=".">',
                        '<tr>',
                            '<td style="text-align:left;">{Full_Name}</td>',
                            '<td style="text-align:left;width:45px;"><a href="#" onclick="btnRemoveAdmin({FBid},false);">Remove</a></td>',
                        '</tr>',
                    '</tpl></table>',
                '</div>');


var Page_Settings = Ext.extend(Ext.Panel, {

    title: 'Settings',
    iconCls: 'settings',    
    items: [
        {
            id: 'theSettingscard',
            layout: 'card',
            html: '&nbsp;',
            fullscreen: true
        }        
    ],
    dockedItems: [
        {
            dock: 'top',
            xtype: 'toolbar',
            ui: 'light',
            align: 'stretch',
            items: [
                {
                    id: 'btnstoresettings',
                    text: 'Store Settings',
                    ui: 'round',
                    handler: function () {
                        btnStoreSettings();
                    }
                },
                {
                    id: 'btnsellersSettings',
                    text: 'Sellers Settings',
                    ui: 'round',
                    handler: function () {
                        btnSellerSettings();
                    }
                }/*,
                {
                    id: 'btnopenstore',
                    text: 'Open New Store',
                    ui: 'round',
                    handler: function () {
                        btnOpenStore();
                    }
                }*/
                ]
        }],
    afterLayout: function (layout) {
        btnStoreSettings();
        this.setScrollable(false);        
    }
});

Ext.reg('Page_Settings', Page_Settings);

var userSettingsNotLoggedin = new Ext.Panel({
    id: 'userSettingsNotLoggedin',
    fullscreen: true,
    layout: {
        type: 'vbox',
        align: 'stretch',
        flex: 1
    },
    items: [
        {
            html: 'Current Admins of this Store<div class="fineprint">Admins can change store settings, sell and add other Admins</div>'
        },
        {
            html: 'here',
            id: 'SettingsUserAdminList',
            store: theSettingsUser,
            xtype: 'list',        
            itemTpl: SellersListTemplate
        },
        {
            html: 'Current Sellers of this Store<div class="fineprint">Sellers can sell on your behalf. They do not have access to change store settings, but can invite other Sellers.</div>'
        },
        {
            html: 'here',
            id: 'SettingsUserSellerList',
            store: theSettingsUserSeller,
            xtype: 'list',        
            itemTpl: SellersListTemplate            
        }
        ],
        dockedItems: [
        {            
            id: 'SettingsUserSellertopbar',
            dock: 'top',
            xtype: 'toolbar',
            ui: 'light',
            align: 'stretch',
            items: [
                {
                    id: 'btnsettingsAddAdmin',
                    text: 'Add Admin',                    
                    handler: function () {
                        btnSettingsAddAdmin();
                    }
                },
                {
                    id: 'btnsettingsAddSeller',
                    text: 'Add Seller',                    
                    handler: function () {
                        btnSettingsAddSeller();
                    }
                }
                ]
        }],
        afterLayout: function (layout) {
                callSettingsUser();
            }
});

    var storeSettings = new Ext.Panel({
        id: 'storeSettings',
        xtype: 'form',
        scroll: 'vertical',
        items: [{
            id: 'storesettingsmsg',
            html: '<b>You are currently using Groupstores DEMO Store</b>, allowing you to try this app by accepting Fake Money.<br/>Begin accepting Real Money by setting up your Store Settings.',
            cls: 'StoreSettingsText'
        },
        
                    {
                        id: 'thefieldset',
                        xtype: 'fieldset',
                        items: [
                {
                    xtype: 'selectfield',
                    name: 'storesettingscurrency',
                    label: 'Currency',
                    id: 'storesettingscurrency',
                    options: [{
                        text: 'CAD',
                        value: 'CAD'
                    }, {
                        text: 'USD',
                        value: 'USD'
                    }, {
                        text: 'EUR',
                        value: 'EUR'
                    },
                        {
                            text: 'GBP',
                            value: 'GBP'
                        },
                        {
                            text: 'ILS',
                            value: 'ILS'
                        }]
                },
                    { xtype: 'textfield',
                        placeHolder: 'Mikes Pretzel Store',
                        maxLength: 40,
                        name: 'storesettingsstorename',
                        id: 'storesettingsstorename',
                        label: 'Store Name',
                        required: true
                    },
                    { xtype: 'textfield',
                        placeHolder: 'paypal@email.com',
                        maxLength: 40,
                        name: 'storesettingsppemail',
                        id: 'storesettingsppemail',
                        label: 'PayPal Email',
                        required: true
                    },
                    {
                        xtype: 'button',
                        id: 'btnSaveStoreinfo',
                        text: '1 - Save Store Info',
                        handler: function () {
                            btnsavestoreinfo();
                        }
                    },/*
                    {
                        id: 'storesettingsconnectpaypal',
                        xtype: 'button',
                        text: '2 - Connect PayPal Account',
                        handler: function () {
                            document.getElementById("hddoppauth").value = "1";
                            document.forms[0].submit();
                            //__doPostBack('DoPPAuth', '');
                        }
                    },*/
                    {
                        id: 'storesettingsfieldsetmsg',
                        html: 'Enter and Save Store Info',
                        cls: 'StoreSettingsText'
                    }
                    
                ]
                }                    
    ]
    });




 function btnSellerSettings() {
    //alert(userSettingsNotLoggedin);    
     toppanel.getComponent('Page_Settings').getComponent('theSettingscard').setActiveItem(userSettingsNotLoggedin, 'slide');        

     //First check if this is demo or live
     if (document.getElementById("hdisDemo").value == "True") { //is demo
         toppanel.getComponent('Page_Settings').getComponent('theSettingscard').getComponent('userSettingsNotLoggedin').getDockedComponent('SettingsUserSellertopbar').hide();
     }     
}

function btnStoreSettings() {
    var changestore = "";
    if (document.getElementById("hdnumberofstores").value != "0") { //member of at least 1 store
        changestore = "| <a href='#' style='font-size:large; color:blue;' onClick='btnchangestore()'>Change Store</a>";
    }

    //First check if this is demo or live
    if (document.getElementById("hdResource_Key").value == "137") { //is demo        
        toppanel.getComponent('Page_Settings').getComponent('theSettingscard').setActiveItem(storeSettings, 'slide');
        settingsformvar = toppanel.getComponent('Page_Settings').getComponent('theSettingscard').getComponent('storeSettings').getComponent('thefieldset');

        toppanel.getComponent('Page_Settings').getComponent('theSettingscard').getComponent('storeSettings').getComponent('storesettingsmsg').update('This is a demo version of the Settings. Here you can choose your Currency, Store Name and connect your PayPal account to Groupstore.' + changestore);

        settingsformvar.getComponent(3).hide(); //hide 1st button to connect to pp
        settingsformvar.getComponent(4).hide(); //hide 2nd button to connect to pp
        settingsformvar.getComponent('storesettingsfieldsetmsg').update('');

        //assign form vars
        settingsformvar.getComponent('storesettingsstorename').setValue(document.getElementById('hdStoreName').value);
        settingsformvar.getComponent('storesettingsppemail').setValue(document.getElementById('hdppemail').value);
        settingsformvar.getComponent('storesettingscurrency').setValue(document.getElementById('hdResourceCurrency').value);
    }
    else { //is live
        
        changestore = "| <a href='#' onClick='btnchangestore()'>Change Store</a> | <a href='#' onClick='btnchangedemostore()'>Go To Demo Store</a>";

        if (document.getElementById("hdisadmin").value == "True") {
            //admin of store, or not part of any store
            toppanel.getComponent('Page_Settings').getComponent('theSettingscard').setActiveItem(storeSettings, 'slide');
            settingsformvar = toppanel.getComponent('Page_Settings').getComponent('theSettingscard').getComponent('storeSettings').getComponent('thefieldset');

            toppanel.getComponent('Page_Settings').getComponent('theSettingscard').getComponent('storeSettings').getComponent('storesettingsmsg').update('You are an Admin of this store. ' + changestore);

            if (document.getElementById("hdhaveppauth").value == "True") { //already connected paypal account
                settingsformvar.getComponent('storesettingsfieldsetmsg').update('You have already connected your PayPal account and can Sell.');
                settingsformvar.getComponent(4).hide(); //hide 2nd button to connect to pp
            }

            //assign form vars
            settingsformvar.getComponent('storesettingsstorename').setValue(document.getElementById('hdStoreName').value);
            settingsformvar.getComponent('storesettingsppemail').setValue(document.getElementById('hdppemail').value);
            settingsformvar.getComponent('storesettingscurrency').setValue(document.getElementById('hdResourceCurrency').value);

        }
        else { //currently is just seller of store       
            changestore = "| <a href='#' onClick='btnchangestore()'>Change Store</a> | <a href='#' onClick='btnchangedemostore()'>Go To Demo Store</a>"; 
            toppanel.getComponent('Page_Settings').getComponent('theSettingscard').update('You are a Seller of this store. ' + changestore + '<br/>Store Name is ' + document.getElementById("hdStoreName").value);
        }    
    }       
}

function btnchangestore() {
    toppanel.getComponent('Page_Settings').getComponent('theSettingscard').setActiveItem(Settings_StoreList, 'slide');
}

function btnsavestoreinfo() {
    callresourceinfosave();
}

function callresourceinfosave() {
    Ext.util.JSONP.request({
        url: pfjson,
        callbackKey: 'callback',
        params: {
            type: "ResourceName",
            Resource_Key: document.getElementById("hdResource_Key").value,
            fbid: document.getElementById("hdnfbid").value,
            Resource_Name: settingsformvar.getComponent('storesettingsstorename').getValue(),
            Currency: settingsformvar.getComponent('storesettingscurrency').getValue(),
            ppemail: settingsformvar.getComponent('storesettingsppemail').getValue()
        },
        callback: function (result) {
            if ((document.getElementById("hdResource_Key").value == "0") || (document.getElementById("hdResource_Key").value == "3")) {
                document.getElementById("hdResource_Key").value = result.resourcekey;
                settingsformvar.getComponent(3).setText('Done - Save Store Info');
                //check to switch Warning about Demo
            }
            if (result.resourcekey) {
                alert('Changes have been Saved');
            }
            else {
                alert("An error occured.");
            }
        }
    });
}

function callinvitenewuser(phonenumber, addadmin, resource) {
    Ext.getBody().mask("<div class='demos-loading'>Sending SMS</div>");
    Ext.util.JSONP.request({
        url: pfjson,
        callbackKey: 'callback',
        params: {
            type: "InviteUser",
            Resource_Key: resource,
            fbid: document.getElementById("hdnfbid").value,
            phonenumber: phonenumber,
            addadmin: addadmin
        },
        callback: function (result) {
            //alert(result);
            if (result) {
                alert('sms has been sent');
            }
            else {
                alert("An error occured.");
            }
        }
    });
    Ext.getBody().unmask();
}



function btnSettingsAddAdmin()
{
    Ext.Msg.prompt("Add Admin", "Invite the new Admin by sms. What's their Phone Number?", function (btn, text) {
        if (btn == 'ok') {
            callinvitenewuser(text, true, document.getElementById("hdResource_Key").value);
        }
    });
}

function btnSettingsAddSeller() {
    Ext.Msg.prompt("Add Seller", "Invite the new Seller by sms. What's their Phone Number?", function (btn, text) {
        if (btn == 'ok') {
            callinvitenewuser(text, false, document.getElementById("hdResource_Key").value);
        }
    });
}

function btnRemoveAdmin(fbidremove, isadmin) {
//First check if this is demo or live
    if (document.getElementById("hdisDemo").value == "True") { //is demo
        alert('In Demo mode you cannot Remove an Admin/Seller');
    }
    else {
        if (document.getElementById("hdisadmin").value == "False") { //is seller
            alert('Sellers do not have access to Remove an Admin/Seller');
        }
        else {
            Ext.getBody().mask("<div class='demos-loading'>Removing User</div>");
            Ext.util.JSONP.request({
                url: pfjson,
                callbackKey: 'callback',
                params: {
                    type: "RemoveUser",
                    Resource_Key: document.getElementById("hdResource_Key").value,
                    fbid: fbidremove, //person removing
                    isadmin: isadmin
                },
                callback: function (result) {
                    alert(result);
                    if (result) {
                        callSettingsUser();
                    }
                    else {
                        alert("An error occured.");
                    }
                }
            });
            Ext.getBody().unmask();
        }        
    }    
}

function btnchangedemostore() {
    // btnGotoStore(document.getElementById("hddemoresourcekey").value);
    btngotoDemo();
}

function btnOpenStore() {
    Ext.Msg.prompt("Open New Store", "What's the name of your New Store?", function (btn, text) {
        if (btn == 'ok') {
            openthenewstore(text);
        }
    });
}

function openthenewstore(thename) {
    Ext.getBody().mask("<div class='demos-loading'>Sending SMS</div>");
    Ext.util.JSONP.request({
        url: pfjson,
        callbackKey: 'callback',
        params: {
            type: "OpenStore",
            storename: thename,
            fbid: document.getElementById("hdnfbid").value
        },
        callback: function (result) {
            //alert(result);
            if (result.resourcekey) {
                alert('Your New Store has been created. You will be directed there now.');
                var rs = result.resourcekey;
                location.href = document.getElementById("hdStore_URL").value + "payforward2.aspx?resource_key=" + rs;
            }
            else {
                alert("An error occured.");
            }
        }
    });
    Ext.getBody().unmask();
}

function btngotoDemo() {
    var strlink = document.getElementById("hdStore_URL").value + "Payforward2.aspx?";
    if (document.getElementById("hdResource_Key").value != "137") //is not currently demo
    {
        strlink += "resource_key=137";
        if (document.getElementById("hdisnative").value == "True") {
            strlink += "&";
        }
    }

    if (document.getElementById("hdisnative").value == "True") {
        strlink += "isnative=true";
    }
    Ext.getBody().mask("<div class='demos-loading'>Going to Demo</div>");
    location.href = strlink;
}