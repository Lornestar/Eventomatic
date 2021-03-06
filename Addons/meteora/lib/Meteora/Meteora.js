/*
  Mootools hacks
*/

/*
  Editor needs fragmentElement to be considered
  an HTML element.
*/
function $type(obj) {
  if (obj == undefined) return false;

  if (obj.$family) return (obj.$family.name == 'number' && !isFinite(obj)) ? false : obj.$family.name;
  if (obj.nodeName){
    switch (obj.nodeType) {
      case 1: return 'element';
      case 9: return 'window';
      case 3: return (/\S/).test(obj.nodeValue) ? 'textnode' : 'whitespace';
    }
  } else if (obj.window) {
    return 'element';
  } else if (typeof obj.length == 'number') {
    if (obj.callee) {
      return 'arguments';
    } else if (obj.item) {
      return 'collection';
    }
  }
  return typeof obj;
};

/*
  Adding a loading mask to Ajax Requests
*/
Request.HTML.implement({
  
  //'onComplete': function() {},

  'initialize': function(options) {
    
    if (!(this.xhr = this.getXHR())) {
      return;
    }
    
    //this.addEvent('onSuccess', this.onComplete);

    if (typeof options.update != 'undefined') {
      options.update = $(options.update);    
    }

    if (Meteora.showLoading) {

      var loading = null;
      
      if ($type(options.loading) == 'element') {
        loading = options.loading;
      } else if ($type(options.loading) == false) {
        loading = options.update;
      }

      if (loading) {

        var loadingMask = Widget.div(
          {'class': 'm-loading-mask'},
          Widget.div(
            null, 
            Widget.img({'src': $meteora['mediaDir']+'/core/loading.gif'})
          )
        );

        if (loading.offsetHeight > 50) {
          loadingMask.className = 'm-loading-mask-2';
        }

        loading.insertBefore(loadingMask, loading.firstChild);
      }

      this.addEvent('onSuccess', function() { free(this); }.bind(loadingMask));
    }
    
    if (typeof options.method == 'undefined') {
      if (typeof options.data == 'undefined' || !options.data) {
        options.method = 'get';
      } else {
        options.method = 'post';
      }
    }

    this.setOptions(options);
    
    this.options.isSuccess = this.options.isSuccess || this.isSuccess;

    this.headers = new Hash(this.options.headers).extend({
      'X-Requested-With': 'XMLHttpRequest',
      'Accept': 'text/javascript, text/html, application/xml, text/xml, */*'
    });

    //this.url = url;
  }
});
/*
  End Mootools hacks
*/

/*
  Compatibility hacks
  Deprecated stuff
*/
var Ajax = new Class({
  'initialize': function(url, options) {
    if (!options) {
      options = {};
    }
    options.url     = url;
    this.options    = options;
  },
  'jsonEval': function(json) {
    new JsonRpc(json);
  },
  'request': function() {
    if (this.options.update) {
      this.request = new Request.HTML(this.options);
    } else {
      this.request = new Request(this.options);
    }
    return this.request.send();
  },
  'rpc': function() {
    this.request = new Request(this.options);
    this.request.addEvent('onSuccess', this.jsonEval);
    if (typeof this.options.onComplete) {
      this.request.addEvent('onSuccess', this.options.onComplete);
    }
    return this.request.send();
  }
});
Function.implement({
  'bindAsEventListener': function(bind, args) {
    return this.bindWithEvent(bind, args);
  }
});
/*
  End compatibility hacks
*/

/*
  Returns a translated string if a translation exists.
*/
__ = function(t) {
  if (Messages && Messages[t]) {
    return Messages[t];
  }
  return t;
} 

/*
  Includes an external javascript file.
*/
include = function(path) {
  var head = document.getElementsByTagName('head');
  if (head) {
    var script = document.createElement('script');
    
    script.setAttribute('type', 'text/javascript');
    script.setAttribute('src', path);
    head[0].appendChild(script);
  }
}

/*
  Hack for FF2, sometimes the caret is missing.
*/
caretHack = function(el) {
  var el = $(el);
  if (el) {
    if (el.getElementsByTagName) {
      var focused = el.getElementsByTagName('input');
      var len = focused.length - 1;
      for (var i = len; i >= 0; i--) {
        focused[i].blur();
      }
    }
  }
}

