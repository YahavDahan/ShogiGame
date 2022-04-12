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
    public class PBishop : Bishop
    {
        public PBishop() : base()
        {
            State = BigInteger.Parse("0");
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/10.png");
            pieceScore = 710;
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                -10, -5, -5, -5, -5, -5, -5, -5,-10,
                 -5,  0,  0,  0, 30,  0,  0,  0, -5,
                 -5,  0,  0, 20, 10, 20, -5,  0, -5,
                 -5,  0, 30, 10, 30, 10, 30,  0, -5,
                 -5,  0, 10, 10, 10, 10, 10,  0, -5,
                 -5,  0, 30, 10, 30, 10, 30,  0, -5,
                 -5, 10, -5, 20, -5, 20, -5, 10, -5,
                 -5,  0,  0,  0,  0,  0,  0,  0, -5,
                -10, -5, -5, -5, -5, -5, -5, -5,-10
            };
        }

        public PBishop(BigInteger state) : base()
        {
            this.state = state;
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/10.png");
            pieceScore = 710;
            moveScore = new int[Constants.ROWS_NUMBER * Constants.ROWS_NUMBER]
            {
                -10, -5, -5, -5, -5, -5, -5, -5,-10,
                 -5,  0,  0,  0, 30,  0,  0,  0, -5,
                 -5,  0,  0, 20, 10, 20, -5,  0, -5,
                 -5,  0, 30, 10, 30, 10, 30,  0, -5,
                 -5,  0, 10, 10, 10, 10, 10,  0, -5,
                 -5,  0, 30, 10, 30, 10, 30,  0, -5,
                 -5, 10, -5, 20, -5, 20, -5, 10, -5,
                 -5,  0,  0,  0,  0,  0,  0,  0, -5,
                -10, -5, -5, -5, -5, -5, -5, -5,-10
            };
        }

        public override BigInteger getPlacesToMove(BigInteger from, Board board)
        {
            BigInteger moveOptions = base.getPlacesToMove(from, board);
            if (!Board.isLocatedInTopRow(from))
                moveOptions |= from >> Constants.ROWS_NUMBER;
            if (!Board.isLocatedInBottomRow(from))
                moveOptions |= from << Constants.ROWS_NUMBER;
            if (!Board.isLocatedInRightColumn(from))
                moveOptions |= from << 1;
            if (!Board.isLocatedInLeftColumn(from))
                moveOptions |= from >> 1;
            return moveOptions & (~board.Turn.GetAllPiecesLocations());
        }
    }
}
