declare var Prism;
declare var html_beautify;

function escapeHtml(html: string): string {
    let tmp = document.createElement("div");
    let textNode = document.createTextNode(html);
    tmp.appendChild(textNode);
    return tmp.innerHTML;
}

function removeBlazorComments(html: string): string {
    return html.replace(/<!--!-->/gi, "");
}

function removeBlazorRefAttributes(html: string): string {
    return html.replace(/\s_bl_[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}=""/gi, "");
}

function tidyMarkup(html: string): string {
    html = removeBlazorComments(html);
    html = removeBlazorRefAttributes(html);
    html = html_beautify(html);
    return escapeHtml(html);
}

function fadeInCode(example: HTMLDivElement) {
    const cards = example.querySelectorAll(".source-code > .card");
    cards[0].classList.add("show");
    setTimeout(() => cards[1].classList.add("show"), 100);
}

export function setOutputHtml(example: HTMLDivElement): void {
    const htmlOutputElm = example.getElementsByClassName("example-html-output")[0];
    const renderedCode = example.getElementsByClassName("output")[0].innerHTML;

    htmlOutputElm.innerHTML = tidyMarkup(renderedCode);

    Prism.highlightAllUnder(example, false);

    fadeInCode(example);
}