namespace PegSolver.Simple {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BoardEvaluator : IBoardEvaluator {
        public BoardEvaluator(IMoveFinder moveFinder) {
            if (moveFinder == null) { throw new ArgumentNullException("moveFinder"); }
            this.MoveFinder = moveFinder;
        }

        private IMoveFinder MoveFinder { get; set; }

        public void ScoreBoard(IBoard board) {
            if (board == null) { throw new ArgumentNullException("board"); }

            // find the moves
            IList<IBoard> moves = this.MoveFinder.FindMoves(board);
            ScoreState scoreState = this.GetBoardState(board);
            int numericScore = int.MinValue;
            int pegsRemaining = board.GetPegsRemaining();

            if (scoreState == ScoreState.Ended) {
                if (pegsRemaining == 1) {
                    scoreState = ScoreState.GameWon;
                    // best possible score
                    numericScore = int.MaxValue;
                }
                //else {
                //    // want to make sure that boards with moves remaing have a higher score than solved boards with > 1 peg
                //    numericScore = -pegsRemaining;
                //}
            }

            if (scoreState != ScoreState.GameWon) {
                // TODO: This is how it should be done *************
                // numericScore = -pegsRemaining;

                numericScore = -moves.Count();
            }

            board.Score = new Score(scoreState, numericScore);
        }

        public ScoreState GetBoardState(IBoard board) {
            if (board == null) { throw new ArgumentNullException("board"); }

            ScoreState state = ScoreState.Inprogress;
            
            // if there are no empty slots or no pegs then its invalid
            IList<ITile> allTiles = board.GetAllTiles();
            var emptyTiles = from t in allTiles
                             where !t.HasPeg.Value
                             select t;
            var occupiedTiles = from t in allTiles
                                where t.HasPeg.Value
                                select t;


            if (emptyTiles.Count() <= 0 || occupiedTiles.Count() <= 0) {
                state = ScoreState.Invalid;
            }
            else {
                // if there are no more moves then the board is solved
                IList<IBoard> moves = this.MoveFinder.FindMoves(board);
                if (moves.Count == 0) {
                    state = ScoreState.Ended;
                }
            }

            return state;
        }
    }
}