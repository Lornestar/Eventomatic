/*var Page_Report = Ext.extend(Ext.Panel, {
    title: 'Report',
    html: 'Report',
    iconCls: 'report',
    cls: 'card card1',
    initComponent: function () {
        callreportlist();
        Page_Report.superclass.initComponent.call(this);
    },
    dockedItems: [
    {
    dock: 'top',
    xtype: 'toolbar',
    ui: 'light',
    align: 'stretch',
    items: [
    {
    id: 'btnall',
    text: 'All Sellers',
    ui: 'round',
    handler: function () {
    btnreportall();
    }
    },                
    {
    id: 'btnuser',
    text: 'Your sales',
    ui: 'round',                    
    handler: function () {
    btnreportuser();
    }
    }
    ]
    }
    ]

});*/

var PendingPaymentjson = "";

Ext.regModel('ReportModel', {
     fields: [
        {name: 'amount', type: 'decimal'},
        {name: 'Init_Date',  type: 'string'},
        {name: 'Seller_Name', type: 'string'},
        {name: 'tx_status',  type: 'int'},
        {name: 'Note',  type: 'string'}
    ]    
});


var theListStore = new Ext.data.Store({
    model: 'ReportModel',
    // sorters: 'amount',
    getGroupString: function (record) {
        return record.get('amount')[0];
    },
    data: [{ amount: '1', Init_Date: '1', Seller_Name: '1', tx_status: '1', Note: '1'}]
});

var Page_Report = Ext.extend(Ext.Panel, {
    id: 'thelist',
    title: 'Report',
    iconCls: 'report',
    cls: 'card card1',
    fullscreen: true,    
    layout: Ext.is.Phone ? 'fit' : {
        type: 'vbox',
        align: 'center',
        pack: 'center'
    },
    cls: 'demo-list',
    items: [{
        id: 'thelisting',
        width: Ext.is.Phone ? undefined : 300,
        fullscreen: true,    
        xtype: 'list',
        store: theListStore,
        itemTpl: new Ext.XTemplate([
                    '<div class="reportlist">',
                    '<tpl for=".">',
                        '<table width="100%"><tr>',
                            '<td style="text-align:left;width:10%;">${amount}</td>',
                            '<td style="text-align:left;width:20%;">{Init_Date}</td>',
                            '<td style="text-align:left;width:20%;">{Seller_Name}</td>',
                            '<td style="text-align:left;width:15%;">',
                            '<tpl if="tx_status ==1">Pending</tpl>',
                            '<tpl if="tx_status ==2">Paid</tpl></td>',
                            '<td style="text-align:left;width:35%;">{Note}</td>',
                        '</tr></table>',
                    '</tpl>',
                '</div>'
            ])
    }],
    afterShow: function (layout) {
        callreportlist();        
    }
});




Ext.reg('Page_Report', Page_Report);

var ReportTemplate = new Ext.Panel({
    id: 'reportTpl',
    cls: 'demo-list',
    reportTpl: new Ext.XTemplate([
            '<div class="reportlist">',            
            '<tpl for=".">',        
                '<table width="100%"><tr>',
                    '<td style="text-align:left;width:10%;">${amount}</td>',
                    '<td style="text-align:left;width:20%;">{Init_Date}</td>',                    
                    '<td style="text-align:left;width:20%;">{Seller_Name}</td>',
                    '<td style="text-align:left;width:15%;">',
                    '<tpl if="tx_status ==1">Pending</tpl>',
                    '<tpl if="tx_status ==2">Paid</tpl></td>',
                    '<td style="text-align:left;width:35%;">{Note}</td>',    
                '</tr></table>',
            '</tpl>',
        '</div>'        
    ])
});

function callreportlist(reporttype) {    //type = 0 is Report all, type = 1 is Report user, type = 2 is Seller Screen
    var localpfjson = "/payforward_ajax.aspx";
    Ext.Ajax.request({
        url: localpfjson,
        method: 'GET',
        params: {
            type: "ReportList",
            Resource_Key: document.getElementById("hdResource_Key").value,
            callback: '',
            reporttype: reporttype,
            fbid: document.getElementById("hdnfbid").value
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
                    if (toppanel.getActiveItem().id == "Page_Report") {
                        theListStore.loadData(jsoning, false);
                    }
                    if (toppanel.getActiveItem().id == "Sell_Page") {
                        PendingPaymentjson = jsoning;
                        //alert(jsoning[0]);
                        //alert(PendingPaymentjson);
                        //toppanel.getComponent('Sell_Page').getComponent('thepaymentupdate').update(PendingPayment.paymentTpl.applyTemplate(jsoning));
                        reportupdatefields(jsoning, 0);

                    }
                    //toppanel.update(result.responseText); //ReportTemplate.reportTpl.applyTemplate(result.responseText));
                }
                else {
                    if (toppanel.getActiveItem().id == "Page_Report") {
                        toppanel.getComponent('Page_Report').update('No Activities to Report');
                    }
                    if (toppanel.getActiveItem().id == "Sell_Page") {
                        Page_Sell_root.getComponent('paymentupdateshell').getComponent('thepaymentupdate').update('Nothing in past 2 hrs');
                    }
                }
            }
            else {
                alert("An error occured.");
            }
        }
    });
    /*Ext.util.JSONP.request({
        url: pfjson,
        callbackKey: 'callback',
        params: {
            type: "ReportList",
            Resource_Key: document.getElementById("hdResource_Key").value
        },
        callback: function (result) {
            if (result.reportlist) {
                if (result.reportlist.length > 0) {
                    alert(result.reportlist);
                    theListStore.loadData(result.reportlist, false);
                    //toppanel.getComponent('Page_Report').update(ReportTemplate.reportTpl.applyTemplate(result.reportlist));
                }
                else {
                    toppanel.getComponent('Page_Report').update('No Activities to Report');
                }
            }
            else {
                alert("An error occured.");
            }
        }
    });*/
}

function btnreportall() {    
}

function btnreportuser() {        
}


function stopreportpayment() {
    //clearInterval(PollingReportingvar);
}