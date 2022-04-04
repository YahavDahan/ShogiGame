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
        public PPawn() : base()
        {
            State = BigInteger.Parse("0");
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/14.png");
        }

        public PPawn(BigInteger state) : base()
        {
            this.state = state;
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/14.png");
        }

    }
}
