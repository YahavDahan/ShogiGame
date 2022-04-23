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
        /// <summary>
        /// the number of squares in a raw
        /// </summary>
        public const int ROWS_NUMBER = 9;

        /// <summary>
        /// bit board of 1 - there are 81 squares in the board so the variable contains 81 one's bits
        /// </summary>
        public static BigInteger BITBOARD_OF_ONE = BigInteger.Parse("1FFFFFFFFFFFFFFFFFFFF", NumberStyles.HexNumber);

        /// <summary>
        /// the location the board starts on the Y-axis in pixels
        /// </summary>
        public const int BOARD_START_HEIGHT = 94;

        /// <summary>
        /// the location the board starts on the X-axis in pixels
        /// </summary>
        public const int BOARD_START_WIDTH = 164;

        /// <summary>
        /// the length of one square in the board in pixels
        /// </summary>
        public const int SQUARE_SIZE = 55;

        /// <summary>
        /// the last location on the board - only bit 81 is on
        /// </summary>
        public static BigInteger THE_LAST_LOCATION_ON_THE_BOARD = BigInteger.Parse("100000000000000000000", NumberStyles.HexNumber);

        /// <summary>
        /// mirror bitboard to get the same location on the opponent side
        /// </summary>
        public static int[] MIRROR_BITBOARD = new int[ROWS_NUMBER * ROWS_NUMBER] {
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
