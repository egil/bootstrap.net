import Popper from '../libs/popper.js-1.15.0/popper.js';

export function addDocumentClickEventListener(dotnetHelper, callbackMethodName) {
    document.addEventListener('click', () => dotnetHelper.invokeMethodAsync(callbackMethodName), {
        once: true,
        passive: true
    });
}

export function positionDropdown(menu, reference, placement) {
    const existingPopper = menu.__bootstrap_dotnet_popper;
    let refElm;

    if (reference === "toggle") {
        refElm = menu.parentElement.querySelector(".dropdown-toggle");
    } else if (reference === "parent") {
        refElm = menu.parentElement;
    }

    menu.__bootstrap_dotnet_popper = new Popper(refElm, menu, {
        placement,
        modifiers: {
            offset: {
                offset: 0
            },
            flip: {
                behavior: "flip"
            },
            preventOverflow: {
                boundary: "scrollParent"
            }
        }
    });

    if (existingPopper) existingPopper.destroy();
}

export function changeFocusedDropdownItem(parentMenu, direction) {
    const items = parentMenu.querySelectorAll('.dropdown-item');
    let currentFocusIndex = -1;
    for (var i = 0; i < items.length; i++) {
        if (items[i] === document.activeElement) {
            currentFocusIndex = i;
            i = items.length;
        }
    }

    if (direction === "ArrowUp" && currentFocusIndex > 0) {
        items[currentFocusIndex - 1].focus();
    } else if (direction === "ArrowDown" && currentFocusIndex < items.length - 1) {
        items[currentFocusIndex + 1].focus();
    }
}