/*
  Frees elements, this should be merged with
  some Mootools magic
*/
free = function(el) {

  if (el) {

    if (el.components) {
      // freeing a control
      if (el.$listeners) {
        var len = el.$listeners.length - 1;
        for (var i = len; i >= 0; i--) {
          var p = el.$listeners[i];
          $(p.el).removeEvent(p.event, p.callback);
          p = null;
        }
      }
      if (el.$timers) {
        for (var i in el.$timers) {
          window.clearTimeout(el.$timers[i]);
          el.$timers[i] = null;
        }
      }
      for (var p in el.components) {
        free(el.components[p]);
      }
      for (var p in el) {
        el[p] = null;
      }
      el.$listeners = el.components = null;

    } else {

      if (el.parentNode) {
        el.parentNode.removeChild(el);
      }

      // freeing an element
      if (el.nodeType != 3) {

        if (el.childNodes) {
          var len = el.childNodes.length - 1;
          for (var i = len; i >= 0; i--) {
            free(el.childNodes[i]);
          }
        }

        if (el.$events) {
          if (el.removeEvents) {
            el.removeEvents();
          } else {
            for (var p in el.$events) {
              el.$events[p] = null;
            }
          }
        }

        if (el.$controls) {
          for (var p in el.$controls) {
            free(el.$controls[p]);
          }
        }

        for (var p in el.$tmp) {
          el.$tmp[p] = null;
        }

      }
    }
    el = null;
  }
}

/*
  Formats data to be output to Meteora's log.
*/
logformat = function(a, level) {
  if (!level) {
    level = 0;
  }
  if (level < 1) {
    switch($type(a)) {
      case 'array':
        var len = a.length;
        for (var i = 0; i < len; i++) {
          a[i] = logformat(a[i]);
        }
        return '<div class="m-log-indent">[<div class="m-log-indent">'+a.join(', ')+'</div>]</div>';
      break;
      case 'object': case 'element':
        var b = new Array();
        for (var i in a) {
          b.push('<span class="m-log-var"><b>'+i+'</b></span>'+': '+logformat(a[i], level + 1));
        }
        return '<div class="m-log-indent">{<div class="m-log-indent">'+b.join(', ')+'</div>}</div>';
      break;
      case 'string': case 'number':
        var span = Widget.span();
        span.appendChild(document.createTextNode(a));
        return '<span class="m-log-constant">"'+span.innerHTML+'"</span>';
      break;
    }
  }
  return a;
}


debug = function(key, data, extended) {
  log(key+' <span><a href="javascript:void(0)" onclick="$(this.nextSibling).toggle()">&raquo;</a><div class="m-log-expand" '+(extended == true ? '' : 'style="display: none"')+'>'+logformat(data)+'</div></span>');
}

log = function(mixed) {
  
  if (document.body && Meteora.options.debug.enable) { 

    var log = $('m-log');

    if (!log) {
      
      log = Widget.div({'id': 'm-log'});

      log.handler = Widget.div({'class': 'm-log-handler'});
      log.appendChild(log.handler);

      log.handler.toggle = Widget.div({'class': 'm-log-toggle'});
      log.handler.appendChild(log.handler.toggle);

      log.content = Widget.div({'class': 'm-log-content'});
      log.appendChild(log.content);

      // resizing
      log.handler.addEvent(
        'mousedown',
        function(e) {
          document.onselectstart = function() {return false; };
          var e = new Event(e);
          this.orig = {
            y: e.page.y,
            size: this.content.offsetHeight
          }
          this.resize = function (e) {
            var e = new Event(e);
            this.content.style.height = this.orig.size+(this.orig.y-e.page.y)+'px';
            this.scroll();
          }.bindWithEvent(this)
          document.addEvent(
            'mousemove',
            this.resize
          );
          document.addEvent(
            'mouseup',
            function() {
              document.removeEvent('mousemove', this.resize);
            }.bind(this)
          );
        }.bindWithEvent(log)
      );

      log.content.style.height = '0px';

      log.handler.toggle.addEvent(
        'mousedown',
        function() {
          if (this.content.style.height == '0px') {
            this.content.style.height = '150px';
          } else {
            this.content.style.height = '0px';
          }
        }.bind(log)
      );

      log.scroll = function() {
        if (this.scrollTimer) {
          window.clearTimeout(this.scrollTimer);
        }
        this.scrollTimer = window.setTimeout(
          function() {
            if (Browser.Engine.trident) {
              this.content.scrollTop = 0;
            }
            this.content.scrollTop = this.content.scrollHeight;
            this.content.scrollTimer = null
          }.bind(this),
          100
        );
      }.bind(log)
      
      document.body.appendChild(log);


      if (Meteora.options.debug.startOpened) {
        log.content.style.height = '150px';
        Meteora.options.debug.startOpened = false;
      }

    }

    if (!$type(mixed).match(/string|number/)) {
      mixed = logformat(mixed);
    }

    var line = Widget.div({'class': 'log-line'}, Widget.fromHTML(mixed));

    window.setTimeout(function(){ log.handler.style.backgroundColor = '' }.bind(log), 180);
    log.handler.style.backgroundColor = '#EEFF91';
    log.content.appendChild(line);

    log.scroll();
  }
}


