Ext.regModel('SettingsResourcesModel', {
    fields: [
        { name: 'Resource_Name', type: 'string' },
        { name: 'Resource_Key', type: 'string' }        
    ]
});


var theSettingsResources = new Ext.data.Store({
    model: 'SettingsResourcesModel',
    getGroupString: function (record) {
        return record.get('Resource_Name')[0];
    },
    data: []
});

var Settings_StoreList = new Ext.Panel({
    id: 'theStorelist',
    title: 'Store Listing',    
    cls: 'card card1',
    fullscreen: true,
    layout: Ext.is.Phone ? 'fit' : {
        type: 'vbox',
        align: 'center',
        pack: 'center'
    },
    cls: 'demo-list',
    items: [{
        id: 'theStorelisting',    
        fullscreen: true,
        xtype: 'list',
        store: theSettingsResources,
        itemTpl: new Ext.XTemplate([
                    '<div class="reportlist">',
                    '<tpl for=".">',
                        '<table width="100%"><tr>',
                            '<td style="text-align:left;width:50%;">{Resource_Name}</td>',
                            '<td style="text-align:left;width:50%;"><a href="#" onClick="btnGotoStore({Resource_Key});">Go To Store</a></td>',                            
                        '</tr></table>',
                    '</tpl>',
                '</div>'
            ])
    }],
    afterLayout: function (layout) {
        callSettingsResources();
    }
});



function callSettingsResources() {    //type = 0 is Report all, type = 1 is Report user, type = 2 is Seller Screen    
    var localpfjson = "/payforward_ajax.aspx";
    Ext.Ajax.request({
        url: localpfjson,
        method: 'GET',
        params: {
            type: "ResourceList",
            fbid: document.getElementById("hdnfbid").value,
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
                    theSettingsResources.loadData(jsoning, false);
                }
                else {
                    jsoning = Ext.decode('[{"Resource_Name":"No Stores"}]');
                    theSettingsResources.loadData(jsoning, false);
                }
            }
            else {
                alert("An error occured.");
            }
        }
    });
}

function btnGotoStore(resource_key) {
    location.href = document.getElementById("hdStore_URL").value + "payforward2.aspx?resource_key=" + resource_key;
}