// assumes that jQuery has been loaded
$(function () {
    var gameCanvasId = "gameCanvas";
    var gameCanvas = $("#" + gameCanvasId);

    drawBoard(gameCanvas);

    function drawBoard(canvasElement) {
        var canvasCtx = gameCanvas[0].getContext("2d");    
        canvasCtx.moveTo(0, 0);
        canvasCtx.lineTo(120, 50);

        canvasCtx.strokeStyle = "#000";
        canvasCtx.stroke();
    }

    function drawTriangle() {
        
    }
});