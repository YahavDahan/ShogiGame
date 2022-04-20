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
        const int DEPTH = 3;
        public static bool DoStep(Board board)
        {
            // ----- NegaAlphaBeta -----
            // get all the possible moves
            List<Move> moves = GetAllPossibleMoves(board);
            // if there is no possible moves
            if (moves.Count == 0)
                return true;
            // find the best move
            Move bestMove = GetBestMove(moves, board, DEPTH);
            // do best move
            board.MovePiece(bestMove);
            // the game didnt over
            return false;

            //// MiniMax without AlphaBeta
            //Move bestMove = Minimax(board, DEPTH);
            //board.MovePiece(bestMove);
            //return false;

            ////////// NegaAlphaBeta
            ////////Move bestMove = GetBestMove(board, DEPTH);
            ////////board.MovePiece(bestMove);
            ////////return false;
        }

        public static List<Move> GetAllPossibleMoves(Board board)
        {
            List<Move> possibleMoves = new List<Move>();
            Player currentPlayer = board.Turn;
            // get king move options
            GetMoveListFromPiece(possibleMoves, currentPlayer.PiecesLocation[0], 0, currentPlayer.PiecesLocation[0].State, board);
            if (!currentPlayer.IsCheck)
            {
                // get all other move options
                for (int i = 1; i < currentPlayer.PiecesLocation.Length; i++)
                {
                    BigInteger maskForFindThePieceLocation = Constants.THE_LAST_LOCATION_ON_THE_BOARD;
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
            BigInteger maskForFindTheMoveOptionsLocation = Constants.THE_LAST_LOCATION_ON_THE_BOARD;
            while (maskForFindTheMoveOptionsLocation != 0)
            {
                if ((pieceMoveOptions & maskForFindTheMoveOptionsLocation) != 0)
                {
                    // check if we must promote the current piece in this location
                    if (board.DoesPieceNeedPromotion(pieceType, maskForFindTheMoveOptionsLocation))
                        possibleMoves.Add(new Move(pieceType, pieceLocation, maskForFindTheMoveOptionsLocation, true, board.Turn.IsCheck));
                    else
                    {
                        // Add the move to the move list
                        possibleMoves.Add(new Move(pieceType, pieceLocation, maskForFindTheMoveOptionsLocation, false, board.Turn.IsCheck));

                        // check if possible to promote the current piece in this location
                        if (board.IsPossibleToPromotePiece(pieceType, maskForFindTheMoveOptionsLocation))
                            possibleMoves.Add(new Move(pieceType, pieceLocation, maskForFindTheMoveOptionsLocation, true, board.Turn.IsCheck));
                    }
                }
                maskForFindTheMoveOptionsLocation >>= 1;
            }
        }

        //public static Move GetBestMove(List<Move> moves, Board board)
        //{
        //    Move bestMove = null;
        //    int maxgrade = int.MinValue;
        //    foreach (Move move in moves)
        //    {
        //        DoVirtualMove(move, board);
        //        int grade = HeuristicFunction(board);
        //        if (grade > maxgrade)
        //        {
        //            maxgrade = grade;
        //            bestMove = move;
        //        }
        //        UndoVirtualMove(move, board);
        //    }
        //    return bestMove;
        //}

        public static Move GetBestMove(List<Move> moves, Board board, int depth)
        {
            Move bestMove = null;
            int maxgrade = int.MinValue;
            foreach (Move move in moves)
            {
                DoVirtualMove(move, board);
                int grade = NegaAlphaBeta(board, depth-1, maxgrade, int.MaxValue);
                if (grade > maxgrade)
                {
                    maxgrade = grade;
                    bestMove = move;
                }
                UndoVirtualMove(move, board);
            }
            return bestMove;
        }

        //public static Move GetBestMove(Board board, int depth)
        //{
        //    Move bestMove = null;
        //    board.Turn = board.getOtherPlayer();
        //    NegaAlphaBeta(board, depth, int.MinValue, int.MaxValue, ref bestMove);
        //    board.Turn = board.getOtherPlayer();
        //    return bestMove;
        //}

        //private static int NegaAlphaBeta(Board board, int depth, int alpha, int beta, ref Move bestMove)
        //{
        //    if (depth == 0 || board.CheckIfGameIsOver())
        //        return HeuristicFunction(board);
        //    board.Turn = board.getOtherPlayer();
        //    List<Move> moves = GetAllPossibleMoves(board);
        //    int best = int.MinValue;
        //    foreach (Move move in moves)
        //    {
        //        DoVirtualMove(move, board);
        //        int value = 0 - NegaAlphaBeta(board, depth - 1, -beta, -alpha);
        //        if (value > best)
        //        {
        //            best = value;
        //            bestMove = move;
        //        }
        //        alpha = Math.Max(best, alpha);
        //        UndoVirtualMove(move, board);
        //        if (alpha != best)
        //            break;
        //        if (alpha >= beta)
        //            break;
        //    }
        //    board.Turn = board.getOtherPlayer();
        //    return best;
        //}

        private static int NegaAlphaBeta(Board board, int depth, int alpha, int beta)
        {
            if (depth == 0 || board.CheckIfGameIsOver())
                return HeuristicFunction(board);
            board.Turn = board.getOtherPlayer();
            List<Move> moves = GetAllPossibleMoves(board);
            int best = int.MinValue;
            foreach (Move move in moves)
            {
                DoVirtualMove(move, board);
                best = Math.Max(best, 0 - NegaAlphaBeta(board, depth - 1, -beta, -alpha));
                alpha = Math.Max(best, alpha);
                UndoVirtualMove(move, board);
                if (alpha != best)
                    break;
                if (alpha >= beta)
                    break;
            }
            board.Turn = board.getOtherPlayer();
            return best;
        }

        public static Move Minimax(Board board, int depth)
        {
            Move bestMove = null;
            board.Turn = board.getOtherPlayer();
            MaxLevel(board, depth, ref bestMove);
            board.Turn = board.getOtherPlayer();
            return bestMove;
        }

        public static int MaxLevel(Board board, int depth, ref Move bestMove)
        {
            if (depth == 0 || board.CheckIfGameIsOver())
                return HeuristicFunction(board);
            board.Turn = board.getOtherPlayer();
            List<Move> moves = GetAllPossibleMoves(board);
            int best = int.MinValue;
            foreach (Move move in moves)
            {
                DoVirtualMove(move, board);
                int value = MinLevel(board, depth - 1, bestMove);
                if (value > best)
                {
                    best = value;
                    bestMove = move;
                }
                UndoVirtualMove(move, board);
            }
            board.Turn = board.getOtherPlayer();
            return best;
        }

        public static int MinLevel(Board board, int depth, Move bestMove)
        {
            if (depth == 0 || board.CheckIfGameIsOver())
                return HeuristicFunction(board);
            board.Turn = board.getOtherPlayer();
            List<Move> moves = GetAllPossibleMoves(board);
            int best = int.MaxValue;
            foreach (Move move in moves)
            {
                DoVirtualMove(move, board);
                int value = MaxLevel(board, depth - 1, ref bestMove);
                if (value < best)
                    best = value;
                UndoVirtualMove(move, board);
            }
            board.Turn = board.getOtherPlayer();
            return best;
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
            move.HasBeenCheckBeforeTheMove = board.Turn.IsCheck;
            board.Turn.IsCheck = false;
            move.DidTheMoveCauseCheckOnTheOtherPlayer = otherPlayer.IsCheck;
            otherPlayer.IsCheck = board.IsThereCheckOnTheOtherPlayer();
        }
        
        public static void UndoVirtualMove(Move move, Board board)
        {
            // undo move
            if (move.IsPromoted)
                board.Turn.UndoPromotePiece(move.To, move.PieceType);
            board.Turn.PiecesLocation[move.PieceType].State ^= move.To;
            board.Turn.PiecesLocation[move.PieceType].State ^= move.From;
            Player otherPlayer = board.getOtherPlayer();
            if (move.AttackedPieceType != -1)
                otherPlayer.PiecesLocation[move.AttackedPieceType].State ^= move.To;
            board.Turn.IsCheck = move.HasBeenCheckBeforeTheMove;
            otherPlayer.IsCheck = move.DidTheMoveCauseCheckOnTheOtherPlayer;
        }

        public static int HeuristicFunction(Board board)
        {
            int scoreComp = EvalPlayer(board, board.Player2);
            int scorePlayer = EvalPlayer(board, board.Player1);
            return board.Turn.IsPlayer1 ? scorePlayer - scoreComp : scoreComp - scorePlayer;
        }

        private static int EvalPlayer(Board board, Player player)
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
                        totalScore += piece.MoveScore[Constants.MIRROR_BITBOARD[square]];

                    bitboard = HandleBitwise.PopFirst1Bit(bitboard);
                }
            }

            if (player.IsPlayer1)
            {
                if (board.IsThereCheckOnPlayer2())
                {
                    totalScore += 40;
                    Player saveTurn = board.Turn;
                    board.Turn = board.Player2;
                    if (board.Player2.PiecesLocation[0].getPlacesToMove(board.Player2.PiecesLocation[0].State, board) == 0)
                        totalScore += 999999;
                    board.Turn = saveTurn;
                }
            }
            else
            {
                if (board.IsThereCheckOnPlayer1())
                {
                    totalScore += 40;
                    Player saveTurn = board.Turn;
                    board.Turn = board.Player1;
                    if (board.Player1.PiecesLocation[0].getPlacesToMove(board.Player1.PiecesLocation[0].State, board) == 0)
                        totalScore += 999999;
                    board.Turn = saveTurn;
                }
            }

            return totalScore;
        }
    }
}
