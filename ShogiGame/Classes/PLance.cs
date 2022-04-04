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
        public PLance() : base()
        {
            State = BigInteger.Parse("0");
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/13.png");
        }

        public PLance(BigInteger state) : base()
        {
            this.state = state;
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/13.png");
        }

    }
}