/*
  Main Meteora Class
*/
var Meteora = new Class({

  Implements: [Events, Options],

  'options': {
    'debug': {
      'enable': false,
      'startOpened': false
    }
  },

  '__resizeOverlay': function() {
    if (document.__overlay) {
      el = document.__overlay;
      el.setStyles({
        'height': Browser.pageHeight()+'px',
        'width':  Browser.pageWidth()+'px'
      });
    }
  },

  'overlay': function(options) {

    var opacity = 0.6;

    if (!document.__overlay) {

      document.__overlayCount = 0;
    
      var el = Widget.div({'class': 'm-overlay'});
      el.hide();
      
      document.body.insertBefore(el, document.body.firstChild);

      if (typeof Effect != 'undefined') {
        Effect.appear(el, { 'endOpacity': opacity });
      } else {
        el.show();
        el.setOpacity(opacity);
      }
      
      document.__overlay = el;

      Meteora.__resizeOverlay();

      if (!document.__resizeOverlayListener) {
        document.__resizeOverlayListener = $(window).addEvent(
          'resize',
          Meteora.__resizeOverlay
        );
      }

    }

    document.__overlayCount++;

    if (options) {
      for (var i in options) {
        if (i.match(/^on.*/)) {
          document.__overlay.addEvent(
            i.substr(2, 1).toLowerCase()+i.substr(3),
            options[i]
          );
        }
      }
    }

  },

  'removeOverlay': function() {
    if ($type(document.__overlayCount)) {
      document.__overlayCount--;
      if (document.__overlayCount == 0) {
        document.__overlay.dump();
        document.__overlay = null;
      }
    }
  },

  'showLoading': true,

  'loadStylesheet': function() {

    this.importStylesheet($meteora['cssDir']+'main.css');

    if (Browser.Engine.trident4) {
      this.importStylesheet($meteora['cssDir']+'ie6.css');
    }

    this.importStylesheet($meteora['themesDir']+$meteora['theme']+'/main.css');

    var hacks = $meteora['hacks'].split(',');

    for (var i = 0; i < hacks.length; i++) {
      var hack = hacks[i];
      // checking for browser ID
      if (typeof window[hack] != 'undefined' && window[hack]) {
        this.importStylesheet($meteora['themesDir']+$meteora['theme']+'/'+hack+'.css');
      }
    }

  },

  'initialize': function() {
    $meteora.started = false;
        
    this.loadStylesheet();
    
    $(window).addEvent(
      'domready',
      function() {
        if (typeof jQuery != 'undefined') {
          jQuery.noConflict();
        }
        //Meteora.loadStylesheet.bind(Meteora)();
        log('To open/close Meteora\'s log click in the little bug icon. Use log() to add content.', 3);
        Meteora.fireEvent('onStart');
        $meteora.started = true;
      }
    );

    $(window).addEvent(
      'unload',
      function() {
        this.fireEvent('onExit');
      }
    );
  },

  'showDebug': function(enable, startOpened) {
    this.options.debug = {
      enable:       enable,
      startOpened:  startOpened
    } 
  },

  'importStylesheet': function(url) {
  
    var head = document.getElementsByTagName('head');

    if (head) {
      var today = new Date();
      var reload = ''+today.getHours()+today.getDate()+(today.getMinutes() > 5 ? 'X' : 'V');

      var link = new Element('link');
      link.setAttribute('type',   'text/css');
      link.setAttribute('rel',    'stylesheet');
      link.setAttribute('href',   url+'?'+reload);

      head[0].appendChild(link);

      log('Importing style '+link.href, 3);
    }
  },

  'uses': function(file) {
    file = file.charAt(0).toUpperCase()+file.substr(1);
    if (file.match(/\./) == null) {
      file = 'Meteora.'+file;
    }
    $lib(file);
  },

  'onStart': function(f) {
    if (!$meteora.started) {
      this.addEvent('onStart', f);
    } else {
      log('Meteora.onStart() has been already called.');
    }
  },

  'onExit': function (f) {
    this.addEvent('onExit', f);
  },

  'jQuery': function(start) {
    if (start) {
      if (!window.$mootools) {
        window.$mootools = window.$;
      }
      window.$ = jQuery; 
    } else {
      window.$ = window.$mootools;
    }
  },

  // Private
  '__loadedCSS': false,
  '__onTopIndex': 10000,

  // deprecated
  'includeScript': function(s) {
    include(s)
  }

});

