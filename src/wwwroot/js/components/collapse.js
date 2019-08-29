import * as util from '../utils.js'

const COLLAPSE = "collapse";
const COLLAPSING = "collapsing";
const SHOW = "show";
const collapseStatus = new WeakMap();

export function show(elm) {
    collapseStatus.set(elm, SHOW);

    const dimension = "height";
    let animationTime = 0;

    elm.classList.remove(COLLAPSE);
    elm.classList.add(COLLAPSING);

    animationTime = util.getTransitionDurationFromElement(elm);

    elm.style[dimension] = 0;
    elm.style[dimension] = elm["scrollHeight"] + "px";

    setTimeout(() => {
        if (collapseStatus.get(elm) !== SHOW) return;
        collapseStatus.delete(elm);

        elm.classList.remove(COLLAPSING);
        elm.classList.add(COLLAPSE);
        elm.classList.add(SHOW);
        elm.style[dimension] = '';
    }, animationTime);

    
    return animationTime;
};

export function hide(elm) {
    collapseStatus.set(elm, COLLAPSE);

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
        if (collapseStatus.get(elm) !== COLLAPSE) return;
        collapseStatus.delete(elm);

        elm.classList.remove(COLLAPSING);
        elm.classList.add(COLLAPSE);
    }, animationTime);
    
    return animationTime;
};