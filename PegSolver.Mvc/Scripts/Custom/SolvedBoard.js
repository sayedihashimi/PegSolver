
$(function () {
    var gameCanvasId = "gameCanvas";
    var canvasElement = $("#" + gameCanvasId);
    var canvasCtx = canvasElement[0].getContext("2d");

    //    debugger;
    var currentIndex = 0;
    var boardData = BuildBoardDataFromString(moves[0]);
    var currentBoard = new Board(5, boardData, canvasElement);

    currentBoard.drawBoard(canvasElement);

    this.updateBoard = function () {
        currentIndex++;
        if (currentIndex >= moves.length) {
            clearInterval(updateBoardSetIntervalId);
        }
        else {
            // build the board and update what's on the screen
            var boardData = BuildBoardDataFromString(moves[currentIndex]);
            var currentBoard = new Board(5, boardData, canvasElement);

            currentBoard.drawBoard(canvasElement);
        }
    };

    var updateBoardSetIntervalId = setInterval(this.updateBoard, 2000);
});
