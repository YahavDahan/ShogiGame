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
    public class Gold : Piece
    {
        /// <summary>
        /// constructor for the piece type. initializes the Piece's features
        /// </summary>
        /// <param name="state">the state of the piece</param>
        public Gold(BigInteger state) : base(state)
        {
            image = Properties.Resources._4;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/4.png");
            pieceScore = 530;
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                -10, -5, -5, -5, -5, -5, -5, -5,-10,
                 -5,  0,  0,  0,  0,  0,  0,  0, -5,
                 -5,  0,  0, 20, 20, 20,  0,  0, -5,
                 -5,  0,  0,  0, 30,  0,  0,  0, -5,
                 -5, 20, 20,  5, 10,  5, 20, 20, -5,
                 -5,  0,  0, 10, 30, 10,  0,  0, -5,
                 -5,  0, 10, 20, 20, 20, 20,  0, -5,
                 -5,  0,  0,  0,  0,  0,  0,  0, -5,
                 -5,  0,  5,  0,  5,  0,  5,  0, -5
            };
        }

        /// <summary>
        /// constructor that Initializing the move score of the piece gold
        /// </summary>
        public Gold() : base() {
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                -10, -5, -5, -5, -5, -5, -5, -5,-10,
                 -5,  0,  0,  0,  0,  0,  0,  0, -5,
                 -5,  0,  0, 20, 20, 20,  0,  0, -5,
                 -5,  0,  0,  0, 30,  0,  0,  0, -5,
                 -5, 20, 20,  5, 10,  5, 20, 20, -5,
                 -5,  0,  0, 10, 30, 10,  0,  0, -5,
                 -5,  0, 10, 20, 20, 20, 20,  0, -5,
                 -5,  0,  0,  0,  0,  0,  0,  0, -5,
                 -5,  0,  5,  0,  5,  0,  5,  0, -5
            };
        }

        /// <summary>
		/// the functions finds all the possible moves of the current piece from specific location
		/// </summary>
		/// <param name="from">The location we want to get the move options from</param>
		/// <param name="board">the game board</param>
		/// <returns>the possible moves in BitBoard format</returns>
        public override BigInteger getPlacesToMove(BigInteger from, Board board)
        {
            Player currentPlayer = board.Turn;
            BigInteger allThePiecesLocationOfTheCurrentPlayer = currentPlayer.GetAllPiecesLocations();
            BigInteger moveOptionResult = 0;
            if (currentPlayer.IsPlayer1)
            {
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
            }
            else
            {
                if (!Board.isLocatedInBottomRow(from))
                    moveOptionResult |= from << Constants.ROWS_NUMBER;
                if (!Board.isLocatedInBottomRow(from) && !Board.isLocatedInRightColumn(from))
                    moveOptionResult |= from << Constants.ROWS_NUMBER + 1;
                if (!Board.isLocatedInBottomRow(from) && !Board.isLocatedInLeftColumn(from))
                    moveOptionResult |= from << Constants.ROWS_NUMBER - 1;
                if (!Board.isLocatedInRightColumn(from))
                    moveOptionResult |= from << 1;
                if (!Board.isLocatedInLeftColumn(from))
                    moveOptionResult |= from >> 1;
                if (!Board.isLocatedInTopRow(from))
                    moveOptionResult |= from >> Constants.ROWS_NUMBER;
            }
            return moveOptionResult & (~allThePiecesLocationOfTheCurrentPlayer);
        }
    }
}
