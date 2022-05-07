using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Logic
{
    public static class ZobristHashing
    {
        private static int[,] zobristTable;
        public static int playerBellowMove;

        /// <summary>
        /// initializing the array for the troops codes
        /// </summary>
        static ZobristHashing()
        {
            zobristTable = new int[81, 28];
            Random rand = new Random();
            for (int i = 0; i < 81; i++)
                for (int j = 0; j < 28; j++)
                    zobristTable[i, j] = rand.Next();
            playerBellowMove = rand.Next();
        }

        public static int GetHashKeyForPiece(Board board, int location)
        {
            BigInteger maskLocation = HandleBitwise.CreateMaskFromNumber(location);
            for(int i = 0; i < board.Player1.PiecesLocation.Length; i++)
                if ((board.Player1.PiecesLocation[i].State & maskLocation) != 0)
                    return i;
            for (int i = 0; i < board.Player2.PiecesLocation.Length; i++)
                if ((board.Player2.PiecesLocation[i].State & maskLocation) != 0)
                    return i + 14;
            return -1;
        }

        public static int GetHashKeyForBoard(Board board)
        {
            int key = 0;
            for (int loc = 0; loc < 81; loc++)
            {
                int pieceKey = GetHashKeyForPiece(board, loc);
                if (pieceKey != -1)
                    key ^= zobristTable[loc, pieceKey];
            }
            if (board.Turn.IsPlayer1)
                key ^= playerBellowMove;
            return key;
        }
    }
}
