var EntireMenujson = "";
var initialMenuItemsSelectedjson = '"description":"Nothing Selected Yet","amount":"","quantity":"","total_amount":"","Ticket_Key":"-1"';
var MenuItemsSelectedjson = Ext.decode('[{' + initialMenuItemsSelectedjson + '}]');
var Menutaxline = { "description": "13% tax", "amount": "0.00", "quantity": "1", "total_amount": "0.00", "Ticket_Key":"0" };
var MenuItemsAmount = 0;
function initMenuItejson() {
    MenuItemsSelectedjson = Ext.decode('[{' + initialMenuItemsSelectedjson + '}]');
    refreshmenuselected();
    MenuItemsAmount = 0;
}


Ext.regModel('MerchantOrderboxModel_ItemsOrderedfields', {
    fields: [
        { name: 'description', type: 'string'},
        { name: 'amount', type: 'decimal' },
        { name: 'quantity', type: 'int' },
        { name: 'total_amount', type: 'decimal' },
        { name: 'Ticket_Key', type: 'int' }
    ]
});


var MerchantOrderboxModel_ItemsOrdered = new Ext.data.Store({
    model: 'MerchantOrderboxModel_ItemsOrderedfields',
    // sorters: 'amount',
    getGroupString: function (record) {
        return record.get('amount')[0];
    },
    autoLoad: true,
    data: [
        { description: "13% tax", amount: '4.5', quantity: '1', total_amount: '4.50' },
        { description: "13% tax", amount: '4.5', quantity: '1', total_amount: '4.50' }    
    ]
});

Ext.regModel('MerchantOrderboxModel_Menufields', {
    fields: [
        { name: 'description', type: 'string' },
        { name: 'amount', type: 'decimal' }        
    ]
});


var MerchantOrderboxModel_Menu = new Ext.data.Store({
    model: 'MerchantOrderboxModel_Menufields',
    // sorters: 'amount',
    getGroupString: function (record) {
        return record.get('description')[0];
    },
    autoLoad: true,
    data: [
        { description: "Grilled Cheese", amount: '4.50'},
        { description: "Coke", amount: '1' }
    ]
});


var MerchantOrderbox_ItemsOrdered = new Ext.Panel({
    id: 'MerchantOrderbox_ItemsOrdered',
    //html: 'merchant order list',
    fullscreen: true,
    width: Ext.is.Phone ? undefined : 300,
    items: [
        {
            id: 'MerchantOrderbox_ItemsOrdered_List',
            xtype: 'list',
            store: MerchantOrderboxModel_ItemsOrdered,
            cls: 'mymerchantorderList',
            highlight: false,
            itemTpl: new Ext.XTemplate([
                    '<tpl class="mymerchantorderList" for=".">',
                        '<table width="100%"><tr class="merchantorderlist">',
                            '<td style="text-align:left;width:60%;">{description}</td>',
                            '<td style="text-align:right;width:40%;">',
                            '<tpl if="Ticket_Key == -1"></tpl>',
                            '<tpl if="Ticket_Key == 0">${amount}</tpl>',
                            '<tpl if="Ticket_Key &gt; 0">{quantity}x&nbsp;&nbsp;${amount}&nbsp;&nbsp;<img onClick="removemenuitemquantity({#}.)" style="border:1px solid black;" src="payforward/themes/minus2.png" height="24px"/>&nbsp;&nbsp;<img onClick="addmenuitemquantity({#}.)" style="border:1px solid black;" src="payforward/themes/add1.png" height="24px"/></tpl>',
                            '</td>',
                        '</tr></table>',
                    '</tpl>'
            ])
        },
        {
            html: '<a href="#" onClick="initMenuItejson()"><div style="border:1px solid black;float:right;padding-right:10px;padding-left:10px;">Clear</div></a>'
        }
    ],
    afterLayout: function (layout) {
        //this.show();
        refreshmenuselected();
        this.getComponent('MerchantOrderbox_ItemsOrdered_List').setScrollable(false);        
    }
});

var MerchantOrderbox_ItemsMenu = new Ext.Panel({
    id: 'MerchantOrderbox_ItemsMenu',
    html: 'MerchantOrderbox_ItemsMenu',    
    menuTpl: new Ext.XTemplate([
                 '<table class="MenuItemsList"><tr valign="top"><td>',
                    '<tpl for=".">',
                        '<a href="#" onClick="addmenuitem({#}.)"><div class="MenuItemsListCell">{description}</div></a>',
                    '</tpl>',
                '</td></tr></table>'
            ])
});


var MerchantOrderbox = new Ext.Panel({
    id: 'MerchantOrderbox',
    title: 'Merchant Orderbox',
    cls: 'card card1',
    cls: 'demo-list',    
    layout: {
        type: 'vbox',
        align: 'stretch'
    },
    defaults: { flex: 1 },
    items: [
        {
            id: 'MerchantOrderbox_ReceiptItems',
            layout: 'card'
        },
        { id: 'MerchantOrderbox_Menu',
            layout: 'card',
            html: 'Loading'
        }
    ],
    afterLayout: function (layout) {
        this.getComponent(0).setActiveItem(MerchantOrderbox_ItemsOrdered);
        //this.getComponent(1).setActiveItem(MerchantOrderbox_ItemsMenu);
        /*var testdata = '[{"description": "Grilled Cheese","amount": "4.50"},{"description": "Coke","amount": "1" }]';
        var jsoning = Ext.decode(testdata);
        this.getComponent(1).update(MerchantOrderbox_ItemsMenu.menuTpl.applyTemplate(jsoning));*/
        callmenumerchant(this);
    }
});


