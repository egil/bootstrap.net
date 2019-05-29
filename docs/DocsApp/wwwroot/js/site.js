(function () {
    const setExampleBootstrapHtml = function (exampleId) {
        const example = $(document.getElementById(exampleId));
        const bootstrapCodeElm = example.find('.example-code-bootstrap > pre > code').get(0);
        const outputCodeElm = example.find('.example-output').get(0);
        let output = outputCodeElm.innerHTML;
        output = output.replace(/<!--!-->/gi, "").replace(/\s+\n/g, "\n");
        output = html_beautify(output);
        output = Prism.highlight(output, Prism.languages.markup, 'markup')
        bootstrapCodeElm.innerHTML = output;
    };

    const getRenderedExampleHtml = function (exampleId) {
        const example = $(document.getElementById(exampleId));
        var output = example.find('.example-output').get(0);
        return output.innerHTML;
    };

    window.setExampleBootstrapHtml = setExampleBootstrapHtml;
    window.getRenderedExampleHtml = getRenderedExampleHtml;
}());