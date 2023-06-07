namespace PegSolver.Mvc.Models {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Script.Serialization;
    using System.Text;

    public class SolvedBoardViewModel {
        public SolvedBoardViewModel() { }

        private MoveStack movesToSolve{get;set;}

        public Board OriginalBoard { get; set; }
        public MoveStack MovesToSolve {
            get { return this.movesToSolve; }
            set {
                this.movesToSolve = value;
                this.UpdateMoves();
            }
        }

        public string MovesJson { get; private set; }
        public List<string> MovesToSolveAsStrings { get; private set; }
        
        private void UpdateMoves(){
            if (this.MovesToSolve != null) {

                StringBuilder jsMovesSb = new StringBuilder();
                jsMovesSb.Append(@"var moves = new Array()");
                jsMovesSb.Append(Environment.NewLine);

                int index = 0;

                this.MovesToSolveAsStrings = new List<string>();
                foreach (var m in this.MovesToSolve.Moves.Reverse()) {
                    this.MovesToSolveAsStrings.Add(m.GetBoardString().Replace('"','\''));
                    jsMovesSb.AppendFormat("moves[{0}] = \"{1}\"", index++, m.GetBoardString());
                    jsMovesSb.Append(Environment.NewLine);
                }

                this.MovesJson = jsMovesSb.ToString();

                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //this.MovesJson = serializer.Serialize(MovesToSolveAsStrings);
            }
            else {
                this.MovesJson = null;
            }
        }
                
        private void Delete() {
            foreach (var m in MovesToSolve.Moves) {
                m.GetBoardString();
            }
        }
    }
}