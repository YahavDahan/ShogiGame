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
        private BigInteger from;
        private BigInteger to;
        private bool isPromoted;
        private int attackedPieceType;
        private bool hasBeenCheckBeforeTheMove;
        private bool didTheMoveCauseCheckOnTheOtherPlayer;

        public int PieceType { get => pieceType; set => pieceType = value; }

        public BigInteger From { get => from; set => from = value; }

        public BigInteger To { get => to; set => to = value; }

        public bool IsPromoted { get => isPromoted; set => isPromoted = value; }

        public int AttackedPieceType { get => attackedPieceType; set => attackedPieceType = value; }

        public bool HasBeenCheckBeforeTheMove { get => hasBeenCheckBeforeTheMove; set => hasBeenCheckBeforeTheMove = value; }

        public bool DidTheMoveCauseCheckOnTheOtherPlayer { get => didTheMoveCauseCheckOnTheOtherPlayer; set => didTheMoveCauseCheckOnTheOtherPlayer = value; }

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
