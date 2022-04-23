using ShogiGame.Classes;
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

        /// <summary>
        /// board constructor, initializes the players
        /// </summary>
        public Board()
        {
            this.player1 = new Player(true);
            this.player2 = new Player(false);
            this.turn = this.player1;
        }

        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="boardToCopy">board to copy</param>
        public Board(Board boardToCopy)
        {
            this.player1 = new Player(boardToCopy.player1);
            this.player2 = new Player(boardToCopy.player2);
            if (boardToCopy.turn.IsPlayer1)
                this.turn = this.player1;
            else
                this.turn = this.player2;
        }

        /// <summary>
        /// the turn now
        /// </summary>
        public Player Turn { get => turn; set => turn = value; }

        /// <summary>
        /// the down player
        /// </summary>
        public Player Player1 { get => player1; }

        /// <summary>
        /// the upper player
        /// </summary>
        public Player Player2 { get => player2; }

        /// <summary>
        /// the function returns the other player
        /// </summary>
        /// <returns>the opponent player</returns>
        public Player getOtherPlayer()
        {
            return this.turn == this.player1 ? this.player2 : this.player1;
        }

        /// <summary>
        /// the function finds all the possible options to move piece from specific location
        /// </summary>
        /// <param name="from">The location we want to get the move options from</param>
        /// <returns>the move options in BitBoard format</returns>
        public BigInteger GetMoveOptions(BigInteger from)
        {
            if (this.turn.IsCheck)
            {
                if ((from & this.turn.PiecesLocation[0].State) != 0)
                    return this.turn.PiecesLocation[0].getPlacesToMove(this.turn.PiecesLocation[0].State, this);
                return 0;  // cant move other piece except of the king
            }
            for (int i = 0; i < this.turn.PiecesLocation.Length; i++)
                if ((from & this.turn.PiecesLocation[i].State) != 0)
                    return this.turn.PiecesLocation[i].getPlacesToMove(from, this);
            return 0;
        }

        /// <summary>
        /// the function perform move of piece on the board and do promotion if need
        /// </summary>
        /// <param name="from">from location</param>
        /// <param name="to">to location</param>
        /// <returns>if the piece can get promotion</returns>
        public bool MovePiece(BigInteger from, BigInteger to)
        {
            for (int i = 0; i < this.turn.PiecesLocation.Length; i++)
                if ((from & this.turn.PiecesLocation[i].State) != 0)
                {
                    // move the piece
                    this.turn.PiecesLocation[i].Move(from, to, this);
                    // check if need to get promotion
                    if (DoesPieceNeedPromotion(i, to))
                    {
                        this.turn.PromotePiece(to, i);
                        break;
                    }
                    // check if the piece can get promotion
                    if (IsPossibleToPromotePiece(i, to))
                        return true;
                    break;
                }
            return false;
        }

        /// <summary>
        /// the function perform move of piece on the board and do promotion if need
        /// </summary>
        /// <param name="move">the move we want to perform</param>
        public void MovePiece(Move move)
        {
            for (int i = 0; i < this.turn.PiecesLocation.Length; i++)
                if ((move.From & this.turn.PiecesLocation[i].State) != 0)
                {
                    // move the piece
                    this.turn.PiecesLocation[i].Move(move.From, move.To, this);
                    // if the piece should get promotion
                    if (move.IsPromoted)
                        this.turn.PromotePiece(move.To, i);
                    break;
                }
        }

        /// <summary>
        /// the function checks if the piece must get promotion
        /// </summary>
        /// <param name="pieceType">the type of the piece we want to check</param>
        /// <param name="location">the location of the piece on the board</param>
        /// <returns>if the piece need to get promotion</returns>
        public bool DoesPieceNeedPromotion(int pieceType, BigInteger location)
        {
            if (this.turn.IsPlayer1)
            {
                if (pieceType >= 4 && pieceType <= 7 && Board.isLocatedInTopRow(location))
                    return true;
                if (pieceType == 5 && (location & BigInteger.Parse("2FE00", NumberStyles.HexNumber)) != 0)
                    return true;
            }
            else
            {
                if (pieceType >= 4 && pieceType <= 7 && Board.isLocatedInBottomRow(location))
                    return true;
                if (pieceType == 5 && (location & BigInteger.Parse("0FF8000000000000000", NumberStyles.HexNumber)) != 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// the function checks if the piece can get promotion
        /// </summary>
        /// <param name="pieceType">the type of the piece we want to check</param>
        /// <param name="location">the location of the piece on the board</param>
        /// <returns>if the piece can get promotion</returns>
        public bool IsPossibleToPromotePiece(int pieceType, BigInteger location)
        {
            if (this.turn.IsPlayer1)
            {
                if (pieceType >= 1 && pieceType <= 7 && pieceType != 3 && (location & BigInteger.Parse("7FFFFFF", NumberStyles.HexNumber)) != 0)
                    return true;
            }
            else
            {
                if (pieceType >= 1 && pieceType <= 7 && pieceType != 3 && (location & BigInteger.Parse("1FFFFFFC0000000000000", NumberStyles.HexNumber)) != 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// the function checks if there is check on the other player
        /// </summary>
        /// <returns>if there check on the other player</returns>
        public bool IsThereCheckOnTheOtherPlayer()
        {
            return (GetAllThePossibleMovesOfAllThePiecesOfTheCurrentPlayer() & getOtherPlayer().PiecesLocation[0].State) != 0;
        }

        /// <summary>
        /// the function checks if there is check on the down player
        /// </summary>
        /// <returns>if there check on the down player</returns>
        public bool IsThereCheckOnPlayer1()
        {
            if (turn.IsPlayer1)
                return (GetAllThePossibleMovesOfAllThePiecesOfTheOtherPlayer() & player1.PiecesLocation[0].State) != 0;
            return (GetAllThePossibleMovesOfAllThePiecesOfTheCurrentPlayer() & player1.PiecesLocation[0].State) != 0;
        }

        /// <summary>
        /// the function checks if there is check on the upper player
        /// </summary>
        /// <returns>if there check on the upper player</returns>
        public bool IsThereCheckOnPlayer2()
        {
            if (turn.IsPlayer1)
                return (GetAllThePossibleMovesOfAllThePiecesOfTheCurrentPlayer() & player2.PiecesLocation[0].State) != 0;
            return (GetAllThePossibleMovesOfAllThePiecesOfTheOtherPlayer() & player2.PiecesLocation[0].State) != 0;
        }

        /// <summary>
        /// the function check if the game is over
        /// </summary>
        /// <returns>returns true if the game is over, otherwise returns false</returns>
        public bool CheckIfGameIsOver()
        {
            this.turn = getOtherPlayer();
            BigInteger kingMovePlacesOfTheOtherPlayer = this.turn.PiecesLocation[0].getPlacesToMove(this.turn.PiecesLocation[0].State, this);
            this.turn = getOtherPlayer();
            return kingMovePlacesOfTheOtherPlayer == 0;
        }

        /// <summary>
        /// the function returns all the possible moves of all the pieces of the current player
        /// </summary>
        /// <returns>bitboard that represent all the possible moves of all the pieces of the current player</returns>
        public BigInteger GetAllThePossibleMovesOfAllThePiecesOfTheCurrentPlayer()
        {
            BigInteger moveOptionsOfAllThePieces = King.KingMoveOptions(this.turn.PiecesLocation[0].State, this);
            for (int i = 1; i < this.turn.PiecesLocation.Length; i++)
                if (this.turn.PiecesLocation[i].State != 0)
                {
                    if (HandleBitwise.IsPowerOfTwo(this.turn.PiecesLocation[i].State))
                        moveOptionsOfAllThePieces |= this.turn.PiecesLocation[i].getPlacesToMove(this.turn.PiecesLocation[i].State, this);
                    else
                    {
                        BigInteger maskForChecking = Constants.THE_LAST_LOCATION_ON_THE_BOARD;
                        while (maskForChecking != 0)
                        {
                            if ((this.turn.PiecesLocation[i].State & maskForChecking) != 0)
                                moveOptionsOfAllThePieces |= this.turn.PiecesLocation[i].getPlacesToMove(maskForChecking, this);
                            maskForChecking >>= 1;
                        }
                    }
                }
            return moveOptionsOfAllThePieces;
        }

        /// <summary>
        /// the function returns all the possible moves of all the pieces of the other player
        /// </summary>
        /// <returns>bitboard that represent all the possible moves of all the pieces of the other player</returns>
        public BigInteger GetAllThePossibleMovesOfAllThePiecesOfTheOtherPlayer()
        {
            this.turn = getOtherPlayer();
            BigInteger moveOptionsOfAllThePieces = GetAllThePossibleMovesOfAllThePiecesOfTheCurrentPlayer();
            this.turn = getOtherPlayer();
            return moveOptionsOfAllThePieces;
        }

        /// <summary>
        /// the function checks if the location that obtained as parameter is on the right side of the board
        /// </summary>
        /// <param name="locationForChecking">location for checking</param>
        /// <returns>returns true if the location is on the right side, otherwise returns false</returns>
        public static bool isLocatedInRightColumn(BigInteger locationForChecking)
        {
            BigInteger maskOfTheRightColumn = BigInteger.Parse("100804020100804020100", NumberStyles.HexNumber);
            return (locationForChecking & maskOfTheRightColumn) != 0;
        }

        /// <summary>
        /// the function checks if the location that obtained as parameter is on the left side of the board
        /// </summary>
        /// <param name="locationForChecking">location for checking</param>
        /// <returns>returns true if the location is on the left side, otherwise returns false</returns>
        public static bool isLocatedInLeftColumn(BigInteger locationForChecking)
        {
            BigInteger maskOfTheLeftColumn = BigInteger.Parse("1008040201008040201", NumberStyles.HexNumber);
            return (locationForChecking & maskOfTheLeftColumn) != 0;
        }

        /// <summary>
        /// the function checks if the location that obtained as parameter is on the top side of the board
        /// </summary>
        /// <param name="locationForChecking">location for checking</param>
        /// <returns>returns true if the location is on the top side, otherwise returns false</returns>
        public static bool isLocatedInTopRow(BigInteger locationForChecking)
        {
            BigInteger maskOfTheTopRow = BigInteger.Parse("1FF", NumberStyles.HexNumber);
            return (locationForChecking & maskOfTheTopRow) != 0;
        }

        /// <summary>
        /// the function checks if the location that obtained as parameter is on the bottom side of the board
        /// </summary>
        /// <param name="locationForChecking">location for checking</param>
        /// <returns>returns true if the location is on the bottom side, otherwise returns false</returns>
        public static bool isLocatedInBottomRow(BigInteger locationForChecking)
        {
            BigInteger maskOfTheBottomRow = BigInteger.Parse("1FF000000000000000000", NumberStyles.HexNumber);
            return (locationForChecking & maskOfTheBottomRow) != 0;
        }
    }
}
