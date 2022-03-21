using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Logic
{
    public class Board
    {
        private Player player1, player2;
        private Player turn;

        public Board()
        {
            this.player1 = new Player(true);
            this.player2 = new Player(false);
            this.turn = this.player1;
        }

        public Player Turn { get => turn; set => turn = value; }

        public Player Player1 { get => player1; }

        public Player Player2 { get => player2; }

        public Player getOtherPlayer()
        {
            if (turn == player1)
                return player2;
            return player1;
        }

        public static bool isLocatedInRightColumn(BigInteger locationForChecking)
        {
            BigInteger maskOfTheRightColumn = BigInteger.Parse("100804020100804020100", NumberStyles.HexNumber);
            return (locationForChecking & maskOfTheRightColumn) != 0;
        }

        public static bool isLocatedInLeftColumn(BigInteger locationForChecking)
        {
            BigInteger maskOfTheLeftColumn = BigInteger.Parse("1008040201008040201", NumberStyles.HexNumber);
            return (locationForChecking & maskOfTheLeftColumn) != 0;
        }

        public static bool isLocatedInTopRow(BigInteger locationForChecking)
        {
            BigInteger maskOfTheTopRow = BigInteger.Parse("1FF", NumberStyles.HexNumber);
            return (locationForChecking & maskOfTheTopRow) != 0;
        }

        public static bool isLocatedInBottomRow(BigInteger locationForChecking)
        {
            BigInteger maskOfTheBottomRow = BigInteger.Parse("1FF000000000000000000", NumberStyles.HexNumber);
            return (locationForChecking & maskOfTheBottomRow) != 0;
        }
    }
}
