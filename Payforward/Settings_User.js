Ext.regModel('SettingsUserModel', {
    fields: [
        { name: 'Full_Name', type: 'string' },
        { name: 'Email', type: 'string' },
        { name: 'FBid', type: 'string' }
    ]
});


var theSettingsUser = new Ext.data.Store({
    model: 'SettingsUserModel',
    getGroupString: function (record) {
        return record.get('Full_Name')[0];
    },
    data: []
});

var theSettingsUserSeller = new Ext.data.Store({
    model: 'SettingsUserModel',
    getGroupString: function (record) {
        return record.get('Full_Name')[0];
    },
    data: []
});


function callSettingsUser() {    //type = 0 is Report all, type = 1 is Report user, type = 2 is Seller Screen    
    var localpfjson = "/payforward_ajax.aspx";
    Ext.Ajax.request({
        url: localpfjson,
        method: 'GET',
        params: {
            type: "AdminList",
            Resource_Key: document.getElementById("hdResource_Key").value,
            callback: ''
        },
        reader: {
            type: 'json',
            root: 'reportlist'
        },
        success: function (result) {
            if (result.responseText) {
                var jsoning;
                if (result.responseText.length > 2) {
                    //alert(result.responseText);
                    jsoning = Ext.decode(result.responseText);
                    theSettingsUser.loadData(jsoning, false);
                }
                else {
                    jsoning = Ext.decode('[{"Full_Name":"No Sellers"}]');
                    theSettingsUserSeller.loadData(jsoning, false);
                }
            }
            else {
                alert("An error occured.");
            }
        }
    });
    Ext.Ajax.request({
        url: localpfjson,
        method: 'GET',
        params: {
            type: "SellerList",
            Resource_Key: document.getElementById("hdResource_Key").value,
            callback: ''
        },
        reader: {
            type: 'json',
            root: 'sellerlist'
        },
        success: function (result) {
            if (result.responseText) {
                var jsoning;
                if (result.responseText.length > 2) {
                    //alert(result.responseText);
                    jsoning = Ext.decode(result.responseText);
                    theSettingsUserSeller.loadData(jsoning, false);
                }
                else {
                    jsoning = Ext.decode('[{"Full_Name":"No Sellers"}]');
                    theSettingsUserSeller.loadData(jsoning, false);
                }
            }
            else {
                alert("An error occured.");
            }
        }
    });
}