function escapeHtml(html) {
    var tmp = document.createElement("div");
    var textNode = document.createTextNode(html);
    tmp.appendChild(textNode);
    return tmp.innerHTML;
}
function removeBlazorComments(html) {
    return html.replace(/<!--!-->/gi, "");
}
function removeBlazorRefAttributes(html) {
    return html.replace(/\s_bl_[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}=""/gi, "");
}
function tidyMarkup(html) {
    html = removeBlazorComments(html);
    html = removeBlazorRefAttributes(html);
    html = html_beautify(html);
    return escapeHtml(html);
}
function fadeInCode(example) {
    var cards = example.querySelectorAll(".source-code > .card");
    cards[0].classList.add("show");
    setTimeout(function () { return cards[1].classList.add("show"); }, 100);
}
export function setOutputHtml(example) {
    var htmlOutputElm = example.getElementsByClassName("example-html-output")[0];
    var renderedCode = example.getElementsByClassName("output")[0].innerHTML;
    htmlOutputElm.innerHTML = tidyMarkup(renderedCode);
    Prism.highlightAllUnder(example, false);
    fadeInCode(example);
}
//# sourceMappingURL=Example.js.map