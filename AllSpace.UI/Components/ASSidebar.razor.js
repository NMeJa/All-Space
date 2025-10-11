export function scrollToPosition(element, position) {
    if (element) {
        element.scrollTop = position;
    }
}

export function addScrollListener(element, dotNetRef) {
    if (element) {
        element.addEventListener('scroll', (e) => {
            dotNetRef.invokeMethodAsync('OnScroll', e.target.scrollTop);
        });
    }
}