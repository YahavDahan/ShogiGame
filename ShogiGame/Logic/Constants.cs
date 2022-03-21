using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Logic
{
    public static class Constants
    {
        public const int ROWS_NUMBER = 9;

        public static BigInteger BITBOARD_OF_ONE = BigInteger.Parse("1FFFFFFFFFFFFFFFFFFFF", NumberStyles.HexNumber);

        public const int BOARD_START_HEIGHT = 94;

        public const int BOARD_START_WIDTH = 164;

        public const int SQUARE_SIZE = 55;
    }
}
