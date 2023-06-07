namespace PegSolver.Test {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestTile {
        [TestMethod]
        public void Test_Equals_True_01() {
            IBoard board = new Board(4);
            ITile tile01 = new Tile(1, 1, board, true);
            ITile tile02 = new Tile(1, 1, board,true);

            Assert.IsTrue(tile01.Equals(tile02));
        }

        [TestMethod]
        public void Test_Equals_True_02() {
            IBoard board = new Board(4);
            IBoard board2 = new Board(4);
            ITile tile01 = new Tile(4, 4, board,false);
            ITile tile02 = new Tile(4, 4, board2,false);

            Assert.IsTrue(tile01.Equals(tile02));
        }

        [TestMethod]
        public void Test_Equals_False_DifferentRow() {
            IBoard board = new Board(4);
            ITile tile01 = new Tile(1, 1, board,true);
            ITile tile02 = new Tile(2, 1, board,true);

            Assert.IsFalse(tile01.Equals(tile02));
        }

        [TestMethod]
        public void Test_Equals_False_DifferentCol() {
            IBoard board = new Board(4);
            ITile tile01 = new Tile(4, 4, board,true);
            ITile tile02 = new Tile(4, 2, board,true);

            Assert.IsFalse(tile01.Equals(tile02));
        }

        [TestMethod]
        public void Test_Equals_False_DifferentHasPeg() {
            IBoard board = new Board(4);
            ITile tile01 = new Tile(5, 5,board, false);
            ITile tile02 = new Tile(5, 5, board, true);

            Assert.IsFalse(tile01.Equals(tile02));
        }

        [TestMethod]
        public void Test_Equals_False_DifferentRowCol() {
            IBoard board = new Board(4);
            ITile tile01 = new Tile(3, 3, board,true);
            ITile tile02 = new Tile(2, 1,board,true);

            Assert.IsFalse(tile01.Equals(tile02));
        }

        [TestMethod]
        public void Test_Equals_False_DifferentRowHasPeg() {
            IBoard board = new Board(4);
            ITile tile01 = new Tile(4, 1, board,true);
            ITile tile02 = new Tile(2, 1, board,false);

            Assert.IsFalse(tile01.Equals(tile02));
        }
    }
}
