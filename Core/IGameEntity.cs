namespace DotNetNinja.Core
{
    internal interface IGameEntity : IRenderable
    {
        Entity Entity { get; }
    }
}