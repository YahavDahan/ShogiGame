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

        public Player(Player playerToCopy)
        {
            this.isPlayer1 = playerToCopy.isPlayer1;
            this.isCheck = playerToCopy.isCheck;
            this.piecesLocation = new Piece[14];
            this.piecesLocation[0] = new King(playerToCopy.piecesLocation[0].State);  // King
            this.piecesLocation[1] = new Rook(playerToCopy.piecesLocation[1].State);  // Rook
            this.piecesLocation[2] = new Bishop(playerToCopy.piecesLocation[2].State);  // Bishop
            this.piecesLocation[3] = new Gold(playerToCopy.piecesLocation[3].State);  // Golds
            this.piecesLocation[4] = new Silver(playerToCopy.piecesLocation[4].State);  // Silvers
            this.piecesLocation[5] = new Knight(playerToCopy.piecesLocation[5].State);  // Knights
            this.piecesLocation[6] = new Lance(playerToCopy.piecesLocation[6].State);  // Lances
            this.piecesLocation[7] = new Pawn(playerToCopy.piecesLocation[7].State);  // Pawns
            this.piecesLocation[8] = new PRook(playerToCopy.piecesLocation[8].State);  // Promoted Rook
            this.piecesLocation[9] = new PBishop(playerToCopy.piecesLocation[9].State);  // Promoted Bishop
            this.piecesLocation[10] = new PSilver(playerToCopy.piecesLocation[10].State);  // Promoted Silvers
            this.piecesLocation[11] = new PKnight(playerToCopy.piecesLocation[11].State);  // Promoted Knights
            this.piecesLocation[12] = new PLance(playerToCopy.piecesLocation[12].State);  // Promoted Lances
            this.piecesLocation[13] = new PPawn(playerToCopy.piecesLocation[13].State);  // Promoted Pawns
        }

        public Piece[] PiecesLocation { get => piecesLocation; }

        public bool IsPlayer1 { get => isPlayer1; set => isPlayer1 = value; }

        public bool IsCheck { get => isCheck; set => isCheck = value; }

        public bool IsOwnPieceSelected(BigInteger square)
        {
            BigInteger bitBoardOfPiecesLocations = GetAllPiecesLocations();
            return (bitBoardOfPiecesLocations & square) != 0;
        }

        public void PromotePiece(BigInteger location, int pieceType)
        {
            // Get piece type from location
            if (pieceType == -1)  // -1 mean unknown piece type
                pieceType = GetPieceTypeFromLocation(location);

            // promote piece
            this.piecesLocation[pieceType].State &= location ^ Constants.BITBOARD_OF_ONE;
            if (pieceType == 1 || pieceType == 2)  // rook or bishop
                this.piecesLocation[pieceType + 7].State |= location;
            else
                this.piecesLocation[pieceType + 6].State |= location;
        }

        public int GetPieceTypeFromLocation(BigInteger location)
        {
            for (int i = 0; i < this.piecesLocation.Length; i++)
                if ((location & this.piecesLocation[i].State) != 0)
                    return i;
            return -1;

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
    }
}