// Control base

var Control = new Class({
  
  '__isControl': true,
  
  'fromJSON': function(json) {
    var json = eval('('+json+')');
    if (json && json.jsonRpc) {
      new JsonRpc(json.jsonRpc);
      delete json.jsonRpc;
    }
    return json;
  },
  
  'destroy': function() {
    free(this);
  },

  'setMainElement': function(el) {
    this.element = $(el);
    if ($type(this.element.$controls) != 'array') this.element.$controls = [];
    this.element.$controls.push(this);
  },
  
  'removeTimer': function(id, time) {
  
  },

  'addTimer': function(callback, time) {
    var id = window.setTimeout(callback, time);
    if (!this.$timers)
      this.$timers = [];
    this.$timers.push(id);
    return id;
  },
  
  'addListener': function(el, e, callback) {
    if (Browser.Engine.trident || Browser.Engine.webkit) {
      if (e == 'keypress') {
        e = 'keydown';
      }
    }
    var el = $(el);
    var li = callback.bindWithEvent(this);
    if ($type(this.$listeners) == false) {
      this.$listeners = [];
    }
    this.$listeners.push({
      'el':       el,
      'event':    e,
      'callback': li
    });
    el.addEvent(e, li);
    return this.$listeners[this.$listeners.length - 1];
  },
  
  'removeListener': function(li) {
    $(li.el).removeEvent(li.event, li.callback);
  }
});

Control.implement(Events);
Control.implement(Options);

// String
String.extend({
  'repeat': function(times) {
    var ret = '';
    for (var i = 0; times > 0 && i < times; i++) {
      ret += this;
    }
    return ret;
  }
});

