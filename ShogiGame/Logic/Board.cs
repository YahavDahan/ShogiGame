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

        public Board()
        {
            this.player1 = new Player(true);
            this.player2 = new Player(false);
            this.turn = this.player1;
        }

        public Board(Board boardToCopy)
        {
            this.player1 = new Player(boardToCopy.player1);
            this.player2 = new Player(boardToCopy.player2);
            if (boardToCopy.turn.IsPlayer1)
                this.turn = this.player1;
            else
                this.turn = this.player2;
        }

        public Player Turn { get => turn; set => turn = value; }

        public Player Player1 { get => player1; }

        public Player Player2 { get => player2; }

        public Player getOtherPlayer()
        {
            if (this.turn == this.player1)
                return this.player2;
            return this.player1;
        }

        public BigInteger GetMoveOptions(BigInteger from)
        {  // 0 אין לאן לזוז
            if (this.turn.IsCheck)
            {
                if ((from & this.turn.PiecesLocation[0].State) != 0)  // המלך נבחר
                    return this.turn.PiecesLocation[0].getPlacesToMove(this.turn.PiecesLocation[0].State, this);
                return 0;  // לא ניתן להזיז כלי שהוא לא המלך כשיש שח
            }
            for (int i = 0; i < this.turn.PiecesLocation.Length; i++)
                if ((from & this.turn.PiecesLocation[i].State) != 0)
                    return this.turn.PiecesLocation[i].getPlacesToMove(from, this);
            return 0;
        }

        public bool MovePiece(BigInteger from, BigInteger to)
        {
            // הזזת כלי בלוח ובדיקה אם ניתן לקידום או קידום בכוח והחלפת תור. מחזיר אם הכלי ניתן לקידום - צריך לשאול את המשתמש
            for (int i = 0; i < this.turn.PiecesLocation.Length; i++)
                if ((from & this.turn.PiecesLocation[i].State) != 0)
                {
                    // move the piece
                    this.turn.PiecesLocation[i].Move(from, to, this);
                    // 2. בדיקת קידום בכוח אם כן לקדם
                    if (DoesPieceNeedPromotion(i, to))
                    {
                        this.turn.PromotePiece(to, i);
                        break;
                    }
                    // 3. אם ן שונה מ0 שונה מ3 קטן שווה ל7 וגם טו נמצא בשורות הקידום
                    if (IsPossibleToPromotePiece(i, to))
                        return true; // לא הוחלף עדיין תור - צריך לבדור אם השחקן רוצה לבצע קידום
                    break;
                }
            return false;
        }

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

        public bool DoesPieceNeedPromotion(int i, BigInteger location)
        {
            if (this.turn.IsPlayer1)
            {
                if (i >= 1 && i <= 7 && i != 3 && Board.isLocatedInTopRow(location))
                    return true;
                if (i == 5 && (location & BigInteger.Parse("2FE00", NumberStyles.HexNumber)) != 0)
                    return true;
            }
            else
            {
                if (i >= 1 && i <= 7 && i != 3 && Board.isLocatedInBottomRow(location))
                    return true;
                if (i == 5 && (location & BigInteger.Parse("0FF8000000000000000", NumberStyles.HexNumber)) != 0)
                    return true;
            }
            return false;
        }

        public bool IsPossibleToPromotePiece(int i, BigInteger location)
        {
            if (this.turn.IsPlayer1)
            {
                if (i >= 1 && i <= 7 && i != 3 && (location & BigInteger.Parse("7FFFFFF", NumberStyles.HexNumber)) != 0)
                    return true;
            }
            else
            {
                if (i >= 1 && i <= 7 && i != 3 && (location & BigInteger.Parse("1FFFFFFC0000000000000", NumberStyles.HexNumber)) != 0)
                    return true;
            }
            return false;
        }

        public bool IsThereCheckOnTheOtherPlayer()
        {
            return (GetAllThePossibleMovesOfAllThePiecesOfTheCurrentPlayer() & getOtherPlayer().PiecesLocation[0].State) != 0;
        }

        public bool IsThereCheckOnPlayer1()
        {
            if (turn.IsPlayer1)
                return (GetAllThePossibleMovesOfAllThePiecesOfTheOtherPlayer() & player1.PiecesLocation[0].State) != 0;
            return (GetAllThePossibleMovesOfAllThePiecesOfTheCurrentPlayer() & player1.PiecesLocation[0].State) != 0;
        }

        public bool IsThereCheckOnPlayer2()
        {
            if (turn.IsPlayer1)
                return (GetAllThePossibleMovesOfAllThePiecesOfTheCurrentPlayer() & player2.PiecesLocation[0].State) != 0;
            return (GetAllThePossibleMovesOfAllThePiecesOfTheOtherPlayer() & player2.PiecesLocation[0].State) != 0;
        }

        public bool CheckIfGameIsOver()
        {
            this.turn = getOtherPlayer();
            BigInteger kingMovePlacesOfTheOtherPlayer = this.turn.PiecesLocation[0].getPlacesToMove(this.turn.PiecesLocation[0].State, this);
            this.turn = getOtherPlayer();
            return kingMovePlacesOfTheOtherPlayer == 0;
        }

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

        public BigInteger GetAllThePossibleMovesOfAllThePiecesOfTheOtherPlayer()
        { // מקבל את כל האופציות לתזוזה של כל החיילים של היריב
            this.turn = getOtherPlayer();
            BigInteger moveOptionsOfAllThePieces = GetAllThePossibleMovesOfAllThePiecesOfTheCurrentPlayer();
            this.turn = getOtherPlayer();
            return moveOptionsOfAllThePieces;
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
