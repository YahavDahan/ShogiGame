using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Classes
{
    public class PSilver : Gold
    {
        public PSilver() : base()
        {
            State = BigInteger.Parse("0");
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/11.png");
            pieceScore = 430;
        }

        public PSilver(BigInteger state) : base()
        {
            this.state = state;
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/11.png");
            pieceScore = 430;
        }

    }
}