Element.implement({
  'adopt': function(){
    Array.flatten(arguments).each(function(element){
      if ($(element)) {
        this.appendChild($(element, true));
      }
    }, this);
    return this;
  },
  
  'dispose': function() {
    if (this.parentNode) {
      return this.parentNode.removeChild(this);
    }
    return true;
  },

  'toggleFullscreen': function() {

    if (this.retrieve('fullscreen')) {

      if (!Browser.Engine.trident4) {
        document.body.style.overflow = '';
      }
      
      this.setStyles({
        'position':   '',
        'background': '',
        'zIndex':     ''
      });
      
    } else {
    
      if (!Browser.Engine.trident4) {
        document.body.style.overflow = 'hidden';
      }

      window.scrollTo(0, 0);

      this.setStyles({
        'position':   'absolute',
        'top':        '0px',
        'left':       '0px',
        'bottom':     '0px',
        'right':      '0px',
        'background': '#fff',
        'zIndex': 10
      });

      this.setOnTop();
    }

    this.store('fullscreen', !this.retrieve('fullscreen'));
  },

  'firstElement': function() {
    var node = this.firstChild;
    while ($type(node) != 'element') {
      node = node.nextSibling;
    }
    return $(node);
  },
  
  'lastElement': function() {
    var node = this.childNodes[this.childNodes.length - 1];
    while ($type(node) != 'element') {
      node = node.previousSibling;
    }
    return $(node);
  },

  'previousElement': function() {
    var node = this.previousSibling;
    while (node) {
      if ($type(node) == 'element') {
        return node;
      }
      node = node.previousSibling;
    }
    return node;
  },

  'nextElement': function() {
    var node = this.nextSibling;
    while (node) {
      if ($type(node) == 'element') {
        return node;
      }
      node = node.nextSibling;
    }
    return node;
  },


  'scrollTo': function(x, y) {
    this.scrollLeft = x;
    this.scrollTop = y;
  },

  'getElementsByClassName': function(className) {
    var results = [];

    var myclass = new RegExp('\\b'+className+'\\b');
    var elem = this.getElementsByTagName('*');
    var len = elem.length;
    for (var i = 0; i < len; i++) {
      var classes = elem[i].className;
      if (myclass.test(classes)) results.push(elem[i]);
    }
    return results;
  },

  'hide': function() {
    this.style.display = 'none';
  },

  'show': function() {
    this.style.display = '';
  },

  'toggle': function() {
    this.style.display = this.style.display == '' ? 'none' : '';
  },

  'appendChildren': function(contents) {
    var len = contents.length;
    for (var i = 0; i < len; i++) {
      this.appendContent(contents[i]);
    }
  },

  'clonePosition': function(source) {
    // Based on Prototype's Position.clone(), MIT License
    var options = $extend(
      arguments[1] || {},
      {
        setLeft:    true,
        setTop:     true,
        setWidth:   true,
        setHeight:  true,
        offsetTop:  0,
        offsetLeft: 0
      }
    );
    var coords = $(source).getCoordinates();
    if (options.setLeft) this.style.left = (coords['left'] + options.offsetLeft)+'px'; 
    if (options.setTop) this.style.top = (coords['top'] + options.offsetTop)+'px'; 
    if (options.setWidth) this.style.width = source.offsetWidth+'px'; 
    if (options.setHeight) this.style.height = source.offsetHeight+'px'; 
  },

  'prependContent': function(content) {
    this.insertBefore(Widget.fetchContent(content), this.firstChild);
  },
  
  'removeChildren': function() {
    caretHack(this);
    var len = this.childNodes.length - 1;
    for (var i = len; i >= 0; i--) {
      this.removeChild(this.childNodes[i]);
    }
  },

  'dumpChildren': function() {
    caretHack(this);
    var len = this.childNodes.length - 1;
    for (var i = len; i >= 0; i--) {
      free(this.childNodes[i]);
    }
  },

  'dump': function() {
    caretHack(this);
    this.delayedFree(this);
  },

  'delayedFree': function(el, delay) {
    var el = $(el);
    el.destroy();
    window.setTimeout(free.bind(el), delay || 3000);
  },

  'setContent': function(content) {
    this.removeChildren();
    this.appendContent(content);
  },

  'replaceContent': function(content) {
    this.dumpChildren();
    this.appendContent(content);
  },

  'appendContent': function(content) {
    this.appendChild(Widget.fetchContent(content));
  },

  'resizeTo': function(width, height) {
    if (width > -1) {
      this.style.width = width + 'px';
    }
    if (height > -1) {
      this.style.height = height + 'px';
    }
  },

  'moveTo': function(top, left) {
    this.setStyles(
      {
        position: 'absolute',
        top: top+'px',
        left: left+'px'
      }
    );
  },
  
  'center': function() {
    var x = Browser.pageScrollY()+(Browser.clientHeight() - this.offsetHeight)/2;
    var y = Browser.pageScrollX()+(Browser.clientWidth() - this.offsetWidth)/2;
    this.moveTo(x > 0 ? x : 0, y > 0 ? y : 0);
  },

  'findParentWithClassName': function(className) {
    var obj = this;
    var regexp = new RegExp('(^|\\s)'+className+'(\\s|$)');
    while (obj) {
      if (obj.className && obj.className.match(regexp)) {
        return $(obj);
      }
      obj = obj.parentNode;
    }
    return null;
  },

  'findParentWithName': function(name) {
    var obj = this;
    while (obj && obj.nodeName.toLowerCase() != name.toLowerCase()) {
      obj = obj.parentNode;
    }
    return $(obj);
  },

  'selectRange': function(start, length) {
    if (this.setSelectionRange) {
      // mozilla
      this.setSelectionRange(start, length);
    } else if (this.createTextRange) {
      // ie
      var range = this.createTextRange();
      range.collapse(true);
      range.moveEnd('character', length);
      range.moveStart('character', start);
      range.select();
    }
    this.focus();
  },

  'eachChild': function(callback) {
    var len = this.childNodes.length;
    for (var i = 0; i < len; i++) {
      callback.call(this.childNodes[i]);
    }
  },

  'getDimensions': function() {
    var size = this.getSize();
    return { width: size['x'], height: size['y'] };
  },

  'setOnTop': function() {
    this.style.zIndex = ++Meteora.__onTopIndex;
  }
});

