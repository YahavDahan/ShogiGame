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
        public Lance(BigInteger state) : base(state)
        {
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/7.png");
            pieceScore = 280;
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                  0,  0,  0,  0,  0,  0,  0,  0,  0,
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
