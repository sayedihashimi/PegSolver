namespace PegSolver {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Score : IScore {
        public Score(ScoreState state, int scoreValue) {
            this.State = state;
            this.ScoreValue = scoreValue;
        }
        public int ScoreValue { get; private set; }

        public ScoreState State{get; set;}
    }

    public interface IScore {
        int ScoreValue { get; }

        ScoreState State { get; }
    }

    public enum ScoreState {
        Inprogress,
        Ended,
        Invalid,
        GameWon
    }
}
