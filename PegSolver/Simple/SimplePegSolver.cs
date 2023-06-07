namespace PegSolver.Simple {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using PegSolver.Exceptions;
    //using PegSolver.Exceptions;

    public class SimplePegSolver : IPegSolver {
        // max num of moves allowed, will ensure we dont cause StackOverflow
        private static int MoveLimit = 100000;
        private int CurrentMoveCount { get; set; }
        public static StringBuilder MoveLogger = new StringBuilder();

        public SimplePegSolver(IMoveFinder moveFinder,IBoardEvaluator boardEvaluator) {
            if (moveFinder == null) { throw new ArgumentNullException("moveFinder"); }
            this.MoveFinder = moveFinder;
            this.BoardEvaluator = boardEvaluator;

            this.CurrentMoveCount = 0;
        }

        private IMoveFinder MoveFinder { get; set; }
        private IBoardEvaluator BoardEvaluator { get; set; }

        public MoveStack Solve(IBoard board) {
            if (board == null) { throw new ArgumentNullException("board"); }
            if (board.Score == null) { this.BoardEvaluator.ScoreBoard(board); }

            SimplePegSolver.MoveLogger = new StringBuilder();

            MoveStack bestMoves = new MoveStack();
            bestMoves.Moves.Push(board);
            bestMoves = this.FindWinningMoveSequence(board, bestMoves);

            // get the list of moves available
            //IList<IBoard> moves = this.MoveFinder.FindMoves(board);
            //moves = this.ScoreAndSortMoves(moves);

            
            //foreach (IBoard move in moves) {
            //    MoveStack moveStack = new MoveStack();
            //    moveStack.BestScore = board.Score;
            //    moveStack.Moves.Push(move);

            //    MoveStack result = this.FindWinningMoveSequence(move, moveStack);
            //    if (result.BestScore.State == ScoreState.GameWon) {
            //        bestMoves = result;
            //        return bestMoves;
            //    }

            //    if (bestMoves == null) {
            //        bestMoves = result;
            //    }

            //    if (result.BestScore.ScoreValue > bestMoves.BestScore.ScoreValue) {
            //        bestMoves = result;
            //    }
            //}

            return bestMoves;
        }

        protected MoveStack FindWinningMoveSequence(IBoard board, MoveStack previousMoves) {
            if (board == null) { throw new ArgumentNullException("board"); }
            if (previousMoves == null) { throw new ArgumentNullException("previousMoves"); }

            if (board.Score == null) { this.BoardEvaluator.ScoreBoard(board); }

            // log the move
            string indent = new string(' ', previousMoves.Moves.Count-1);
            SimplePegSolver.MoveLogger.AppendLine(string.Format("{0}\t{1}[{2}]: [{3}]: [{4}]",CurrentMoveCount,indent, previousMoves.Moves.Count, board.GetBoardString(),board.Score.ScoreValue));

            if (board.Score.State == ScoreState.GameWon) {
                return previousMoves;
            }

            CurrentMoveCount++;
            //if (CurrentMoveCount > SimplePegSolver.MoveLimit) {
            //    throw new ExecutedTooManyMovesException();
            //}

            MoveStack bestMoves = previousMoves;

            IList<IBoard> possibleMoves = this.MoveFinder.FindMoves(board);
            possibleMoves = this.ScoreAndSortMoves(possibleMoves);

            foreach (IBoard move in possibleMoves) {
                MoveStack nextPreviousMoves = (MoveStack)previousMoves.Clone();
                nextPreviousMoves.Moves.Push(move);

                if (CurrentMoveCount > SimplePegSolver.MoveLimit) {
                    return bestMoves;
                }

                MoveStack childResult = this.FindWinningMoveSequence(move, nextPreviousMoves);
                if (childResult.BestScore.ScoreValue > bestMoves.BestScore.ScoreValue) {
                    bestMoves = childResult;
                }

                if (bestMoves.BestScore.State == ScoreState.GameWon) {
                    return bestMoves;
                }
            }

            return bestMoves;
        }

        protected MoveStack FindWinningMoveSequence_OLD(IBoard board, MoveStack previousMoves) {
            if (board == null) { throw new ArgumentNullException("board"); }
            if (previousMoves == null) { throw new ArgumentNullException("previousMoves"); }

            // log the move
            SimplePegSolver.MoveLogger.AppendLine(string.Format("[{0}]: [{1}]", previousMoves.Moves.Count, board.GetBoardString()));

            if (board.Score == null) {
                this.BoardEvaluator.ScoreBoard(board);
            }

            IList<IBoard> possibleMoves = this.MoveFinder.FindMoves(board);
            possibleMoves = this.ScoreAndSortMoves(possibleMoves);

            MoveStack bestMoves = previousMoves;
            //if (bestMoves.BestScore == null) {
            //    bestMoves.BestScore = board.Score;
            //}

            foreach (IBoard move in possibleMoves) {
                CurrentMoveCount++;
                if (CurrentMoveCount > SimplePegSolver.MoveLimit) {
                    throw new ExecutedTooManyMovesException();
                }
                MoveStack currentMoves = (MoveStack)previousMoves.Clone();
                currentMoves.Moves.Push(move);
                // currentMoves.BestScore = move.Score;

                if (move.Score.State == ScoreState.GameWon) {
                    bestMoves = currentMoves;
                    bestMoves.BestScore.State = ScoreState.GameWon;
                    return bestMoves;
                }


                if (currentMoves.BestScore.ScoreValue > bestMoves.BestScore.ScoreValue) {
                    bestMoves = currentMoves;
                }

                // the game is not yet won, recurse now              
                MoveStack bestSubMoves = this.FindWinningMoveSequence(move, currentMoves);
                if (bestSubMoves.BestScore.ScoreValue > currentMoves.BestScore.ScoreValue) {
                    bestMoves = bestSubMoves;
                }

                if (bestMoves.BestScore.State == ScoreState.GameWon) {
                    return bestMoves;
                }
            }

            return bestMoves;
        }

        public IList<IBoard> FindBestMoveSequence(IBoard board) {
            IList<IBoard> bestMoves = new List<IBoard>();

            // depth first search for the best move sequence
            IList<IBoard> moves = this.MoveFinder.FindMoves(board);
            moves = this.ScoreAndSortMoves(moves);
            foreach (IBoard move in moves) {
                // if the board is won stop processing moves
                if (move.Score.State == ScoreState.GameWon) {

                }
            }

            return bestMoves;
        }

        protected internal IList<IBoard> ScoreAndSortMoves(IList<IBoard> moves) {
            if (moves == null) { throw new ArgumentNullException("moves"); }
            // we want to sort the moves so that the highest score is on top
            List<IBoard> sortedList = new List<IBoard>();
            moves.ToList().ForEach(move => {
                this.BoardEvaluator.ScoreBoard(move);        
                sortedList.Add(move);
            });

            sortedList.Sort(new BoardComparer());

            return sortedList;
        }

        public bool IsSolved(IBoard board) {
            throw new NotImplementedException();
        }

        protected internal class BoardComparer : IComparer<IBoard> {
            public int Compare(IBoard x, IBoard y) {
                int result = 0;

                if (x.Score.ScoreValue == y.Score.ScoreValue) {
                    result = 0;
                }
                else if (x.Score.ScoreValue > y.Score.ScoreValue) {
                    result = 1;
                }
                else {
                    result = -1;
                }

                return result;
            }
        }
    }
}
