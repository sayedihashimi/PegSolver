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


    var defaultBoard = new Board(5, defaultBoardData);
    currentBoard = defaultBoard;

    drawTriangle(canvasElement, canvasCtx, defaultBoard);
    drawGridLines(canvasElement, canvasCtx, defaultBoard);
    drawPegHoles(canvasElement, canvasCtx, defaultBoard);

    function drawTriangle(canvasElement, canvasCtx, board) {
        // TODO: We should get the triagle size from the board
        var height = canvasElement[0].height;
        var width = canvasElement[0].width;

        var lineWidth = (1 / 150 * width);

        if (lineWidth < minLineWidth) { lineWidth = minLineWidth; }
        if (lineWidth > maxLineWidth) { lineWidth = maxLineWidth; }

        canvasCtx.lineWidth = lineWidth;

        canvasCtx.beginPath();
        canvasCtx.strokeStyle = "#000";
        // Left side
        canvasCtx.moveTo(0, height - (1 / 2 * lineWidth));
        canvasCtx.lineTo((1 / 2 * width), 0);

        // Right side
        canvasCtx.moveTo((1 / 2 * width), 0 + 1 / 2 * lineWidth);
        canvasCtx.lineTo(width, height);

        // Bottom
        canvasCtx.moveTo(width, height - (1 / 2 * lineWidth));
        canvasCtx.lineTo(0, height - (1 / 2 * lineWidth));

        canvasCtx.stroke();
        // bottom row only draws half the line so double the width
        // canvasCtx.lineWidth = 2*lineWidth;

        canvasCtx.stroke();
    }

    function drawPegHoles(canvasElement, canvasCtx, board) {
        var height = canvasElement[0].height;
        var width = canvasElement[0].width;

        var radius = (5 / 10) * (1 / 10 * height);
        var lineWidth = (1 / 150 * width);

        if (lineWidth < minLineWidth) { lineWidth = minLineWidth; }
        if (lineWidth > maxLineWidth) { lineWidth = maxLineWidth; }

        var pegHoles = [
                [(1 / 2 * width), 1 / 10 * height],              // 1

                [(1 / 2 * width) - (1 / 10 * width), (3 / 10 * height)], // 2
                [(1 / 2 * width) + (1 / 10 * width), (3 / 10 * height)], // 3

                [(1 / 2 * width) - (2 / 10 * width), 5 / 10 * height], //4
                [(1 / 2 * width), (5 / 10 * height)], // 5
                [(1 / 2 * width) + (2 / 10 * width), 5 / 10 * height], // 6

                [(1 / 2 * width) - (3 / 10 * width), 7 / 10 * height], // 7
                [(1 / 2 * width) - (1 / 10 * width), 7 / 10 * height], // 8
                [(1 / 2 * width) + (1 / 10 * width), 7 / 10 * height], // 9
                [(1 / 2 * width) + (3 / 10 * width), 7 / 10 * height], // 10

                [(1 / 2 * width) - (2 / 5 * width), 9 / 10 * height], // 11
                [(1 / 2 * width) - (1 / 5 * width), 9 / 10 * height], // 12
                [(1 / 2 * width), 9 / 10 * height],                   // 13
                [(1 / 2 * width) + (1 / 5 * width), 9 / 10 * height], // 14
                [(1 / 2 * width) + (2 / 5 * width), 9 / 10 * height] // 15
            ];

        canvasCtx.strokeStyle = "red";
        canvasCtx.fillStyle = "green";
        for (i = 0; i < pegHoles.length; i++) {
            canvasCtx.beginPath();
            var res = pegHoles[i];
            var str = "w=" + res[0] + ", h=" + res[1];

            var centerX = res[0];
            var centerY = res[1];

            var peg = board.getPegAtIndex(i + 1);
            peg.center = [centerX, centerY];
            peg.radius = radius;

            canvasCtx.arc(centerX, centerY, radius, 0, 2 * Math.PI, true);

            canvasCtx.lineWidth = lineWidth;

            var rowCol = board.getPegRowCol(i + 1);
            if (peg.hasPeg) {
                // if the peg hole has a peg then fill it
                canvasCtx.fill();
            }
            else {
                var debug = "ddd";
            }

            canvasCtx.stroke();
        }
    }

    function drawGridLines(canvasElement, canvasCtx, board) {
        // TODO: We should get the triagle size from the board
        var height = canvasElement[0].height;
        var width = canvasElement[0].width;
        canvasCtx.beginPath();

        // draw horizontal grid lines
        // 0.5 to make a 1 pixel line see: http://diveintohtml5.org/canvas.html
        var heightOffset = 1 / 10 * height;
        for (var i = 0; i < 5; i++) {
            canvasCtx.moveTo(0.5, heightOffset + (i / 5) * height - 0.5);
            canvasCtx.lineTo(width + 0.5, heightOffset + (i / 5) * height - 0.5);
        }

        // draw vertical grid lines
        for (var i = 1; i < 10; i++) {
            canvasCtx.moveTo((i / 10) * width + 0.5, 0.5);
            canvasCtx.lineTo((i / 10) * width + 0.5, height - 0.5);
        }

        canvasCtx.lineWidth = 1;
        canvasCtx.strokeStyle = "green";
        canvasCtx.stroke();
    }

    canvasElement.live('mousedown', function (e) {
        // alert('clicked');
        var debug = "deddddd";

        // we have to determine which peg is underneath this point
        // var point = [e.pageX, e.pageY];
        var point = [e.offsetX, e.offsetY];

        // TODO: this is hard coded for 5 sided board
        var maxNumPegs = 15;

        // search through the pegs to see which contains the point
        for (var i = 0; i < maxNumPegs; i++) {
            var peg = currentBoard.getPegAtIndex(i + 1);
            if (peg.containsPoint(point)) {
                // alert('contains point, index: ' + i);

                canvasCtx.save();
                canvasCtx.beginPath();
                canvasCtx.moveTo(peg.center[0], peg.center[1]);
                canvasCtx.fillStyle = "blue";
                canvasCtx.arc(peg.center[0], peg.center[1], peg.radius, 0, 2 * Math.PI, true);
                canvasCtx.fill();
                canvasCtx.restore();
            }
        }
    });
});