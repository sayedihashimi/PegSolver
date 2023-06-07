namespace PegSolver.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using PegSolver.Mvc.Models;
    using PegSolver.Simple;

    public class BoardController : Controller
    {
        public ActionResult Index() {
            BoardViewModel bvm = this.BuildDefaultBoard();
            return View(bvm);
        }

        //[HttpGet]
        //[HttpPost]
        public ActionResult SolveBoard(string boardString) {
            string debugStr = boardString;
            Board board = new Board(boardString);
            IPegSolver pegSolver = this.CreateNewPegSolver();
            MoveStack moves = pegSolver.Solve(board);

            SolvedBoardViewModel solvedBoard = new SolvedBoardViewModel {
                OriginalBoard = board,
                MovesToSolve = moves,
            };

            return View(solvedBoard);
        }

        private BoardViewModel BuildDefaultBoard() {
            return new BoardViewModel(new Board("00000;11000;11100;11110;11111"));
        }

        private IPegSolver CreateNewPegSolver() {
            IMoveFinder moveFinder = new MoveFinder();
            return new SimplePegSolver(moveFinder, new BoardEvaluator(moveFinder));
        }
    }
}
