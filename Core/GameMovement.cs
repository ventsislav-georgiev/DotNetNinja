using DotNetNinja.Core.Human;
using DotNetNinja.Core.Human.Enemies;
using DotNetNinja.Core.Item;
using DotNetNinja.Core.Map;
using DotNetNinja.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace DotNetNinja.Core
{
    internal class GameMovement : World
    {
        private const string MissingAreaKey = "-";

        private static readonly Random random = new Random();

        private readonly GameState gameState;

        private IList<TextPopup> textPopups;

        private IPlayer player;

        public GameMovement(GameState gameState, SaveGameData savegame = null)
            : base(savegame)
        {
            this.gameState = gameState;

            this.textPopups = new List<TextPopup>();

            bool playerFound = false;
            for (int row = 0; row < Area.MapSizeY && !playerFound; row++)
            {
                for (int col = 0; col < Area.MapSizeX && !playerFound; col++)
                {
                    var mapTile = this.CurrentArea.GetMapTile(row, col);
                    if (mapTile.Type.HasValue && mapTile.Type == EntityType.Player)
                    {
                        playerFound = true;
                        this.player = mapTile.Sprite as Player;
                        if (savegame != null)
                        {
                            this.player.LoadSaveGame(savegame);
                            this.CurrentArea = this.WorldMap[savegame.Area];
                        }
                        mapTile.SetForegroundSprite(null);
                    }
                }
            }

            gameState.HUD.Update(this.player);
            Sounds.PlayBackgroundSound(LevelType.Level1);
        }

        public SaveGameData SaveGame()
        {
            return this.SaveGame(this.player);
        }

        public override void Update(double gameTime, double elapsedTime)
        {
            base.Update(gameTime, elapsedTime);

            this.player.Update(gameTime, elapsedTime);

            this.gameState.HUD.Update(this.player);
        }

        public override void Draw(IRenderer renderer)
        {
            base.Draw(renderer);

            this.player.Draw(renderer);

            if (this.player.IsFighting)
            {
                foreach (TextPopup popup in this.textPopups)
                {
                    popup.Draw(renderer);
                }
            }
        }

        public void KeyDown(KeyEventArgs e)
        {
            if (!this.player.IsAnimationEnabled && !this.player.IsFighting)
            {
                if (this.CheckForTeleportation(e))
                {
                    return;
                }

                switch (e.KeyCode)
                {
                    case Keys.Right:
                        this.TryMoveRight();
                        break;

                    case Keys.Left:
                        this.TryMoveLeft();
                        break;

                    case Keys.Up:
                        this.TryMoveUp();
                        break;

                    case Keys.Down:
                        this.TryMoveDown();
                        break;

                    case Keys.P:
                        this.player.AttackWithMagic(this.CurrentArea, this.textPopups);
                        break;
                }
            }
        }

        private void TryMoveRight()
        {
            if (this.player.CanMoveRight(Area.MapSizeXMaxIndex))
            {
                if (this.CheckNextTile(Direction.Right))
                {
                    this.player.MoveRight(GameState.ENTITIES_MOVE_SPEED);
                    this.CalculateSpriteNextLocation(false);
                }
            }
            else
            {
                this.ChangeArea(CurrentArea.EastArea, Direction.Right);
            }
        }

        private void TryMoveLeft()
        {
            if (this.player.CanMoveLeft(Area.MapSizeXMinIndex))
            {
                if (this.CheckNextTile(Direction.Left))
                {
                    this.player.MoveLeft(GameState.ENTITIES_MOVE_SPEED);
                    this.CalculateSpriteNextLocation(false);
                }
            }
            else
            {
                this.ChangeArea(CurrentArea.WestArea, Direction.Left);
            }
        }

        private void TryMoveUp()
        {
            if (this.player.CanMoveUp(Area.MapSizeYMinIndex))
            {
                if (this.CheckNextTile(Direction.Up))
                {
                    this.player.MoveUp(GameState.ENTITIES_MOVE_SPEED);
                    this.CalculateSpriteNextLocation(false);
                }
            }
            else
            {
                this.ChangeArea(CurrentArea.NorthArea, Direction.Up);
            }
        }

        private void TryMoveDown()
        {
            if (this.player.CanMoveDown(Area.MapSizeYMaxIndex))
            {
                if (this.CheckNextTile(Direction.Down))
                {
                    this.player.MoveDown(GameState.ENTITIES_MOVE_SPEED);
                    this.CalculateSpriteNextLocation(false);
                }
            }
            else
            {
                this.ChangeArea(CurrentArea.SouthArea, Direction.Down);
            }
        }

        private bool CheckForTeleportation(KeyEventArgs e)
        {
            var teleportLevel = GameMaster.Teleport(e);
            if (teleportLevel != null)
            {
                this.CurrentArea = this.WorldMap[teleportLevel];
                this.player.Position = new Point(GameMaster.SaveSpot);
                this.CalculateSpriteNextLocation(true);
                return true;
            }
            return false;
        }

        private bool CheckNextTile(Direction direction)
        {
            MapTile nextMapTile = this.CurrentArea.GetNextMapTile(direction, this.player.Position);
            if (this.CheckDoors(nextMapTile))
            {
                return false;
            }

            IEnemy enemy = nextMapTile.Sprite as IEnemy;
            if (enemy != null)
            {
                this.gameState.Fight(random, player, enemy, this.textPopups);
                return false;
            }

            if (nextMapTile.IsPassable)
            {
                nextMapTile.OnPlayerMove(player);
                return true;
            }

            return false;
        }

        private bool CheckDoors(MapTile mapTile)
        {
            if (mapTile.IsStateChangable && mapTile.IsPassable)
            {
                var obstacle = mapTile.Sprite as IObstacle;
                if (obstacle.State)
                {
                    return false;
                }

                if (this.player.HasKey)
                {
                    this.player.HasKey = false;
                    obstacle.ChangeState();
                    return false;
                }
                return true;
            }
            return false;
        }

        private void ChangeArea(string areaName, Direction direction)
        {
            if (areaName != MissingAreaKey)
            {
                this.CurrentArea = this.WorldMap[areaName];
                switch (direction)
                {
                    case Direction.Left:
                        this.player.Position.X = Area.MapSizeX - 1;
                        break;

                    case Direction.Right:
                        this.player.Position.X = 0;
                        break;

                    case Direction.Up:
                        this.player.Position.Y = Area.MapSizeY - 1;
                        break;

                    case Direction.Down:
                        this.player.Position.Y = 0;
                        break;

                    default:
                        throw new InvalidOperationException();
                }
                this.CalculateSpriteNextLocation(true);

                var areaNameTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(areaName);
                Sounds.PlayBackgroundSound((LevelType)Enum.Parse(typeof(LevelType), areaNameTitleCase));
            }
        }

        private void CalculateSpriteNextLocation(bool updateTheLocation)
        {
            this.player.CalculateSpriteLocation(updateTheLocation, Tile.TileSizeX, Tile.TileSizeY, Area.AreaOffsetX, Area.AreaOffsetY);
        }
    }
}