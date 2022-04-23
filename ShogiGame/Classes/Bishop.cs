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
    public class Bishop : Piece
    {
        /// <summary>
        /// constructor for the piece type. initializes the Piece's features
        /// </summary>
        /// <param name="state">the state of the piece</param>
        public Bishop(BigInteger state) : base(state)
        {
            image = Properties.Resources._3;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/3.png");
            pieceScore = 620;
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                 10,  0,  0,  0,  0,  0,  0,  0, 10,
                  0,  0,  0, 20, 20, 30,  0,  0,  0,
                  0, 20, 10, 20, 20, 20, 10, 20,  0,
                  0,  0,  0, 20, 20, 20,  0,  0,  0,
                 20,  0, 10,  0,  0,  0, 10,  0, 20,
                  0,  0, 10,  0,  0,  0, 10,  0,  0,
                  0,  0, 10,  0,  0,  0, 10,  0,  0,
                  0, 20,  0,  0,  0,  0,  0, 20,  0,
                  0,  0,  0,  0,  0,  0,  0,  0,  0
            };
        }

        /// <summary>
        /// empty constructor
        /// </summary>
        public Bishop() : base() { }

        /// <summary>
		/// the functions finds all the possible moves of the current piece from specific location
		/// </summary>
		/// <param name="from">The location we want to get the move options from</param>
		/// <param name="board">the game board</param>
		/// <returns>the possible moves in BitBoard format</returns>
        public override BigInteger getPlacesToMove(BigInteger from, Board board)
        {
            BigInteger allThePiecesLocationOfTheCurrentPlayer = board.Turn.GetAllPiecesLocations();
            BigInteger allThePiecesLocationOfTheOtherPlayer = board.getOtherPlayer().GetAllPiecesLocations();

            BigInteger moveOptionResult = 0;
            BigInteger locationForChecking = from >> Constants.ROWS_NUMBER - 1;
            while (locationForChecking != 0 && !Board.isLocatedInLeftColumn(locationForChecking))
            {
                if ((locationForChecking & allThePiecesLocationOfTheCurrentPlayer) != 0)
                    break;
                else
                {
                    moveOptionResult |= locationForChecking;
                    if ((locationForChecking & allThePiecesLocationOfTheOtherPlayer) != 0)
                        break;
                }
                locationForChecking >>= Constants.ROWS_NUMBER - 1;
            }

            locationForChecking = from << Constants.ROWS_NUMBER + 1;
            while ((locationForChecking & Constants.BITBOARD_OF_ONE) != 0 && !Board.isLocatedInLeftColumn(locationForChecking))
            {
                if ((locationForChecking & allThePiecesLocationOfTheCurrentPlayer) != 0)
                    break;
                else
                {
                    moveOptionResult |= locationForChecking;
                    if ((locationForChecking & allThePiecesLocationOfTheOtherPlayer) != 0)
                        break;
                }
                locationForChecking <<= Constants.ROWS_NUMBER + 1;
            }

            locationForChecking = from >> Constants.ROWS_NUMBER + 1;
            while (locationForChecking != 0 && !Board.isLocatedInRightColumn(locationForChecking))
            {
                if ((locationForChecking & allThePiecesLocationOfTheCurrentPlayer) != 0)
                    break;
                else
                {
                    moveOptionResult |= locationForChecking;
                    if ((locationForChecking & allThePiecesLocationOfTheOtherPlayer) != 0)
                        break;
                }
                locationForChecking >>= Constants.ROWS_NUMBER + 1;
            }

            locationForChecking = from << Constants.ROWS_NUMBER - 1;
            while ((locationForChecking & Constants.BITBOARD_OF_ONE) != 0 && !Board.isLocatedInRightColumn(locationForChecking))
            {
                if ((locationForChecking & allThePiecesLocationOfTheCurrentPlayer) != 0)
                    break;
                else
                {
                    moveOptionResult |= locationForChecking;
                    if ((locationForChecking & allThePiecesLocationOfTheOtherPlayer) != 0)
                        break;
                }
                locationForChecking <<= Constants.ROWS_NUMBER - 1;
            }

            return moveOptionResult;
        }
    }
}
