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
        /// <summary>
        /// constructor with state 0 - there are no pieces in this type
        /// </summary>
        public PRook() : base()
        {
            state = BigInteger.Parse("0");
            image = Properties.Resources._9;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/9.png");
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

        /// <summary>
        /// constructor for the piece type. initializes the Piece's features
        /// </summary>
        /// <param name="state">the state of the piece</param>
        public PRook(BigInteger state) : base()
        {
            this.state = state;
            image = Properties.Resources._9;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/9.png");
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

        /// <summary>
		/// the functions finds all the possible moves of the current piece from specific location
		/// </summary>
		/// <param name="from">The location we want to get the move options from</param>
		/// <param name="board">the game board</param>
		/// <returns>the possible moves in BitBoard format</returns>
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
