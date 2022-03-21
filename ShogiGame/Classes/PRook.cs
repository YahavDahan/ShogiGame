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
            State = BigInteger.Parse("0");
            image = Image.FromFile("C:/nisayon2/ShogiGame/ShogiGame/Resources/Images/Western/9.png");
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
