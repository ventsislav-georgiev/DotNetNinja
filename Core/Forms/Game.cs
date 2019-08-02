using DotNetNinja.UI;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DotNetNinja.Core.Forms
{
    internal partial class Game : Form
    {
        private readonly Stopwatch gameTimeTracker;
        private double gameLastTimeUpdate;
        private GDIRenderer gameRenderer;
        private bool hasSavedState;

        public Game(GameState state = null)
        {
            this.gameTimeTracker = new Stopwatch();

            this.InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            this.GameState = state ?? new GameState();
            this.hasSavedState = state != null;
            this.gameRenderer = new GDIRenderer();
            this.Initialize();
        }

        public GameState GameState { get; set; }

        public void SaveGame()
        {
            if (this.GameState != null)
            {
                this.GameState.SaveGame();
                this.GameState.HUD.DrawMessage(this.gameRenderer, new[] { "Game saved!" });
            }
        }

        public void LoadGame()
        {
            var savedGameState = GameState.LoadGame();
            if (savedGameState != null)
            {
                this.GameState = savedGameState;
                this.GameState.HUD.DrawMessage(this.gameRenderer, new[] { "Game loaded!" });
            }
        }

        private void Initialize()
        {
            if (!this.hasSavedState)
            {
                this.GameState.Initialize();
            }

            gameLastTimeUpdate = 0.0;
            gameTimeTracker.Reset();
            gameTimeTracker.Start();
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            double gameTime = gameTimeTracker.ElapsedMilliseconds / 1000.0;
            double elapsedTime = gameTime - gameLastTimeUpdate;
            gameLastTimeUpdate = gameTime;

            this.GameState.Update(gameTime, elapsedTime);

            this.gameRenderer.SetGraphics(e.Graphics);
            this.GameState.Draw(this.gameRenderer);

            this.Invalidate();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    this.SaveGame();
                    break;

                case Keys.F6:
                    this.LoadGame();
                    break;

                default:
                    GameState.KeyDown(e);
                    break;
            }
        }

        private void Game_Shown(object sender, EventArgs e)
        {
            if (!this.hasSavedState)
            {
                Form help = new HelpForm();
                help.ShowDialog();
            }
        }
    }
}