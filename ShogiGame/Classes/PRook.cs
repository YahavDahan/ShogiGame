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
    public class PRook : Rook
    {
        public PRook() : base()
        {
            state = BigInteger.Parse("0");
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/9.png");
            pieceScore = 850;
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                 20, 30, 30, 30, 30, 30, 30, 30, 20,
                 30, 40, 50, 30, 50, 30, 50, 40, 30,
                 -5,  0, 10, 20, 30, 20, 10,  0, -5,
                 -5,  0, 10, 20, 30, 10, 10,  0, -5,
                 -5,  0, 10, 10, 20, 10, 10,  0, -5,
                 -5,  0, 10, 20, 30, 20, 10,  0, -5,
                 -5,  0, 10, 20, 10, 20, 10,  0, -5,
                 -5,  5,  0, 20, 30, 20,  0,  5, -5,
                -10, -5, -5, 10, 20, 10, -5, -5,-10
            };
        }

        public PRook(BigInteger state) : base()
        {
            this.state = state;
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/9.png");
            pieceScore = 850;
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                 20, 30, 30, 30, 30, 30, 30, 30, 20,
                 30, 40, 50, 30, 50, 30, 50, 40, 30,
                 -5,  0, 10, 20, 30, 20, 10,  0, -5,
                 -5,  0, 10, 20, 30, 10, 10,  0, -5,
                 -5,  0, 10, 10, 20, 10, 10,  0, -5,
                 -5,  0, 10, 20, 30, 20, 10,  0, -5,
                 -5,  0, 10, 20, 10, 20, 10,  0, -5,
                 -5,  5,  0, 20, 30, 20,  0,  5, -5,
                -10, -5, -5, 10, 20, 10, -5, -5,-10
            };
        }

        public override BigInteger getPlacesToMove(BigInteger from, Board board)
        {
            BigInteger moveOptions = base.getPlacesToMove(from, board);
            if (!Board.isLocatedInTopRow(from) && !Board.isLocatedInRightColumn(from))
                moveOptions |= from >> Constants.ROWS_NUMBER - 1;
            if (!Board.isLocatedInTopRow(from) && !Board.isLocatedInLeftColumn(from))
                moveOptions |= from >> Constants.ROWS_NUMBER + 1;
            if (!Board.isLocatedInBottomRow(from) && !Board.isLocatedInRightColumn(from))
                moveOptions |= from << Constants.ROWS_NUMBER + 1;
            if (!Board.isLocatedInBottomRow(from) && !Board.isLocatedInLeftColumn(from))
                moveOptions |= from << Constants.ROWS_NUMBER - 1;
            return moveOptions & (~board.Turn.GetAllPiecesLocations());
        }
    }
}
