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

var Board = function (size, data) {
    if (!data) { throw "data cannot be null"; }

    // default size
    if (!size) { size = 5; }

    // size of the board side length
    this.size = size;

    // can be "InProgress","Ended","Invalid","GameWon"
    this.state = "InProgress";

    this.data = data;

    // drawing related fields
    this.minLineWidth = 1;
    this.maxLineWidth = 20;



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

    this.getPegAtIndex = function (index) {
        var rowCol = this.getPegRowCol(index);
        return this.getPeg(rowCol[0], rowCol[1]);
    };


    this.drawBoard = function (canvasElement) {
        if (!canvasElement) { throw "canvasElement cannot be null for drawBoard"; }

        var canvasCtx = canvasElement[0].getContext("2d");
        if (!canvasCtx) { throw "canvasCtx cannot be null for drawBoard"; }

        this.drawTriangle(canvasElement, canvasCtx);
        this.drawGridLines(canvasElement, canvasCtx);
        this.drawPegHoles(canvasElement, canvasCtx);
    };

    this.drawTriangle = function (canvasElement, canvasCtx) {
        
    };

    this.drawGridLines = function (canvasElement, canvasCtx) {

    };

    this.drawPegHoles = function (canvasElement, canvasCtx) {

    };
};



