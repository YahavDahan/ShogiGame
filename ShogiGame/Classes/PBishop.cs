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
        /// <summary>
        /// constructor with state 0 - there are no pieces in this type
        /// </summary>
        public PBishop() : base()
        {
            State = BigInteger.Parse("0");
            image = Properties.Resources._10;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/10.png");
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

        /// <summary>
        /// constructor for the piece type. initializes the Piece's features
        /// </summary>
        /// <param name="state">the state of the piece</param>
        public PBishop(BigInteger state) : base()
        {
            this.state = state;
            image = Properties.Resources._10;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/10.png");
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

        /// <summary>
		/// the functions finds all the possible moves of the current piece from specific location
		/// </summary>
		/// <param name="from">The location we want to get the move options from</param>
		/// <param name="board">the game board</param>
		/// <returns>the possible moves in BitBoard format</returns>
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
