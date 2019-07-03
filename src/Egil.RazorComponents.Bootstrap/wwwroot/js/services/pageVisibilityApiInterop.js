let dotnetCallback = undefined;
let callbackMethodName = '';
let visibilityChangeListener;
let hidden, visibilityChange;

if (typeof document.hidden !== 'undefined') {
    hidden = 'hidden';
    visibilityChange = 'visibilitychange';
} else if (typeof document.msHidden !== 'undefined') {
    hidden = 'msHidden';
    visibilityChange = 'msvisibilitychange';
} else if (typeof document.webkitHidden !== 'undefined') {
    hidden = 'webkitHidden';
    visibilityChange = 'webkitvisibilitychange';
}

function handleVisibilityChange() {
    dotnetCallback.invokeMethodAsync(callbackMethodName, isPageVisible());
}

export function isPageVisible() {
    return document.visibilityState === 'visible';
}

export function isSupported() {
    return typeof document.addEventListener !== 'undefined' && hidden !== undefined;
}

export function subscribe(dotnetHelper, callbackMethod) {
    if (dotnetCallback === undefined && isSupported()) {
        dotnetCallback = dotnetHelper;
        callbackMethodName = callbackMethod;
        handleVisibilityChange();
        visibilityChangeListener = document.addEventListener(visibilityChange, handleVisibilityChange, false);
    }
}

export function unsubscribe() {
    if (dotnetCallback !== undefined && isSupported()) {
        document.removeEventListener(visibilityChange, visibilityChangeListener);
    }
}