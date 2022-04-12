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
        public Bishop(BigInteger state) : base(state)
        {
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/3.png");
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

        public Bishop() : base() { }

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
