using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Logic
{
    public class CachedData
    {
        public enum TheType
        {
            EXACT_VALUE, UPPERBOUND, LOWERBOUND
        }

        private int depth;
        private int score;
        private TheType type;

        public int Depth { get => depth; set => depth = value; }

        public int Score { get => score; set => score = value; }

        public TheType Type { get => type; set => type = value; }

        public CachedData(int depth, int score, TheType type)
        {
            this.depth = depth;
            this.score = score;
            this.type = type;
        }
    }
}
