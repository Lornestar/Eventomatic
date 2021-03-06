/**
 * Editor
 * WYSIWYG Editor
 * ---
 * Written by José Carlos Nieto Jarquín <xiam@astrata.com.mx>
 * Copyright (c) 2007-2009 Astrata Software S.A. de C.V.
 *
 * Licensed under The MIT License
 * Redistributions of files must retain the above copyright notice.
 *
 * @author          José Carlos Nieto Jarquín
 * @copyright       Copyright (c) 2007-2009, Astrata Software S.A. de C.V.
 * @link            http://astrata.com.mx Astrata Open Source Projects
 * @version         $Revision: $
 * @modifiedby      $LastChangedBy: $
 * @lastmodified    $Date: $
 * @license         http://www.opensource.org/licenses/mit-license.php The MIT License
 *
 */

// http://www.mozilla.org/editor/midas-spec.html

var Editor = new Class({
  
  'Implements': [ Control ],

  'options': {
    'buttons':          null,
    'mode':             'complete',
    'blank':            $meteora['mediaDir']+'/blank.html?nocache='+$time(),
    'showSource':       true,
    'tabIndents':       true,
    'autoSave':         false,
    'autoSaveInterval': 30,
    'variant':          0
  },
  
  'buttons': {},

  '__iframeLoad': function() {
    this.__started = false;
    this.__start();
  },

  '__getIcon': function(icon) {
    return $meteora['mediaDir']+'/editor/'+icon+this.__imageExtension;
    return Widget.img({src: $meteora['mediaDir']+'/editor/'+icon+this.__imageExtension});
  },

  '__onDialogClose': function() {
    this.loadBookmark();
    Meteora.removeOverlay();
  },

  '__insertImageError': function(el) {
    var div = $(el).findParentWithClassName('m-editor-insertimage');
    var err = div.getElementsByClassName('m-error')[0];
    err.innerHTML = __('The image cannot be loaded.');
    err.show();
  },

  '__insertImageOriginalSize': function(el) {
    var div = $(el).findParentWithClassName('m-editor-insertimage');
    var img = $(div.getElementsByClassName('m-image-test')[0]);

    div.getElementsByClassName('m-input-width')[0].value = img.origWidth;
    div.getElementsByClassName('m-input-height')[0].value = img.origHeight;
  },

  '__insertImageLoad': function(img) {
    img = $(img);

    var maxW = 500;
    var maxH = 280;
    var factor = 1;

    if (factor*img.height > maxH) {
      factor = maxH/img.height;
    }
    if (factor*img.width > maxW) {
      factor = maxW/img.width;
    }

    var nw = Math.floor(img.width*factor);
    var nh = Math.floor(img.height*factor);

    img.origWidth  = img.width;
    img.origHeight = img.height;

    img.width = nw;
    if (Browser.Engine.trident) {
      img.height = nh;
    }

    img.setStyles({
      'position':   'static',
      'visibility': 'visible'
    });

    var div = $(img.findParentWithClassName('m-editor-insertimage'));

    div.getElementsByClassName('m-input-width')[0].value = nw;
    div.getElementsByClassName('m-input-height')[0].value = nh;
  },
  
  '__insertImageVerify': function(el) {

    var div = $(el).findParentWithClassName('m-editor-insertimage');

    var img = $(div.getElementsByClassName('m-image-test')[0]);

    var parent = img.parentNode;

    img.parentNode.removeChild(img);

    img = Widget.img({'class': 'm-image-test'});
    
    img.hide(); 

    parent.insertBefore(img, parent.firstChild);

    if (el.value) {

      if (el.value.substr(0,1) != '/' && !el.value.match(/^http:\/\//)) {
        el.value = 'http://'+el.value;
      }

      var err = $(div.getElementsByClassName('m-error')[0]);
      err.hide();

      img.onload = this.__insertImageLoad.bind(this, img);
      img.onerror = this.__insertImageError.bind(this, img);
      img.src = el.value;
      img.setStyles({
        'visibility': 'hidden',
        'position':   'absolute'
      });
      img.show();

    }

  },

  '__insertImageTest': function(el) {
    var div = $(el).findParentWithClassName('m-editor-insertimage');
    var url = div.getElementsByClassName('m-input-url')[0];
    if (url.value) {
      window.open(url.value);
    } else {
      url.focus();
    }
  },

  '__insertImageSubmit': function(el) {

    var div = $(el).findParentWithClassName('m-editor-insertimage');

    var dialog = div.retrieve('dialog');

    var url  = div.getElementsByClassName('m-input-url')[0];
    var width  = div.getElementsByClassName('m-input-width')[0];
    var height  = div.getElementsByClassName('m-input-height')[0];

    if (!url.value || !url.value.match(/^http:\/\//i)) {
      url.focus();
      return false;
    }

    this.loadBookmark();

    var img = this.findParent(/^img$/);

    if (img) {
      // updating
      img.src = url.value;
      img.width = width.value;
      img.height = height.value;
      this.selectNode(img);
    } else {
      log('new image');
      // new link
      this.execAction({
        'matchTag':     /^(img)$/,
        'wrapWithTag':  'img',
        'stripTags':    'img',
        'wrapWithAttr': {
          'src': url.value,
          'width': width.value,
          'height': height.value
        }
      });
    }

    dialog.close();

    this._updateSource();

    return false;
  },

  '__createLinkVerify': function(el) {
    if (!el.value.match(/^([a-z]+:\/\/|mailto:)/)) {
      if (el.value.match(/[a-z0-9\-\.+]+@[a-z0-9\-\.]+/)) {
        el.value = 'mailto:'+el.value;
      } else {
        el.value = 'http://'+el.value;
      }
    }
  },

  '__createLinkTest': function(el) {
    var div = $(el).findParentWithClassName('m-editor-createlink');
    var url = div.getElementsByClassName('m-input-url')[0];
    if (url.value) {
      window.open(url.value);
    } else {
      url.focus();
    }
  },

  '__insertImageClose': function(el) {
    var div = $(el).findParentWithClassName('m-editor-insertimage');
    div.retrieve('dialog').close();
  },

  '__createLinkClose': function(el) {
    var div = $(el).findParentWithClassName('m-editor-createlink');
    div.retrieve('dialog').close();
  },

  '__createLinkRemove': function(el) {
    var div = $(el).findParentWithClassName('m-editor-createlink');
    
    this.loadBookmark();

    var a = this.findParent(/^a$/);

    this.execAction({
      'matchTag': /^(a)$/,
      'matchForRemove': true
    });

    div.retrieve('dialog').close();

    this._updateSource();

    return false;
  },

  '__createLinkSubmit': function(el) {

    var div = $(el).findParentWithClassName('m-editor-createlink');

    var dialog = div.retrieve('dialog');
    
    var text = div.getElementsByClassName('m-input-text')[0];
    var url  = div.getElementsByClassName('m-input-url')[0];

    if (!url.value || url.value.match(/^([a-z]+:\/\/|mailto:)$/i)) {
      url.focus();
      return false;
    }

    if (url.value.substr(0, 1) != '/' && !url.value.match(/[a-z]+:/i)) {
      url.value = 'http://'+url.value;
    }

    if (!text.value) {
      text.value = url.value;
    }

    this.loadBookmark();

    var a = this.findParent(/^a$/);

    if (a) {
      // updating
      a.href = url.value;
      a.innerHTML = text.value;
      this.selectNode(a);
    } else {
      log('new link');
      // new link
      this.execAction({
        'matchTag':     /^(a)$/,
        'wrapWithTag':  'a',
        'stripTags':    'a',
        'insertValue':  text.value,
        'wrapWithAttr': {
          'href': url.value
        }
      });
    }

    dialog.close();

    this._updateSource();

    return false;
  },

  '__formSubmit': function() {
    if (this.options.autoSave) {
      this.__autoSave(true);
    }
  },

  '__windowFullScreenResize': function() {
    this.components.iframe.setStyles({
      'height': (Browser.clientHeight() - this.__iframeOffset.y) + 'px',
      'width':  (Browser.clientWidth() - this.__iframeOffset.x) + 'px'
    });
  },

  '__imageExtension': '.png',
  
  '__colors': [
    '#000000', '#333333', '#666666', '#888888', '#999999', '#aaaaaa',
    '#dddddd', '#000088', '#0000ff', '#008800', '#008888', '#0088ff',
    '#00ff00', '#00ff88', '#00ffff', '#ff0000', '#ff0088', '#ff00ff',
    '#ff8800', '#ff8888', '#ff88ff', '#ffffff'
  ],

  /*
    Returns true if the element or any of its
    parents are matched with the proper style
  */
  '__paramMatch': function(params, el) {

    // is the selection surrounded by a matching style?
    var parent  = el;
    var found   = false;

    while (parent && found == false) {
      var tagName = parent.nodeName.toLowerCase();

      // matching by style
      if (params.matchStyleTag) {
        if (tagName.match(params.matchStyleTag)) {
          if (parent.style[params.matchStyle[0]].match(params.matchStyle[1])) {
            found = true;
          }
        }
      }

      // matching by tag
      if (params.matchTag) {
        if (tagName.match(params.matchTag)) {
          found = true; 
        }
      }

      // upper levparent
      parent = parent.parentNode;
    }

    return found;
  },

  /*
    Removes the given element leaving its children instead.
  */
  '__removeParent': function(el) {

    el = $(el);

    while(el.childNodes.length) {
      // moving children elements
      el.parentNode.insertBefore(el.firstChild, el);
    }
    
    // deleting tag
    el.dump();
  },

  /*
    Removes matching style or tag in recursive mode.
  */
  '__paramRemoveStyle': function(params, el, clean) {

    el = $(el);

    if ($type(el) == 'element') {
    
      var tagName = el.nodeName.toLowerCase();

      if (params.matchTag) {
        if (tagName.match(params.matchTag)) {
          this.__removeParent(el);
        }
      }

      // matching style
      if (params.matchStyleTag) {
        if (tagName.match(params.matchStyleTag)) {
          if ($type(el.style[params.matchStyle[0]])) {
            // removing style from attribute list
            var newstyle = el.style[params.matchStyle[0]].replace(new RegExp(params.matchStyle[1]), '');
            if (params.replaceStyle || newstyle == 'normal') {
              newstyle = '';
            }
            el.setStyle(params.matchStyle[0], newstyle);
          }
        }
      }

      // recursion
      for (var i = el.childNodes.length - 1; i >= 0; i--) {
        this.__paramRemoveStyle(params, el.childNodes[i], true);
      }

      // cleaning nodes (avoiding empty style="" attributes)
      if (params.matchStyleTag) {
        if (tagName.match(params.matchStyleTag)) {
          if (clean) {
            if (!el.style.cssText) {
              this.__removeParent(el); 
            }
          }
        }
      }

    }
  },

  /*
    Removes matching styles from children
    and sets style for main element.
  */
  '__paramSetStyle': function(params, el) {
    el = $(el);
    
    this.__paramRemoveStyle(params, el);
    
    if (params.replaceStyle) {
      // replacing
      var inherit = '';
    } else {
      // appending
      var inherit = el.style[params.matchStyle[0]].replace(/normal/i, '');
    }

    el.setStyle(params.matchStyle[0], inherit + ' ' + params.matchStyle[1]);
  },

  /*
    Toggles editor's fullscreen feature
  */
  'toggleFullscreen': function() {
 
    var el = this.components.visualarea;
    
    if (this.__notebook) {
      el = this.__notebook.element;
    }

    $(el).toggleFullscreen();
  },

  /*
    Puts the selection around the given node
  */

  'selectNode': function(el) {

    var sel   = this.__getSelection();
    var range = this.__createRange(sel);
      
    if (el.childNodes.length == 0) {
      if (Browser.Engine.presto) {
        el.innerHTML = '&nbsp;';
      }
    }

    if (Browser.Engine.trident) {

      if (range.moveToElementText) {
        range.moveToElementText(el); 
      }

      range.select();
    } else {
      var range = this.iframeDocument().createRange();

      range.selectNode(el);
      
      sel.removeAllRanges();
      sel.addRange(range);
    }

    this.nodeChange(el);
  },

  /*
    Set's browser focus inside the editor
  */
  'focusVisual': function() {
    if (Browser.Engine.presto) {
      this.components.iframe.focus();
    } else {
      this.components.iframe.contentWindow.focus();
    }
  },

  'setBookmark': function() {
    if (Browser.Engine.trident) {
     
      this.focusVisual(); 
      
      var range = this.__createRange();

      if (range.getBookmark) {
        this.__bm = range.getBookmark();
      } else {
        this.__bm = range.item(0);
      }
    }
  },


  'loadBookmark': function() {
    if (Browser.Engine.trident) {
      if (this.__bm) {
        
        this.focusVisual(); 

        var range = this.__createRange();

        if ($type(this.__bm) != 'element') {
          range.moveToBookmark(this.__bm);
          range.select();
        } else {
          this.selectNode(this.__bm);
        }
      }
    }
  },

  /*
    Clears the editor
  */
  'reset': function() {
    this.element.value = '';
    this._updateVisual();
  },

  /*
    Updates textearea with the contents
    of the visual editor.
  */
  'updateSource': function() {
    if (this.$timers.updateSource) {
      window.clearTimeout(this.$timers.updateSource);
    }
    this.$timers.updateSource = window.setTimeout(function() { this._updateSource() }.bind(this), 500);
  },

  /*
    Updates visual editor with the contents
    of the textarea.
  */
  'updateVisual': function() {
    if (this.$timers.updateVisual) {
      window.clearTimeout(this.$timers.updateVisual);
    }
    this.$timers.updateVisual = window.setTimeout(function() { this._updateVisual() }.bind(this), 500);
  },

  /*
    Searches and returns the first parent of the
    element that matches the given callback
  */
  'findParent': function(callback) {

    if (Browser.Engine.trident) {
      this.focusVisual();
    }

    var range = this.__createRange();
    var parent = this.getRangeParent(range);

    while (parent) {
      if (Browser.Engine.trident) {
        if (typeof callback == 'function') {
          if (callback(parent)) {
            return parent;
          }
        } else if (parent.nodeName.toLowerCase().match(callback)) {
          return parent;
        }
      } else {
        if ($type(callback) == 'regexp') {
          if (parent.nodeName.toLowerCase().match(callback)) {
            return parent;
          }
        } else if (callback(parent)) {
          return parent;
        }
      }
      parent = parent.parentNode;
    }
    return null;
  },

  'removeAttribute': function(node, style) {
    node = $(node);
    if ($type(node) == 'element') {
      node.setStyle(style, '');
      for (var i = 0; i < node.childNodes.length; i++) {
        this.removeAttribute(node.childNodes[i], style);
      } 
    }
  },

  'getRangeParent': function(range) {

    if (Browser.Engine.trident) {
      
      if (range.htmlText) {
        
        var html = range.htmlText;

        this.execCommand('delete');
        this.execCommand('inserthtml', '<span id="__temp_node__">'+html+'</span>');
        var node = this.iframeDocument().getElementById('__temp_node__');
        node.removeAttribute('id');

        if (node.childNodes.length == 1) {
          var el = node.childNodes[0];
          node.parentNode.insertBefore(el, node);
          node.parentNode.removeChild(node);
          node = el;
        }

        try {
          range.moveToElementText(node);
        } catch(e) {};

        range.select();
        
        return $(node);
      } else if (range.item) {
        return $(range.item(0));
      }

      return range.parentElement();

    } else {

      // removing selected contents

      var contents = range.extractContents();

      this.trimTextNodes(contents);

      if (contents.childNodes.length == 1) {
        // the user selected only one node
        var el = contents.childNodes[0];
      } else {
        // a wrapper is needed
        var el = this.iframeDocument().createElement('span');
        el.appendChild(contents);
      }
      // reselecting
      range.insertNode(el);
      range.selectNode(el);
    }

    this.selectNode(el);

    return $(el);
  },

  'trimTextNodes': function(contents) {
    // garbage hack to delete childNodes
    var garbage = document.createDocumentFragment();

    try {
      // emptying nodes from the end
      var node = contents.childNodes[contents.childNodes.length - 1];
      while (node && node.nodeType == 3 && node.nodeValue == '') {
        garbage.appendChild(node);
        node = node.previousSibling;
      }

      // emptying nodes from the start
      var node = contents.childNodes[0];
      while (node && node.nodeType == 3 && node.nodeValue == '') {
        garbage.appendChild(node);
        node = node.nextSibling;
      }
    } catch(e) {
    
    }
  },

  'execAction': function(params) {

    //this.focusVisual(); 

    // This is the tricky part so it needs to be documented
    var formatted = false;
    var tagName   = false;

    var sel       = this.__getSelection();
    var range     = this.__createRange(sel);
    
    // getting selected node
    var el        = this.getRangeParent(range);

    var parent = el;
    if ($type(el) == 'element') {
      tagName = parent.nodeName.toLowerCase();
    }

    // in case of having selected a textnode
    if (!params.parentWrapping || tagName == 'img') {
      var wrapper = false;
      if ($type(parent) == 'textnode') {
        wrapper = 'span';
      }
      if (tagName == 'img') {
        wrapper = 'p'; 
        var p = parent;
        while (p && wrapper) {
          if (p.nodeName.toLowerCase() == 'p') {
            parent = p;
            wrapper = '';
          }
          p = p.parentNode;
        }
      }
      if (wrapper) {
        var wrapper = this.iframeDocument().createElement(wrapper);
        parent.parentNode.insertBefore(wrapper, parent);
        wrapper.appendChild(parent);
        parent = wrapper;
        
        this.selectNode(parent);
        range = this.__createRange();
      }
    }
    
    // Has the user made any selection?
    var collapsed = !(parent.innerHTML);
    
    if (collapsed) {
      // He did not
      log('trying to expand');
      while (parent) {
        var name = parent.nodeName.toLowerCase();
        if (params.matchTag) {
          if (name.match(params.matchTag)) {
            log('expanded by tag');
            this.selectNode(parent);
            break;
          }
        }
        if (params.matchStyleTag) {
          if (name.match(params.matchStyleTag)) {
            if (parent.style[params.matchStyle[0]]) {
              if (parent.style[params.matchStyle[0]].match(params.matchStyle[1])) {
                log('expanded by style');
                this.selectNode(parent);
                break;
              }
            }
            if (params.replaceStyle) {
              log('expanded for replacement');
              this.selectNode(parent);
              break;
            }
          }
        }
        parent = parent.parentNode;
      }
      if (!parent) {
        log('not expanded');
      }
    }

    log('while');

    while (parent && formatted == false) {
    
      var name = parent.nodeName.toLowerCase();

      if (!name.match(/^(html|body|head)$/)) {
        if ((params.matchTag && name.match(params.matchTag)) || (params.replaceTag && name.match(params.replaceTag))) {
          log('matchTag');
          if (Browser.Engine.trident) {
            range.moveToElementText(parent);
            var html = new String(parent.innerHTML);
            range.select();
            var bm = range.getBookmark();
            this.execCommand('delete');
            range.collapse();
            range.select();
            range.pasteHTML(html);
            range.moveToBookmark(bm);
            range.select();
          } else {
            var sel = this.__getSelection();
            sel.removeAllRanges();
            var range = this.__createRange();
            var start = false;
            while (parent.firstChild) {
              var node = parent.firstChild;
              parent.parentNode.insertBefore(node, parent);
              if (!start) {
                range.setStart(node, 0);
                start = true;
              }
            }
            range.setEndBefore(node.nextSibling);
            sel.addRange(range);
            $(parent).dump();
          }
          if (params.replaceTag && name.match(params.replaceTag)) {
            break;
          }
          formatted = true;
        }
        if (params.matchStyleTag && name.match(params.matchStyleTag)) {
          // MATCH STYLE FOR CSS FORMATTING
          log('matchStyle');
          if ($type(parent) == 'element') {
            var found = this.__paramMatch(params, parent);
            if (found) {
              // Style matches, removing style
              log('style match, removing....');
              // removing original style
              this.__paramRemoveStyle(params, parent);
              //if (Browser.Engine.trident) range.moveToElementText(parent);
              // forcing style to be normal
              parent.setStyle(params.matchStyle[0], 'normal');
              //if (Browser.Engine.trident) range.select();
            } else {
              log('style does not match');
              // setting style
              this.__paramSetStyle(params, parent);
            }
            this.selectNode(parent);
            formatted = true;
          } else {
            log('Unhandled node type');
            log($type(parent));
          }
        }
        parent = parent.parentNode;
      } else {
        parent = false;
      }
    }


    if (formatted == false && !params.matchForRemove) {

      log('non formatted');

      // Patterns were not found, so the text needs to
      // be formatted.
      var selected = this.getSelectedHTML(sel); 

      var node = this.iframeDocument().createElement(params.wrapWithTag ? params.wrapWithTag : 'span');
      node.setAttribute('id', '__temp_node__');
      if (params.matchStyle && (params.wrapWithStyle || !params.wrapWithTag)) {
        node.style[params.matchStyle[0]] = params.matchStyle[1];
      }
      if (params.wrapWithAttr) {
        for (var i in params.wrapWithAttr) {
          node.setAttribute(i, params.wrapWithAttr[i]);
        }
      }

      if (params.stripTags) {
        selected = selected.replace(new RegExp('<\\/?('+params.stripTags+')(\\s[^>]*)?>', 'ig'));
      }

      try {
        if (params.insertValue) {
          node.innerHTML = params.insertValue;
        } else {
          node.innerHTML = selected ? selected : '&nbsp;';
        }
      } catch(e) {
        log('possible bug? do you need to strip some tags?');
        log(node.nodeName);
        log(selected);
      }

      if (Browser.Engine.trident) {
        this.execCommand('inserthtml', node.outerHTML);
        var node = this.iframeDocument().getElementById('__temp_node__');
        node.removeAttribute('id');

        range.moveToElementText(node);
        range.select();

        if (!selected && params.collapseIfNull) {
          range.collapse(true);
          range.select();
        }
      } else {
        range.deleteContents();
        range.insertNode(node);
        var sel = this.__getSelection();
        sel.removeAllRanges();
        range.selectNodeContents(node);
        sel.addRange(range);
        
        node.removeAttribute('id');
        if (params.collapseIfNull) {
          range.collapse(true);
        }
      }
    }


  },

  '__createButtons': function() {
    
    this.__buttons = $extend({
      bold: {
        tooltip: 'Bold',
        onAction: function() {

          this.execAction({
            'matchTag': /^(b|strong)$/,
            'matchStyleTag': /^(span)$/,
            'matchStyle': [ 'fontWeight', 'bold' ],
            'wrapWithTag': 'b'
          });

        },
        nodeStyle: 'fontWeight',
        nodeStyleValue: 'bold',
        changeElementPattern: /^(b|strong)$/
      },
      italic: {
        tooltip: 'Italics',
        onAction: function() {
          this.execAction({
            'matchTag': /^(i)$/,
            'matchStyleTag': /^(span)$/,
            'matchStyle': [ 'fontStyle', 'italic' ],
            'wrapWithTag': 'i'
          });
        },
        nodeStyle: 'fontStyle',
        nodeStyleValue: 'italic',
        changeElementPattern: /^i$/
      },
      underline: {
        tooltip: 'Underline',
        onAction: function() {
          this.execAction({
            'matchTag': /^(u)$/,
            'matchStyleTag': /^(span)$/,
            'matchStyle': [ 'textDecoration', 'underline' ]
          });
        },
        nodeStyle: 'textDecoration',
        nodeStyleValue: 'underline',
        changeElementPattern: /^u$/
      },
      strikethrough: {
        tooltip: 'Strikethrough',
        onAction: function() {
          this.execAction({
            'matchTag': /^(s)$/,
            'matchStyleTag': /^(span)$/,
            'matchStyle': [ 'textDecoration', 'line-through' ]
          });
        },
        nodeStyle: 'textDecoration',
        nodeStyleValue: 'line-through',
        changeElementPattern: /^s$/
      },
      'createlink': {
        tooltip: 'Create a link',
        onAction: function() {

          var a = null;

          if (this.isPressed('createlink')) {
            var a = this.findParent(/^a$/);
            if (a) {
              this.selectNode(a);
            }
          } 

          var url   = a ? a.href      : '';
          var text  = a ? a.innerHTML : this.getSelectedHTML();

          this.setBookmark();

          var form = Widget.div(
            {'class': 'm-editor-createlink'},
            Widget.fromHTML(
               '<form method="post" onsubmit="return $(this).findParentWithClassName(\'m-editor-createlink\').retrieve(\'control\').__createLinkSubmit(this)">'
              +'  <div class="m-editor-set">'
              +'    '+__('Text to display')+':<br />'
              +'    <input size="60" class="m-form-input m-input-text" value="'+text+'" /><br />'
              +'  </div>'
              +'  <div class="m-editor-set">'
              +'    '+__('Web address or e-mail')+'<br />'
              +'    <input size="60" class="m-form-input m-input-url" value="'+url+'" onblur="$(this).findParentWithClassName(\'m-editor-createlink\').retrieve(\'control\').__createLinkVerify(this)" />'
              +'    <a href="javascript:void(0)" onclick="$(this).findParentWithClassName(\'m-editor-createlink\').retrieve(\'control\').__createLinkTest(this)">'+__('Test this URL')+'</a>'
              +'  </div>'
              +'  <div class="m-buttons">'
              +'    <button type="button" onclick="$(this).findParentWithClassName(\'m-editor-createlink\').retrieve(\'control\').__createLinkClose(this)" class="m-form-button">'+__('Cancel')+'</button>'
              +'    <button type="button" onclick="$(this).findParentWithClassName(\'m-editor-createlink\').retrieve(\'control\').__createLinkRemove(this)" class="m-form-button">'+__('Remove')+'</button>'
              +'    <button type="submit" class="m-form-button">'+__('OK')+'</button>'
              +'  </div>'
              +'</form>'
            )
          );

          form.store('control', this);
          
          var dialog = new Dialog(
            form,
            {
              'title': __('Edit link'),
              'onClose': this.__onDialogClose.bind(this),
              'width':  500,
              'height': 190
            }
          );

          dialog.show();
          dialog.center();
          
          form.store('dialog', dialog);

          Meteora.overlay();

          dialog.focusInput(form.getElementsByClassName('m-input-url')[0]);

        },
        changeElementPattern: /^a$/
      },

      'insertimage': {

        tooltip: 'Insert image',
        onAction: function() {
          
          var img = null;

          if (this.isPressed('insertimage')) {
            var img = this.findParent(/^img$/);
            if (img) {
              this.selectNode(img);
            }
          } 

          if (img) {
            var url     = img.src;
            var width   = img.width;
            var height  = img.height;
          } else {
            var url     = '';
            var width   = '';
            var height  = '';
          }

          this.setBookmark();

          var form = Widget.div(
            {'class': 'm-editor-insertimage'},
            Widget.fromHTML(
               '<form method="post" onsubmit="return $(this).findParentWithClassName(\'m-editor-insertimage\').retrieve(\'control\').__insertImageSubmit(this)">'
              +'  <div class="m-editor-set">'
              +'    '+__('Image URL')+'<br />'
              +'    <input size="60" class="m-form-input m-input-url" value="'+url+'" onchange="$(this).findParentWithClassName(\'m-editor-insertimage\').retrieve(\'control\').__insertImageVerify(this)" /><br />'
              +'    <a href="javascript:void(0)" onclick="$(this).findParentWithClassName(\'m-editor-insertimage\').retrieve(\'control\').__insertImageTest(this)">'+__('Test this URL')+'</a><br />'
              +'    '+__('Width')+': <input size="3" value="'+width+'" class="m-form-input m-input-width" />'
              +'    '+__('Height')+': <input size="3" value="'+height+'" class="m-form-input m-input-height" />'
              +'    <button type="button" onclick="$(this).findParentWithClassName(\'m-editor-insertimage\').retrieve(\'control\').__insertImageOriginalSize(this)" class="m-form-button">'+__('Original size')+'</button>'
              +'  </div>'
              +'  <div class="m-editor-set" style="text-align: center">'
              +'    <img class="m-image-test" />'
              +'    <div class="m-error"></div>'
              +'  </div>'
              +'  <div class="m-buttons">'
              +'    <button type="button" onclick="$(this).findParentWithClassName(\'m-editor-insertimage\').retrieve(\'control\').__insertImageClose(this)" class="m-form-button">'+__('Cancel')+'</button>'
              +'    <button type="submit" class="m-form-button">'+__('OK')+'</button>'
              +'  </div>'
              +'</form>'
            )
          );
          form.store('control', this)

          var dialog = new Dialog(
            form,
            {
              'title':    __('Insert image'),
              'onClose':  this.__onDialogClose.bind(this),
              'width':    550,
              'height':   450
            }
          );

          dialog.show();
          dialog.center();
          
          form.store('dialog', dialog);

          Meteora.overlay();

          dialog.focusInput(form.getElementsByClassName('m-input-url')[0]);
          
          if (url) {
            var img     = form.getElementsByClassName('m-image-test')[0];
            img.src     = url;
            img.width   = width;
            img.height  = height;
            this.__insertImageLoad(img);
          }
        },
        changeElementPattern: /^img$/
      },
      justifyleft: {
        tooltip: 'Align left',
        onAction: function() {
          this.unselectButton('justifycenter');
          this.unselectButton('justifyfull');
          this.unselectButton('justifyright');
          this.execAction({
            'matchStyleTag': /^(div|p|h\d+)$/,
            'matchStyle': ['textAlign', 'left'],
            'collapseIfNull': true,
            'wrapWithTag': 'p',
            'replaceStyle': true,
            'stripTags': 'p',
            'parentWrapping': true,
            'wrapWithStyle': true
          });
        },
        nodeStyle: 'textAlign',
        nodeStyleValue: 'left'
      },
      justifycenter: {
        tooltip: 'Align center',
        onAction: function() {
          this.unselectButton('justifyleft');
          this.unselectButton('justifyfull');
          this.unselectButton('justifyright');
          
          this.execAction({
            'matchStyleTag': /^(div|p|h\d+)$/,
            'matchStyle': ['textAlign', 'center'],
            'collapseIfNull': true,
            'wrapWithTag': 'p',
            'replaceStyle': true,
            'stripTags': 'p',
            'parentWrapping': true,
            'wrapWithStyle': true
          });
        },
        nodeStyle: 'textAlign',
        nodeStyleValue: 'center'
      },
      justifyfull: {        
        tooltip: 'Justify',
        onAction: function() {
          this.unselectButton('justifycenter');
          this.unselectButton('justifyleft');
          this.unselectButton('justifyright');
          this.execAction({
            'matchStyleTag': /^(div|p|h\d+)$/,
            'matchStyle': ['textAlign', 'justify'],
            'collapseIfNull': true,
            'wrapWithTag': 'p',
            'replaceStyle': true,
            'stripTags': 'p',
            'parentWrapping': true,
            'wrapWithStyle': true
          });
        },
        nodeStyle: 'textAlign',
        nodeStyleValue: 'justify'
      },
      justifyright: {
        tooltip: 'Align right',
        onAction: function() {
          this.unselectButton('justifycenter');
          this.unselectButton('justifyfull');
          this.unselectButton('justifyleft');

          this.execAction({
            'matchStyleTag': /^(div|p|h\d+)$/,
            'matchStyle': ['textAlign', 'right'],
            'collapseIfNull': true,
            'wrapWithTag': 'p',
            'replaceStyle': true,
            'stripTags': 'p',
            'parentWrapping': true,
            'wrapWithStyle': true
          });

        },
        nodeStyle: 'textAlign',
        nodeStyleValue: 'right'
      },
      subscript: {
        tooltip: 'Subscript',
        onAction: function() {
          this.unselectButton('superscript');
          this.execAction({
            'matchTag': /^(sub)$/,
            'replaceTag': /^(sup)$/,
            'wrapWithTag': 'sub'
          });
        },
        changeElementPattern: /^sub$/
      },
      superscript: {
        tooltip: 'Superscript',
        onAction: function() {
          this.unselectButton('subscript');
          this.execAction({
            'matchTag': /^(sup)$/,
            'replaceTag': /^(sub)$/,
            'wrapWithTag': 'sup'
          });
        },
        changeElementPattern: /^sup$/
      },
      insertorderedlist: {
        tooltip: 'Ordered List',
        onAction: function() {
          this.execCommand('insertorderedlist');
        }
      },
      insertunorderedlist: {
        tooltip: 'Unordered List',
        onAction: function() {
          this.execCommand('insertunorderedlist');
        }
      },
      indent: {
        tooltip: 'Indent',
        onAction: function() {
          this.execCommand('indent', null);
        }
      },
      outdent: {
        tooltip: 'Outdent',
        onAction: function() {
          this.execCommand('outdent', null);
        }
      },
      inserthorizontalrule: {
        tooltip: 'Insert Horizontal Rule',
        onAction: function() {
          this.execCommand('inserthorizontalrule');
        }
      },

      removeformat: {
        tooltip: 'Remove format',
        onAction: function() {
          this.execCommand('removeformat');
        }
      },
      
      fullscreen: {
        tooltip: 'Fullscreen editor',
        onAction: function() {

          if (this.isPressed('fullscreen')) {

            this.components.iframe.setStyle('height', this.components.iframe.origHeight+'px');
            this.unselectButton('fullscreen');

            this.toggleFullscreen();

            this.components.iframe.setStyles({
              'width':   this.__iframeDimensions.width+'px',
              'height':  this.__iframeDimensions.height+'px'
            });
            
            window.removeEvent(
              'resize',
              this.__windowFullScreenResizeListener
            );

            this.__isFullScreen = false;

          } else {

            this.selectButton('fullscreen');
            
            this.__iframeDimensions = $(this.components.iframe).getDimensions();

            this.__iframeOffset     = {
              'x': this.components.container.offsetWidth - this.components.iframe.offsetWidth,
              'y': this.components.container.offsetHeight - this.components.iframe.offsetHeight
            }

            this.toggleFullscreen();

            this.__windowFullScreenResizeListener = this.__windowFullScreenResize.bindWithEvent(this);

            window.addEvent(
              'resize',
              this.__windowFullScreenResizeListener
            );
              
            this.__windowFullScreenResizeListener();

            this.__isFullScreen = true;

          }
        }.bindWithEvent(this)
      },

      // HEADING
      heading: {
        tooltip: 'Paragraph type',
        element: Widget.span({'class': 'm-select'}, 
          [
            Widget.img({'src': this.__getIcon('heading')}),
            Widget.select({
              'class': 'm-form-input',
              'onchange': function(e) {
                this.__buttons['heading'].onAction.bind(this)(e);
                this.updateSource();
              }.bind(this) 
            }, 
            [
              Widget.option({value: ''}, 'Text format'),
              Widget.option({value: 'h2'}, 'Main title'),
              Widget.option({value: 'h3'}, 'Subtitle 1'),
              Widget.option({value: 'h4'}, 'Subtitle 2'),
              Widget.option({value: 'code'}, 'Code'),
              Widget.option({value: 'blockquote'}, 'Quote')
            ])
          ]),
        onAction: function(e) {
          e = new Event(e);
          if (e.target.value) {
            this.execAction({
              'matchTag': new RegExp('/^('+e.target.value+')$/', 'i'),
              'replaceTag': /^(h\d+|code|pre)$/,
              'wrapWithTag': e.target.value
            });
          } else {
            this.execAction({
              'matchTag': /^(h\d+|code|pre)$/,
              'matchForRemove': true
            });
          }
        },
        onNodeChange: function(node) {
          var select = this.element.getElementsByTagName('select')[0];
          while (node && !select.value) {
            if ($type(node) == 'element') {
              var name = node.nodeName.toLowerCase();
              if (name.match(/^(h\d+|code|pre)$/)) {
                select.value = name;
              }
            }
            node = node.parentNode;
          }
        },
        listenEvent: 'auto',
        changeElementPattern: /^h\d$/
      },


      // FONT FACE
      fontname: {
        tooltip: 'Font face',
        element: Widget.span({'class': 'm-select'}, [
          Widget.img({'src': this.__getIcon('fontface')}),
          Widget.select({
            'class': 'm-form-input',
            'onchange': function(e) {
              this.__buttons['fontname'].onAction.bind(this)(e);
              this.updateSource();
            }.bind(this)
          }, Widget.option({'value': ''}, 'Font name'))
        ]),
        onStart: function(parent, el) {
          var fonts = {
            'Verdana':          'Verdana',
            'Arial':            'Arial',
            'Trebuchet MS':     'Trebuchet MS',
            'Times New Roman':  'Times New Roman',
            'monotype':         'Monotype',
            'sans-serif':       'Sans Serif'
          };
          var select = el.getElementsByTagName('select')[0];
          for (var font in fonts) {
            select.appendChild(Widget.option({'value': font, 'style': {'fontFamily': font}}, fonts[font]));
          }
        },
        onNodeChange: function(node) {
          var select = this.element.getElementsByTagName('select')[0];
          while (node && !select.value) {
            if ($type(node) == 'element') {
              select.value = node.style.fontFamily;
            }
            node = node.parentNode;
          }
        },
        onAction: function(e) {
          var e = new Event(e);
          this.execAction({
            'matchTag': /^(font)$/,
            'matchStyleTag': /^(span)$/,
            'wrapWithTag': 'span',
            'wrapWithStyle': true,
            'replaceStyle': true,
            'matchStyle': [ 'fontFamily', e.target.value ]
          });
        },
        nodeStyle: 'fontFamily',
        nodeStyleValue: /.*/,
        listenEvent: 'auto'
      },
      
      // FONT SIZE
      fontsize: {
        tooltip: 'Font size',
        element: Widget.span({'class': 'm-select'}, [
          Widget.img({'src': this.__getIcon('fontsize')}),
          Widget.select({
            'class': 'm-form-input',
            'onchange': function(e) {
              this.__buttons['fontsize'].onAction.bind(this)(e);
              this.updateSource();
            }.bind(this)
          }, Widget.option({'value': ''}, 'Font size'))
        ]),
        onStart: function(parent, el) {
          var sizes = {
            'xx-small': 'Micro',
            'x-small': 'Extra small',
            'small': 'Small',
            'medium': 'Medium',
            'large': 'Large',
            'x-large': 'Extra large',
            'xx-large': 'Huge'
          };
          var select = el.getElementsByTagName('select')[0];
          for (var i in sizes) {
            select.appendChild(Widget.option({'value': i}, sizes[i]));
          }
        },
        onNodeChange: function(node) {
          var select = this.element.getElementsByTagName('select')[0];
          while (node && !select.value) {
            if ($type(node) == 'element') {
              select.value = node.style.fontSize;
            }
            node = node.parentNode;
          }
        },
        onAction: function(e) {
          var e = new Event(e);
          this.execAction({
            'matchTag': /^(font)$/,
            'matchStyleTag': /^(span)$/,
            'wrapWithTag': 'span',
            'wrapWithStyle': true,
            'replaceStyle': true,
            'matchStyle': [ 'fontSize', e.target.value ]
          });
        },
        nodeStyle: 'fontSize',
        nodeStyleValue: /.*/,
        listenEvent: 'auto'
      },


      // FORECOLOR
      forecolor: {
        tooltip: 'Font color',
        element: Widget.span({'class': 'm-select'}, [
          Widget.img({'src': this.__getIcon('forecolor') }),
          Widget.select({
            'class': 'm-form-input',
            'onchange': function(e) {
              this.__buttons['forecolor'].onAction.bind(this)(e);
              this.updateSource();
            }.bind(this)
          }, Widget.option({'value': ''}, 'Font color'))
        ]),
        onStart: function(parent, el) {
          for (var i = 0; i < el.childNodes.length; i++) {
            var node = el.childNodes[i];
            if ($type(node) == 'element') {
              if (node.nodeName.toLowerCase() == 'select') {
                var colors = parent.__colors;
                for (var i = 0; i < colors.length; i++) {
                  node.appendChild(Widget.option({'value': colors[i], 'style': { 'backgroundColor': colors[i] } }));
                }
                return;
              }  
            }
          }
        },
        onNodeChange: function(node) {
          var select  = this.element.getElementsByTagName('select')[0];
          var color   = '';

          while (node && !select.value) {
            if ($type(node) == 'element' && node.style) {
              if (node.style.color) {
                color = node.style.color;
                if (color.match(/^rgb/)) {
                  color = color.replace(/[^0-9,]/g, '').split(',');
                  color = new String(color).rgbToHex();
                }
              }
            }
            node = node.parentNode;
          }

          select.setStyle('backgroundColor', color);
        },
        onAction: function(e) {
          var e = new Event(e);
          var color = e.target.value;
          e.target.setStyle('backgroundColor', color);
          if (color.length == '4') {
            color = '#'+color.charAt(1)+color.charAt(1)+color.charAt(2)+color.charAt(2)+color.charAt(3)+color.charAt(3);
          }
          this.execAction({
            'matchTag': /^(font)$/,
            'matchStyleTag': /^(span)$/,
            'wrapWithTag': 'span',
            'wrapWithStyle': true,
            'replaceStyle': true,
            'matchStyle': [ 'color', color ]
          });
        },
        nodeStyle: 'color',
        nodeStyleValue: /.*/,
        listenEvent: 'auto'
      },

      // BACKGROUND COLOR
      hilitecolor: {
        tooltip: 'Background color',
        element: Widget.span({'class': 'm-select'}, [
          Widget.img({'src': this.__getIcon('hilitecolor') }),
          Widget.select({
            'class': 'm-form-input',
            'onchange': function(e) {
              this.__buttons['hilitecolor'].onAction.bind(this)(e);
              this.updateSource();
            }.bind(this)
          }, Widget.option({value: ''}, 'Background'))
        ]),
        onStart: function(parent, el) {
          
          for (var i = 0; i < el.childNodes.length; i++) {
            var node = el.childNodes[i];
            if ($type(node) == 'element') {
              if (node.nodeName.toLowerCase() == 'select') {
                var colors = parent.__colors;
                for (var i = 0; i < colors.length; i++) {
                  node.appendChild(Widget.option({'value': colors[i], 'style': {'backgroundColor': colors[i]}}));
                }
                return;
              }
            }
          }
        },
        onAction: function(e) {
          var color = e.target.value;
          e.target.setStyle('backgroundColor', color);
          if (color.length == '4') {
            color = '#'+color.charAt(1)+color.charAt(1)+color.charAt(2)+color.charAt(2)+color.charAt(3)+color.charAt(3);
          }
          this.execAction({
            'matchTag': /^(font)$/,
            'matchStyleTag': /^(span)$/,
            'wrapWithTag': 'span',
            'wrapWithStyle': true,
            'replaceStyle': true,
            'matchStyle': [ 'backgroundColor', color ]
          });
        },
        listenEvent: 'auto',
        nodeStyle: 'backgroundColor',
        nodeStyleValue: /.*/,
        onNodeChange: function(node) {

          var select  = this.element.getElementsByTagName('select')[0];
          var color   = '';
          while (node && !select.value) {
            if ($type(node) == 'element' && node.style) {
              if (node.style.backgroundColor) {
                color = node.style.backgroundColor;
                if (color.match(/^rgb/)) {
                  color = color.replace(/[^0-9,]/g, '').split(',');
                  color = new String(color).rgbToHex();
                }
              }
            }
            node = node.parentNode;
          }

          select.setStyle('backgroundColor', color);

        }
      }
    }, this.buttons);

    if (!this.options.buttons) {
      this.options.buttons = [];
      for (var i in this.__buttons) {
        this.options.buttons.push(i);
      }
    }

    for (var i = 0; i < this.options.buttons.length; i++) {
      
      var name = this.options.buttons[i];

      if ($type(this.__buttons[name])) {

        var button = this.__buttons[name];

        button.name = name;
        button.parent = this;
        
        // creating an element if the button doesn't has one
        if (!button.element) {
          button.element = Widget.img({ 'class': 'm-button', 'src': this.__getIcon(name) });
        }

        if (button.onStart) {
          button.onStart(this, button.element);
        }
        
        if (!button.listenEvent) {
          button.listenEvent = 'onclick';
        }

        button.element[button.listenEvent] = function () {

          // calling the button's action
          this.onAction.bind(this.parent).call();

          this.parent.updateSource();
          
        }.bind(button);
        
        if (button.tooltip) {
          button.element.title = button.tooltip;
        }
        if (button.element.nodeName.toLowerCase() == 'img') {
          this.components.buttonsUpper.appendChild(button.element);
        } else {
          this.components.buttonsLower.appendChild(button.element);
        }

      } else {
        log('The button '+name+' does not exist.');
      }
    }
  },
  
  '__execCommand': function(command, value) {
    try {
      this.iframeDocument().execCommand(command, false, value);      
    } catch(e) {
      log('Unsupported command: '+command);
    }
  },

  'execCommand': function(command, value) {
    switch (command) {
      case 'inserthtml':
        if (Browser.Engine.trident) {
          var sel = this.__getSelection();
          var range = this.__createRange(sel);
          range.pasteHTML(value);
        } else {
          var range = this.__createRange();
          range.deleteContents();
          this.__execCommand('inserthtml', value);
        }
      break;
      default:
        this.__execCommand(command, value);
      break;
    }
    if (this.__started) {
      this.updateSource();
    }
  },
  
  'resizeTo': function(width, height) {
    this.components.container.setStyle('width', width+'px');
    this.components.iframe.setStyle('height', height+'px');
  },

  '__buildComponents': function() {
      
    this.components = {
      container:    Widget.div({'class': 'm-editor'}),
      buttonsUpper: Widget.div(),
      buttonsLower: Widget.div(),
      buttons:      Widget.div({'class': 'm-editor-buttons'}),
      visualarea:   Widget.div({'class': 'm-visual-area'}),
      iframe:       Widget.iframe({
                      'src':          this.options.blank,
                      'border':       '0',
                      'frameborder':  '0',
                      'style':        {  'display': 'block' }
                    })
    }

    this.components.buttons.appendChildren([
      this.components.buttonsUpper,
      this.components.buttonsLower
    ]);

    this.element.setAttribute('wrap', 'off');

    if (this.element.offsetWidth && this.element.offsetHeight) {
      this.resizeTo(this.element.offsetWidth, this.element.offsetHeight);
    } else {
      if (this.element.style.width && this.element.style.height) {
        if (this.element.style.width.indexOf('%') == -1 && this.element.style.height.indexOf('%') == -1) {
          this.resizeTo(parseInt(this.element.style.width), parseInt(this.element.style.height));
        }
      } else {
        this.components.container.setStyle('width', '100%');
        if (Browser.Engine.trident4) {
          // set width and height explicitly 
          this.components.iframe.setStyle('width', '350px');
        }
      }
    }
    
    this.__createButtons();

    if (!this.options.buttons.length) {
      this.components.buttons.hide();
    }

    this.element.parentNode.insertBefore(this.components.container, this.element);

    switch (this.options.variant) {
      case 1:
        this.components.visualarea.appendChildren([
          this.components.iframe,
          this.components.buttons
        ]);
      break;
      default:
        this.components.visualarea.appendChildren([
          this.components.buttons,
          this.components.iframe
        ]);
      break;
    }

    if (this.options.showSource) {
      var nb = new Notebook(
        this.components.container,
        {
          'allowBookmark': false
        }
      );
      nb.addPage({'id': 'visual', 'title': 'Visual mode'}, this.components.visualarea);
      nb.addPage({'id': 'source', 'title': 'Source mode'}, this.element);
      this.__notebook = nb;
    } else {
      this.components.container.appendChild(this.components.visualarea);
      this.element.parentNode.insertBefore(this.components.container, this.element);
      this.element.hide();
    }

    var form = this.element;
    while (form) {
      if (form.nodeName.toLowerCase() == 'form') {
        this.addListener(
          form,
          'reset',
          this.reset
        );
        this.addListener(
          form,
          'submit',
          this.__formSubmit
        );
        break;
      }
      form = form.parentNode;
    }
  },

  '__bindEvents': function() {
    this.addListener(
      this.components.iframe,
      'load',
      this.__iframeLoad
    );
  },
  
  '__createRange': function(sel) {
    if (!sel) {
      sel = this.__getSelection();
    }
    if (Browser.Engine.trident) {
      return sel.createRange();
    } else {
      try {
        return sel.getRangeAt(0);
      } catch(e) {
        return this.iframeDocument().createRange();
      }
    }
  },
  
  '__domToHTML': function(content) {
    
    var buff = '';
    
    var isTag = (content.nodeName.substr(0, 1) != '#');
    
    if (!isTag) {
      if (content.nodeName == '#text') {
        buff += content.nodeValue;
      }
    }
    
    if (isTag) {
      buff += '<'+content.nodeName.toLowerCase();
      
      if (content.attributes.length) {
        var attr = new Array();
        for (var i = 0; i < content.attributes.length; i++) {
          var name = content.attributes[i].nodeName;
          var value = content.attributes[i].nodeValue;
          value = value.replace(/-moz-.*;/, '');
          
          if (name.substr(0, 1) != '-')
            attr.push(name+'="'+value+'"');
        }
        buff += ' '+attr.join(' ');
      }
      
      if (this.__isSingleTag(content.nodeName))
        buff += ' /';
      
      buff += '>';
    }
    
    for (var i = 0; i < content.childNodes.length; i++) {
      buff += this.__domToHTML(content.childNodes[i]);
    }
    
    if (isTag && !this.__isSingleTag(content.nodeName)) {
      buff += '</'+content.nodeName.toLowerCase()+'>';
    }
    
    return buff;
  },
  
  '__isSingleTag': function (tag) {
    return tag.toLowerCase().match('/^(br|img|input|hr)$/i');
  },

  'getSelectedHTML': function(sel) {
    if (!sel) {
      var sel = this.__getSelection();
    }
    var range = this.__createRange(sel);
    
    if ($type(range.htmlText)) {
      return range.htmlText;
    } else {
      try {
        var content = range.cloneContents();
        return this.__domToHTML(content);
      } catch(e) {
        return '';
      }
    }
  },
  
  '__unselectButtons': function() {
    for (i in this.__buttons) {
      this.__unselectButton(this.__buttons[i].element);
    }
  },

  'isPressed': function(buttonName) {
    return (this.__buttons[buttonName].element.src.match(/.*active-[\.a-z0-9]+\.png$/i));
  },
  
  'selectButton': function(buttonName) {
    if (this.__buttons[buttonName]) {
      this.__selectButton(this.__buttons[buttonName].element);
    }
  },

  'unselectButton': function(buttonName) {
    if (this.__buttons[buttonName]) {
      this.__unselectButton(this.__buttons[buttonName].element);
    }
  },

  '__selectButton': function(button) {
    if (button) {
      this.__unselectButton(button);
      var match = button.src.match(/(.+)\/([^\/]+)$/);
      button.src = match[1]+'/'+'active-'+match[2];
    }
  },
  
  '__unselectButton': function(button) {
    if (button) {
      button.src = button.src.replace(/active-/,  '');
    }
  },

  'nodeChange': function(el) {

    el = $(el);

    var i = 0;

    // unselecting butons
      
    var button = this.components.buttons.getElementsByTagName('img');
    for (i = button.length - 1; i >= 0; i--) {
      this.__unselectButton(button[i]);
    }

    var select = this.components.buttons.getElementsByTagName('select');
    for (i = select.length - 1; i >= 0; i--) {
      select[i].value = '';
      select[i].style.backgroundColor = '';
    }

    // is this full screen?
    if (this.__isFullScreen) {
      this.selectButton('fullscreen');
    }
    
    while (el) {
      if ($type(el) == 'element') {

        var name = el.nodeName.toLowerCase();

        if (name.match(/^(body|head|html)$/)) {
          el = false;
        } else {

          for (var i in this.__buttons) {
            var button = this.__buttons[i];
              
            if (button.changeElementPattern) {
              if (name.match(button.changeElementPattern)) {
                if ($type(button.onNodeChange)) {
                  button.onNodeChange(el);
                } else {
                  this.selectButton(button.name);
                }
              }
            }

            if (button.nodeStyle) {
              if (el.style[button.nodeStyle]) {
                if (el.style[button.nodeStyle].match(button.nodeStyleValue)) {
                  if ($type(button.onNodeChange)) {
                    button.onNodeChange(el);
                  } else {
                    this.selectButton(button.name);
                  }
                } 
              }
            }
            
          }

        }
      }
      el = $(el.parentNode);
    }
  },
  
  '__iframeMouseUp': function(e) {

    var sel   = this.__getSelection();
    var range = this.__createRange(sel);

    var parent = null;

    if (Browser.Engine.trident) {
      if (range.parentElement) {
        parent = range.parentElement();
      } else if (range.item) {
        parent = range.item(0);
      }
    } else {

      parent = range.commonAncestorContainer;

      if (range.startContainer == range.endContainer && ($type(range.startContainer) == 'element') && (range.startOffset + 1) == range.endOffset) {

        parent = range.startContainer.childNodes[range.startOffset];

      } else {

        // read comments from getRangeParent
        var contents = range.cloneContents();
        var garbage = document.createDocumentFragment();

        // emptying nodes from the end
        if (contents.childNodes.length > 0) {
          var node = contents.childNodes[contents.childNodes.length - 1];
          while ($type(node) == 'textnode' && node.nodeValue == '') {
            garbage.appendChild(node);
            node = node.previousSibling;
          }
          // emptying nodes from the start
          var node = contents.childNodes[0];
          while ($type(node) == 'textnode' && node.nodeValue == '') {
            garbage.appendChild(node);
            node = node.nextSibling;
          }
        }

        if (contents.childNodes.length == 1 && $type(contents.childNodes[0]) == 'element') {
          // the user selected only one node
          parent = contents.childNodes[0];
        } else {
          // a wrapper is needed
          while ($type(parent) != 'element') {
            parent = parent.parentNode;
          }
        }
      }
    }

    // ** special cases **

    // select whole anchor
    if (parent.nodeName.toLowerCase() == 'a') {
      this.selectNode(parent);
    }

    if (parent) {
      this.nodeChange(parent);
    }
    
  },

  'localAddEvent': function(name, callback) {
    this.addListener(
      this.components.iframe.contentWindow.document,
      name, callback
    );
  },
  
  'localRemoveEvent': function(name, callback) {
    $(this.components.iframe.contentWindow.document).removeEvent(name, callback);
  },
  
  '__getSelection': function() {
    this.focusVisual();
    if (Browser.Engine.trident) {
      var sel = this.iframeDocument().selection;
    } else {
      var sel = this.components.iframe.contentWindow.getSelection();  
    }
    return sel;
  },
  
  '__start': function() {

    if (!this.__started) {

      var parent = this.components.iframe;

      try {

        if (this.components.iframe.contentWindow) {
          this.components.iframe.contentWindow.document.body.innerHTML = this.element.value ? this.element.value : '<br />';
        }
        
        this.designMode(true);

        if (this.$timers.startAgain) {
          window.clearTimeout(this.$timers.startAgain);
        }

        this.addListener(
          this.iframeDocument(),
          'mouseup',
          this.__iframeMouseUp
        );

        this.addListener(
          this.element,
          'keydown',
          this.updateVisual
        );

        if (this.options.autoSave) {
          this._autoSave();
        }

        var events = ['keyup', 'keydown', 'mousein', 'mouseout'];

        for (var i = 0; i < events.length; i++) {
          
          this.localAddEvent(
            events[i],
            function(e) {

              e = new Event(e);

              if (e.type == 'keydown' && !this.options.tabIndents) {
                if (e.key == 'tab') {
                  var form = this.element;
                  var change = false;
                  while (form) {
                    if (form.nodeName.toLowerCase() == 'form') {
                      for (var i = 0; i < form.elements.length; i++) {
                        var el = form.elements[i];
                        if (change == true) {
                          el.focus();
                          return e.stop();
                        }
                        if (el == this.element) {
                          var change = true;
                        }
                      }
                    }
                    form = form.parentNode;
                  }
                }
              }

              this.updateSource();

            }.bind(this)
          );
        }

        this.__started = true;

      } catch(e) {
        if (!this.$timers.startAgain) {
          this.$timers.startAgain = window.setInterval(this.__start.bind(this), 3500);
        }
      }
    }

  },

  '__autoSave': function(clear) {
    if (!this.__autoSaveForm) {
      this.__autoSaveForm = Widget.form({
        'action': this.options.autoSave,
        'method': 'post',
        'style': {'display': 'none'}
      });
      this.__autoSaveForm.data = Widget.textarea({'name': 'data'});
      this.__autoSaveForm.ident = Widget.input({
        'name': 'ident',
        'value': this.__ident
      });
      this.__autoSaveForm.appendChild(this.__autoSaveForm.data);
      this.__autoSaveForm.appendChild(this.__autoSaveForm.ident);
      document.body.appendChild(this.__autoSaveForm);
    }

    if (clear == true) {
      window.clearTimeout(this.$timers.autoSave);
      this.__autoSaveForm.data.value = '';
    } else {
      this.__autoSaveForm.data.value = this.element.value;
    }

    if (!this.element.parentNode) {
      return window.clearTimeout(this.$timers.autoSave);
    }

    // saving...
    return formSubmit(this.__autoSaveForm);
  }, 

  '_autoSave': function() {

    var parent = this.element;
    
    while (parent && parent.nodeName.toLowerCase() != 'form') {
      parent = parent.parentNode;
    }

    this.__ident = location.href+'|'+this.element.name+(parent ? '|'+parent.action+'|'+parent.method : null);
    
    if (parent) {
      parent.appendChild(Widget.input({
        'name': '__ident',
        'type': 'hidden',
        'value': this.__ident
      }));
    }
    
    this.__autoSaveForm = null;
   
    new Request(
      {
        'url': this.options.autoSave,
        'method': 'get',
        'data': 'ident='+encodeURIComponent(this.__ident),
        'onComplete': function(t) {
          var json = eval('('+t+')');
          if (json) {
            new JsonRpc(json);
            this.element.value = json.data;
            this._updateVisual();
          }
        }.bind(this)
      }
    ).send();
    
    this.$timers.autoSave = window.setInterval(this.__autoSave.bind(this), parseInt(this.options.autoSaveInterval) * 1000);

  },
  
  '_updateSource': function() {
    if (this.__started && this.components.iframe.contentWindow) {
      log('source up');
      this.element.value = this.components.iframe.contentWindow.document.body.innerHTML;
    }
  },
  
  '_updateVisual': function() {
    if (this.components.iframe.contentWindow) {
      log('visual up');
      this.components.iframe.contentWindow.document.body.innerHTML = this.element.value ? this.element.value : '<br />';
    }
  },
  
  'iframeDocument': function() {
    if (Browser.Engine.trident) {
      var el = this.components.iframe.contentWindow.document;
    } else {    
      var el = this.components.iframe.contentDocument;
    } 
    new Window(el);
    return $(el);
  },
  
  'designMode': function(status) {
    this.components.iframe.setStyle('display', 'block');

    if (Browser.Engine.trident) {
      this.iframeDocument().body.contentEditable = true;
    } else { 
      this.iframeDocument().designMode = status ? 'On' : 'Off';
      this.execCommand('styleWithCSS', true);
    } 

  },

  'initialize': function(el, options) {

    this.$timers = {};

    this.setOptions(options);

    if (!this.options.buttons) {
      switch (this.options.mode) {
        case 'nano':
          this.options.showSource = false; 
          this.options.buttons = [
            'bold',
            'italic',
            'underline',
            'strikethrough',
            'createlink',
            'removeformat'
          ];
        break;
        case 'mini':
          this.options.showSource = false; 
          this.options.buttons = [
            'bold',
            'italic',
            'underline',
            'strikethrough',
            'createlink',
            'insertorderedlist',
            'insertunorderedlist',
            'removeformat',
            'fullscreen'
          ];
        break;
        case 'basic':
          this.options.showSource = false; 
          this.options.buttons = [
            'bold',
            'italic',
            'underline',
            'strikethrough',
            'createlink',
            'insertorderedlist',
            'insertunorderedlist',
            'justifyleft',
            'justifyright',
            'justifyfull',
            'justifycenter',
            'indent',
            'outdent',
            'removeformat',
            'fullscreen'
          ];
        break;
        case 'advanced':
          this.options.showSource = true; 
          this.options.buttons = [
            'bold',
            'italic',
            'underline',
            'strikethrough',
            'createlink',
            'insertorderedlist',
            'insertunorderedlist',
            'justifyleft',
            'justifyright',
            'justifyfull',
            'justifycenter',
            'indent',
            'outdent',
            'inserthorizontalrule',
            'insertimage',
            'removeformat',
            'fullscreen'
          ];
        break;
        case 'complete':
          // all buttons
          this.options.showSource = true;
        break;
      }
    }

    // explicit rewrite
    if (options && $type(options.showSource)) {
      this.options.showSource = options.showSource;
    }
    
    this.setMainElement(el);
      
    this.__buildComponents();
    this.__bindEvents();
  }
});

