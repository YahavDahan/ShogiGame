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
        public static BigInteger GetSquareFromLocation(Point location)
        {  // מחזיר את המשבצת בביג אינטג'ר. אם לא נבחרה משבצת, יוחזר 0
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

        public static BigInteger CreateMaskFromNumber(int numberForCreatingMask)
        {
            BigInteger mask = 1;
            mask <<= numberForCreatingMask;
            return mask;
        }

        public static bool IsPowerOfTwo(BigInteger number)
        {
            return (number != 0) && ((number & (number - 1)) == 0);
        }

        public static int CreateNumberFromMask(BigInteger maskForCreatingNumber)
        {
            return Log2ToNumber(maskForCreatingNumber);
        }

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
    }
}
