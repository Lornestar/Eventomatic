var PollingPaymentvar;
var buttonheight = 50;

var amountbox = new Ext.Panel({
    id:'theamountboxcomponent',
    items: [
        {
            id:'amountboxamount',
            layout:'hbox',
            items:[
            {
                xtype: 'textfield',
                value: '$ 0.00',
                name: 'amountboxname',
                cls: 'amountboxclass',
                flex: 1
            },

            {
                id: 'sellingtoolbartext',
                html: 'name',
                cls: 'sellingtoolbartext',
                flex: 1
            }
            ]
        },
    {
        xtype: 'textfield',
        name: 'txtnote',
        maxLength: 60,
        placeHolder: 'Add Note'
    }
    ]
});


var newcard = new Ext.Panel({
    items: [
        { xtype: 'textfield',
            placeHolder: '(###) ###-####',
            name: 'smsbox'
        },
    {
        html: 'Send buyer Payment by SMS'
    }
    ]
});

var PendingPayment = new Ext.Panel({
    id: 'paymentTpl',
    paymentTpl: new Ext.XTemplate([
            '<table class="activepaymentbox"><tr>',
            '<tpl for="."><td class="activepaymentcell">',
                '<div >',
                    '${amount} - {Init_Date_Diff}<br/>', // {[reportgetseconds(values,0)]}
                    //'<tpl if="tx_status ==1">Pending</tpl>',
    //'<tpl if="tx_status ==2">Paid</tpl></td>',
                    '</td>',
                '</div>',
            '</td></tpl>',
        '</tr></table>'
    ])
});

var thekeypadbuttons = new Ext.Panel({
    defaults: {
        layout: 'hbox',
        flex: 1,
        defaults: {
            xtype: 'button',
            flex: 1,
            height: buttonheight
        }
    },
    items: [
            { height: buttonheight,
                items: [
                {
                    text: '1<div class="phonebuttons">&nbsp;</div>',
                    handler: function () {
                        keypadpress(1);
                    }
                },
                {
                    xtype: 'button',
                    text: '2<div class="phonebuttons">ABC</div>',
                    handler: function () {
                        keypadpress(2);
                    }
                },
                {
                    xtype: 'button',
                    text: '3<div class="phonebuttons">DEF</div>',
                    handler: function () {
                        keypadpress(3);
                    }
                }
                ]
            },
            { height: buttonheight,
                items: [
                    {
                        text: '4<div class="phonebuttons">GHI</div>',
                        handler: function () {
                            keypadpress(4);
                        }
                    },
                    {
                        xtype: 'button',
                        text: '5<div class="phonebuttons">JKL</div>',
                        handler: function () {
                            keypadpress(5);
                        }
                    },
                    {
                        xtype: 'button',
                        text: '6<div class="phonebuttons">MNO</div>',
                        handler: function () {
                            keypadpress(6);
                        }
                    }
                ]
            },
            {
                height: buttonheight,
                items: [
                    {
                        text: '7<div class="phonebuttons">PQRS</div>',
                        handler: function () {
                            keypadpress(7);
                        }
                    },
                    {
                        xtype: 'button',
                        text: '8<div class="phonebuttons">TUV</div>',
                        handler: function () {
                            keypadpress(8);
                        }
                    },
                    {
                        xtype: 'button',
                        text: '9<div class="phonebuttons">WXYZ</div>',
                        handler: function () {
                            keypadpress(9);
                        }
                    }
                ]
            },
            {
                height: buttonheight,
                items: [
                    {
                        xtype: 'button',
                        text: 'Delete',
                        handler: function () {
                            keypaddelete();
                        }
                    },
                    {
                        xtype: 'button',
                        text: '0',
                        handler: function () {
                            keypadpress(0);
                        }
                    },
                    {
                        xtype: 'button',
                        text: 'Reset',
                        handler: function () {
                            refreshkeypad();                            
                        }
                    }
                ]
            }

            ]
});

 
function reportgetsecondsdiff(data) {
    var timeVar = "";
    var task = new Ext.util.DelayedTask(function () {
        timeVar = reportgetseconds(data);                
    });
    task.delay(1000);
    return timeVar;
}

function reportgetseconds(data,offset) {
    var date1 = new Date(data.Server_Current_Date);
    var date2 = new Date(data.Init_Date);
    var secs = parseInt(((data.Init_Date_Diff2) ) + offset); //parseInt(((date1 - date2) / 1000)+offset);
    var hours = Math.floor(secs / (60 * 60));
    var minutes = Math.floor(secs % (60 * 60) / 60);
    var seconds = Math.ceil(secs % (60 * 60) % 60);
    var timeObj = {
        "hours": hours < 10 ? "0" + hours : hours,
        "minutes": minutes < 10 ? "0" + minutes : minutes,
        "seconds": seconds < 10 ? "0" + seconds : seconds
    };
    var timeVar = "";
    //alert(date2);
    if (timeObj.hours == 0) {
        timeVar = timeObj.minutes + ":" + timeObj.seconds;
    }
    else {
        timeVar = timeObj.hours + ":" + timeObj.minutes + ":" + timeObj.seconds;
    }
    return timeVar;
}

function reportupdatefields(data, offset) {
    var newdata = data;
    var atleastoneupdate = false;
    //alert(newdata[0]['Init_Date_Diff2']);
    for (i = 0; i < newdata.length; i += 1) {
        //alert(reportgetseconds(newdata[i]));
        if (newdata[i]['tx_status'] == '1') {
            newdata[i]['Init_Date_Diff'] = reportgetseconds(newdata[i], offset);
            atleastoneupdate = true;
            //alert(newdata[i]['Init_Date_Diff']);
        }
        else {
            newdata[i]['Init_Date_Diff'] = 'Paid';            
        }        
    }

    if (offset < 5) {
        if (atleastoneupdate) {
            var task = new Ext.util.DelayedTask(function () {
                reportupdatefields(data, offset + 1);
            });
            task.delay(1000);
        }        
        try{
            Page_Sell_root.getComponent('paymentupdateshell').getComponent('thepaymentupdate').update(PendingPayment.paymentTpl.applyTemplate(newdata));
        }        
        catch(err){
        }
    }                    
}

function pendingpayment() {
    Ext.util.JSONP.request({
        url: pfjson,
        callbackKey: 'callback',
        params: {
            type: "ActivePayments",
            fbid: document.getElementById("hdnfbid").value
        },
        callback: function (result) {
            if (result.activepayments) {
                Page_Sell_root.getComponent('paymentupdateshell').getComponent('thepaymentupdate').update(PendingPayment.paymentTpl.applyTemplate(result.activepayments));
                //alert();
                //toppanel.getComponent('Sell_Page').getComponent('thepaymentupdate').paymentTpl.applyTemplate(result.activepayments);
                //toppanel.getComponent('Sell_Page').update(toppanel.getComponent('Sell_Page').getComponent('thepaymentupdate'));
                
                //.update(PendingPayment.applyTemplate(result.activepayments));
                //.update(weatherTpl.applyTemplate(weather))
            }
            else {
                alert("An error occured.");
            }
        }
    });
}

function startpendingpayment() {
    PollingPaymentvar = setInterval(pendingpayment, 5000);
}

function stoppendingpayment() {
    clearInterval(PollingPaymentvar);
}