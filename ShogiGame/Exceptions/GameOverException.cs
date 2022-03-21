using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShogiGame.Exceptions
{
    class GameOverException : Exception
    {
        public GameOverException(string message)
        : base(message) { }

        public void PrintGameOverMessage(System.Windows.Forms.TextBox text)
        {
            text.Text = Message;
        }
    }
}
