using DotNetNinja.Core.Human;
using DotNetNinja.Core.Human.Enemies;
using DotNetNinja.Core.Item;
using System;

namespace DotNetNinja.Core
{
    internal static class SpriteFactory
    {
        public static Sprite Create(Point location, EntityType entityType)
        {
            return Create(location.X, location.Y, new Entity(entityType.ToString()));
        }

        public static Sprite Create(int x, int y, Entity entity)
        {
            return Create((float)x, (float)y, entity);
        }

        public static Sprite Create(float x, float y, Entity entity)
        {
            Sprite sprite = null;
            switch (entity.Category)
            {
                case EntityCategoryType.Human:
                    switch (entity.Type)
                    {
                        case EntityType.Player:
                            sprite = new Player(x, y);
                            break;

                        case EntityType.Boss1:
                        case EntityType.Boss2:
                        case EntityType.Boss3:
                        case EntityType.Boss4:
                        case EntityType.Boss5:
                            sprite = new Boss(x, y, entity.Type);
                            break;

                        case EntityType.Student1:
                        case EntityType.Student2:
                        case EntityType.Student3:
                        case EntityType.Student4:
                            sprite = new Student(x, y, entity.Type);
                            break;

                        default:
                            throw new InvalidOperationException();
                    }
                    break;

                case EntityCategoryType.Key:
                case EntityCategoryType.Health:
                case EntityCategoryType.Mana:
                case EntityCategoryType.Knowledge:
                case EntityCategoryType.Defense:
                case EntityCategoryType.Certificate:
                    sprite = new DynamicItem(x, y, entity);
                    break;

                case EntityCategoryType.Door:
                case EntityCategoryType.WorldItems:
                    sprite = new StaticItem(x, y, entity);
                    break;

                default:
                    throw new InvalidOperationException();
            }
            return sprite;
        }
    }
}