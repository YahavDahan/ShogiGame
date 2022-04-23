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
    public class Lance : Piece
    {
        /// <summary>
        /// constructor for the piece type. initializes the Piece's features
        /// </summary>
        /// <param name="state">the state of the piece</param>
        public Lance(BigInteger state) : base(state)
        {
            image = Properties.Resources._7;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/7.png");
            pieceScore = 280;
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                 50,  0,  0,  0,  0,  0,  0,  0, 50,
                 20,  0,  0,  0,  0,  0,  0,  0, 20,
                 20,  0,  0,  0,  0,  0,  0,  0, 20,
                 20,  0,  0,  0,  0,  0,  0,  0, 20,
                 10,  0,  0,  0,  0,  0,  0,  0, 10,
                 10,  0,  0,  0,  0,  0,  0,  0, 10,
                  0,  0,  0,  0,  0,  0,  0,  0,  0,
                  0,  0,  0,  0,  0,  0,  0,  0,  0,
                 20,  0,  0,  0,  0,  0,  0,  0, 20
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
            BigInteger allThePiecesLocationOfTheOtherPlayer = board.getOtherPlayer().GetAllPiecesLocations();

            BigInteger locationForChecking, moveOptionResult = 0;
            if (currentPlayer.IsPlayer1)
            {
                locationForChecking = from >> Constants.ROWS_NUMBER;
                while (locationForChecking != 0)
                {
                    if ((locationForChecking & allThePiecesLocationOfTheCurrentPlayer) != 0)
                        break;
                    else
                    {
                        moveOptionResult |= locationForChecking;
                        if ((locationForChecking & allThePiecesLocationOfTheOtherPlayer) != 0)
                            break;
                    }
                    locationForChecking >>= Constants.ROWS_NUMBER;
                }
            }
            else
            {
                locationForChecking = from << Constants.ROWS_NUMBER;
                while ((locationForChecking & Constants.BITBOARD_OF_ONE) != 0)
                {
                    if ((locationForChecking & allThePiecesLocationOfTheCurrentPlayer) != 0)
                        break;
                    else
                    {
                        moveOptionResult |= locationForChecking;
                        if ((locationForChecking & allThePiecesLocationOfTheOtherPlayer) != 0)
                            break;
                    }
                    locationForChecking <<= Constants.ROWS_NUMBER;
                }
            }
            return moveOptionResult;
        }
    }
}
