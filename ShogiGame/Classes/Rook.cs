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
    public class Rook : Piece
    {
        public Rook(BigInteger state) : base(state)
        {
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/2.png");
        }

        public Rook() : base() { }

        public override BigInteger getPlacesToMove(BigInteger from, Board board)
        {
            BigInteger allThePiecesLocationOfTheCurrentPlayer = board.Turn.GetAllPiecesLocations();
            BigInteger allThePiecesLocationOfTheOtherPlayer = board.getOtherPlayer().GetAllPiecesLocations();

            BigInteger moveOptionResult = 0;
            BigInteger locationForChecking = from >> Constants.ROWS_NUMBER;
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

            locationForChecking = from >> 1;
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
                locationForChecking >>= 1;
            }

            locationForChecking = from << 1;
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
                locationForChecking <<= 1;
            }

            return moveOptionResult;
        }
    }
}
