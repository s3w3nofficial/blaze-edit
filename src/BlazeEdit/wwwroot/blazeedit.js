let canvas;

function loadCanvas() {
    canvas = document.querySelector(`.blazeedit-canvas`);
}

function getHeightFromRef(element) {
    var el = document.querySelector(`.${element}`);
    return el.clientHeight;
}

function getCanvasTopOffset() {
    return parseInt(canvas.getBoundingClientRect().top);
}
