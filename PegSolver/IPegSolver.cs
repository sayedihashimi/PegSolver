namespace PegSolver {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IPegSolver {
        /// <summary>
        /// Solves the board and returns the list of moves it took to get there
        /// </summary>
        MoveStack Solve(IBoard board);

        bool IsSolved(IBoard board);
    }

    public interface IMoveFinder {
        IList<IBoard> FindMoves(IBoard board);
    }
}