Element.create = function(el, attr, inner) { 

  if (!attr) attr = {};

  if (Browser.Engine.trident && (attr.name || attr.type)) {
    var name = attr.name ? ' name="'+attr.name+'"' : '';
    var type = attr.type ? ' type="'+attr.type+'"' : '';
    delete attr.name;
    delete attr.type;
    el = '<'+el+name+type+'>';
  }

  var node = document.createElement(el);

  node = $(node);

  if (attr) {
    for (var p in attr) {
      if (p.match(/^on.*/)) {
        if (typeof(attr[p]) == 'function') {
          node[p] = attr[p];
        } else {
          if (Browser.Engine.trident) {
            node.setProperty(p, new Function(attr[p]));
          } else {
            node.setProperty(p, attr[p]);
          }
        }
      } else {
        if (p == 'style') {
          Element.setStyles(node, attr[p]);
        } else {
          node.setProperty(p, attr[p]);
        }
      }
    }
  }
  if (inner) {
    if ($type(inner) == 'string') {
      inner = document.createTextNode(inner);
      node.appendChild(inner);
    }
    if ($type(inner) == 'array') {
      node.appendChildren(inner);
    } else if ($type(inner) == 'element') {
      node.appendChild(inner);
    } else if ($type(inner) == 'object') {
      node.setContent(inner);
    }
  }
  return node;
}

