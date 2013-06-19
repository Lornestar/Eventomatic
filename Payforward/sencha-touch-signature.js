var xwidth = 480;
var ywidth = 800;




    var clicked, canvas, ctx, coords, offsetX, offsetY, oldX, oldY;
    
    //app.views.Signature = Ext.extend(Ext.Panel, {

    Payment_Signature = new Ext.Panel({
        layout: {
            type: 'vbox',
            align: 'stretch'
        },
        fullscreen: true,
        items: [
        {
            layout: 'hbox',
            //width: 500,            
            flex: 1,
            items: [
                {
                    xtype: 'button',
                    text: 'Submit',
                    handler: function (b, e) {
                        Ext.getBody().mask("<div class='demos-loading'>Saving Signature</div>");

                        var img;
                        if (document.getElementById("hdos").value == "ios") {
                            img = canvas.toDataURL();
                        }
                        else {
                            var encoder = new JPEGEncoder(30);
                            var source_image = document.getElementById('canvas');
                            var imagedata = getImageDataFromImage(source_image);
                            img = encoder.encode(imagedata, 30);
                        }

                        //alert(img);
                        //var myEncoder = new JPEGEncoder(30);
                        //var JPEGImage = myEncoder.encode(canvas, 30);
                        
                        
                        //alert(jpeg_version);
                        var localpfjson = "/payforward_ajax.aspx";
                        Ext.Ajax.request({
                            url: localpfjson,
                            method: 'POST',
                            params: {
                                callback: '',
                                type: "GetSig",
                                txkey: txtopay,
                                img: img
                            },
                            reader: {
                                type: 'json',
                                root: 'reportlist'
                            },
                            success: function (result) {
                                if (result.responseText) {
                                    if (result.responseText.length > 2) {
                                        if (result.responseText.indexOf("true") != -1) { //contains
                                            document.getElementById('canvas').getContext("2d").clearRect(0, 0, xwidth, ywidth);

                                            if (toppanel.getComponent('toppaneltabbar')) { //seller's                                                
                                                toppanel.getComponent('toppaneltabbar').show();
                                                try {
                                                    toppanel.setActiveItem('Sell_Page');
                                                }
                                                catch (err) {

                                                }

                                            }
                                            else {
                                                toppanel.setActiveItem('toppanelroot');
                                            }
                                            toggleSellingPayment();
                                            if (toppanel.getComponent('toppaneltabbar')) {
                                                refreshkeypad();
                                                resetamountbox();
                                                if (toppanel.getComponent("toppaneltabbar").getComponent(5)) {
                                                    toppanel.getComponent("toppaneltabbar").getComponent(5).destroy();
                                                }
                                                try {
                                                    toppanel.setActiveItem('Sell_Page');
                                                }
                                                catch (err) {

                                                }
                                            }
                                            Ext.getBody().unmask();
                                            Ext.Msg.alert('', 'Transaction was Successful.', Ext.emptyFn);
                                        }
                                        else {
                                            Ext.Msg.alert('', 'Something went wrong, please send signature again', Ext.emptyFn);
                                        }
                                    }
                                    else {
                                        Ext.Msg.alert('', 'Something went wrong, please send signature again', Ext.emptyFn);
                                    }
                                }
                            }
                        });
                        /*
                        Ext.util.JSONP.request({
                        url: localpfjson,
                        callbackKey: 'callback',
                        method: 'POST',
                        params: {
                        type: "GetSig",
                        txkey: txtopay,
                        img: img
                        },
                        callback: function (result) {
                        //alert(result.PaymentComplete);
                        if (result.SigComplete) {
                        if (result.SigComplete == "true") {//payment went through

                        }
                        else {//payment did not go through
                        Ext.Msg.alert('', 'Your Signature did not get sent. Please Try again.', Ext.emptyFn);
                        }
                        }
                        else {
                        alert("An error occured.");
                        }
                        Ext.getBody().unmask();
                        }
                        });*/

                        //document.getElementById('canvas').style.display = 'none';


                    }
                },
                { xtype: 'spacer' },
                {
                    xtype: 'button',
                    text: 'Clear',
                    handler: function (b, e) {
                        clearSig();
                    }
                }
            ]
        },
        {

            scroll: false,
            html: "<canvas width='" + xwidth + "' id='canvas' height='" + ywidth + "'/>",
            flex: 8
        }
        ],
        scroll: false,
        listeners: {
            activate: function () {
                setupCanvas();
                canvas.ontouchmove = handleMove;
                canvas.onmousemove = handleMouseMove;
            }
        },
        afterLayout: function (layout) {
            this.setScrollable(false);
        }
    });

    window.onmousedown = function () {
        clicked = true;
    }

    window.onmouseup = function () {
        oldX = oldY = clicked = false;
    }

    window.ontouchend = function () {
        oldX = oldY = clicked = false;
    }

    function handleMouseMove(e) {
        var x = e.offsetX,
			y = e.offsetY;
        if (clicked) drawCircle(x, y);
    }

    function handleMove(e) {
        var x, y, i;
        for (i = 0; i < e.targetTouches.length; i++) {
            x = e.targetTouches[i].clientX - offsetX;
            y = e.targetTouches[i].clientY - offsetY;
            drawCircle(x, y);
        }
    }

    function setupCanvas() {
        document.getElementById('canvas').style.display = 'block';
        canvas = document.getElementById('canvas');
        ctx = canvas.getContext("2d");
        coords = getCumulativeOffset(canvas);
        offsetX = coords.x;
        offsetY = coords.y;
        drawBg(ctx);
    }

    function drawBg() {
        ctx.strokeStyle = "rgb(0,0,0)";
        ctx.beginPath();
        ctx.moveTo(10, 400);
        ctx.lineTo(1200, 400);
        ctx.stroke();
        ctx.font = "16pt Arial";
        ctx.fillStyle = "rgb(180,33,33)";
        ctx.fillText("X", 10, 400);
    }

    function drawCircle(x, y) {
        ctx.strokeStyle = "rgb(55,55,255)";
        ctx.beginPath();
        if (oldX && oldY) {
            ctx.moveTo(oldX, oldY);
            ctx.lineTo(x, y);
            ctx.stroke();
            ctx.closePath();
        }
        oldX = x;
        oldY = y;
    }

    // see: http://stackoverflow.com/questions/160144/find-x-y-of-an-html-element-with-javascript
    function getCumulativeOffset(obj) {
        var left, top;
        left = top = 0;
        if (obj.offsetParent) {
            do {
                left += obj.offsetLeft;
                top += obj.offsetTop;
            } while (obj = obj.offsetParent);
        }
        return {
            x: left,
            y: top
        };
    };



function clearSig() {
    canvas.getContext("2d").clearRect(0, 0, xwidth, ywidth);
    canvas.getContext("2d").strokeStyle = "rgb(0,0,0)";
    canvas.getContext("2d").beginPath();
    canvas.getContext("2d").moveTo(10, 400);
    canvas.getContext("2d").lineTo(1200,400);
    canvas.getContext("2d").stroke();
    canvas.getContext("2d").font = "16pt Arial";
    canvas.getContext("2d").fillStyle = "rgb(180,33,33)";
    canvas.getContext("2d").fillText("X", 10, 400);
}