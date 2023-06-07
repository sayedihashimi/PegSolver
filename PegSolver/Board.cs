namespace PegSolver {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using PegSolver.Exceptions;

    [System.Diagnostics.DebuggerDisplay("{GetBoardString()}")]
    public class Board : IBoard {
        private bool[,] data;
        private int size;
        private IList<ITile> tiles;

        #region Constructors
        public Board(int size) { 
            this.ValidateSize(size);
            this.Size = size;
            this.data = new bool[this.Size,this.Size];
        }

        public Board(IBoard board) {
            if (board == null) { throw new ArgumentNullException("board"); }
            this.Size = board.Size;
            this.data = new bool[this.Size, this.Size];

            for (int i = 0; i < board.Size; i++) {
                for (int j = 0; j < board.Size; j++) {
                    this[i, j] = board[i, j];
                }
            }
        }
        
        /// <summary>
        /// String has 0 for empty and 1 for has a peg and rows are seperated by a ;
        /// </summary>
        public Board(string boardString) {
            this.InitalizeFromString(boardString);
        }

        public static Board CreateBoard(string boardString) {
            return new Board(boardString);
        }
        #endregion

        #region IBoard members
        public int Size {
            get { return this.size; }
            private set {
                this.ValidateSize(value);
                this.size = value;
            }
        }

        public bool this[int row, int col]{
            get {
                return this.data[row, col];
            }
            set{
                this.data[row, col] = value;
                this.tiles = null;
                this.Score = null;
            }
        }

        public Score Score {
            get;
            set;
        }

        public string GetBoardString(BoardStringType type = BoardStringType.Collapsed) {
            StringBuilder sb = new StringBuilder();

            if (type == BoardStringType.Collapsed) {
                for (int i = 0; i < this.Size; i++) {
                    for (int j = 0; j < this.Size; j++) {
                        sb.Append(this.data[i, j] ? "1" : "0");
                    }
                    sb.Append(";");
                }
            }
            else {
                for (int i = 0; i < this.Size; i++) {
                    for (int j = 0; j < this.Size; j++) {
                        sb.Append(this.data[i, j] ? "1" : "0");
                    }
                    sb.Append(Environment.NewLine);
                }
            }


            return sb.ToString();
        }

        public IList<ITile> GetAllTiles() {
            if (this.tiles == null) {
                this.tiles = new List<ITile>();

                for (int row = 0; row < this.Size; row++) {
                    for (int col = 0; col < row + 1; col++) {
                        ITile tile = new Tile(row, col, this, data[row, col]);
                        tiles.Add(tile);
                    }
                }
            }
            return this.tiles;
        }

        public int GetPegsRemaining() {
            // TODO: Could cache this along with GetAllTiles() result
            var occupiedTiles = from t in this.GetAllTiles()
                                where t.HasPeg.Value
                                select t;

            return occupiedTiles.Count();
        }

        /// <summary>
        /// Detemines if the destination tile is valid to move a peg there.
        /// </summary>
        //public virtual bool IsMoveDestValid(IBoard board, int destRow, int destCol, IList<ITile> allTiles) {
        //    if (board == null) { throw new ArgumentNullException("board"); }

        //    bool isValid = false;
        //    if (destRow >= 0 && destCol >= 0 && destCol <= destRow &&
        //        destRow < board.Size && destCol < board.Size) {
        //        // now see if the tile is empty or not
        //        ITile destTile = (from t in allTiles
        //                          where t.Row == destRow && t.Col == destCol
        //                          select t).Single();

        //        bool isDestValid = !destTile.HasPeg.Value;
        //        if (isDestValid) {
        //            // we need to check to ensure that the peg getting skipped is there
        //            int skipRow = (int)Math.Floor((double)((destRow + destTile.Row) / 2));
        //            int skipCol = (int)Math.Floor((double)((destCol + destTile.Col) / 2));
        //            isValid = board[skipRow, skipRow];
        //        }              
        //    }

        //    return isValid;
        //}

        public virtual bool IsMoveDestValid(IBoard board, ITile sourceTile, ITile destTile, IList<ITile> allTiles) {
            if (board == null) { throw new ArgumentNullException("board"); }

            bool isValid = false;
            int destRow = destTile.Row;
            int destCol = destTile.Col;
            if (destRow >= 0 && destCol >= 0 && destCol <= destRow &&
                destRow < board.Size && destCol < board.Size) {
                // now see if the tile is empty or not
                // TODO: Can we not just use destTime for this?
                ITile destTileFromBoard = (from t in allTiles
                                  where t.Row == destRow && t.Col == destCol
                                  select t).Single();

                bool isDestValid = !destTileFromBoard.HasPeg.Value;
                if (isDestValid) {
                    // we need to check to ensure that the peg getting skipped is there
                    int skipRow = (int)Math.Floor((double)((destRow + sourceTile.Row) / 2));
                    int skipCol = (int)Math.Floor((double)((destCol + sourceTile.Col) / 2));
                    isValid = board[skipRow, skipCol];
                }
            }

            return isValid;
        }

        public virtual bool IsMoveSourceValid(IBoard board, int row, int col) {
            if (board == null) { throw new ArgumentNullException("board"); }

            bool isValid = false;
            if (row >= 0 && col >= 0 && col <= row && board[row, col]) {
                isValid = true;
            }
            return isValid;
        }

        public IBoard PlayMove(ITile source, ITile dest) {
            if (source == null) { throw new ArgumentNullException("source"); }
            if (dest == null) { throw new ArgumentNullException("dest"); }

            if (!this.IsMoveDestValid(this, source, dest, this.GetAllTiles()) ||
                !this.IsMoveSourceValid(this, source.Row, source.Col)) {
                string message = message = string.Format(
                    "Invalid move: [row={0},col={1}] -> [row={2},col={3}]",
                    source.Row,
                    source.Col,
                    dest.Row,
                    dest.Col);

                throw new InvalidStateException(message);
            }

            int skipRow = (int)Math.Floor((double)((dest.Row + source.Row) / 2));
            int skipCol = (int)Math.Floor((double)((dest.Col + source.Col) / 2));

            Board newBoard = (Board)this.Clone();
            newBoard.data[source.Row, source.Col] = false;
            newBoard.data[dest.Row, dest.Col] = true;
            newBoard.data[skipRow, skipCol] = false;
            

            // TODO: Need to remove the peg which was jumped over

            return newBoard;
        }
        #endregion

        /// <summary>
        /// If its invalid an exception will be thrown
        /// </summary>
        protected virtual void ValidateSize(int size) {
            if (size < 4) {
                string message = string.Format("Invalid size specified for board: [{0}]",size);
                throw new InvalidSizeException(message);
            }
        }

        /// <summary>
        /// If the board is invalid an exception will be thrown
        /// </summary>
        protected virtual void ValidateBoard(IBoard board) {
            if (board == null) { throw new ArgumentNullException("board"); }

            this.ValidateSize(board.Size);
        }

        protected void InitalizeFromString(string boardString) {
            if (string.IsNullOrWhiteSpace(boardString)) { throw new ArgumentNullException("boardString"); }

            string[] rows = boardString.Split(';');

            if (rows == null || rows.Length < 0) {
                throw new InvalidStateException(string.Format("Invalid board string: [{0}]", boardString));
            }

            int rowLength = int.MinValue;
            IList<string> rowList = new List<string>();
            // the number of Rows must equal the number of columns, but we can validate that as we set these values
            for (int i = 0; i < rows.Length; i++) {
                string rowStr = rows[i];

                if (!string.IsNullOrEmpty(rowStr)) {
                    rowStr = rowStr.Trim(';').Replace(" ", "");
                    int currentRowLength = rowStr.Length;

                    if (rowLength < 0) {
                        rowLength = currentRowLength;
                    }

                    if (currentRowLength != rowLength) {
                        throw new InvalidStateException(string.Format("Invalid board string (un-even row length): [{0}]", boardString));
                    }

                    rowList.Add(rowStr);
                }
            }

            if (rowList.Count != rowLength) {
                string message = string.Format("Invalid board string (mis-matching row/col length): [{0}]", boardString);
                throw new InvalidStateException(message);
            }

            this.Size = rowLength;
            this.data = new bool[rowLength, rowLength];
            for (int i = 0; i < rowLength; i++) {
                string currentRowStr = rowList[i];
                char[] chars = currentRowStr.ToArray();
                for (int j = 0; j < rowLength; j++) {
                    this.data[i, j] = chars[j] == '0' ? false : true;
                }
            }
        }

        public object Clone() {
            IBoard board = new Board(this);
            return board;
        }
    }

    public enum BoardStringType {
        Collapsed,
        Expanded
    }
}