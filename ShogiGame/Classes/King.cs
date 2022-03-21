using ShogiGame.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Classes
{
    public class King : Piece
    {
        public King(BigInteger state) : base(state)
        {
            image = Image.FromFile("C:/nisayon2/ShogiGame/ShogiGame/Resources/Images/Western/1.png");
        }

        public override BigInteger getPlacesToMove(BigInteger from, Board board)
        {
            BigInteger kingMoveOptions = KingMoveOptions(from, board);
            BigInteger allThePossibleMovesOfTheOtherPlayer = board.getOtherPlayer().GetAllThePossibleMovesOfAllThePiecesOfTheOtherPlayer(board);
            return kingMoveOptions & (~allThePossibleMovesOfTheOtherPlayer);
        }

        public static BigInteger KingMoveOptions(BigInteger from, Board board)
        {
            BigInteger allThePiecesLocationOfTheCurrentPlayer = board.Turn.GetAllPiecesLocations();
            BigInteger moveOptionResult = 0;
            if (!Board.isLocatedInTopRow(from))
                moveOptionResult |= from >> Constants.ROWS_NUMBER;
            if (!Board.isLocatedInTopRow(from) && !Board.isLocatedInRightColumn(from))
                moveOptionResult |= from >> Constants.ROWS_NUMBER - 1;
            if (!Board.isLocatedInTopRow(from) && !Board.isLocatedInLeftColumn(from))
                moveOptionResult |= from >> Constants.ROWS_NUMBER + 1;
            if (!Board.isLocatedInRightColumn(from))
                moveOptionResult |= from << 1;
            if (!Board.isLocatedInLeftColumn(from))
                moveOptionResult |= from >> 1;
            if (!Board.isLocatedInBottomRow(from))
                moveOptionResult |= from << Constants.ROWS_NUMBER;
            if (!Board.isLocatedInBottomRow(from) && !Board.isLocatedInRightColumn(from))
                moveOptionResult |= from << Constants.ROWS_NUMBER + 1;
            if (!Board.isLocatedInBottomRow(from) && !Board.isLocatedInLeftColumn(from))
                moveOptionResult |= from << Constants.ROWS_NUMBER - 1;
            return moveOptionResult & (~allThePiecesLocationOfTheCurrentPlayer);
        }
    }
}
