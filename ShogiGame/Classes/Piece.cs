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

		public Piece(BigInteger state)
		{
			this.State = state;
		}

		public Piece() { }

		public BigInteger State { get => state; set => state = value; }

		public abstract BigInteger getPlacesToMove(BigInteger from, Board board);

		public void Move(BigInteger from, BigInteger to, Board board, Graphics g)
		{
			this.state ^= from;
			this.state ^= to;
			Player otherPlayer = board.getOtherPlayer();
			if ((otherPlayer.GetAllPiecesLocations() & to) != 0)
				otherPlayer.DeletePieceFromLocation(to);

			// remove the check warning
			board.Turn.IsCheck = false;
			Image coverImg = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/coverImage.png");
			if (board.Turn.IsPlayer1)
				g.DrawImage(coverImg, 710, 477, 156, 50);
			else
				g.DrawImage(coverImg, 710, 150, 156, 50);
		}

		public void PrintPiece(Graphics g, bool isPlayer1)
		{
			if (this.state != 0)
			{
				BigInteger maskForChecking = BigInteger.Parse("100000000000000000000", NumberStyles.HexNumber);
				while (maskForChecking != 0)
				{
					if ((this.state & maskForChecking) != 0)
						PrintPieceInSpecificLocation(g, maskForChecking, isPlayer1);
					maskForChecking >>= 1;
				}
			}
		}

		public void PrintPieceInSpecificLocation(Graphics g, BigInteger mask, bool isPlayer1)
		{
			Point location = HandleBitwise.GetLocationFromMask(mask);
			if (isPlayer1)  // piece of down player
				g.DrawImage(this.image, location.X + 7, location.Y + 2, 36, 49);
			else
				// הדפסת כלי הפוך
				g.DrawImage(this.image, location.X + 7, location.Y + 2, 36, 49);
		}
	}
}
