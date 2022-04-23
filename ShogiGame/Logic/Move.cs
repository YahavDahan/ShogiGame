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
        private int pieceType;  // before the promotion
        private BigInteger from;  // from location
        private BigInteger to;  // to location
        private bool isPromoted;  // is the move include promotion
        private int attackedPieceType;  // if the other player's piece get attacked, save his type
        private bool hasBeenCheckBeforeTheMove;
        private bool didTheMoveCauseCheckOnTheOtherPlayer;

        public int PieceType { get => pieceType; set => pieceType = value; }

        public BigInteger From { get => from; set => from = value; }

        public BigInteger To { get => to; set => to = value; }

        public bool IsPromoted { get => isPromoted; set => isPromoted = value; }

        public int AttackedPieceType { get => attackedPieceType; set => attackedPieceType = value; }

        public bool HasBeenCheckBeforeTheMove { get => hasBeenCheckBeforeTheMove; set => hasBeenCheckBeforeTheMove = value; }

        public bool DidTheMoveCauseCheckOnTheOtherPlayer { get => didTheMoveCauseCheckOnTheOtherPlayer; set => didTheMoveCauseCheckOnTheOtherPlayer = value; }

        /// <summary>
        /// the function creates move object
        /// </summary>
        /// <param name="pieceType">the type of the piece to move before the promotion</param>
        /// <param name="from">from location</param>
        /// <param name="to">to location</param>
        /// <param name="isPromoted">is the move include promotion</param>
        /// <param name="hasCheck">is the player has check warning</param>
        public Move(int pieceType, BigInteger from, BigInteger to, bool isPromoted, bool hasCheck)
        {
            this.pieceType = pieceType;
            this.from = from;
            this.to = to;
            this.isPromoted = isPromoted;
            this.attackedPieceType = -1;  // has to check (temporary value)
            this.hasBeenCheckBeforeTheMove = hasCheck;
            this.hasBeenCheckBeforeTheMove = false;  // have to check (temporary value)
        }
    }
}
