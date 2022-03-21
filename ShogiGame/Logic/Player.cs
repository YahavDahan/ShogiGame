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
    public class Player
    {
        private Piece[] piecesLocation;
        private bool isPlayer1;
        private bool isCheck;

        public Player(bool isPlayer1)
        {
            this.isPlayer1 = isPlayer1;
            this.isCheck = false;
            this.piecesLocation = new Piece[14];
            if (isPlayer1)  // Down player
            {
                // King
                this.piecesLocation[0] = new King(BigInteger.Parse("10000000000000000000", NumberStyles.HexNumber));  //   000010000000000000000000000000000000000000000000000000000000000000000000000000000
                // Rook
                this.piecesLocation[1] = new Rook(BigInteger.Parse("400000000000000000", NumberStyles.HexNumber));  //   000000000010000000000000000000000000000000000000000000000000000000000000000000000
                // Bishop
                this.piecesLocation[2] = new Bishop(BigInteger.Parse("10000000000000000", NumberStyles.HexNumber));  //   000000000000000010000000000000000000000000000000000000000000000000000000000000000
                // Golds
                this.piecesLocation[3] = new Gold(BigInteger.Parse("28000000000000000000", NumberStyles.HexNumber));  //   000101000000000000000000000000000000000000000000000000000000000000000000000000000
                // Silvers
                this.piecesLocation[4] = new Silver(BigInteger.Parse("44000000000000000000", NumberStyles.HexNumber));  //   001000100000000000000000000000000000000000000000000000000000000000000000000000000
                // Knights
                this.piecesLocation[5] = new Knight(BigInteger.Parse("082000000000000000000", NumberStyles.HexNumber));  //   010000010000000000000000000000000000000000000000000000000000000000000000000000000
                // Lances
                this.piecesLocation[6] = new Lance(BigInteger.Parse("101000000000000000000", NumberStyles.HexNumber));  //   100000001000000000000000000000000000000000000000000000000000000000000000000000000
                // Pawns
                this.piecesLocation[7] = new Pawn(BigInteger.Parse("7FC0000000000000", NumberStyles.HexNumber));  //   000000000000000000111111111000000000000000000000000000000000000000000000000000000
            }
            else
            {
                // King
                this.piecesLocation[0] = new King(BigInteger.Parse("10", NumberStyles.HexNumber));  //   000000000000000000000000000000000000000000000000000000000000000000000000000010000
                // Rook
                this.piecesLocation[1] = new Rook(BigInteger.Parse("400", NumberStyles.HexNumber));  //   000000000000000000000000000000000000000000000000000000000000000000000010000000000
                // Bishop
                this.piecesLocation[2] = new Bishop(BigInteger.Parse("10000", NumberStyles.HexNumber));  //   000000000000000000000000000000000000000000000000000000000000000010000000000000000
                // Golds
                this.piecesLocation[3] = new Gold(BigInteger.Parse("28", NumberStyles.HexNumber));  //   000000000000000000000000000000000000000000000000000000000000000000000000100000001
                // Silvers
                this.piecesLocation[4] = new Silver(BigInteger.Parse("44", NumberStyles.HexNumber));  //   000000000000000000000000000000000000000000000000000000000000000000000000010000010
                // Knights
                this.piecesLocation[5] = new Knight(BigInteger.Parse("082", NumberStyles.HexNumber));  //   000000000000000000000000000000000000000000000000000000000000000000000000001000100
                // Lances
                this.piecesLocation[6] = new Lance(BigInteger.Parse("101", NumberStyles.HexNumber));  //   000000000000000000000000000000000000000000000000000000000000000000000000000101000
                // Pawns
                this.piecesLocation[7] = new Pawn(BigInteger.Parse("7FC0000", NumberStyles.HexNumber));  //   000000000000000000000000000000000000000000000000000000111111111000000000000000000
            }
            // Promoted Rook
            this.piecesLocation[8] = new PRook();
            // Promoted Bishop
            this.piecesLocation[9] = new PBishop();
            // Promoted Silvers
            this.piecesLocation[10] = new PSilver();
            // Promoted Knights
            this.piecesLocation[11] = new PKnight();
            // Promoted Lances
            this.piecesLocation[12] = new PLance();
            // Promoted Pawns
            this.piecesLocation[13] = new PPawn();
        }

        public Piece[] PiecesLocation { get => piecesLocation; }

        public bool IsPlayer1 { get => isPlayer1; set => isPlayer1 = value; }

        public bool IsCheck { get => isCheck; set => isCheck = value; }

        public bool IsOwnPieceSelected(BigInteger square)
        {
            BigInteger bitBoardOfPiecesLocations = GetAllPiecesLocations();
            return (bitBoardOfPiecesLocations & square) != 0;
        }

        public BigInteger GetMoveOptions(BigInteger from, Board board)
        {  // 0 אין לאן לזוז
            if (this.isCheck)
            {
                BigInteger kingMovePlaces = this.piecesLocation[0].getPlacesToMove(from, board);
                if (kingMovePlaces == 0)
                    throw new Exceptions.GameOverException(string.Format("the player {0}\n won the game !!", board.getOtherPlayer().isPlayer1 ? "below" : "above"));  // המשחק נגמר - היריב של השחקן הנוכחי ניצח
                else if ((from & this.piecesLocation[0].State) != 0)  // המלך נבחר
                    return kingMovePlaces;
                else
                    return 0;  // לא ניתן להזיז כלי שהוא לא המלך כשיש שח
            }
            for (int i = 0; i < this.piecesLocation.Length; i++)
                if ((from & this.piecesLocation[i].State) != 0)
                    return this.piecesLocation[i].getPlacesToMove(from, board);
            return 0;
        }

        public bool MovePiece(BigInteger from, BigInteger to, Board board, Graphics g)
        {
            // הזזת כלי בלוח ובדיקה אם ניתן לקידום או קידום בכוח והחלפת תור. מחזיר אם הכלי ניתן לקידום - צריך לשאול את המשתמש
            int i;
            for (i = 0; i < this.piecesLocation.Length; i++)
                if ((from & this.piecesLocation[i].State) != 0)
                {
                    // move the piece
                    this.piecesLocation[i].Move(from, to, board, g);
                    // 2. בדיקת קידום בכוח אם כן לקדם
                    if (this.DoesPieceNeedPromotion(i, to))
                    {
                        PromotePiece(i, to, board, g);
                        break;
                    }
                    // 3. אם ן שונה מ0 שונה מ3 קטן שווה ל7 וגם טו נמצא בשורות הקידום
                    if (this.IsPossibleToPromotePiece(i, to))
                        return true; // לא הוחלף עדיין תור - צריך לבדור אם השחקן רוצה לבצע קידום
                    break;
                }
            IsThereCheckAndReplaceTurn(to, board, g);
            return false;
        }

        public void PromotePiece(int pieceType, BigInteger location, Board board, Graphics g)
        {
            // Get piece type from location
            bool haveToCheckIfGameOverAndReplaceTurn = false;
            if (pieceType == -1)  // -1 mean unknown piece type
            {
                pieceType = GetPieceTypeFromLocation(location);
                haveToCheckIfGameOverAndReplaceTurn = true;
            }

            // promote piece
            this.piecesLocation[pieceType].State &= location ^ Constants.BITBOARD_OF_ONE;
            if (pieceType == 1 || pieceType == 2)  // rook or bishop
                this.piecesLocation[pieceType + 7].State |= location;
            else
                this.piecesLocation[pieceType + 6].State |= location;

            if (haveToCheckIfGameOverAndReplaceTurn)
                IsThereCheckAndReplaceTurn(location, board, g);
        }

        public void IsThereCheckAndReplaceTurn(BigInteger location, Board board, Graphics g)
        { // בדיקה אם שח בהפשעת תזוזת חייל או קידום
            // בדיקת שח או משחק נגמר במחלקת לוח
            Player otherPlayer = board.getOtherPlayer();
            if ((GetMoveOptions(location, board) & otherPlayer.piecesLocation[0].State) != 0)  // check
            {
                otherPlayer.isCheck = true;
                Image checkImg = Image.FromFile("C:/nisayon2/ShogiGame/ShogiGame/Resources/Images/check.png");
                if (otherPlayer.isPlayer1)
                    g.DrawImage(checkImg, 710, 477, 156, 50);
                else
                    g.DrawImage(checkImg, 710, 150, 156, 50);
            }

            // replace turn
            board.Turn = board.getOtherPlayer();
        }

        public int GetPieceTypeFromLocation(BigInteger location)
        {
            for (int i = 0; i < this.piecesLocation.Length; i++)
                if ((location & this.piecesLocation[i].State) != 0)
                    return i;
            return -1;

        }

        private bool IsPossibleToPromotePiece(int i, BigInteger location)
        {
            if (this.isPlayer1)
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

        private bool DoesPieceNeedPromotion(int i, BigInteger location)
        {
            if (this.isPlayer1)
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

        public void DeletePieceFromLocation(BigInteger from)
        {
            for (int i = 0; i < this.piecesLocation.Length; i++)
                if ((from & this.piecesLocation[i].State) != 0)
                {
                    this.piecesLocation[i].State ^= from;
                    break;
                }
        }

        public BigInteger GetAllPiecesLocations()
        {
            BigInteger result = BigInteger.Parse("0");
            for (int i = 0; i < this.piecesLocation.Length; i++)
                result |= piecesLocation[i].State;
            return result;
        }

        public BigInteger GetAllThePossibleMovesOfAllThePiecesOfTheOtherPlayer(Board board)
        { // מקבל את כל האופציות לתזוזה של כל החיילים של היריב 
            board.Turn = board.getOtherPlayer();

            BigInteger moveOptionsOfAllThePieces = King.KingMoveOptions(this.piecesLocation[0].State, board);
            for (int i = 1; i < this.piecesLocation.Length; i++)
                if (this.piecesLocation[i].State != 0)
                {
                    if (HandleBitwise.IsPowerOfTwo(this.piecesLocation[i].State))
                        moveOptionsOfAllThePieces |= this.piecesLocation[i].getPlacesToMove(this.piecesLocation[i].State, board);
                    else
                    {
                        BigInteger maskForChecking = BigInteger.Parse("100000000000000000000", NumberStyles.HexNumber);
                        while (maskForChecking != 0)
                        {
                            if ((this.piecesLocation[i].State & maskForChecking) != 0)
                                moveOptionsOfAllThePieces |= this.piecesLocation[i].getPlacesToMove(maskForChecking, board);
                            maskForChecking >>= 1;
                        }
                    }
                }

            board.Turn = board.getOtherPlayer();
            return moveOptionsOfAllThePieces;
        }
    }
}
