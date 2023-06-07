var currentBoard;

$(function () {
    var gameCanvasId = "gameCanvas";
    var canvasElement = $("#" + gameCanvasId);
    var canvasCtx = canvasElement[0].getContext("2d");

    var minLineWidth = 1;
    var maxLineWidth = 20;

    var defaultBoardData = [[false],
                            [true, true],
                            [true, true, true],
                            [true, true, true, true],
                            [true, true, true, true, true]];


    var defaultBoard = new Board(5, defaultBoardData,canvasElement);
    currentBoard = defaultBoard;

    currentBoard.drawBoard(canvasElement);
});