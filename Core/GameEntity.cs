using DotNetNinja.UI;

namespace DotNetNinja.Core
{
    internal abstract class GameEntity : IRenderable, IGameEntity
    {
        public Entity Entity { get; private set; }

        public EntityCategoryType Category
        {
            get
            {
                return this.Entity.Category;
            }
        }

        public bool IsPassable
        {
            get
            {
                return this.Entity.IsPassable;
            }
        }

        public GameEntity(Entity entity)
        {
            this.Entity = entity;
        }

        public abstract void Update(double gameTime, double elapsedTime);

        public abstract void Draw(IRenderer graphics);
    }
}