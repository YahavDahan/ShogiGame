﻿using ShogiGame.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Classes
{
    public class Gold : Piece
    {
        public Gold(BigInteger state) : base(state)
        {
            image = Image.FromFile("C:/nisayon2/ShogiGame/ShogiGame/Resources/Images/Western/4.png");
        }

        public Gold() : base() { }

        public override BigInteger getPlacesToMove(BigInteger from, Board board)
        {
            Player currentPlayer = board.Turn;
            BigInteger allThePiecesLocationOfTheCurrentPlayer = currentPlayer.GetAllPiecesLocations();
            BigInteger moveOptionResult = 0;
            if (currentPlayer.IsPlayer1)
            {
                if (!Board.isLocatedInTopRow(from))
                    moveOptionResult |= from >> Constants.ROWS_NUMBER;
                if (!Board.isLocatedInTopRow(from) && !Board.isLocatedInRightColumn(from))
                    moveOptionResult |= from >> Constants.ROWS_NUMBER - 1;
                if (!Board.isLocatedInTopRow(from) && !Board.isLocatedInLeftColumn(from))
                    moveOptionResult |= from >> Constants.ROWS_NUMBER + 1;
                if (!Board.isLocatedInRightColumn(from))
                    moveOptionResult |= from << 1;
                if (!Board.isLocatedInLeftColumn(from))
                    moveOptionResult |= from >> 1;
                if (!Board.isLocatedInBottomRow(from))
                    moveOptionResult |= from << Constants.ROWS_NUMBER;
            }
            else
            {
                if (!Board.isLocatedInBottomRow(from))
                    moveOptionResult |= from << Constants.ROWS_NUMBER;
                if (!Board.isLocatedInBottomRow(from) && !Board.isLocatedInRightColumn(from))
                    moveOptionResult |= from << Constants.ROWS_NUMBER + 1;
                if (!Board.isLocatedInBottomRow(from) && !Board.isLocatedInLeftColumn(from))
                    moveOptionResult |= from << Constants.ROWS_NUMBER - 1;
                if (!Board.isLocatedInRightColumn(from))
                    moveOptionResult |= from << 1;
                if (!Board.isLocatedInLeftColumn(from))
                    moveOptionResult |= from >> 1;
                if (!Board.isLocatedInTopRow(from))
                    moveOptionResult |= from >> Constants.ROWS_NUMBER;
            }
            return moveOptionResult & (~allThePiecesLocationOfTheCurrentPlayer);
        }
    }
}
