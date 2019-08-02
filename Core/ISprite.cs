namespace DotNetNinja.Core
{
    internal interface ISprite : IGameEntity
    {
        PointF Location { get; set; }

        bool IsAnimationEnabled { get; set; }

        Point Position { get; set; }
    }
}