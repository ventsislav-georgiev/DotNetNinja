using System;
using System.Windows.Forms;

namespace DotNetNinja.Core.Forms
{
    public partial class MainMenu : Form
    {
        private GameState _gameState;

        public MainMenu()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.InitializeComponent();

            this._gameState = null;
            MessageForm = new Form() { WindowState = FormWindowState.Maximized, TopMost = true };
        }

        public static Form MessageForm { get; private set; }

        private void NewGame(object sender, EventArgs e)
        {
            var game = new Game(this._gameState);
            game.FormClosed += game_FormClosed;
            this._gameState = game.GameState;

            btn_NewGame.Text = "Continue";
            btn_Restart.Show();
            this.Hide();
            game.Show();
        }

        private void game_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void btn_Restart_Click(object sender, EventArgs e)
        {
            this._gameState = null;
            Sounds.StopSound();
            btn_NewGame.Text = "New Game";
            btn_Restart.Hide();
        }
    }
}