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
        public Pawn(BigInteger state) : base(state)
        {
            image = Image.FromFile("C:/nisayon2/ShogiGame/ShogiGame/Resources/Images/Western/8.png");
        }

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