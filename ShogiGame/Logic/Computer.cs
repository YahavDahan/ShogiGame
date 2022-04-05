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
            board.MovePiece(moves[0]);
            // find the best move
            // Move bestMove = getBestMove(moves, board);
            // do best move
            // board.MovePiece(bestMove);
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
                    // Add the move to the move list
                    possibleMoves.Add(new Move(pieceLocation, maskForFindTheMoveOptionsLocation, false));

                    // check if possible to promote the current piece in this location
                    if (board.IsPossibleToPromotePiece(pieceType, maskForFindTheMoveOptionsLocation))
                        possibleMoves.Add(new Move(pieceLocation, maskForFindTheMoveOptionsLocation, true));
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
        }

        public static void UndoVirtualMove(Move move, Board board)
        {
            // undo move
        }

        public static int HeuristicFunction(Board board)
        {
            // return grade for the move
            return 0;
        }
    }
}
