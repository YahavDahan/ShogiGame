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

        public Piece(BigInteger state)
		{
			this.state = state;
		}

		public Piece() { }

		public BigInteger State { get => state; set => state = value; }

		public int PieceScore { get => pieceScore;}

		public int[] MoveScore { get => moveScore;}

        public abstract BigInteger getPlacesToMove(BigInteger from, Board board);

		public void Move(BigInteger from, BigInteger to, Board board)
		{
			this.state ^= from;
			this.state ^= to;
			Player otherPlayer = board.getOtherPlayer();
			if ((otherPlayer.GetAllPiecesLocations() & to) != 0)
				otherPlayer.DeletePieceFromLocation(to);
		}

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
