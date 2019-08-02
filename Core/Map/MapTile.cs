using DotNetNinja.Core.Human;
using DotNetNinja.Core.Item;
using DotNetNinja.UI;

namespace DotNetNinja.Core.Map
{
    internal class MapTile
    {
        private Sprite backgroundSprite;
        private Sprite foregroundSprite;

        public EntityType? Type
        {
            get
            {
                if (this.foregroundSprite != null)
                {
                    return this.foregroundSprite.Entity.Type;
                }
                else
                {
                    return null;
                }
            }
        }

        public EntityCategoryType? Category
        {
            get
            {
                if (this.foregroundSprite != null)
                {
                    return this.foregroundSprite.Category;
                }
                else
                {
                    return null;
                }
            }
        }

        public Sprite Sprite
        {
            get
            {
                if (this.foregroundSprite != null)
                {
                    return this.foregroundSprite;
                }
                else
                {
                    return null;
                }
            }
        }

        public PointF Location
        {
            get
            {
                return this.backgroundSprite.Location;
            }
        }

        public bool IsPassable
        {
            get
            {
                return (this.backgroundSprite.IsPassable || (this.foregroundSprite != null && this.foregroundSprite.IsStateChangable)) &&
                    (this.foregroundSprite == null || this.foregroundSprite.IsPassable);
            }
        }

        public bool IsStateChangable
        {
            get
            {
                return this.foregroundSprite != null && this.foregroundSprite.IsStateChangable;
            }
        }

        public MapTile(Sprite backgroundSprite, Sprite foregroundSprite = null)
        {
            this.backgroundSprite = backgroundSprite;
            this.backgroundSprite.UpdateSprite += UpdateBackgroundSprite;
            this.SetForegroundSprite(foregroundSprite);
        }

        public void SetForegroundSprite(Sprite foregroundSprite)
        {
            this.foregroundSprite = foregroundSprite;
            if (foregroundSprite != null)
            {
                this.foregroundSprite.UpdateSprite += UpdateForegroundSprite;
            }
        }

        public void UpdateBackgroundSprite(EntityType type, Point location)
        {
            this.backgroundSprite = SpriteFactory.Create(location, type);
        }

        public void UpdateForegroundSprite(EntityType type, Point location)
        {
            this.foregroundSprite = SpriteFactory.Create(location, type);
        }

        public void OnPlayerMove(IPlayer player)
        {
            var dynamicItem = this.Sprite as DynamicItem;
            if (dynamicItem == null)
            {
                return;
            }

            switch (dynamicItem.Category)
            {
                case EntityCategoryType.Knowledge:
                    player.Knowledge += dynamicItem.ItemRate;
                    Sounds.KnowledgeUp();
                    break;

                case EntityCategoryType.Defense:
                    player.Defense += dynamicItem.ItemRate;
                    Sounds.DefenseUp();
                    break;

                case EntityCategoryType.Health:
                    player.Health += dynamicItem.ItemRate;
                    Sounds.HealthUp();
                    break;

                case EntityCategoryType.Mana:
                    player.Mana += dynamicItem.ItemRate;
                    Sounds.ManaUp();
                    break;

                case EntityCategoryType.Certificate:
                    player.HasCertificate = true;
                    break;

                case EntityCategoryType.Key:
                    player.HasKey = true;
                    Sounds.Pickup();
                    break;
            }

            this.foregroundSprite = null;
        }

        public void Update(double gameTime, double elapsedTime)
        {
            this.backgroundSprite.Update(gameTime, elapsedTime);
            if (this.foregroundSprite != null)
            {
                if (this.foregroundSprite.FramesCount > 1)
                {
                    this.foregroundSprite.CurrentFrameIndex = Sprite.CalculateNextFrame(gameTime, this.foregroundSprite.FramesCount);
                }
                this.foregroundSprite.Update(gameTime, elapsedTime);
            }
        }

        public void Draw(IRenderer renderer)
        {
            this.backgroundSprite.Draw(renderer);
            if (this.foregroundSprite != null)
            {
                this.foregroundSprite.Draw(renderer);
            }
        }
    }
}