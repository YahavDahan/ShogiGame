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

        public static int[] mirrorBitBoard = new int[ROWS_NUMBER * ROWS_NUMBER] {
            80, 79, 78, 77, 76, 75, 74, 73, 72,
            71, 70, 69, 68, 67, 66, 65, 64, 63,
            62, 61, 60, 59, 58, 57, 56, 55, 54,
            53, 52, 51, 50, 49, 48, 47, 46, 45,
            44, 43, 42, 41, 40, 39, 38, 37, 36,
            35, 34, 33, 32, 31, 30, 29, 28, 27,
            26, 25, 24, 23, 22, 21, 20, 19, 18,
            17, 16, 15, 14, 13, 12, 11, 10,  9,
             8,  7,  6,  5,  4,  3,  2,  1,  0
        };
    }
}
