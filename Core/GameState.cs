using DotNetNinja.Core.Human;
using DotNetNinja.Core.Human.Enemies;
using DotNetNinja.Core.Map;
using DotNetNinja.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace DotNetNinja.Core
{
    internal class GameState
    {
        public const string MISS_MESSAGE = "miss";

        public const int FRAME_RATE = 8;
        public const int ENTITIES_MOVE_SPEED = 200;

        private const int LUCKY_SCORE = 10;
        private const int MISS_NUMBER = 6;
        private const int MAX_SCORE_FOR_LUCKY_HIT = 5;

        private const string SaveGameFileName = "save.bin";

        public static GameState LoadGame()
        {
            if (!File.Exists(SaveGameFileName))
            {
                return null;
            }

            using (var stream = File.OpenRead(SaveGameFileName))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                var savedGameState = new GameState((SaveGameData)binaryFormatter.Deserialize(stream));
                return savedGameState;
            }
        }

        private GameMovement gameMovement;
        private SaveGameData savegame;

        public GameState(SaveGameData savegame = null)
        {
            this.savegame = savegame;
            this.HUD = Core.HUD.Instance;
            if (savegame != null)
            {
                this.Initialize();
            }
        }

        public IHUD HUD { get; private set; }

        public void Initialize()
        {
            this.HUD.Initialize();
            this.gameMovement = new GameMovement(this, this.savegame);
        }

        public void Fight(Random random, IPlayer player, IEnemy enemy, IList<TextPopup> popups)
        {
            player.IsFighting = true;
            popups.Clear();

            if (enemy as IBoss != null)
            {
                Sounds.BossFight();
            }
            else
            {
                Sounds.StudentFight();
            }

            int playerDamage = 0;

            if (random.Next(LUCKY_SCORE) != MISS_NUMBER)
            {
                if (enemy.Strength > player.Defense)
                {
                    int scopeHit = enemy.Strength - player.Defense;

                    playerDamage = random.Next((scopeHit / 100) * 10, scopeHit + 1);
                }
                else
                {
                    if (enemy.Strength + MAX_SCORE_FOR_LUCKY_HIT > player.Defense)
                    {
                        int scopeHit = (enemy.Strength + MAX_SCORE_FOR_LUCKY_HIT) - player.Defense;
                        playerDamage = random.Next((scopeHit / 100) * 50, scopeHit + 1);
                    }
                }

                player.Health -= playerDamage;

                if (player.Health <= 0)
                {
                    player.Die();
                }
            }

            string playerDamageMessage = playerDamage != 0 ? playerDamage.ToString() : MISS_MESSAGE;
            popups.Add(new TextPopup(player.Location.X + 40, player.Location.Y + 20, playerDamageMessage));

            if (random.Next(LUCKY_SCORE) != MISS_NUMBER)
            {
                int enemyDamage = 0;
                if (player.Knowledge >= enemy.Defense)
                {
                    int scopeHit = (player.Knowledge * 2) - player.Defense;
                    enemyDamage = random.Next((scopeHit / 100) * 30, scopeHit + 1);
                }
                if (enemyDamage > 0)
                {
                    int experiance = enemy.GetDamage(enemyDamage);
                    player.Experience += experiance;
                }
                string message = enemyDamage != 0 ? enemyDamage.ToString() : MISS_MESSAGE;
                popups.Add(new TextPopup(enemy.Location.X + 40, enemy.Location.Y + 20, message));
            }
            else
            {
                popups.Add(new TextPopup(enemy.Location.X + 40, enemy.Location.Y + 20, MISS_MESSAGE));
            }

            player.FightStartTime = -1;
        }

        public void SaveGame()
        {
            var savegame = this.gameMovement.SaveGame();
            using (var stream = File.OpenWrite(SaveGameFileName))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, savegame);
            }
        }

        public void Draw(IRenderer renderer)
        {
            this.gameMovement.Draw(renderer);
            this.HUD.Draw(renderer);
        }

        public void Update(double gameTime, double elapsedTime)
        {
            this.gameMovement.Update(gameTime, elapsedTime);
        }

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                default:
                    if (this.HUD.Health > 0 && !this.HUD.GameIsWon)
                    {
                        this.gameMovement.KeyDown(e);
                    }
                    else
                    {
                        if (e.KeyCode == Keys.S)
                        {
                            this.Initialize();
                        }
                    }
                    break;
            }
        }
    }
}