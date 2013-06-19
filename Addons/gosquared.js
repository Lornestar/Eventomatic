
        var GoSquared = {};
        GoSquared.acct = "GSN-105322-Y";
        (function (w) {
            function gs() {
                w._gstc_lt = +(new Date); var d = document;
                var g = d.createElement("script"); g.type = "text/javascript"; g.async = true; g.src = "//d1l6p2sc9645hc.cloudfront.net/tracker.js";
                var s = d.getElementsByTagName("script")[0]; s.parentNode.insertBefore(g, s);
            }
            w.addEventListener ? w.addEventListener("load", gs, false) : w.attachEvent("onload", gs);
        })(window);

//Mixpanel
var mpq=[];mpq.push(["init","e0afd794a2333d40a4b828e10e8d9592"]);(function(){var b,a,e,d,c;b=document.createElement("script");b.type="text/javascript";b.async=true;b.src=(document.location.protocol==="https:"?"https:":"http:")+"//api.mixpanel.com/site_media/js/api/mixpanel.js";a=document.getElementsByTagName("script")[0];a.parentNode.insertBefore(b,a);e=function(f){return function(){mpq.push([f].concat(Array.prototype.slice.call(arguments,0)))}};d=["init","track","track_links","track_forms","register","register_once","identify","name_tag","set_config"];for(c=0;c<d.length;c++){mpq[d[c]]=e(d[c])}})();
