namespace PegSolver.Test {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestMoveStack {
        [TestMethod]
        public void TestMoveStack_Basic() {
            MoveStack moveStack = new MoveStack();
            moveStack.Moves.Push(new Board(BoardStrings.DefaultBoard));
            moveStack.Moves.Push(new Board(BoardStrings.EmptyBoard));
            moveStack.Moves.Push(new Board(BoardStrings.FullBoard));
        
            string expectedBoardString = BoardStrings.FullBoard;
            Assert.IsTrue(string.Compare(expectedBoardString, moveStack.Moves.Pop().GetBoardString()) == 0);

            expectedBoardString = BoardStrings.EmptyBoard;
            Assert.IsTrue(string.Compare(expectedBoardString, moveStack.Moves.Pop().GetBoardString()) == 0);

            expectedBoardString = BoardStrings.DefaultBoard;
            Assert.IsTrue(string.Compare(expectedBoardString, moveStack.Moves.Pop().GetBoardString()) == 0);
        }

        [TestMethod]
        public void TestClone_02() {
            MoveStack moveStack = new MoveStack();
            moveStack.Moves.Push(new Board(BoardStrings.DefaultBoard));
            moveStack.Moves.Push(new Board(BoardStrings.EmptyBoard));
            moveStack.Moves.Push(new Board(BoardStrings.FullBoard));

            MoveStack cloned = (MoveStack)moveStack.Clone(); 
            Assert.IsNotNull(cloned);

            string expectedBoardString = BoardStrings.FullBoard;
            Assert.IsTrue(string.Compare(expectedBoardString, cloned.Moves.Pop().GetBoardString()) == 0);

            expectedBoardString = BoardStrings.EmptyBoard;
            Assert.IsTrue(string.Compare(expectedBoardString, cloned.Moves.Pop().GetBoardString()) == 0);

            expectedBoardString = BoardStrings.DefaultBoard;
            Assert.IsTrue(string.Compare(expectedBoardString, cloned.Moves.Pop().GetBoardString()) == 0);
        }

    }
}
