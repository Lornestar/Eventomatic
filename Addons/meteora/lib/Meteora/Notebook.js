/**
 * Tabcontrol
 * ---
 * Written by Jordi Adame <jordi@astrata.com.mx>
 *            Diego Carrera <diego@astrata.com.mx>
 *            J. Carlos Nieto <xiam@astrata.com.mx>
 *
 * Copyright (c) 2007-2009 Astrata Software S.A. de C.V.
 *
 * Licensed under The MIT License
 * Redistributions of files must retain the above copyright notice.
 *
 * @author          Jorge Adame
 * @author          Diego Carrera
 * @author          J. Carlos Nieto
 * @copyright       Copyright (c) 2007-2009, Astrata Software S.A. de C.V.
 * @link            http://astrata.com.mx Astrata Open Source Projects
 * @version         $Revision: $
 * @modifiedby      $LastChangedBy: $
 * @lastmodified    $Date: $
 * @license         http://www.opensource.org/licenses/mit-license.php The MIT License
 *
 */

var Notebook = new Class({
  
  'Implements': [ Control ],

  'history': [],
  
  'initialize': function (container, options) {
    
    this.options = {
      'selectEvent':        'mousedown',
      'forceUpdate':        false,
      'allowBookmark':      true,
      'autoSelect':         true,
      'preloadContent':     false,
      'removePageContent':  false,
      'confirmClose':       false
    }

    this.history = [];

    this.element  = $(container);
    this.setOptions(options);

    this.__buildComponents();
  },

  '__buildComponents': function() {

    this.components = {
      'element' : Widget.div({'class': 'm-notebook'}),
      'tabs'    : Widget.ul({'class': 'm-tabs'}),
      'frame'   : Widget.div({'class': 'm-frame'}),
      'page'    : Widget.div({'class': 'm-page'})
    }

    this.components.frame.appendChild(this.components.page);

    this.components.element.appendChildren([
      this.components.tabs,
      this.components.frame
    ]);

    this.element.appendChild(this.components.element);
    this.components.element.parent = this;

    this.__tabs     = {};
    this.__contents = {};

    this.__bindEvents();
  },

  '__bindEvents': function() {
    this.components.tabs.addEvent(
      this.options.selectEvent,
      function (e) {
        var e = new Event(e);
        var obj = e.target;
        while (obj && obj.nodeName.toLowerCase() != 'li') { 
          obj = obj.parentNode;
        }
        if (obj && obj.parent) {
          obj.parent.selectPage(obj.name, false);
          if (obj.options && obj.options.onSelect) {
            obj.options.onSelect();  
          }
        }
      }
    );
  },

  'resizeTo': function(width, height) {
    $(this.components.element).resizeTo(width, -1);  
    if (Browser.Engine.trident) {
      for (var i in this.__contents) {
        $(this.__contents[i]).resizeTo(-1, height);
      }
    }
  },
  
  'selectPage': function(name, focus) {

    if (this.__selected) {
      caretHack(this.__contents[this.__selected]);
    }

    focus = $type(focus) ? focus : true;

    if (this.options.forceUpdate == true || this.__tabs[name].retrieve('firstView') == null) {
      this.updatePage(name);
    }

    if (this.options.removePageContent) {

      for (var i in this.__tabs) {
        this.__tabs[i].className = 'm-tab';
      }

      if (this.__tabs[name].retrieve('firstView') != null) {
        var scripts = this.__contents[name].getElementsByTagName('script');
        while (scripts.length) {
          $(scripts[0]).remove();
        }
      }
      
      this.components.page.setContent(this.__contents[name]);

    } else {
      // deselecting tabs
      for (var i in this.__tabs) {
        this.__tabs[i].className = 'm-tab';
        this.__contents[i].hide();
      }
    }
    this.__contents[name].show();

    this.__tabs[name].className = 'm-tab m-selected';

    this.__selected = name;

    if (this.options.allowBookmark) {
      
      var y = Browser.pageScrollY();
      Browser.followMark(name);

      Browser.focusElement(this.element);
    }

    if (this.history.length) {
      var tail = this.history.pop();
      if (tail != name) {
        this.history.push(tail);
      }
    }

    this.history.push(name);
  },

  '__initialCheck': function() {
    log('No bookmarked page found.');
    if (!this.__selected) {
      for (var index in this.__tabs) {
        this.selectPage(index);
        return;
      }
    }
  },

  'pageExists': function(id) {
    return $type(this.__tabs[id]);
  },

  'renamePage': function(id, title) {

    var tab     = this.__tabs[id];
    var text    = tab.retrieve('text');
    var options = tab.retrieve('options');

    text.replaceChild(document.createTextNode(title), text.firstChild);
    tab.store('title', title);

    if (options.onRename) {
      options.onRename();
    }
  },

  '__isSelected': function(id) {
    return this.__tabs[id].className.match(/m-selected/);
  },

  '__renameInputBlur': function(e, tab) {
    this.renamePage(tab.name, tab.retrieve('input').value);
    tab.retrieve('text').show();
    tab.retrieve('input').dump();
    tab.store('input', null);
  },

  '__renameInputKeydown': function(e, tab) {
    var e = new Event(e);
    if (e.key.match(/^(enter|esc|tab)$/)) {
      this.__renameInputBlur(e, tab);
      e.stop();
    }
  },

  '__renamePage': function(tab) {
    if (!tab.retrieve('input')) {
      if (this.__isSelected(tab.name)) {
        var input = Widget.input({'class': 'm-rename'});

        tab.retrieve('text').parentNode.appendChild(input);

        tab.retrieve('text').hide();

        input.value = tab.retrieve('text').firstChild.nodeValue;

        input.addEvent(
          'keydown', this.__renameInputKeydown.bindWithEvent(this, tab)
        );
        
        input.addEvent(
          'blur', this.__renameInputBlur.bindWithEvent(this, tab)
        );
        
        this.addTimer(
          function() {
            this.select();
            this.focus();
          }.bind(input),
          100
        );

        tab.store('input', input);
      }
    }
  },

  '__createPage': function(name, content) {

    var tab = Widget.li();
    tab.name = name.id;

    tab.store('title',    name.title);
    tab.store('options',  name.options);
    tab.store('text',     $(Widget.fromHTML(name.title)));

    var title = Widget.span(null, tab.retrieve('text'));

    tab.appendChild(Widget.span(null, tab.retrieve('text')));

    if (name.options.allowRename) {
      tab.addEvent(
        'mousedown', this.__renamePage.bind(this, tab)
      );
    }

    if (name.options.allowClose) {

      var close = Widget.span({'class': 'm-close'}, Widget.fromHTML('&times;'));

      close.addEvent(
        'mousedown',
        function(e) {
          
          e = new Event(e);

          if (this.parent.options.confirmClose) {

            Dialog.question(
              __('Are you sure you want to close this tab?'),
              {
                'onYes': function(dialog) {
                  var options = this.retrieve('options');
                  if (options.onClose) {
                    options.onClose();
                  }
                  this.parent.closePage(this.name);
                  dialog.close();
                }.bind(this)
              }
            );

          } else {

            var options = this.retrieve('options');

            if (options.onClose){
              options.onClose();
            }

            this.parent.closePage(this.name);
          }

          e.stop();

        }.bindWithEvent(tab)
      );
      tab.appendChild(close);
    }

    tab.parent = this;

    this.components.tabs.appendChild(tab);

    tab.store('content', content)

    this.__tabs[name.id] = tab;

    if ($type(content) == 'element') {
      var content = Widget.div({'class': 'm-notebook-content', 'name': name.id}, content);
    } else {
      var content = Widget.div({'class': 'm-notebook-content', 'name': name.id});
    }

    if (!this.options.removePageContent) {
      this.components.page.appendChild(content);
    }

    content.hide();

    this.__contents[name.id] = content;
  },

  'updatePage': function(name, content) {
    if (typeof this.__tabs[name] != 'undefined') {
      var tab = this.__tabs[name];
      tab.store('filter', false);
      if (content) {
        tab.store('content', content);
      } else {
        tab.store('filter', true);
      }
      this.__updateContent(name);
    }
  },
  
  'cleanCache': function() {
    for (var name in this.__tabs) {
      if (typeof(this.__tabs[name].retrieve('content')) == 'object') {
        this.__tabs[name].store('firstView', null);
      } else {
        var content = $(this.__contents[name]);
        if (content.getElementsByClassName('filter-form')) {
          this.__tabs[name].store('firstView', null);
        }
      }
    }
  },
  
  'cleanPage': function(name, content) {
    if (typeof this.__tabs[name] != 'undefined') {
      var tab = this.__tabs[name];
      if (content) {
        tab.store('content', content)
      }
      this.__tabs[name].store('firstView', null);
    }
  },

  '__updateContent': function(name) {
    
    var content = this.__tabs[name].retrieve('content');

    if (typeof(content) == 'string' && content.match(/^http:\/\//i)) {
      this.__contents[name].setContent({'url': content});
    } else {

      var currel  = $(this.__contents[name]);

      var form    = currel.getElementsByClassName('filter-form');
      var pager   = currel.getElementsByClassName('pager');

      if (form.length && this.__tabs[name].retrieve('filter')) {
        // a filter form
        form = $(form[0]);
        if (pager.length) {
          pager     = $(pager[0]);
          var sel   = pager.getElementsByClassName('selected');
          if (sel) {
            sel     = $(sel[0]);
            var href  = sel.getElementsByTagName('a')[0];
            form.action = href.getAttribute('href');
          }
        }
        var form = new Form(form);
        form.__onSubmit();
        
        this.__tabs[name].store('firstView', true);
        return;
      } else {
        // an element
        this.__contents[name].setContent(content);
      }

    }
    if ($type(content) == 'element') {
      $(content).show();
    }
    this.__tabs[name].store('firstView', true);
  },

  'getObject': function() {
    return this.components.element;
  },

  'getContent': function(name) {
    return this.__contents[name];
  },

  'closePage': function(name) {
    if (this.__tabs[name]) {

      this.__contents[name].dump();
      this.__tabs[name].dump();

      delete this.__tabs[name];
      delete this.__contents[name];

      this.history.pop();

      // selecting last selected tab available
      while (this.history.length) {
        var p = this.history.pop();
        if (this.__tabs[p]) {
          return this.selectPage(p);
        }
      }

      // selecting any tab 
      for (var first in this.__tabs) {
        return this.selectPage(first);
      }
    }
  },

  'injectBefore': function(what, name, content, options) {
    var id = this.addPage(name, content, options); 
    this.__tabs[id].injectBefore(this.__tabs[what]);
  },

  'injectAfter': function(what, name, content, options) {
    var id = this.addPage(name, content, options); 
    this.__tabs[id].injectAfter(this.__tabs[what]);
  },

  'setPageTitle': function(name, title) {
    if (this.__tabs[name]) {
      this.__tabs[name].pageTitle.setContent(Widget.fromHTML(title));
    }
  },

  'map': function(params) {
    for (var i = 0; i < params.length; i++) {
      
      var param = params[i];
      var match = location.href.match(new RegExp('.*#('+param.id+')'));

      if (match) {

        param.id = match[1];
        if (typeof param.url != 'undefined') {
          var url = param['url'];
          url = url.replace('%match', '%1');
          for (var i = 2; i < match.length; i++) {
            url = url.replace('%'+(i-1), match[i]);
          }
          param.url = url;
          if (typeof param.rpc != 'undefined') {
            new Request({ 'url': param.url }).rpc();
          } else {
            this.addPage(param, param, param);
          }
        }
        if (typeof param.callback != 'undefined') {
          param.callback(match);
        }
      }
    }
  },

  'addPage': function (name, content, options) {
    if ($type(name) == 'string') {
      var temp = name;
      name = { 'id': name, 'title': name };
    }
    name.options = options || {};
    if (this.__tabs[name.id]) {
      log('A tab with the same name already exists!');
      this.updatePage(name.id, content);
    } else {
      this.__createPage(name, content);
      if (this.options.preloadContent) {
        this.__updateContent(name.id);
      }
      if (!this.__selected && this.options.autoSelect) {
        if (this.options.allowBookmark && location.href.indexOf('#') > -1) {
          var sel = location.href.replace(/^.*#/, '');
          if (sel == name.id) {
            this.selectPage(sel, false);
          } else {
            if (this.__initTimer) {
              window.clearTimeout(this.__initTimer);
            }
            this.__initTimer = window.setTimeout(this.__initialCheck.bind(this), 500);
          }
        } else {
          this.selectPage(name.id, false);
        }
      }
    }

    return name.id;
  }
});

