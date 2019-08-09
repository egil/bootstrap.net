(function () {
    "use strict";

    window.bootstrapDotNet.domElementUtil.getAttribute = function (elm, attrName) {
        return elm[attrName];
    };

    window.bootstrapDotNet.domElementUtil.getStyle = function (elm, styleName) {
        return elm.style[attrName];
    };

    window.bootstrapDotNet.domElementUtil.setStyle = function (elm, styleName, value) {
        elm.style[styleName] = value;
    };

})();