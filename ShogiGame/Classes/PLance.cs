using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Classes
{
    public class PLance : Gold
    {
        /// <summary>
        /// constructor with state 0 - there are no pieces in this type
        /// </summary>
        public PLance() : base()
        {
            State = BigInteger.Parse("0");
            image = Properties.Resources._13;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/13.png");
            pieceScore = 320;
        }

        /// <summary>
        /// constructor for the piece type. initializes the Piece's features
        /// </summary>
        /// <param name="state">the state of the piece</param>
        public PLance(BigInteger state) : base()
        {
            this.state = state;
            image = Properties.Resources._13;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/13.png");
            pieceScore = 320;
        }

    }
}