function callmenumerchant(thepanel) {    //type = 0 is Report all, type = 1 is Report user, type = 2 is Seller Screen
    var localpfjson = "/payforward_ajax.aspx";
    Ext.Ajax.request({
        url: localpfjson,
        method: 'GET',
        params: {
            type: "Menu_Merchant",
            Resource_Key: document.getElementById("hdResource_Key").value,
            callback: ''
        },
        reader: {
            type: 'json',
            root: 'reportlist'
        },
        success: function (result) {
            if (result.responseText) {
                if (result.responseText.length > 2) {
                    //alert(toppanel.getActiveItem().id);
                    var jsoning = Ext.decode(result.responseText);
                    EntireMenujson = jsoning;
                    thepanel.getComponent(1).update(MerchantOrderbox_ItemsMenu.menuTpl.applyTemplate(jsoning));
                }
                else {
                    thepanel.getComponent(1).update('No Items on the Menu');
                }
            }
            else {
                alert("An error occured.");
            }
        }
    });
}

function addmenuitemquantity(index) {
    index = index - 1;
    if (MenuItemsSelectedjson[index].data) {        
        MenuItemsSelectedjson[index].data['quantity'] = parseFloat(MenuItemsSelectedjson[index].data['quantity']) + 1;
    }    
    refreshmenuselected();
    var txtamount = gettxtamount();
    txtamount.value = calculateitemsselected();
}

function removemenuitemquantity(index) {
    index = index - 1;
    if (MenuItemsSelectedjson[index].data) {
        MenuItemsSelectedjson[index].data['quantity'] = parseFloat(MenuItemsSelectedjson[index].data['quantity']) - 1;
        if (parseFloat(MenuItemsSelectedjson[index].data['quantity']) == 0) {
            //alert(parseFloat(MenuItemsSelectedjson[index].data['quantity']));            
            if (MenuItemsAmount == 1) {
                initMenuItejson();
            }
            else {
                for (i = index; i > 0; i -= 1) {
                    MenuItemsSelectedjson[i] = MenuItemsSelectedjson[i - 1];                    
                }                
                //delete MenuItemsSelectedjson[index];
                MenuItemsSelectedjson.shift();
                MenuItemsAmount -= 1;
            }            
        }
    }    
    refreshmenuselected();
    var txtamount = gettxtamount();
    txtamount.value = calculateitemsselected();
}


function addmenuitem(index) {
    index = index - 1;
    //alert(EntireMenujson[index]['Ticket_Key']);
    var therowtoadd = { "description": EntireMenujson[index]['description'], "amount": ensure3decimals(EntireMenujson[index]['amount']), "quantity": "1", "total_amount": EntireMenujson[index]['amount'], "Ticket_Key": EntireMenujson[index]['Ticket_Key'] };

    //check if therowtoadd is already in list
    var justadd = false;
    for (i = 0; i < MenuItemsSelectedjson.length - 1; i += 1) {
        if (MenuItemsSelectedjson[i].data['Ticket_Key'] == therowtoadd['Ticket_Key']) {
            addmenuitemquantity(i + 1);
            justadd = true;
            break;
        }        
    }

    if (!justadd) {
        if (MenuItemsAmount == 0) { //Nothing in menu yet
            MenuItemsSelectedjson[0] = therowtoadd;
            MenuItemsSelectedjson.push(Menutaxline);
        }
        else if (MenuItemsAmount > 0) {
            MenuItemsSelectedjson[MenuItemsAmount] = therowtoadd;
            MenuItemsSelectedjson.push(Menutaxline);
        }
        MenuItemsAmount += 1;
        //MenuItemsSelectedjson[0]= { "description": EntireMenujson[index]['description'], "amount": EntireMenujson[index]['amount'], "quantity": "1", "total_amount": EntireMenujson[index]['amount'] };    
        refreshmenuselected();
        var txtamount = gettxtamount();
        txtamount.value = calculateitemsselected();
    }    
}


function calculateitemsselected() {
    var thenewtxtamount = '000';
    var totalamount = 0;
    for (i = 0; i < MenuItemsSelectedjson.length - 1; i += 1) {
        if (MenuItemsSelectedjson[i].data) {
            //var rowamount = parseFloat(MenuItemsSelectedjson[i].data['amount']) * parseFloat(MenuItemsSelectedjson[i].data['quantity']);
            totalamount += (parseFloat(MenuItemsSelectedjson[i].data['amount']) * parseFloat(MenuItemsSelectedjson[i].data['quantity']));
        }
        else if (MenuItemsSelectedjson[i]) {
            totalamount += (parseFloat(MenuItemsSelectedjson[i]['amount']) * parseFloat(MenuItemsSelectedjson[i]['quantity']));
        }
    }
    thenewtxtamount = totalamount.toString();
    thenewtxtamount = ensure3decimals(thenewtxtamount);
    thenewtxtamount = addthesymbol(thenewtxtamount);
    return thenewtxtamount;
}

function refreshmenuselected() {
    //alert(MenuItemsSelectedjson.length);
    MerchantOrderboxModel_ItemsOrdered.loadData(MenuItemsSelectedjson, false);    
}

function ensure3decimals(thevalue) {
    var newvalue = "000";
    thevalue = thevalue.toString();
    if (thevalue.indexOf(".") == -1) { //there is no decimal which means add 2
        newvalue = thevalue + ".00";
    }
    else {
        //thevalue = thevalue.replace(".", "");
        //alert('index - ' +thevalue.indexOf(".") + 'length - ' + thevalue.length.toString());
        if (thevalue.indexOf(".") == thevalue.length-1) {
            newvalue = thevalue + "00";
        }
        else if (thevalue.indexOf(".") == thevalue.length-2) {
            newvalue = thevalue + "0";
        }
        else {
            //alert(thevalue);
            newvalue = thevalue;
        }
    }    
    //newvalue = addthedecimal(newvalue);
    return newvalue;
}