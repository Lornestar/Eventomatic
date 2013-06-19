var Page_Share = Ext.extend(Ext.Panel, {

    title: 'Share',
    //html: 'Share Groupstore.',
    iconCls: 'share',
    cls: 'card card1',
    items: [
        { xtype: 'button',
            id: 'btnShareInviteSeller',
            text: 'SMS friend about Snappay',
            cls: 'btnShare',
            handler: function () {
                btnShareInviteTryGS();
            }
        },        
        {
        },
        { xtype: 'button',
            id: 'btnShareInviteSellerfbwall',
            text: 'Post on your Facebook Wall',
            cls: 'btnShare',
            handler: function () {
                btnShareInviteTryGSfbwall();
            }
        },
        {
        },
        { xtype: 'button',
            id: 'btnShareInviteSellertwitter',
            text: 'Tweet about Snappay',
            cls: 'btnShare',
            handler: function () {
                btnShareInviteTryGSTwitter();
            }
        },
        {
        },
        { xtype: 'button',
            id: 'btnShareInviteTryGS',
            text: 'Add Admin/Seller to your Store',
            cls: 'btnShare',
            handler: function () {
                btnShareInviteSeller();
            }
        }
    ],
    afterLayout: function (layout) {
        this.setScrollable(false);
    }
});

Ext.reg('Share_Page', Page_Share);

function btnShareInviteSeller() {
    toppanel.setActiveItem('Page_Settings', 'slide');
    btnSellerSettings();
}

function btnShareInviteTryGS()
{
    Ext.Msg.prompt("Invite Friend", "What's your friends Phone Number?", function (btn, text) {
        if (btn == 'ok') {
            callinvitenewuser(text, true,"0");
        }
    });
}

function btnShareInviteTryGSfb() {    
    Ext.util.JSONP.request({
        url: pfjson,
        callbackKey: 'callback',
        params: {
            type: "InviteUser",
            Resource_Key: "0",
            fbid: document.getElementById("hdnfbid").value,
            phonenumber: "0",
            addadmin: true
        },
        callback: function (result) {
            //alert(result);
            if (result) {


                var newinvitekey = result.invitekey; ;
                var newurl = document.getElementById("hdStore_URL").value + "payforward2.aspx?invitekey=" + newinvitekey;
                var fbappid = document.getElementById("hdfbAppid").value;
                var redirecturl = document.getElementById("hdStore_URL").value + "payforward2.aspx?resource_key=" + document.getElementById("hdnfbid").value;

                location.href = "http://www.facebook.com/dialog/send?app_id=" + fbappid + "&name=Try Snappay Mobile&link=" + newurl + "&redirect_uri=" + redirecturl
            }
            else {
                alert("An error occured.");
            }
        }
    });
}

function btnShareInviteTryGSfbwall(){
    Ext.util.JSONP.request({
        url: pfjson,
        callbackKey: 'callback',
        params: {
            type: "InviteUser",
            Resource_Key: "0",
            fbid: document.getElementById("hdnfbid").value,
            phonenumber: "0",
            addadmin: true
        },
        callback: function (result) {
            //alert(result);
            if (result) {


                var newinvitekey = result.invitekey; ;
                var newurl = document.getElementById("hdStore_URL").value + "payforward2.aspx?invitekey=" + newinvitekey;
                var fbappid = document.getElementById("hdfbAppid").value;
                var redirecturl = document.getElementById("hdStore_URL").value + "payforward2.aspx?resource_key=" + document.getElementById("hdnfbid").value;

                location.href = " http://m.facebook.com/dialog/feed?display=touch&app_id=" + fbappid + "&redirect_uri=" + redirecturl
            }
            else {
                alert("An error occured.");
            }
        }
    });
}

function btnShareInviteTryGSTwitter() {
    Ext.util.JSONP.request({
        url: pfjson,
        callbackKey: 'callback',
        params: {
            type: "InviteUser",
            Resource_Key: "0",
            fbid: document.getElementById("hdnfbid").value,
            phonenumber: "0",
            addadmin: true
        },
        callback: function (result) {
            //alert(result);
            if (result) {


                var newinvitekey = result.invitekey; ;
                var newurl = document.getElementById("hdStore_URL").value + "payforward2.aspx?invitekey=" + newinvitekey;
                var fbappid = document.getElementById("hdfbAppid").value;
                var redirecturl = document.getElementById("hdStore_URL").value + "payforward2.aspx?resource_key=" + document.getElementById("hdnfbid").value;

                location.href = "http://twitter.com/share?url=" + newurl + "&text=Try Snappay Mobile";
            }
            else {
                alert("An error occured.");
            }
        }
    });
}
