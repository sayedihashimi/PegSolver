namespace PegSolver {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IMoveStack : ICloneable {
        Stack<IBoard> Moves { get; }
        Score BestScore { get; }
    }

    public class MoveStack : IMoveStack {

        public MoveStack() {
            this.Moves = new Stack<IBoard>();
            // this.BestScore = new Score(ScoreState.Inprogress, int.MinValue);
        }

        public MoveStack(MoveStack moves) : this() {
            if (moves == null) { throw new ArgumentNullException("moves"); }
            
        }

        public Stack<IBoard> Moves { get; private set; }
        public Score BestScore { 
            get {
                Score result = null;
                if (this.Moves != null && this.Moves.Peek() != null) {
                    result = this.Moves.Peek().Score;
                }
                return result;
            } 
        }

        public object Clone() {
            MoveStack clone = new MoveStack();

            List<IBoard> moveList = this.Moves.ToList();
            moveList.Reverse();

            moveList.ForEach(mv => {
                clone.Moves.Push(mv);    
            });

            // clone.BestScore = new Score(this.BestScore.State, this.BestScore.ScoreValue);
            return clone;
        }
    }


}
