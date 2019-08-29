declare global {
    interface Window { bootstrapDotNetDocs: any; }
}
window.bootstrapDotNetDocs = window.bootstrapDotNetDocs || {};

import '/_content/bootstrapdotnet/js/bootstrap.net.js';
import '../libs/PrismJS/prism.js';
import '../libs/js-beautify-1.10.2/beautify-html.min.js';

import * as example from './Components/Example.razor.js';

window.bootstrapDotNetDocs.example = example;