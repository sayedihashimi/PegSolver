namespace PegSolver.Test {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PegSolver.Simple;

    [TestClass]
    public class TestSimplePegSolver {
        [TestMethod]
        public void Test_1Move_ToWin_01() {
            string boardString = BoardStrings.Board_1Move_01;
            IPegSolver simplePegSolver = this.CreatePegSolver();

            MoveStack moveList = simplePegSolver.Solve(new Board(boardString));

            Assert.IsNotNull(moveList);
            Assert.IsTrue(moveList.Moves.Count == 2);
            Assert.IsTrue(moveList.BestScore.State == ScoreState.GameWon);
        }

        [TestMethod]
        public void Test_1Move_ToWin_02() {
            string boardString = BoardStrings.Board_1Move_02;
            IPegSolver simplePegSolver = this.CreatePegSolver();

            MoveStack moveList = simplePegSolver.Solve(new Board(boardString));

            Assert.IsNotNull(moveList);
            Assert.IsTrue(moveList.Moves.Count == 2);
            Assert.IsTrue(moveList.BestScore.State == ScoreState.GameWon);
        }

        [TestMethod]
        public void Test_2Moves_ToWin_01() {
            string boardString = BoardStrings.Board_2Moves_01;
            IPegSolver simplePegSolver = this.CreatePegSolver();

            MoveStack moveList = simplePegSolver.Solve(new Board(boardString));

            Assert.IsNotNull(moveList);
            Assert.IsTrue(moveList.Moves.Count == 3);
            Assert.IsTrue(moveList.BestScore.State == ScoreState.GameWon);
        }

        [TestMethod]
        public void Test_2Moves_ToWin_02() {
            string boardString = BoardStrings.Board_2Moves_02;
            IPegSolver simplePegSolver = this.CreatePegSolver();

            MoveStack moveList = simplePegSolver.Solve(new Board(boardString));

            Assert.IsNotNull(moveList);
            Assert.IsTrue(moveList.Moves.Count == 3);
            Assert.IsTrue(moveList.BestScore.State == ScoreState.GameWon);
        }

        [TestMethod]
        public void Test_3Moves_ToWin_01() {
            string boardString = BoardStrings.Board_3Moves_01;
            IPegSolver simplePegSolver = this.CreatePegSolver();

            MoveStack moveList = simplePegSolver.Solve(new Board(boardString));

            Assert.IsNotNull(moveList);
            Assert.IsTrue(moveList.Moves.Count == 4);
            Assert.IsTrue(moveList.BestScore.State == ScoreState.GameWon);
        }

        [TestMethod]
        public void Test_3Moves_ToWin_02() {
            string boardString = BoardStrings.Board_3Moves_02;
            IPegSolver simplePegSolver = this.CreatePegSolver();

            MoveStack moveList = simplePegSolver.Solve(new Board(boardString));

            Assert.IsNotNull(moveList);
            Assert.IsTrue(moveList.Moves.Count == 4);
            Assert.IsTrue(moveList.BestScore.State == ScoreState.GameWon);
        }

        [TestMethod]
        public void Test_4Moves_ToWin_01() {
            string boardString = BoardStrings.Board_4Moves_01;
            IPegSolver simplePegSolver = this.CreatePegSolver();

            MoveStack moveList = simplePegSolver.Solve(new Board(boardString));

            Assert.IsNotNull(moveList);
            Assert.IsTrue(moveList.Moves.Count == 5);
            Assert.IsTrue(moveList.BestScore.State == ScoreState.GameWon);
        }

        [TestMethod]
        public void Test_5Moves_ToWin_01() {
            string boardString = BoardStrings.Board_5Moves_01;
            IPegSolver simplePegSolver = this.CreatePegSolver();

            MoveStack moveList = simplePegSolver.Solve(new Board(boardString));

            Assert.IsNotNull(moveList);
            Assert.IsTrue(moveList.Moves.Count == 6);
            Assert.IsTrue(moveList.BestScore.State == ScoreState.GameWon);
        }

        [TestMethod]
        public void Test_DefaultBoard() {
            string boardString = BoardStrings.Board_Standard;
            IPegSolver simplePegSolver = this.CreatePegSolver();

            MoveStack moveList = simplePegSolver.Solve(new Board(boardString));

            string moveLog = SimplePegSolver.MoveLogger.ToString();

            Assert.IsNotNull(moveList);
            Assert.IsTrue(moveList.BestScore.State == ScoreState.GameWon);

            //StringBuilder result = new StringBuilder();
            //foreach (IBoard move in moveList.Moves) {
            //    result.AppendLine(move.GetBoardString(BoardStringType.Expanded));
            //}

            //string fullList = result.ToString();
        }

        [TestMethod,Ignore]
        public void Test_Board_Size7_01() {
            string boardString = BoardStrings.Board_Size_7;
            IPegSolver simplePegSolver = this.CreatePegSolver();

            MoveStack moveList = simplePegSolver.Solve(new Board(boardString));

            string moveLog = SimplePegSolver.MoveLogger.ToString();

            Assert.IsNotNull(moveList);
            Assert.IsTrue(moveList.BestScore.State == ScoreState.GameWon);

            //StringBuilder result = new StringBuilder();
            //foreach (IBoard move in moveList.Moves) {
            //    result.AppendLine(move.GetBoardString(BoardStringType.Expanded));
            //}

            //string fullList = result.ToString();
        }

        protected IPegSolver CreatePegSolver() {
            IMoveFinder moveFinder = new MoveFinder();
            return new SimplePegSolver(moveFinder, new BoardEvaluator(moveFinder));
        }
    }

}
