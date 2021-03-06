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
    public class Pawn : Piece
    {
        /// <summary>
        /// constructor for the piece type. initializes the Piece's features
        /// </summary>
        /// <param name="state">the state of the piece</param>
        public Pawn(BigInteger state) : base(state)
        {
            image = Properties.Resources._8;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/8.png");
            pieceScore = 100;
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                 30, 30, 40, 40, 40, 40, 40, 30, 30,
                 20, 30, 30, 30, 40, 30, 30, 30, 20,
                 20, 20, 10, 20, 30, 20, 10, 20, 20,
                 10, 10, 10, 10, 20, 10, 10, 10, 10,
                  0,  5, 10, 20, 20, 20, 10,  5,  0,
                  5,  0, 10, 10, 10, 10, 10, 10,  5,
                  0,  0,-30,  5,  5,  5,  0,-30,  0,
                  0,  0,  0,  0,  0,  0,  0,  0,  0,
                  0,  0,  0,  0,  0,  0,  0,  0,  0
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
            BigInteger moveOptionResult;
            if (currentPlayer.IsPlayer1)
                moveOptionResult = from >> Constants.ROWS_NUMBER;
            else
                moveOptionResult = from << Constants.ROWS_NUMBER;
            return moveOptionResult & (~allThePiecesLocationOfTheCurrentPlayer);
        }
    }
}