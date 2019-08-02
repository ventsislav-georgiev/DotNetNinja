using System;

namespace DotNetNinja.Core.Item
{
    internal class DynamicItem : Item, ICollectable
    {
        public int ItemRate { get; private set; }

        public DynamicItem(float x, float y, Entity entity, bool flip = false)
            : base(x, y, entity, flip)
        {
            if (this.Entity.Special != string.Empty)
            {
                this.ItemRate = Convert.ToInt32(this.Entity.Special);
            }
        }
    }
}