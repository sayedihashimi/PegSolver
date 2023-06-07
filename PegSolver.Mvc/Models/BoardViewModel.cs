namespace PegSolver.Mvc.Models {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class BoardViewModel {
        public BoardViewModel() { }
        
        public BoardViewModel(Board board) {
            this.Board = board;
        }

        public Board Board { get; set; }
    }
}