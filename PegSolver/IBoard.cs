namespace PegSolver {
    using System;
    using System.Collections.Generic;
    
    public interface IBoard : ICloneable {
        bool this[int row, int col] { get; set; }

        Score Score { get; set; }

        int Size { get; }

        string GetBoardString(BoardStringType type=BoardStringType.Collapsed);

        IList<ITile> GetAllTiles();

        bool IsMoveSourceValid(IBoard board, int row, int col);

        // bool IsMoveDestValid(IBoard board, int row, int col, IList<ITile> allTiles);
        bool IsMoveDestValid(IBoard board, ITile sourceTile, ITile destTile, IList<ITile> allTiles);


        IBoard PlayMove(ITile source, ITile dest);

        int GetPegsRemaining();
    }
}
