using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShogiGame.GUI
{
    public partial class MainMenu : Form
    {
        private bool isGameAlreadyStarted;

        public MainMenu()
        {
            InitializeComponent();
            this.isGameAlreadyStarted = false;
        }

        private void buttonOneVOne_Click(object sender, EventArgs e)
        {
            if (!this.isGameAlreadyStarted)
            {
                this.isGameAlreadyStarted = true;
                Form1 f1 = new Form1(true); // Instantiate the game as one V one.
                f1.Show(); // Show Form1
            }
        }

        private void buttonPlayerAgainstComputer_Click(object sender, EventArgs e)
        {
            if (!this.isGameAlreadyStarted)
            {
                this.isGameAlreadyStarted = true;
                Form1 f1 = new Form1(false); // Instantiate the game as player against computer.
                f1.Show(); // Show Form1
            }
        }

        private void buttonGameInstructions_Click(object sender, EventArgs e)
        {
            GameInstructionsForm f3 = new GameInstructionsForm(); // Instantiate the game instructions form.
            f3.Show(); // Show GameInstructionsForm
        }
    }
}
