//using ShogiGame.Classes;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;

//namespace ShogiGame.Logic
//{
//    public static class Computer
//    {
//        public static void DoStep(Board board)
//        {
//            // get all the possible moves
//            List<Move> moves = GetAllPossiableMoves(board);
//            // find the best move
//            Move bestMove = getBestMove(moves);
//            // do best move
//            board.Turn.MovePiece(bestMove, board, g);
//        }

//        public static List<Move> GetAllPossibleMoves(Board board)
//        {
//            List<Move> possibleMoves = new List<Move>();
//            // get king move options
//            BigIntegerMovesToListOfMoves(possibleMoves, board.Turn.PiecesLocation[0].State, board.Turn.PiecesLocation[0].getPlacesToMove(board.Turn., board));
//            if (!board.Turn.IsCheck)
//            {
//                // get all move options
//            }
//            // return move options


//            for (int i = 1; i < board.Turn.PiecesLocation.Length; i++)
//                if (board.Turn.PiecesLocation[i].State != 0)
//                {
//                    if (HandleBitwise.IsPowerOfTwo(this.piecesLocation[i].State))
//                        moveOptionsOfAllThePieces |= this.piecesLocation[i].getPlacesToMove(this.piecesLocation[i].State, board);
//                    else
//                    {
//                        BigInteger maskForChecking = BigInteger.Parse("100000000000000000000", NumberStyles.HexNumber);
//                        while (maskForChecking != 0)
//                        {
//                            if ((this.piecesLocation[i].State & maskForChecking) != 0)
//                                moveOptionsOfAllThePieces |= this.piecesLocation[i].getPlacesToMove(maskForChecking, board);
//                            maskForChecking >>= 1;
//                        }
//                    }
//                }


//            for (int i = 0; i < board.Turn.PiecesLocation.Length; i++)
//            {
//                BigInteger moveOptions = 0;
//                try
//                {
//                    moveOptions = board.Turn.GetMoveOptions(currentSquare, this.board);
//                }
//                catch (Exceptions.GameOverException ex)
//                {
//                    ex.PrintGameOverMessage(textBox1);
//                }
//            }
//        }

//        public static void BigIntegerMovesToListOfMoves(BigInteger from, BigInteger toPossibles)
//        {


//        }

//        public static Move getBestMove(List<Move> moves)
//        {
//            Move bestMove = null;
//            int maxgrade = int.MinValue;
//            foreach (Move move in moves)
//            {
//                DoVirtualMove(move);
//                int grade = HeuristicFunction();
//                if (grade > maxgrade)
//                {
//                    maxgrade = grade;
//                    bestMove = move;
//                }
//                UndoVirtualMove(move);
//            }
//            return bestMove;
//        }
//    }
//}
