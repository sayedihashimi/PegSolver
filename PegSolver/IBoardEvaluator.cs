namespace PegSolver {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IBoardEvaluator {
        /// <summary>
        /// Assigns the score of the board passed in.
        /// </summary>
        void ScoreBoard(IBoard board);
    }
}
