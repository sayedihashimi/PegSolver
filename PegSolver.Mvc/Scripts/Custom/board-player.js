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


    var defaultBoard = new Board(5, defaultBoardData, canvasElement);
    currentBoard = defaultBoard;

    currentBoard.drawBoard(canvasElement);

    // Event handlers
    $("#buttonSolve").click(function (e) {
        // we need to get the state of the board and then post that back to the solve URL
        var debug = 'foo';

        var boardString = currentBoard.getBoardAsString();

        var url = "/SolveBoard/" + boardString;

        window.location.href = url;


        var bar = 'bar';
    });


});