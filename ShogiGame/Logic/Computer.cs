using ShogiGame.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Logic
{
    public static class Computer
    {
        public static bool DoStep(Board board)
        {
            // get all the possible moves
            List<Move> moves = GetAllPossibleMoves(board);
            // if there is no possible moves
            if (moves.Count == 0)
                return true;
            // find the best move
            Move bestMove = getBestMove(moves, board);
            // do best move
            board.MovePiece(bestMove);
            // the game didnt over
            return false;
        }

        public static List<Move> GetAllPossibleMoves(Board board)
        {
            List<Move> possibleMoves = new List<Move>();
            Player currentPlayer = board.Turn;
            // get king move options
            GetMoveListFromPiece(possibleMoves, currentPlayer.PiecesLocation[0], 0, currentPlayer.PiecesLocation[0].State, board);
            if (!board.Turn.IsCheck)
            {
                // get all other move options
                for (int i = 1; i < currentPlayer.PiecesLocation.Length; i++)
                {
                    BigInteger maskForFindThePieceLocation = BigInteger.Parse("100000000000000000000", NumberStyles.HexNumber);
                    while (maskForFindThePieceLocation != 0)
                    {
                        // if there is piece in the current location
                        if ((currentPlayer.PiecesLocation[i].State & maskForFindThePieceLocation) != 0)
                            GetMoveListFromPiece(possibleMoves, currentPlayer.PiecesLocation[i], i, maskForFindThePieceLocation, board);
                        maskForFindThePieceLocation >>= 1;
                    }
                }
            }
            // return move options
            return possibleMoves;
        }

        public static void GetMoveListFromPiece(List<Move> possibleMoves, Piece currentPiece, int pieceType, BigInteger pieceLocation, Board board)
        {
            BigInteger pieceMoveOptions = currentPiece.getPlacesToMove(pieceLocation, board);
            BigInteger maskForFindTheMoveOptionsLocation = BigInteger.Parse("100000000000000000000", NumberStyles.HexNumber);
            while (maskForFindTheMoveOptionsLocation != 0)
            {
                if ((pieceMoveOptions & maskForFindTheMoveOptionsLocation) != 0)
                {
                    // check if we must promote the current piece in this location
                    if (board.DoesPieceNeedPromotion(pieceType, maskForFindTheMoveOptionsLocation))
                        possibleMoves.Add(new Move(pieceType, pieceLocation, maskForFindTheMoveOptionsLocation, true));
                    else
                    {
                        // Add the move to the move list
                        possibleMoves.Add(new Move(pieceType, pieceLocation, maskForFindTheMoveOptionsLocation, false));

                        // check if possible to promote the current piece in this location
                        if (board.IsPossibleToPromotePiece(pieceType, maskForFindTheMoveOptionsLocation))
                            possibleMoves.Add(new Move(pieceType, pieceLocation, maskForFindTheMoveOptionsLocation, true));
                    }
                }
                maskForFindTheMoveOptionsLocation >>= 1;
            }
        }

        public static Move getBestMove(List<Move> moves, Board board)
        {
            Move bestMove = null;
            int maxgrade = int.MinValue;
            foreach (Move move in moves)
            {
                DoVirtualMove(move, board);
                int grade = HeuristicFunction(board);
                if (grade > maxgrade)
                {
                    maxgrade = grade;
                    bestMove = move;
                }
                UndoVirtualMove(move, board);
            }
            return bestMove;
        }

        public static void DoVirtualMove(Move move, Board board)
        {
            // do move
            board.Turn.PiecesLocation[move.PieceType].State ^= move.From;
            board.Turn.PiecesLocation[move.PieceType].State ^= move.To;
            if (move.IsPromoted)
                board.Turn.PromotePiece(move.To, move.PieceType);
            Player otherPlayer = board.getOtherPlayer();
            if ((otherPlayer.GetAllPiecesLocations() & move.To) != 0)
                move.AttackedPieceType = otherPlayer.DeletePieceFromLocation(move.To);
        }
        
        public static void UndoVirtualMove(Move move, Board board)
        {
            // undo move
            if (move.IsPromoted)
                board.Turn.UndoPromotePiece(move.To, move.PieceType);
            board.Turn.PiecesLocation[move.PieceType].State ^= move.To;
            board.Turn.PiecesLocation[move.PieceType].State ^= move.From;
            if (move.AttackedPieceType != -1)
                board.getOtherPlayer().PiecesLocation[move.AttackedPieceType].State ^= move.To;
        }

        public static int HeuristicFunction(Board board)
        {
            int scoreComp = EvalPlayer(board.Player2);
            int scorePlayer = EvalPlayer(board.Player1);
            return board.Turn.IsPlayer1 ? scorePlayer - scoreComp : scoreComp - scorePlayer;
        }

        private static int EvalPlayer(Player player)
        {
            int totalScore = 0;
            foreach (Piece piece in player.PiecesLocation)
            {
                BigInteger bitboard = piece.State;
                while (bitboard != 0)
                {
                    int square = HandleBitwise.GetFirst1BitLocation(bitboard);
                    totalScore += piece.PieceScore;

                    if (player.IsPlayer1)
                        totalScore += piece.MoveScore[square];
                    else
                        totalScore += piece.MoveScore[Constants.mirrorBitBoard[square]];

                    bitboard = HandleBitwise.PopFirst1Bit(bitboard);
                }
            }
            return totalScore;
        }
    }
}
