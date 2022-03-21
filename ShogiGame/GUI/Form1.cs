using ShogiGame.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShogiGame.GUI
{
    public partial class Form1 : Form
    {
        private Board board;
        private BigInteger choosenSquare;
        private Image backgroundImage;
        private Graphics g;

        public Form1()
        {
            InitializeComponent();
            backgroundImage = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/board.png");
            this.g = this.CreateGraphics();
            this.choosenSquare = 0;
            this.board = new Board();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Rectangle rc = new Rectangle(110, 40, 600, 600);
            e.Graphics.DrawImage(backgroundImage, rc);
            this.PrintBoardState();
        }


        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            //// if the user have to answer the quastion ("do you want to promote this piece?") before continue playing
            //if ()
            BigInteger currentSquare = HandleBitwise.GetSquareFromLocation(e.Location);  // צריך לשלוח מיקום לחיצה על הלוח
            if (currentSquare == 0)  // אם לא נבחרה משבצת
                return;
            Player currentPlayer = board.Turn;  // השחקן הנוכחי
            // אם נבחר כלי של השחקן הנוכחי
            if (currentPlayer.IsOwnPieceSelected(currentSquare))
            {
                BigInteger moveOptions = 0;
                this.choosenSquare = currentSquare;
                try
                {
                    moveOptions = currentPlayer.GetMoveOptions(currentSquare, this.board);
                }
                catch (Exceptions.GameOverException ex)
                {
                    ex.PrintGameOverMessage(textBox1);
                }
                this.ShowOptions(moveOptions);  // private - פעולה במחלקה הנוכחית
            }
            // אם כבר נבחר כלי וכעת בוחרים אחת מאפשרויות התזוזה
            else if (this.choosenSquare != 0 && (currentSquare & currentPlayer.GetMoveOptions(this.choosenSquare, this.board)) != 0)
            {
                if (currentPlayer.MovePiece(this.choosenSquare, currentSquare, this.board, this.g))  // הזזת כלי בלוח ובדיקה אם ניתן לקידום והחלפת תור
                {
                    this.PrintBoardState();
                    DialogResult confirmResult = MessageBox.Show("Do you want to promote this piece?", "Confirm Promote", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)  // ask the user if he want to promote the piece, if yes :
                        currentPlayer.PromotePiece(-1, currentSquare, this.board, this.g);
                    else
                        currentPlayer.IsThereCheckAndReplaceTurn(currentSquare, this.board, this.g);
                }
                this.choosenSquare = 0;
                this.PrintBoardState();  // הדפסת מצב הלוח לאחר ההזזה   ,  private - פעולה במחלקה הנוכחית
            }
        }

        private void ShowOptions(BigInteger moveOptions)
        {
            PrintBoardState();
            if (moveOptions != 0)
            {
                BigInteger maskForChecking = BigInteger.Parse("100000000000000000000", NumberStyles.HexNumber);
                while (maskForChecking != 0)
                {
                    if ((moveOptions & maskForChecking) != 0)
                    {
                        Point location = HandleBitwise.GetLocationFromMask(maskForChecking);
                        Image greenFrame = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/greenFrame.png");
                        g.DrawImage(greenFrame, location.X, location.Y, 55, 55);
                    }
                    maskForChecking >>= 1;
                }
            }
        }

        private void PrintBoardState()
        {
            // איפוס לוח
            Rectangle rc = new Rectangle(110, 40, 600, 600);
            this.g.DrawImage(backgroundImage, rc);

            // print down player's pieces
            Player downPlayer = board.Player1;
            for (int i = 0; i < downPlayer.PiecesLocation.Length; i++)
                downPlayer.PiecesLocation[i].PrintPiece(this.g, true);

            // print down player's pieces
            Player abovePlayer = board.Player2;
            for (int i = 0; i < abovePlayer.PiecesLocation.Length; i++)
                abovePlayer.PiecesLocation[i].PrintPiece(this.g, false);
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show("Do you want to close the game?", "Confirm Exit", MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes)
                e.Cancel = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("The game has been closed successfully.", "Game Closed", MessageBoxButtons.OK);
            Application.Exit();
        }
    }
}
