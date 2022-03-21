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
