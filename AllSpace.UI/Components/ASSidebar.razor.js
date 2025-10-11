window.scrollToPosition = (element, position) => {
    if (element) {
        element.scrollTop = position;
    }
};

window.addScrollListener = (element, dotNetRef) => {
    if (element) {
        element.addEventListener('scroll', (e) => {
            dotNetRef.invokeMethodAsync('OnScroll', e.target.scrollTop);
        });
    }
};
