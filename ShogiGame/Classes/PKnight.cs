using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Classes
{
    public class PKnight : Gold
    {
        public PKnight() : base()
        {
            State = BigInteger.Parse("0");
            image = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/Western/12.png");
        }
    }
}
