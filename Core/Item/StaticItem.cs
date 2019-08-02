namespace DotNetNinja.Core.Item
{
    internal class StaticItem : Item, IObstacle
    {
        public bool State { get; private set; }

        public StaticItem(float x, float y, Entity entity, bool flip = false)
            : base(x, y, entity, flip)
        {
            if (this.IsStateChangable)
            {
                this.IsAnimationEnabled = false;
            }
        }

        public void ChangeState()
        {
            this.State = !this.State;
            this.IsAnimationEnabled = !this.IsAnimationEnabled;
            switch (this.Category)
            {
                case EntityCategoryType.Door:
                    Sounds.DoorOpen();
                    break;
            }
        }
    }
}