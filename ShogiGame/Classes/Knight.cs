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
    public class Knight : Piece
    {
        /// <summary>
        /// constructor for the piece type. initializes the Piece's features
        /// </summary>
        /// <param name="state">the state of the piece</param>
        public Knight(BigInteger state) : base(state)
        {
            image = Properties.Resources._6;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/6.png");
            pieceScore = 300;
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                 20,  0, 20,  0, 20,  0, 20,  0, 20,
                  0,  0,  0,  0,  0,  0,  0,  0,  0,
                 -5,  0,  5, 20, 10, 20,  5,  0, -5,
                 -5,  0, 20, 20, 20, 10,  0,  0, -5,
                 -5, 10, 20, 30,  0, 30, 20, 10, -5,
                 -5,  0, 20,  0, 20,  0, 20,  0, -5,
                 -5, 10, 20,  0,  0,  0, 20,  0, -5,
                  0,  0,  0,  0,  0,  0,  0,  0,  0,
                  0,-10,  0,  0,  0,  0,  0,-10,  0
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
                if (!Board.isLocatedInRightColumn(from))
                    moveOptionResult |= from >> Constants.ROWS_NUMBER * 2 - 1;
                if (!Board.isLocatedInLeftColumn(from))
                    moveOptionResult |= from >> Constants.ROWS_NUMBER * 2 + 1;
            }
            else
            {
                if (!Board.isLocatedInRightColumn(from))
                    moveOptionResult |= from << Constants.ROWS_NUMBER * 2 + 1;
                if (!Board.isLocatedInLeftColumn(from))
                    moveOptionResult |= from << Constants.ROWS_NUMBER * 2 - 1;
            }
            return moveOptionResult & (~allThePiecesLocationOfTheCurrentPlayer);
        }
    }
}
