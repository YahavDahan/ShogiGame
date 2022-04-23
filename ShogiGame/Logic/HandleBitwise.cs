using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Logic
{
    public static class HandleBitwise
    {
        /// <summary>
        /// the function calculate the square the user clicked from the location in pixels
        /// </summary>
        /// <param name="location">localion to the board in pixels</param>
        /// <returns>the square thet the location represent</returns>
        public static BigInteger GetSquareFromLocation(Point location)
        {
            BigInteger result = 0;
            if (location.Y >= Constants.BOARD_START_HEIGHT && location.Y <= Constants.BOARD_START_HEIGHT + Constants.ROWS_NUMBER * Constants.SQUARE_SIZE &&
                location.X >= Constants.BOARD_START_WIDTH && location.X <= Constants.BOARD_START_WIDTH + Constants.ROWS_NUMBER * Constants.SQUARE_SIZE)
            {
                int row = (location.Y - Constants.BOARD_START_HEIGHT) / Constants.SQUARE_SIZE;
                int col = (location.X - Constants.BOARD_START_WIDTH) / Constants.SQUARE_SIZE;
                result = CreateMaskFromNumber(row * Constants.ROWS_NUMBER + col);
            }
            return result;
        }

        /// <summary>
        /// the function creates BigInteger mask from integer number
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="numberForCreatingMask">the number we want to convert to mask</param>
        /// <returns>the mask that the number represents</returns>
        /// <example>
        /// if number = 4  ,  the result will be -> 1000
        /// if number = 7  ,  the result will be -> 1000000
        /// </example>
        public static BigInteger CreateMaskFromNumber(int numberForCreatingMask)
        {
            BigInteger mask = 1;
            mask <<= numberForCreatingMask;
            return mask;
        }

        /// <summary>
        /// checks if number is power of 2 (only one bit is on)
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="number">the number we want to check if its power of two</param>
        /// <returns>true if only one bit of the number is on, otherwise returns false</returns>
        public static bool IsPowerOfTwo(BigInteger number)
        {
            return (number != 0) && ((number & (number - 1)) == 0);
        }

        /// <summary>
        /// the function creates number from BigInteger mask
        /// </summary>
        /// <param name="maskForCreatingNumber">mask of a number (only one bit is on)</param>
        /// <returns>the number that the mask represents</returns>
        /// <example>
        /// if mask = 10000  ,  the result will be -> 5
        /// if mask = 100000000  ,  the result will be -> 9
        /// </example>
        public static int CreateNumberFromMask(BigInteger maskForCreatingNumber)
        {
            return Log2ToNumber(maskForCreatingNumber);
        }

        /// <summary>
        /// Checks the log2 mathematical operation of a number
        /// </summary>
        /// <param name="number">A number we want to calculate its log2</param>
        /// <returns>the result of log2 of the number</returns>
        /// <example>
        /// <code>
        /// BigInteger num = 16;
        /// Console.WriteLine(Log2ToNumber(num));
        /// </code>
        /// the result will be: 4
        /// </example>
        public static int Log2ToNumber(BigInteger number)
        {
            return (number > 1) ? 1 + Log2ToNumber(number / 2) : 0;
        }

        public static Point GetLocationFromMask(BigInteger mask)
        {
            int squareNum = CreateNumberFromMask(mask);
            int row = Constants.BOARD_START_HEIGHT + squareNum / Constants.ROWS_NUMBER * Constants.SQUARE_SIZE;
            int col = Constants.BOARD_START_WIDTH + squareNum % Constants.ROWS_NUMBER * Constants.SQUARE_SIZE;
            return new Point(col, row);
        }

        /// <summary>
        /// the function finds the first one bit in a number
        /// </summary>
        /// <param name="bitboard">the number to find its first bit on</param>
        /// <returns>the location of the first one bit</returns>
        public static int GetFirst1BitLocation(BigInteger bitboard)
        {
            if (bitboard == 0)
                return -1;
            return CreateNumberFromMask(bitboard ^ PopFirst1Bit(bitboard));
        }

        /// <summary>
        /// the function removes the first one bit in a number
        /// </summary>
        /// <param name="bitboard">the number to remove its first bit on</param>
        /// <returns>the location of the first one bit</returns>
        public static BigInteger PopFirst1Bit(BigInteger bitboard)
        {
            if (bitboard == 0)
                return -1;
            return bitboard & (bitboard - 1);
        }
    }
}
