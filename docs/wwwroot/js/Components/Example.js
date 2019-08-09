function escapeHtml(html) {
    var tmp = document.createElement("div");
    var textNode = document.createTextNode(html);
    tmp.appendChild(textNode);
    return tmp.innerHTML;
}
function removeBlazorComments(html) {
    return html.replace(/<!--!-->/gi, "");
}
export function setOutputHtml(example) {
    var htmlOutputElm = example.getElementsByClassName("example-html-output")[0];
    var renderedCode = example.getElementsByClassName("output")[0].innerHTML;
    htmlOutputElm.innerHTML = escapeHtml(removeBlazorComments(renderedCode));
    Prism.highlightAllUnder(example);
}
//# sourceMappingURL=Example.js.map