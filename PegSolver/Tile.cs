namespace PegSolver {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tile : ITile {
        public Tile(int row, int col, IBoard board, bool? hasPeg = null) {
            if (board == null) { throw new ArgumentNullException("board"); }

            this.Row = row;
            this.Col = col;
            this.HasPeg = hasPeg;
            this.Board = board;
        }
        
        public int Row {
            get;
            private set;
        }

        public int Col {
            get;
            private set;
        }

        public bool? HasPeg {
            get;
            private set;
        }

        public IBoard Board {
            get;
            private set;
        }

        /// <summary>
        /// Computes based on:
        ///     Row
        ///     Col
        ///     HasPeg
        /// </summary>
        public override bool Equals(object obj) {
            bool areEqual = false;
            ITile other = obj as ITile;
            if (other != null) {
                if (this.Row == other.Row &&
                    this.Col == other.Col &&
                    this.HasPeg == other.HasPeg) {
                        areEqual = true;
                }
            }
            return areEqual;
        }

        public override int GetHashCode() {
            return this.Row + this.Col + this.HasPeg.GetHashCode();
        }
    }
}
