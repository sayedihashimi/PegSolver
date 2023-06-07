var Peg = function (row, col, hasPeg) {
    this.row = row;    // int
    this.col = col;    // int
    this.hasPeg = hasPeg; // bool

    // needed for drawing and to be able to determine what peg was selected
    this.center = null;
    this.radius = null;

    this.containsPoint = function (point) {
        var containsPoint = false;
        // if a^2 + b^2 > r^2 then its outside the circle
        var distanceSq = Math.pow((this.center[0] - point[0]), 2) + Math.pow((this.center[1] - point[1]), 2);
        if (distanceSq <= Math.pow(this.radius, 2)) {
            containsPoint = true;
        }

        return containsPoint;
    };
};

function BuildBoardDataFromString(boardString){
    // TODO: Assumes size 5 board
    var board = [[boardString.substring(0, 1) === "1" ? true : false],

                [boardString.substring(6, 7) === "1" ? true : false,
                    boardString.substring(7, 8) === "1" ? true : false],

                [boardString.substring(12, 13) === "1" ? true : false,
                    boardString.substring(13, 14) === "1" ? true : false,
                    boardString.substring(14, 15) === "1" ? true : false],

                [boardString.substring(18, 19) === "1" ? true : false,
                    boardString.substring(19, 20) === "1" ? true : false,
                    boardString.substring(20, 21) === "1" ? true : false,
                    boardString.substring(21, 22) === "1" ? true : false],

                [boardString.substring(24, 25) === "1" ? true : false,
                    boardString.substring(25, 26) === "1" ? true : false,
                    boardString.substring(26, 27) === "1" ? true : false,
                    boardString.substring(27, 28) === "1" ? true : false,
                    boardString.substring(28, 29) === "1" ? true : false]];

    return board;
}

