using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Logic
{
    public class Move
    {
        private BigInteger from;
        private BigInteger to;
        private bool isPromoted;

        public BigInteger From { get => from; set => from = value; }

        public BigInteger To { get => to; set => to = value; }

        public bool IsPromoted { get => isPromoted; set => isPromoted = value; }

        public Move(BigInteger from, BigInteger to, bool isPromoted)
        {
            this.from = from;
            this.to = to;
            this.isPromoted = isPromoted;
        }
    }
}
