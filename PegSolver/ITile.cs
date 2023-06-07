namespace PegSolver {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A tile is a spot on the board where a Peg can go.
    /// This is an immutable object.
    /// </summary>
    public interface ITile {
        int Row { get; }
        
        int Col { get; }

        bool? HasPeg { get; }

        IBoard Board { get; }
    }
}