var Board = function (size, data, canvasElement) {
    if (!data) { throw "data cannot be null"; }
    if (!canvasElement) { throw "canvasElement cannot be null"; }

    // default size
    if (!size) { size = 5; }

    // size of the board side length
    this.size = size;

    // can be "InProgress","Ended","Invalid","GameWon"
    this.state = "InProgress";

    this.data = data;

    // drawing related fields
    this.canvasElement = canvasElement;
    this.minLineWidth = 1;
    this.maxLineWidth = 20;

    // fields related to events
    this.dragging = false;
    this.selectedStartPeg = null;
    this.selectedDestPeg = null;

    this.eventHanldersAttached = false;

    this.pegs = new Array();
    for (var r = 0; r < size; r++) {
        this.pegs[r] = new Array();
        for (var c = 0; c < size; c++) {
            if (c <= r) {
                this.pegs[r][c] = new Peg(r, c, this.data[r][c]);
            }
        }
    }

    this.getPegRowCol = function (index) {
        // need to translate index into row,col
        // TODO: This is hard coded to a 5 sided board
        var row = 0;
        var col = 0;

        if (index === 1) { row = 0; col = 0; }

        if (index === 2) { row = 1; col = 0; }
        if (index === 3) { row = 1; col = 1; }

        if (index === 4) { row = 2; col = 0; }
        if (index === 5) { row = 2; col = 1; }
        if (index === 6) { row = 2; col = 2; }

        if (index === 7) { row = 3; col = 0; }
        if (index === 8) { row = 3; col = 1; }
        if (index === 9) { row = 3; col = 2; }
        if (index === 10) { row = 3; col = 3; }

        if (index === 11) { row = 4; col = 0; }
        if (index === 12) { row = 4; col = 1; }
        if (index === 13) { row = 4; col = 2; }
        if (index === 14) { row = 4; col = 3; }
        if (index === 15) { row = 4; col = 4; }

        return [row, col];
    };

    this.getPeg = function (row, col) {
        if (row != 0 && !row) { throw "row cannot be null"; }
        if (col != 0 && !col) { throw "col cannot be null"; }

        // TODO: Ensure that the row/col is not out of bounds
        return this.pegs[row][col];
    };

    this.removePeg = function (row, col) {
        this.data[row][col] = false;
        this.getPeg(row, col).hasPeg = false;
    };

    this.insertPeg = function (row, col) {
        this.data[row][col] = true;
        this.getPeg(row, col).hasPeg = true;
    };

    this.getPegAtIndex = function (index) {
        var rowCol = this.getPegRowCol(index);
        return this.getPeg(rowCol[0], rowCol[1]);
    };

    this.getBoardAsString = function () {
        // for example: 00000;11000;11100;11110;11111
        var str = "";
        for (var row = 0; row < this.size; row++) {
            for (var col = 0; col < this.size; col++) {
                str += this.data[row][col] ? "1" : "0";
            }
            str += ";";
        }

        return str;
    };

    this.drawBoard = function () {
        // if (!canvasElement) { throw "canvasElement cannot be null for drawBoard"; }

        var canvasCtx = this.canvasElement[0].getContext("2d");
        if (!canvasCtx) { throw "canvasCtx cannot be null for drawBoard"; }

        // clear out the context first
        canvasCtx.moveTo(0, 0);
        canvasCtx.clearRect(0, 0, canvasElement[0].width, canvasElement[0].height);

        this.drawTriangle(this.canvasElement, canvasCtx);
        this.drawGridLines(this.canvasElement, canvasCtx);
        this.drawPegHoles(this.canvasElement, canvasCtx);

        this.attachEventHandlers(this.canvasElement, canvasCtx, this);
    };

    this.drawTriangle = function (canvasElement, canvasCtx) {
        // TODO: We should get the triagle size from the board
        var height = canvasElement[0].height;
        var width = canvasElement[0].width;

        var lineWidth = (1 / 150 * width);

        if (lineWidth < this.minLineWidth) { lineWidth = this.minLineWidth; }
        if (lineWidth > this.maxLineWidth) { lineWidth = this.maxLineWidth; }

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
    };

    this.drawPegHoles = function (canvasElement, canvasCtx) {
        var height = canvasElement[0].height;
        var width = canvasElement[0].width;

        var radius = (5 / 10) * (1 / 10 * height);
        var lineWidth = (1 / 150 * width);

        if (lineWidth < this.minLineWidth) { lineWidth = this.minLineWidth; }
        if (lineWidth > this.maxLineWidth) { lineWidth = this.maxLineWidth; }

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

            var peg = this.getPegAtIndex(i + 1);
            peg.center = [centerX, centerY];
            peg.radius = radius;

            canvasCtx.arc(centerX, centerY, radius, 0, 2 * Math.PI, true);

            canvasCtx.lineWidth = lineWidth;

            var rowCol = this.getPegRowCol(i + 1);
            if (peg.hasPeg) {
                // if the peg hole has a peg then fill it
                canvasCtx.fill();
            }
            else {
                var debug = "ddd";
            }

            canvasCtx.stroke();
        }
    };

    this.drawGridLines = function (canvasElement, canvasCtx) {
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
    };

    this.startDragging = function (peg) {
        if (!peg) { this.resetDragging(); }

        this.selectedStartPeg = peg;
        this.dragging = true;
    };

    this.stopDragging = function (peg) {
        // see if there is an empty hole under this one
        if (peg.hasPeg) {
            alert('invalid play: dest hole not empty!');
            this.resetDragging();
        }
        else {
            this.selectedDestPeg = peg;
            this.playMove(this.selectedStartPeg, this.selectedDestPeg);
        }
    };

    this.playMove = function (startPeg, endPeg, canvasElement) {
        // if it's an invalid move reset dragging
        var dx = Math.abs(startPeg.row - endPeg.row);
        var dy = Math.abs(startPeg.col - endPeg.col);

        var isValid = true;
        if (!(dx == 2 || dx == 0)) {
            isValid = false;
        }
        if (!(dy == 2 || dy == 0)) {
            isValid = false;
        }

        if (!isValid) {
            // TODO: Remove alert
            alert('invalid play');
        }

        var midPegRow = (startPeg.row + endPeg.row) / 2;
        var midPegCol = (startPeg.col + endPeg.col) / 2;
        // get the peg and make sure that it is open
        var midPeg = this.getPeg(midPegRow, midPegCol);
        if (!midPeg.hasPeg) {
            // TODO: Remove alert
            alert("invalid play: middle hole doesn't have a peg");
        }
        else {
            // remove the peg from the source
            this.removePeg(startPeg.row, startPeg.col);
            // remove the peg from the middle
            this.removePeg(midPeg.row, midPeg.col);
            // insert a peg at the dest
            this.insertPeg(endPeg.row, endPeg.col);
        }

        this.drawBoard();
    };

    this.resetDragging = function () {
        this.selectedStartPeg = null;
        this.selectedDestPeg = null;
        this.dragging = false;
    };

    this.attachEventHandlers = function (canvasElement, canvasCtx, board) {
        if (!this.eventHanldersAttached) {
            this.eventHanldersAttached = true;
            canvasElement.live('mousedown', function (e) {
                board.resetDragging();
                var point = [e.offsetX, e.offsetY];
                // Find the peg under the point
                // TODO: this is hard coded for 5 sided board
                var maxNumPegs = 15;

                var selPeg = null;
                // set selected peg
                // search through the pegs to see which contains the point
                for (var i = 0; i < maxNumPegs; i++) {
                    var peg = board.getPegAtIndex(i + 1);
                    if (peg.containsPoint(point)) {
                        selPeg = peg;
                        break;
                    }
                }

                board.startDragging(selPeg);
            });

            canvasElement.live('mouseup', function (e) {
                if (board.dragging == true) {
                    var point = [e.offsetX, e.offsetY];
                    // find the peg underneath the point
                    // Find the peg under the point
                    // TODO: this is hard coded for 5 sided board
                    var maxNumPegs = 15;

                    var selDestPeg = null;
                    // set selected peg
                    // search through the pegs to see which contains the point
                    for (var i = 0; i < maxNumPegs; i++) {
                        var peg = board.getPegAtIndex(i + 1);
                        if (peg.containsPoint(point)) {
                            selDestPeg = peg;
                            break;
                        }
                    }

                    board.stopDragging(selDestPeg);
                }
            });

        }
    }
};



