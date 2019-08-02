namespace DotNetNinja.Core.Item
{
    internal abstract class Item : Sprite, IItem
    {
        public Item(float x, float y, Entity entity, bool flip = false)
            : base(x, y, entity, flip)
        {
        }
    }
}