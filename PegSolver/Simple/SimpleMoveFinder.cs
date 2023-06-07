namespace PegSolver.Simple {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MoveFinder : IMoveFinder {
        public IList<IBoard> FindMoves(IBoard board) {
            if (board == null) { throw new ArgumentNullException("board"); }

            IList<IBoard> moves = new List<IBoard>();

            IList<ITile> allTiles = board.GetAllTiles();
            // 6 possible moves:
            //    (r−2, c−2)
            //    (r−2, c)
            //    (r, c+2)
            //    (r+2, c+2)
            //    (r+2, c)
            //    (r, c−2)

            allTiles.ToList().ForEach(tile => {
                // if there is no tile in the current tile then just continue, no move here
                if (tile.HasPeg.Value) {
                    int row = tile.Row;
                    int col = tile.Col;

                    List<ITile> possibleMoves = new List<ITile>{
                        new Tile(row-2, col-2,board),
                        new Tile(row-2, col,board),
                        new Tile(row, col+2,board),
                        new Tile(row+2, col+2,board),
                        new Tile(row+2, col,board),
                        new Tile(row, col-2,board),
                    };

                    possibleMoves.ForEach(pm => {
                        if (board.IsMoveSourceValid(board,tile.Row,tile.Col) && 
                            board.IsMoveDestValid(board, tile, pm, allTiles)) {
                            moves.Add(
                                board.PlayMove(tile, pm));
                        }
                    });
                }
            });

            return moves;
        }

        /// <summary>
        /// Detemines if the destination tile is valid to move a peg there.
        /// internal for testing.
        /// </summary>
        //protected internal virtual bool IsMoveDestValid(IBoard board, int row, int col,IList<ITile>allTiles) {
        //    if (board == null) { throw new ArgumentNullException("board"); }

        //    bool isValid = false;
        //    if (row >= 0 && col >= 0 && col <= row) {
        //        // now see if the tile is empty or not
        //        ITile destTile = (from t in allTiles
        //                          where t.Row == row && t.Col == col
        //                          select t).Single();

        //        isValid = !destTile.HasPeg;
        //    }

        //    return isValid;
        //}

        /// <summary>
        /// Plays the given move on the board.
        /// This does NOT modify the source board, just returns a new board which has been modified.
        /// </summary>
        protected internal IBoard PlayMove(IBoard board, ITile source, ITile dest) {
            throw new NotImplementedException();
        }
    }
}
