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
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/1.png");
            pieceScore = 10000;
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                -30,-10,-10,-10,-10,-10,-10,-10,-30,
                -10, -5,  0, -5,  0, -5,  0, -5,-10,
                -10,  0,-10,  0, -5,  0,-10,  0,-10,
                -10, -5,  0, -5,  5, -5,  0, -5,-10,
                -10, -5, -5, -5, -5, -5, -5, -5,-10,
                -10, -5, 10, -5, 20, -5, 10, -5,-10,
                -10, 10,-10, 20,-10, 20,-10, 10,-10,
                -10, -5, 20, -5, 20, -5, 20, -5,-10,
                -30,-10,-10,-10,-10,-10,-10,-10,-30
            };
        }

        public override BigInteger getPlacesToMove(BigInteger from, Board board)
        {
            if (from == 0)
                return 0;
            BigInteger kingMoveOptions = KingMoveOptions(from, board);
            
            Board copyBoard = new Board(board);  // Copy Constractor
            copyBoard.Turn.PiecesLocation[0].State = 0;
            BigInteger allThePossibleMovesOfTheOtherPlayer = copyBoard.GetAllThePossibleMovesOfAllThePiecesOfTheOtherPlayer();
            Player otherPlayer = copyBoard.getOtherPlayer();
            BigInteger maskToRemovePiecesAroundTheKing = kingMoveOptions ^ Constants.BITBOARD_OF_ONE;
            for (int i = 0; i < otherPlayer.PiecesLocation.Length; i++)
                otherPlayer.PiecesLocation[i].State &= maskToRemovePiecesAroundTheKing;

            allThePossibleMovesOfTheOtherPlayer |= copyBoard.GetAllThePossibleMovesOfAllThePiecesOfTheOtherPlayer();
            return kingMoveOptions & (~allThePossibleMovesOfTheOtherPlayer);
        }

        public static BigInteger KingMoveOptions(BigInteger from, Board board)
        {
            if (from == 0)
                return 0;
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
