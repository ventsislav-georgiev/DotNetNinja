using DotNetNinja.UI;
using System;
using System.IO;

namespace DotNetNinja.Core.Map
{
    internal class Area : IArea
    {
        public const int AreaOffsetX = 0;
        public const int AreaOffsetY = 0;
        public const int MapSizeX = 10;
        public const int MapSizeY = 10;

        public const int MapSizeXMaxIndex = MapSizeX - 1;
        public const int MapSizeYMaxIndex = MapSizeY - 1;

        public const int MapSizeXMinIndex = 0;
        public const int MapSizeYMinIndex = 0;

        private MapTile[,] tilesMap;

        public Area(StreamReader stream)
        {
            this.tilesMap = new MapTile[MapSizeX, MapSizeY];
            string line;

            this.Name = stream.ReadLine().ToLower();

            this.NorthArea = stream.ReadLine().ToLower();
            this.EastArea = stream.ReadLine().ToLower();
            this.SouthArea = stream.ReadLine().ToLower();
            this.WestArea = stream.ReadLine().ToLower();

            for (int row = 0; row < MapSizeY; row++)
            {
                line = stream.ReadLine();

                for (int col = 0; col < MapSizeX; col++)
                {
                    var entityKey = line[col].ToString();

                    var backgroundSprite = SpriteFactory.Create(col, row, new Entity(entityKey));
                    MapTile mapTile = new MapTile(backgroundSprite);
                    this.tilesMap[col, row] = mapTile;
                }
            }

            while (!stream.EndOfStream && (line = stream.ReadLine().Trim()) != string.Empty)
            {
                string[] elements = line.Split(',');
                int x = Convert.ToInt32(elements[0]);
                int y = Convert.ToInt32(elements[1]);
                var entityKey = elements[2].ToString();

                var foregroundSprite = SpriteFactory.Create(x, y, new Entity(entityKey));
                MapTile mapTile = this.tilesMap[x, y];
                mapTile.SetForegroundSprite(foregroundSprite);
            }
        }

        public string Name { get; private set; }

        public string NorthArea { get; private set; }

        public string EastArea { get; private set; }

        public string SouthArea { get; private set; }

        public string WestArea { get; private set; }

        public MapTile GetNextMapTile(Direction direction, Point position)
        {
            Point nextPosition = new Point(position);
            switch (direction)
            {
                case Direction.Left:
                    nextPosition.X -= 1;
                    break;

                case Direction.Right:
                    nextPosition.X += 1;
                    break;

                case Direction.Up:
                    nextPosition.Y -= 1;
                    break;

                case Direction.Down:
                    nextPosition.Y += 1;
                    break;

                default:
                    throw new InvalidOperationException();
            }
            return this.GetMapTile(nextPosition);
        }

        public MapTile GetMapTile(Point position)
        {
            return this.GetMapTile(position.X, position.Y);
        }

        public MapTile GetMapTile(int x, int y)
        {
            return this.tilesMap[x, y];
        }

        public void Update(double gameTime, double elapsedTime)
        {
            foreach (MapTile mapTile in this.tilesMap)
            {
                mapTile.Update(gameTime, elapsedTime);
            }
        }

        public void Draw(IRenderer renderer)
        {
            foreach (MapTile mapTile in this.tilesMap)
            {
                mapTile.Draw(renderer);
            }
        }
    }
}