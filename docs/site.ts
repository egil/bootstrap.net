declare global {
    interface Window { bootstrapDotNetDocs: any; }
}
window.bootstrapDotNetDocs = window.bootstrapDotNetDocs || {};

import '/_content/bootstrapdotnet/js/bootstrap.net.js';
import '../libs/PrismJS/prism.js';
import * as example from './Components/Example.js';

window.bootstrapDotNetDocs.example = example;