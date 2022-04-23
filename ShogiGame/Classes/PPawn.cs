using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Classes
{
    public class PPawn : Gold
    {
        /// <summary>
        /// constructor with state 0 - there are no pieces in this type
        /// </summary>
        public PPawn() : base()
        {
            State = BigInteger.Parse("0");
            image = Properties.Resources._14;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/14.png");
            pieceScore = 270;
        }

        /// <summary>
        /// constructor for the piece type. initializes the Piece's features
        /// </summary>
        /// <param name="state">the state of the piece</param>
        public PPawn(BigInteger state) : base()
        {
            this.state = state;
            image = Properties.Resources._14;
            // image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/14.png");
            pieceScore = 270;
        }
    }
}
