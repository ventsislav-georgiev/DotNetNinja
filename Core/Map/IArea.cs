namespace DotNetNinja.Core.Map
{
    internal interface IArea : IRenderable
    {
        string Name { get; }

        string NorthArea { get; }

        string EastArea { get; }

        string SouthArea { get; }

        string WestArea { get; }

        MapTile GetNextMapTile(Direction direction, Point position);

        MapTile GetMapTile(Point position);

        MapTile GetMapTile(int x, int y);
    }
}