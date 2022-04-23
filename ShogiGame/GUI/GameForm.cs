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
    public partial class GameForm : Form
    {
        /// <summary>
        /// the game board with the pieces location of every player
        /// </summary>
        private Board board;

        /// <summary>
        /// Indicates whether the game is one-VS-one or not
        /// </summary>
        private bool doesTheGameOneVSOne;

        /// <summary>
        /// Indicates whether the game is over
        /// </summary>
        private bool isGameOver;

        /// <summary>
        /// Contains the selected tool in the form of a BitBoard
        /// </summary>
        private BigInteger choosenSquare;

        /// <summary>
        /// Will contain the image of the board so that it can be put as a background
        /// </summary>
        private Image backgroundImage;

        /// <summary>
        /// Will contain the graphics of the form
        /// </summary>
        private Graphics g;

        /// <summary>
        /// Constractor for the form. Initializes the object properties, creates the game board and the graphics.
        /// </summary>
        /// <param name="doesTheGameOneVSOne">Indicates whether the game is one-VS-one or not (True if YES, False if NOT)</param>
        public GameForm(bool doesTheGameOneVSOne)
        {
            InitializeComponent();
            //this.DoubleBuffered = true;
            this.isGameOver = false;
            this.doesTheGameOneVSOne = doesTheGameOneVSOne;
            this.backgroundImage = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/board.png");
            this.g = this.CreateGraphics();
            this.choosenSquare = 0;
            this.board = new Board();
        }

        /// <summary>
        /// The function occurs at the beginning of the form creation, during the background creation
        /// </summary>
        /// <param name="e">The arguments of the painting</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Rectangle rc = new Rectangle(110, 40, 600, 600);
            e.Graphics.DrawImage(backgroundImage, rc);
            this.PrintBoardState();
        }

        /// <summary>
        /// The function occurs when the user click on the form and manages the game
        /// </summary>
        /// <param name="e">The arguments of the mouse click</param>
        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            // recieve the current square in bits form, from the location of the mouse click on the board
            BigInteger currentSquare = HandleBitwise.GetSquareFromLocation(e.Location);
            // If no square is selected do nothing
            if (currentSquare == 0)
                return;
            // if the game is one-VS-one
            if (this.doesTheGameOneVSOne)
                this.HumanPlayerStep(currentSquare);
            // if the game is player-VS-computer
            else
                this.PlayerVSComputerManager(currentSquare);
        }

        /// <summary>
        /// The function is responsible for managing player-VS-computer game
        /// </summary>
        /// <param name="currentSquare">the square the user chose on the board. The position is described by using bits</param>
        private void PlayerVSComputerManager(BigInteger currentSquare)
        {
            this.HumanPlayerStep(currentSquare);
            if (!this.board.Turn.IsPlayer1)
            {
                // do computer step
                if (Computer.DoStep(this.board))
                    // game over
                    textBox1.Text = "the player bellow won the game !!";

                // check if the other player king was attacked, if so the current player won the game
                if (this.board.getOtherPlayer().PiecesLocation[0].State == 0)
                {
                    textBox1.Text = "the player above won the game !!";
                    this.isGameOver = true;
                }

                // remove the check warning after the move
                this.RemoveCurrentPlayerCheckWarning();

                // check if there check on the other player
                if (this.board.IsThereCheckOnTheOtherPlayer())
                {
                    this.AddOtherPlayerCheckWarning();
                    if (this.board.CheckIfGameIsOver())
                        textBox1.Text = "the player above won the game !!";
                }

                // replace turn
                this.board.Turn = this.board.getOtherPlayer();
                this.PrintBoardState();
            }
        }

        /// <summary>
        /// The function is responsible for performing a user step
        /// </summary>
        /// <param name="currentSquare">the square the user chose on the board. The position is described by using bits</param>
        private void HumanPlayerStep(BigInteger currentSquare)
        {
            if (!this.isGameOver)
            {
                // if the user chose his own piece
                if (this.board.Turn.IsOwnPieceSelected(currentSquare))
                    this.ShowOptions(currentSquare);
                
                // if the user already chose a piece
                else if (this.choosenSquare != 0 && (currentSquare & this.board.GetMoveOptions(this.choosenSquare)) != 0)
                {
                    // move the piece from the choosenSquare to the currentSquare
                    if (this.board.MovePiece(this.choosenSquare, currentSquare))
                    {
                        this.PrintBoardState();
                        this.CheckIfTheUserWantToPromoteThePiece(currentSquare);
                    }

                    // check if the other player king was attacked, if so the current player won the game
                    if (this.board.getOtherPlayer().PiecesLocation[0].State == 0)
                    {
                        textBox1.Text = string.Format("the player {0}\n won the game !!", board.Turn.IsPlayer1 ? "below" : "above");
                        this.isGameOver = true;
                    }

                    // remove the check warning after the move
                    this.RemoveCurrentPlayerCheckWarning();

                    // check if there is check or game over
                    if (this.board.IsThereCheckOnTheOtherPlayer())
                    {
                        this.AddOtherPlayerCheckWarning();
                        if (this.board.CheckIfGameIsOver())
                            textBox1.Text = string.Format("the player {0}\n won the game !!", board.Turn.IsPlayer1 ? "below" : "above");
                    }

                    // replace turn
                    this.board.Turn = this.board.getOtherPlayer();

                    this.choosenSquare = 0;
                    this.PrintBoardState();
                }
            }
        }

        /// <summary>
        /// The function checks if the user wants to promote the piece and if so promotes it
        /// </summary>
        /// <param name="currentSquare">the square the user chose on the board. The position is described by using bits</param>
        private void CheckIfTheUserWantToPromoteThePiece(BigInteger currentSquare)
        {
            // ask the user if he want to promote the piece
            DialogResult confirmResult = MessageBox.Show("Do you want to promote this piece?", "Confirm Promote", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
                // promote the piece
                this.board.Turn.PromotePiece(currentSquare, -1);
        }

        /// <summary>
        /// The function removes the check warning of the current player
        /// </summary>
        private void RemoveCurrentPlayerCheckWarning()
        {
            this.board.Turn.IsCheck = false;
            // image that cover the check image
            Image coverImg = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/coverImage.png");
            if (this.board.Turn.IsPlayer1)
                g.DrawImage(coverImg, 710, 477, 156, 50);
            else
                g.DrawImage(coverImg, 710, 150, 156, 50);
        }

        /// <summary>
        /// The function adds check warning in the side of the other player
        /// </summary>
        private void AddOtherPlayerCheckWarning()
        {
            this.board.getOtherPlayer().IsCheck = true;
            // check image
            Image checkImg = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/check.png");
            if (this.board.Turn.IsPlayer1)
                g.DrawImage(checkImg, 710, 150, 156, 50);
            else
                g.DrawImage(checkImg, 710, 477, 156, 50);
        }

        /// <summary>
        /// The function shows the move options of the piece located in the location that selected by the user
        /// </summary>
        /// <param name="currentSquare">the square the user chose on the board. The position is described by using bits</param>
        private void ShowOptions(BigInteger currentSquare)
        {
            PrintBoardState();
            this.choosenSquare = currentSquare;
            // get the move options
            BigInteger moveOptions = this.board.GetMoveOptions(currentSquare);
            if (moveOptions != 0)
            {
                BigInteger maskForChecking = BigInteger.Parse("100000000000000000000", NumberStyles.HexNumber);
                while (maskForChecking != 0)
                {
                    if ((moveOptions & maskForChecking) != 0)
                    {
                        Point location = HandleBitwise.GetLocationFromMask(maskForChecking);
                        Image greenFrame = Image.FromFile("C:/ShogiGame/ShogiGame/Resources/Images/greenFrame.png");
                        // draw green square around the move option
                        g.DrawImage(greenFrame, location.X, location.Y, 55, 55);
                    }
                    maskForChecking >>= 1;
                }
            }
        }

        /// <summary>
        /// The function print the current state of the board
        /// </summary>
        private void PrintBoardState()
        {
            // create new empty board background
            Rectangle rc = new Rectangle(110, 40, 600, 600);
            this.g.DrawImage(backgroundImage, rc);

            // print down player's pieces
            Player downPlayer = board.Player1;
            for (int i = 0; i < downPlayer.PiecesLocation.Length; i++)
                downPlayer.PiecesLocation[i].PrintPiece(this.g, true);

            // print above player's pieces
            Player abovePlayer = board.Player2;
            for (int i = 0; i < abovePlayer.PiecesLocation.Length; i++)
                abovePlayer.PiecesLocation[i].PrintPiece(this.g, false);
        }

        /// <summary>
        /// The function ask the user if he sure that he wants to close the game when he click on the close button of the form
        /// </summary>
        /// <param name="e">The arguments of the form closing button</param>
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show("Do you want to close the game?", "Confirm Exit", MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes)
                e.Cancel = true;
        }

        /// <summary>
        /// The function tell the user that the form closed and close the form
        /// </summary>
        /// <param name="e">The arguments of the form close event</param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("The game has been closed successfully.", "Game Closed", MessageBoxButtons.OK);
            Application.Exit();
        }
    }
}
