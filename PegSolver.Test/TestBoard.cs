namespace PegSolver.Test {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestBoard {

        #region Build board tests
        [TestMethod]
        public void Test_Build_FromString_DefaultBoard() {
            IBoard board = new Board(BoardStrings.DefaultBoard);

            Assert.IsNotNull(board);
            Assert.AreEqual(4, board.Size);

            Assert.AreEqual(false, board[0, 0]);
            Assert.AreEqual(false, board[0, 1]);
            Assert.AreEqual(false, board[0, 2]);
            Assert.AreEqual(false, board[0, 3]);

            Assert.AreEqual(true, board[1, 0]);
            Assert.AreEqual(true, board[1, 1]);
            Assert.AreEqual(false, board[1, 2]);
            Assert.AreEqual(false, board[1, 3]);

            Assert.AreEqual(true, board[2, 0]);
            Assert.AreEqual(true, board[2, 1]);
            Assert.AreEqual(true, board[2, 2]);
            Assert.AreEqual(false, board[2, 3]);

            Assert.AreEqual(true, board[3, 0]);
            Assert.AreEqual(true, board[3, 1]);
            Assert.AreEqual(true, board[3, 2]);
            Assert.AreEqual(true, board[3, 3]);
        }

        [TestMethod]
        public void Test_Build_FromString_FullBoard() {
            IBoard board = new Board(BoardStrings.FullBoard);

            Assert.IsNotNull(board);
            Assert.AreEqual(4, board.Size);
            for (int i = 0; i < board.Size; i++) {
                for (int j = 0; j < board.Size; j++) {
                    Assert.AreEqual(true, board[i, j]);
                }
            }
        }

        [TestMethod]
        public void Test_Build_FromString_EmptyBoard() {
            IBoard board = new Board(BoardStrings.EmptyBoard);

            Assert.IsNotNull(board);
            Assert.AreEqual(4, board.Size);
            for (int i = 0; i < board.Size; i++) {
                for (int j = 0; j < board.Size; j++) {
                    Assert.AreEqual(false, board[i, j]);
                }
            }
        }
        #endregion

        #region GetBoardString tests
        [TestMethod]
        public void Test_GetBoardString_DefaultBoard() {
            string sourceBoard = BoardStrings.DefaultBoard;
            IBoard board = new Board(sourceBoard);
            string boardString = board.GetBoardString();
            Assert.IsTrue(string.Compare(sourceBoard, boardString, StringComparison.OrdinalIgnoreCase) == 0);
        }

        [TestMethod]
        public void Test_GetBoardString_FullBoard() {
            string sourceBoard = BoardStrings.FullBoard;
            IBoard board = new Board(sourceBoard);
            string boardString = board.GetBoardString();
            Assert.IsTrue(string.Compare(sourceBoard, boardString, StringComparison.OrdinalIgnoreCase) == 0);
        }

        [TestMethod]
        public void Test_GetBoardString_EmptyBoard() {
            string sourceBoard = BoardStrings.EmptyBoard;
            IBoard board = new Board(sourceBoard);
            string boardString = board.GetBoardString();
            Assert.IsTrue(string.Compare(sourceBoard, boardString, StringComparison.OrdinalIgnoreCase) == 0);
        }
        #endregion

        #region GetAllTiles tests
        [TestMethod]
        public void Test_GetAllTiles_DefaultBoard() {
            string boardStr = BoardStrings.DefaultBoard;
            IBoard board = new Board(boardStr);
            IList<ITile> tiles = board.GetAllTiles();

            Assert.IsNotNull(tiles);
            Assert.AreEqual(10, tiles.Count);

            List<ITile> expectedTiles = new List<ITile>() {
                new Tile(0,0,board,false),
                new Tile(1,0,board,true),
                new Tile(1,1,board,true),
                new Tile(2,0,board,true),
                new Tile(2,1,board,true),
                new Tile(2,2,board,true),
                new Tile(3,0,board,true),
                new Tile(3,1,board,true),
                new Tile(3,2,board,true),
                new Tile(3,3,board,true),
            };

            tiles.ToList().ForEach(tile => {
                // make sure its on the list of expected tiles and then remove it from that list
                var result = (from et in expectedTiles
                              where tile.Equals(et)
                              select et).SingleOrDefault();

                Assert.IsNotNull(result);
                expectedTiles.Remove(result);
            });

            // we should have removed all tiles by now
            Assert.AreEqual(0, expectedTiles.Count);
        }
        #endregion

        #region GetPegsRemaining test
        [TestMethod]
        public void Test_GetPegsRemaining_DefaultBoard() {
            string boardString = BoardStrings.DefaultBoard;
            int numExpectedPegs = 9;

            IBoard board = new Board(boardString);
            Assert.AreEqual(numExpectedPegs, board.GetPegsRemaining());
        }
        
        [TestMethod]
        public void Test_GetPegsRemaining_FullBoard() {
            string boardString = BoardStrings.FullBoard;
            int numExpectedPegs = 10;

            IBoard board = new Board(boardString);
            Assert.AreEqual(numExpectedPegs, board.GetPegsRemaining());
        }

        [TestMethod]
        public void Test_GetPegsRemaining_EmptyBoard() {
            string boardString = BoardStrings.EmptyBoard;
            int numExpectedPegs = 0;

            IBoard board = new Board(boardString);
            Assert.AreEqual(numExpectedPegs, board.GetPegsRemaining());
        }
        #endregion
    }


    public static class BoardStrings {
        public const string DefaultBoard = "0000;1100;1110;1111;";
        public const string FullBoard = "1111;1111;1111;1111;";
        public const string EmptyBoard = "0000;0000;0000;0000;";

        public const string Board_1Move_01 = "0000;1000;1000;0000;";
        public const string Board_1Move_02 = "0000;0000;0110;0000;";

        public const string Board_2Moves_01 = "0000;1000;0110;0000;";
        public const string Board_2Moves_02 = "0000;0000;0100;0011;";

        public const string Board_3Moves_01 = "1000;1100;0100;0000;";
        public const string Board_3Moves_02 = "0000;0000;0100;1101";

        public const string Board_4Moves_01 = "1000;0100;1100;1000;";

        public const string Board_5Moves_01 = "1000;0100;1100;0110;";


        public const string Board_Standard = "00000;11000;11100;11110;11111";

        public const string Board_Size_7 = "0000000;1100000;1110000;1111000;1111100;1111110;1111111";
    }
}
