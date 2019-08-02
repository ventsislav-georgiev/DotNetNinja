using DotNetNinja.Core.Human;
using DotNetNinja.UI;
using System;
using System.Diagnostics;
using System.Drawing;

namespace DotNetNinja.Core
{
    internal sealed class HUD : IHUD
    {
        private const float HUDSpritesSpacing = 1.46f;
        private const float HUDSpritePositionX = 10.5f;
        private const float HUDSpritePositionY = 0.3f;

        private const int HUDTextSpacing = 97;
        private const int HUDTextPositionX = 750;
        private const int HUDTextPositionY = 20;

        private const string FontFamily = "Arial";
        private const int FontSize = 24;

        private const int MaxMessageTime = 1000;

        private static readonly PointF hudSpritePosition = new PointF(HUDSpritePositionX, HUDSpritePositionY);
        private static readonly Point hudTextPosition = new Point(HUDTextPositionX, HUDTextPositionY);
        private static readonly Font font = new Font(FontFamily, FontSize);
        private static readonly Brush brush = new SolidBrush(Color.Black);
        private static readonly Brush yellowGreenBrush = new SolidBrush(Color.YellowGreen);

        public static HUD Instance { get; } = new HUD();

        private readonly Sprite currentLevel;
        private readonly Sprite experienceSprite;
        private readonly Sprite healthSprite;
        private readonly Sprite manaSprite;
        private readonly Sprite knowledgeSprite;
        private readonly Sprite defenseSprite;
        private readonly Sprite keySprite;

        private bool gameFinishSoundPlayed;

        private Stopwatch timer;
        private Action onUpdate;

        private HUD()
        {
            this.SetSprite(out this.currentLevel, hudSpritePosition, 0, new Entity(EntityType.Level));
            this.SetSprite(out this.experienceSprite, hudSpritePosition, HUDSpritesSpacing, new Entity(EntityType.Experience));
            this.SetSprite(out this.healthSprite, hudSpritePosition, HUDSpritesSpacing, new Entity(EntityType.Burger));
            this.SetSprite(out this.manaSprite, hudSpritePosition, HUDSpritesSpacing, new Entity(EntityType.Beer));
            this.SetSprite(out this.knowledgeSprite, hudSpritePosition, HUDSpritesSpacing, new Entity(EntityType.IntroCSharp));
            this.SetSprite(out this.defenseSprite, hudSpritePosition, HUDSpritesSpacing, new Entity(EntityType.Keyboard));
            this.SetSprite(out this.keySprite, hudSpritePosition, HUDSpritesSpacing, new Entity(EntityType.Key));

            this.timer = new Stopwatch();
        }

        public bool GameIsWon { get; set; }

        public int Health { get; set; }

        public int Mana { get; set; }

        public int Knowledge { get; set; }

        public int Defense { get; set; }

        public int Experience { get; set; }

        public bool HasKey { get; set; }

        public int Level { get; set; }

        public void Initialize()
        {
            this.gameFinishSoundPlayed = false;
            this.GameIsWon = false;
        }

        public void Draw(IRenderer renderer)
        {
            this.currentLevel.Draw(renderer);
            this.experienceSprite.Draw(renderer);
            this.healthSprite.Draw(renderer);
            this.manaSprite.Draw(renderer);
            this.knowledgeSprite.Draw(renderer);
            this.defenseSprite.Draw(renderer);
            if (this.HasKey)
            {
                this.keySprite.Draw(renderer);
            }

            var hudTextPosition = new Point(HUD.hudTextPosition);
            DrawString(renderer, this.Level.ToString(), hudTextPosition, 0);
            DrawString(renderer, this.Experience.ToString(), hudTextPosition, HUDTextSpacing);
            DrawString(renderer, this.Health.ToString(), hudTextPosition, HUDTextSpacing);
            DrawString(renderer, this.Mana.ToString(), hudTextPosition, HUDTextSpacing);
            DrawString(renderer, this.Knowledge.ToString(), hudTextPosition, HUDTextSpacing);
            DrawString(renderer, this.Defense.ToString(), hudTextPosition, HUDTextSpacing);

            if (this.Health == 0)
            {
                this.DrawMessage(renderer, new[] { "You died!", "Press 's' to play again" }, false);
            }

            if (this.GameIsWon)
            {
                this.DrawMessage(renderer, new[] { "You won!", "Press 's' to play again" }, false);
            }

            if (onUpdate != null && this.timer.ElapsedMilliseconds < MaxMessageTime)
            {
                onUpdate();
            }
            else
            {
                this.timer.Stop();
                onUpdate = null;
            }
        }

        public void DrawMessage(IRenderer renderer, string[] text, bool isTemporary = true)
        {
            Action action = () =>
            {
                renderer.DrawStringWithOutline(text[0], font, brush, yellowGreenBrush, 200, 250);
                if (text.Length > 1)
                {
                    renderer.DrawStringWithOutline(text[1], font, brush, yellowGreenBrush, 100, 300);
                }
            };
            if (isTemporary)
            {
                this.timer.Restart();
                this.onUpdate = action;
            }
            else
            {
                action();
            }
        }

        public void Update(IPlayer player)
        {
            this.Defense = player.Defense;
            this.Health = player.Health;
            this.Knowledge = player.Knowledge;
            this.Level = player.Level;
            this.Mana = player.Mana;
            this.Experience = player.Experience;
            this.HasKey = player.HasKey;

            if (player.HasCertificate && !this.gameFinishSoundPlayed)
            {
                this.GameIsWon = true;
                Sounds.StopSound();
                Sounds.End();
                this.gameFinishSoundPlayed = true;
            }
        }

        private void SetSprite(out Sprite sprite, PointF position, float spacing, Entity entity)
        {
            sprite = SpriteFactory.Create(position.X, position.Y += spacing, entity);
        }

        private void DrawString(IRenderer renderer, string text, Point position, int spacing)
        {
            renderer.DrawString(text, font, brush, position.X, position.Y += spacing);
        }
    }
}