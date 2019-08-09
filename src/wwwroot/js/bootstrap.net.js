import * as utils from './utils.js';
import * as collapse from './components/collapse.js';
import * as dropdown from './components/dropdown.js';
import * as pageVisibilityApiInterop from './services/pageVisibilityApiInterop.js'

window.bootstrapDotNet = window.bootstrapDotNet || {};
window.bootstrapDotNet.components = window.bootstrapDotNet.components || {};
window.bootstrapDotNet.services = window.bootstrapDotNet.services || {};

window.bootstrapDotNet.utils = utils;
window.bootstrapDotNet.components.collapse = collapse;
window.bootstrapDotNet.components.dropdown = dropdown;
window.bootstrapDotNet.services.pageVisibilityApiInterop = pageVisibilityApiInterop;