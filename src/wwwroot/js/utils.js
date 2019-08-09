export function reflow(elm) {
    elm.offsetHeight;
}

export function focus(elm) {
    elm.focus();
}

export function cssTimeToMilliseconds(time) {
    var num = parseFloat(time, 10),
        unit = time.match(/m?s/),
        milliseconds;

    if (unit) {
        unit = unit[0];
    }

    switch (unit) {
        case "s": // seconds
            milliseconds = num * 1000;
            break;
        case "ms": // milliseconds
            milliseconds = num;
            break;
        default:
            milliseconds = 0;
            break;
    }

    return milliseconds;
}

export function getTransitionDurationFromElement(element) {
    if (!element) {
        return 0
    }

    const computedStyles = getComputedStyle(element);

    // Get transition-duration of the element
    let transitionDuration = computedStyles.transitionDuration;
    let transitionDelay = computedStyles.transitionDelay;

    const floatTransitionDuration = parseFloat(transitionDuration)
    const floatTransitionDelay = parseFloat(transitionDelay)

    // Return 0 if element or transition duration is not found
    if (!floatTransitionDuration && !floatTransitionDelay) {
        return 0
    }

    // If multiple durations are defined, take the first
    transitionDuration = transitionDuration.split(',')[0]
    transitionDelay = transitionDelay.split(',')[0]

    return cssTimeToMilliseconds(transitionDuration) + cssTimeToMilliseconds(transitionDelay);
}

function preventDefaultHandler(e) {
    e.preventDefault();
}

export function preventDefault(element, eventType) {
    element.addEventListener(eventType, preventDefaultHandler, { capture: true });
}

export function allowDefault(element, eventType) {
    element.removeEventListener(eventType, preventDefaultHandler, { capture: true });
}