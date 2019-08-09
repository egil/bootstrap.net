declare var Prism;

function escapeHtml(html: string): string {
    let tmp = document.createElement("div");
    let textNode = document.createTextNode(html);
    tmp.appendChild(textNode);
    return tmp.innerHTML;
}

function removeBlazorComments(html: string): string {
    return html.replace(/<!--!-->/gi, "");
}

export function setOutputHtml(example: HTMLDivElement): void {
    const htmlOutputElm = example.getElementsByClassName("example-html-output")[0];
    let renderedCode = example.getElementsByClassName("output")[0].innerHTML;
    htmlOutputElm.innerHTML = escapeHtml(removeBlazorComments(renderedCode));
    Prism.highlightAllUnder(example);
}