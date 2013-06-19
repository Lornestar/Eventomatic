Ext.ns('App.View');

App.View.CounterList = Ext.extend(Ext.Panel, {

    // privat
    initComponent: function () {

        var config = {
            layout: 'fit',
            dockedItems: [{
                dock: 'top',
                xtype: 'toolbar',
                ui: 'dark',
                title: 'Counter List',
                items: [{
                    text: 'Back',
                    ui: 'back',
                    handler: this.onTapButtonAction
                }, {
                    xtype: 'select',
                    name: 'sort_by',
                    prependText: 'Sort by:',
                    options: [
                        { text: 'Duration', value: 'duration' },
                        { text: 'Alphabet', value: 'alphabet' },
                        { text: 'IDs', value: 'ids' }
                    ],
                    listeners: {
                        scope: this,
                        change: this.onTabSortChange
                    }
                }, {
                    xtype: 'spacer'
                }, {
                    text: 'Edit',
                    handler: this.onTapButtonAction
                }]
            }],
            items: [{
                id: 'viewCounterList',
                xtype: 'list',

                store: 'counterStore',
                //data: '',

                itemTpl: new Ext.XTemplate(
                    '<tpl for="."><div class="counterListObj">',
                        '<div class="counterGfx" style="background-image:url({counterGfx});" alt="{id}"></div>',
                        '<div class="counterName">{counterName}</div>',
                        '<div class="counterNr">{[this.secondsToTime(values)]}</div>',
                        '<div class="counterBar" style="width:0;"></div>',
                        '<div class="clearBoth"></div>',
                    '</div></tpl>',
                    {
                        secondsToTime: function (data) {
                            var secs = parseInt(data.counterDuration, 10);
                            var hours = Math.floor(secs / (60 * 60));
                            var minutes = Math.floor(secs % (60 * 60) / 60);
                            var seconds = Math.ceil(secs % (60 * 60) % 60);
                            var timeObj = {
                                "hours": hours < 10 ? "0" + hours : hours,
                                "minutes": minutes < 10 ? "0" + minutes : minutes,
                                "seconds": seconds < 10 ? "0" + seconds : seconds
                            };
                            var timeVar = timeObj.hours + ":" + timeObj.minutes + ":" + timeObj.seconds;
                            return timeVar;
                        },
                        draggable: true,
                        compiled: true
                    }
                ),
                grouped: true,
                indexBar: false,
                listeners: {
                    scope: this,
                    itemSwipe: this.onTapListAction,
                    itemDoubleTap: this.onTapListAction,
                    itemTap: this.onTapListAction
                }
            }]
        };
        Ext.apply(this, config);
        App.View.CounterList.superclass.initComponent.call(this);

    },

    secondsToTime: function (data) {
        var secs = parseInt(data, 10);
        var hours = Math.floor(secs / (60 * 60));
        var minutes = Math.floor(secs % (60 * 60) / 60);
        var seconds = Math.ceil(secs % (60 * 60) % 60);
        var timeObj = {
            "hours": hours < 10 ? "0" + hours : hours,
            "minutes": minutes < 10 ? "0" + minutes : minutes,
            "seconds": seconds < 10 ? "0" + seconds : seconds
        };
        var timeVar = timeObj.hours + ":" + timeObj.minutes + ":" + timeObj.seconds;
        return timeVar;
    },

    timeToSeconds: function (data) {
        var dataObj = data.split(":");
        var hoursToSec = parseInt(dataObj[0], 10) * 60 * 60;
        var minutesToSec = parseInt(dataObj[1], 10) * 60;
        var seconds = parseInt(dataObj[2], 10);
        var durationInSec = hoursToSec + minutesToSec + seconds;
        return durationInSec;
    },

    countDownEntry: function (counterNrDiv, counterBarDiv, counterNr) {
        if (!Ext.DomQuery.selectNode('span.itemActive', counterNrDiv)) { return }; // Check if the countdown got remotely stopped
        var countdownValue = this.timeToSeconds(counterNrDiv.innerText) - 1;
        this.updateEntryValue(counterNrDiv, counterBarDiv, counterNr, countdownValue);
        if (countdownValue > 0) {
            this.countDown = new Ext.util.DelayedTask(function () {
                this.countDownEntry(counterNrDiv, counterBarDiv, counterNr);
            }, this);
            this.countDown.delay(1000);
        }
    },

    updateEntryValue: function (counterNrDiv, counterBarDiv, counterNr, countdownValue) {
        if (countdownValue != 0) {
            var counterBarDivValue = 100 - (100 / counterNr * countdownValue);
            counterBarDiv.style.width = counterBarDivValue + '%';
            counterBarDiv.style.visibility = 'visible';
            counterNrDiv.innerHTML = '<span class="itemActive"><div class="countdownSpinner"></div>' + this.secondsToTime(countdownValue) + "</span>";
        } else {
            // App.View.Audio.playSound('res/audio/quad.mp3');
            counterNrDiv.innerHTML = '<span class="itemThere">READY! ' + this.secondsToTime(counterNr) + '</span>';
            counterBarDiv.style.width = '101%';
        }
    },

    onTapListAction: function (listComponent, index, item, eventObject) {
        var counterNr = listComponent.store.getAt(index).data.counterDuration;
        var counterBarDiv = Ext.DomQuery.selectNode('div.counterBar', item);
        var counterNrDiv = Ext.DomQuery.selectNode('div.counterNr', item);
        var counterActiveDiv = Ext.DomQuery.selectNode('span.itemActive', counterNrDiv);
        switch (eventObject.type) {
            case 'swipe':
                if (counterActiveDiv) {
                    var countdownValue = this.timeToSeconds(counterNrDiv.innerText);
                    countdownValue = (eventObject.direction == "right") ? countdownValue - 1 : countdownValue + 2;
                    this.updateEntryValue(counterNrDiv, counterBarDiv, counterNr, countdownValue);
                }
                break;
            case 'singletap':
                if (!counterActiveDiv) {  // Catch multiple countdowns
                    if (counterNr >= 1) { // Catch Error: 0-1
                        this.updateEntryValue(counterNrDiv, counterBarDiv, counterNr, counterNr); // Direct response
                        this.countDownEntry(counterNrDiv, counterBarDiv, counterNr);
                    } else {
                        counterNrDiv.innerHTML = '<span class="itemError">Error: Input value is negative.</span>';
                    }
                }
                break;
            case 'doubletap':
                if (counterActiveDiv) {
                    counterNrDiv.innerHTML = this.secondsToTime(counterNr);
                    counterBarDiv.style.width = '0%';
                    counterBarDiv.style.visibility = 'hidden';
                }
                break;
        }
    },

    onTapButtonAction: function (button, event) {
        switch (button.text) {
            case 'Edit':
                App.fireEvent('setView', 'CounterListEdit', 'fade', '');
                break;
            case 'Back':
                App.fireEvent('setView', 'PresetList', 'slide', 'right');
                break;
        }
    },

    onTabSortChange: function (comp, value) {
        // this.store.sort(value, 'ASC'); //TODO: How to access the store?
        // this.fireEvent('filter', this.getValues(), this);
    }

});

Ext.reg('App.View.CounterList', App.View.CounterList);