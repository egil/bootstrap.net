import * as util from '../utils.js'

const COLLAPSE = "collapse";
const COLLAPSING = "collapsing";
const SHOW = "show";

export function show(elm) {
    const dimension = "height";
    let animationTime = 0;

    elm.classList.remove(COLLAPSE);
    elm.classList.add(COLLAPSING);

    animationTime = util.getTransitionDurationFromElement(elm);

    elm.style[dimension] = 0;
    elm.style[dimension] = elm["scrollHeight"] + "px";

    setTimeout(() => {
        elm.classList.remove(COLLAPSING);
        elm.classList.add(COLLAPSE);
        elm.classList.add(SHOW);

        elm.style[dimension] = '';
    }, animationTime);

    return animationTime;
};

export function hide(elm) {
    const dimension = "height";
    let animationTime = 0;

    elm.style[dimension] = elm.getBoundingClientRect()[dimension] + "px";

    util.reflow(elm);

    elm.classList.add(COLLAPSING);
    elm.classList.remove(COLLAPSE);
    elm.classList.remove(SHOW);
    elm.style[dimension] = '';

    animationTime = util.getTransitionDurationFromElement(elm);

    setTimeout(() => {
        elm.classList.remove(COLLAPSING);
        elm.classList.add(COLLAPSE);
    }, animationTime);

    return animationTime;
};