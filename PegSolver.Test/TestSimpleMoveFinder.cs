namespace PegSolver.Test {
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PegSolver.Simple;

    [TestClass]
    public class TestSimpleMoveFinde {

        [TestMethod]
        public void TestFindMoves_DefaultBoard() {
            IBoard board = new Board(BoardStrings.DefaultBoard);
            IMoveFinder moveFinder = new MoveFinder();

            IList<IBoard> moves = moveFinder.FindMoves(board);
            Assert.AreEqual(2, moves.Count);

            List<string> moveStrings = new List<string>();
            moves.ToList().ForEach(mv => {
                moveStrings.Add(mv.GetBoardString());
            });

            moveStrings.Sort();

            List<string> expectedMoves = new List<string>(){
                "1000;0100;0110;1111;",
                "1000;1000;1100;1111;",
            };

            Assert.AreEqual(expectedMoves.Count, moveStrings.Count);
            for (int i = 0; i < expectedMoves.Count; i++) {
                string eString = expectedMoves[i];
                string actualStr = moveStrings[i];
                Assert.AreEqual(eString, actualStr, StringComparison.OrdinalIgnoreCase == 0);
            }
        }
    }
}