Widget = new Class({
  
  '__tagList': [ 'a','abbr','acronym','address','area','b','base','bdo','big','blockquote','body','br','button','caption','cite','code','col','colgroup','dd','del','div','dfn','dl','dt','em','embed', 'fieldset','form','frame','frameset', 'h1', 'h2', 'h3', 'h4', 'h5', 'head','hr','html','i','iframe','img','input','ins','kbd','label','legend','li','link','map','meta','noframes','noscript','object','ol','optgroup','option','p','param','pre','q','samp','script','select','small','span','strong','style','sub','sup','table','tbody','td','textarea','tfoot','th','thead','title','tr','tt','ul','var' ],

  '__tag': function (name, attr, inner) {

    if (attr && $type(attr.style) == 'string') {
      var style = {};
      var temp  = attr.style.split(';');
      while(temp.length > 0) {
        var curr = temp.pop();
        if (curr) {
          var elem = curr.split(':');
          if (elem && elem.length == 2) {
            style[elem[0]] = elem[1];
          }
        }
      }
      attr.style = style;
    }

    return Element.create(name, attr, inner);
  },

  'initialize': function() {
    var len = this.__tagList.length;
    for (var i = 0; i < len; i++) {
      var name = this.__tagList[i];
      this[name] = function(attr, children) {
        return this.parent.__tag(this.name, attr, children);
      }.bind({'parent': this, 'name': name})
    }
  },

  'fromHTML': function(html) {

    if (html) {
      html = html.replace(/^[\r\n\s\f\t]+/, '');
      html = html.replace(/[\r\n\s\f\t]+$/, '');
    } else {
      html = '';
    }

    var span = document.createElement('span');
    span.innerHTML = html;

    if (Browser.Engine.trident) {
      window.setTimeout(
      function() {
        var scripts = [];
        var regexp = /<script[^>]*>([\s\S]*?)<\/script>/gi;
        while ((script = regexp.exec(this))) scripts.push(script[1]);
        scripts = scripts.join('\n');
        if (scripts) (window.execScript) ? window.execScript(scripts) : window.setTimeout(scripts, 0);
      }.bind(html), 200);
    }

    if (span.childNodes.length == 1 && $type(span.childNodes[0]) == 'element') {
      return span.childNodes[0];
    }

    return span;
  },

  'fetchContent': function(obj) {
    if (typeof(obj) == 'object') {
      if (obj.url) {
        var span = Widget.div();
        if (obj.url.match(/^http:\/\//i)) {
          var iframe = Widget.iframe({ src: obj.url, 'border': 0, 'frameborder': 0, 'height': '100%', 'width': '100%' }); 
          return iframe;
        } else {
          
          obj.update = span;
          obj.evalScripts = true;

          var req = new Request.HTML(
            obj
          ).send();

        }
        return span;
      } else {
        return obj;
      }
    } else {
      return Widget.fromHTML(obj);
    }
  }
});


/*
  Extending Browser class
*/
var Browser = Browser.extend({

  'clientWidth': function() {
    var width = 0;
    if (window.innerWidth) {
      width = window.innerWidth;
    } else if (document.documentElement && document.documentElement.clientWidth) {
      width = document.documentElement.clientWidth;
    } else if (document.body) {
      width = document.body.clientWidth;
    }
    return width;
  },
  
  'focusElement': function(el) {
    if  (typeof(Fx) != 'undefined' && Fx.Scroll) {
      var epos = el.getPosition();

      var cpos = {
        x: Browser.pageScrollX(),
        y: Browser.pageScrollY(),
        /*
        w: Browser.windowWidth(),
        h: Browser.windowHeight()
        */
        w: Browser.clientWidth(),
        h: Browser.clientHeight()
      }

      if ((epos.y < cpos.y || epos.y > cpos.y+cpos.h) || (epos.x < cpos.x || epos.x > cpos.y+cpos.w)) {
        var scroll = new Fx.Scroll(
          window, {
          offset: {
            x: 0,
            y: -100
          },
          wheelStops: true
        });
        scroll.toElement(el);
      }
    } else {
      log('Missing Fx.Scroll'); 
    }
  },

  'clientHeight': function() {
    var heigth = 0;
    if (window.innerHeight) {
      height = window.innerHeight;
    } else if (document.documentElement && document.documentElement.clientHeight) {
      height = document.documentElement.clientHeight;
    } else if (document.body) {
      height = document.body.clientHeight;
    }
    return height;
  },

  'pageBottom': function() {
    var ch = this.pageScrollY()+this.clientHeight();
    return ch;
  },

  'windowWidth': function() {
    var cw = this.clientWidth();
    var pw = this.pageWidth();
    return cw > pw ? cw : pw;
  },

  'windowHeight': function() {
    var ch = this.clientHeight();
    var ph = this.pageHeight();
    return ch > ph ? ch : ph;
  },

  'pageWidth': function() {
    var size;
    
    var clientsize = this.clientWidth();

    if (window.innerWidth && window.scrollMaxX) {
      size = window.innerWidth + window.scrollMaxX;
    } else if (document.body.scrollWidth > document.body.offsetWidth) {
      if (Browser.Engine.trident4) {
        size = document.body.scrollWidth+(document.body.offsetWidth-document.body.offsetWidth);
      } else {
        size = clientsize;
      }
    } else {
      size = document.body.offsetWidth;
    }

    return (size > clientsize ? clientsize: size);
  },
  
  'pageHeight': function() {
    var size;

    var clientsize = this.clientHeight();

    if (window.innerHeight && window.scrollMaxY) {
      size = window.innerHeight + window.scrollMaxY;
    } else if (document.body.scrollHeight > document.body.offsetHeight) {
      size = document.body.scrollHeight;
    } else {
      size = document.body.offsetHeight;
    }

    return (size > clientsize ? size : clientsize);
  },

  'pageScrollX': function() {
    if (self.pageXOffset) {
      return self.pageXOffset;
    } else if (document.documentElement && document.documentElement.scrollLeft) {
      return document.documentElement.scrollLeft;
    } else if (document.body) {
      return document.body.scrollLeft;
    }
  },

  'allowScroll': function(s) {
    if (s) {
      if (this.__scrollEvent) {
        window.removeEvent('scroll', this.__scrollEvent);
      }
      this.__scrollEvent = null;
      document.body.style.overflow = '';
    } else {
      if (!this.__scrollEvent) {
        this.__scrollEvent = function() {
          window.scrollTo(0, 0);
        }.bind(this);
        window.addEvent('scroll', this.__scrollEvent);
      }
      document.body.style.overflow = 'hidden';
    }
  },
  
  'pageScrollY': function() {
    if (self.pageYOffset) {
      return self.pageYOffset;
    } else if (document.documentElement && document.documentElement.scrollTop) {
      return document.documentElement.scrollTop;
    } else if (document.body) {
      return document.body.scrollTop;
    }
  },
  
  'followMark': function(name) {
    location.href = location.href.replace(/#.*/, '')+'#'+name;    
  }
});

/*
  Invoking the magic
*/
Widget  = new Widget();
Meteora = new Meteora();
