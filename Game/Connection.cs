using System;
using System.Windows.Forms;

namespace Game
{
    public partial class Connection : Form
    {
        public Connection()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Game newGame = new Game(false, textBox1.Text);
            Visible = false;
            if (!newGame.IsDisposed) //No issues in the class ctor
                newGame.ShowDialog(); //Show the class
            Visible = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Game newGame = new Game(true);
            Visible = false;
            if (!newGame.IsDisposed)
                newGame.ShowDialog();
            Visible = true;
        }
    }
}
