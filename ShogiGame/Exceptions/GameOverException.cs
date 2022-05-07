using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Exceptions
{
    public class GameOverException : Exception
    {
        /// <summary>
        /// constructor for the exception with a message
        /// </summary>
        /// <param name="message">the message of the exception</param>
        public GameOverException(string message)
        : base(message) { }

        /// <summary>
        /// the function tell the user whern the game is over and which player won the game
        /// </summary>
        /// <param name="text">the text bot to print the message in</param>
        public void PrintGameOverMessage(System.Windows.Forms.TextBox text)
        {
            text.Text = Message;
        }
    }
}
