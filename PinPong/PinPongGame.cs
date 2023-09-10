using System;
using System.Windows.Forms;

namespace PinPong
{
    public partial class P_area : Form
    {
        public P_area()
        {
            InitializeComponent();
            game = new Game(this);
        }
        Game game;
        private void PinPongGame_Load(object sender, EventArgs e)
        {
            game.Start();
        }
    }
}
