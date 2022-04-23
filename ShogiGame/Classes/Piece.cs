using ShogiGame.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Classes
{
	public abstract class Piece
	{
		protected BigInteger state;
		protected Image image;
		protected int pieceScore;
		protected int[] moveScore;

		/// <summary>
		/// constructor for piece, initializes the initial state of the pieces
		/// </summary>
		/// <param name="state">the state of the pieces</param>
		public Piece(BigInteger state)
		{
			this.state = state;
		}

		/// <summary>
		/// empty constructor
		/// </summary>
		public Piece() { }

		/// <summary>
		/// the state of all the pieces from the same type in BitBoard format
		/// </summary>
		public BigInteger State { get => state; set => state = value; }

		/// <summary>
		/// the score of the piece
		/// </summary>
		public int PieceScore { get => pieceScore;}

		/// <summary>
		/// The strategic locations of the piece
		/// </summary>
		public int[] MoveScore { get => moveScore;}

		/// <summary>
		/// the functions finds all the possible moves of the current piece from specific location
		/// </summary>
		/// <param name="from">The location we want to get the move options from</param>
		/// <param name="board">the game board</param>
		/// <returns>the possible moves in BitBoard format</returns>
		public abstract BigInteger getPlacesToMove(BigInteger from, Board board);

		/// <summary>
		/// the function perform move of piece on the board
		/// </summary>
		/// <param name="from">from location</param>
		/// <param name="to">to location</param>
		/// <param name="board">the game board</param>
		public void Move(BigInteger from, BigInteger to, Board board)
		{
			this.state ^= from;
			this.state ^= to;
			Player otherPlayer = board.getOtherPlayer();
			if ((otherPlayer.GetAllPiecesLocations() & to) != 0)
				otherPlayer.DeletePieceFromLocation(to);
		}

		/// <summary>
		/// the function prints all the pieces from the same type on the board
		/// </summary>
		/// <param name="g">graphics object</param>
		/// <param name="isPlayer1">Indicates if the player is the down player</param>
		public void PrintPiece(Graphics g, bool isPlayer1)
		{
			if (this.state != 0)
			{
				Image currentImage = (Image)this.image.Clone();
				if (!isPlayer1)
					currentImage.RotateFlip(RotateFlipType.RotateNoneFlipXY);  // rotate the image for the player above
				BigInteger maskForChecking = BigInteger.Parse("100000000000000000000", NumberStyles.HexNumber);
				while (maskForChecking != 0)
				{
					if ((this.state & maskForChecking) != 0)
						PrintPieceInSpecificLocation(g, currentImage, maskForChecking, isPlayer1);
					maskForChecking >>= 1;
				}
			}
		}

		/// <summary>
		/// the function print one piece in specific location on the board
		/// </summary>
		/// <param name="g">graphics object</param>
		/// <param name="currentImage">image to print (the image of the piece)</param>
		/// <param name="mask">location on the board to print the piece</param>
		/// <param name="isPlayer1">Indicates if the player is the down player</param>
		public void PrintPieceInSpecificLocation(Graphics g, Image currentImage, BigInteger mask, bool isPlayer1)
		{
			Point location = HandleBitwise.GetLocationFromMask(mask);
			if (isPlayer1)  // piece of down player
				g.DrawImage(currentImage, location.X + 7, location.Y + 2, 36, 49);
			else
				g.DrawImage(currentImage, location.X + 7, location.Y + 2, 36, 49);
		}
	}
}
