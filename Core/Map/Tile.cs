using DotNetNinja.Core.Utils;
using System;
using System.Drawing;

namespace DotNetNinja.Core.Map
{
    internal class Tile
    {
        public const int TileSizeX = 64;
        public const int TileSizeY = 64;
        private static readonly BitmapCache bitmaps = new BitmapCache();

        public Bitmap Bitmap { get; private set; }

        public Rectangle Rectangle { get; private set; }

        public bool IsTransparent { get; private set; }

        public int FramesCount { get; private set; }

        public Tile(EntityRawData entityRawData)
        {
            this.Bitmap = bitmaps[entityRawData.FilePath];
            this.FramesCount = Convert.ToInt32(entityRawData.Frames);
            this.IsTransparent = bool.Parse(entityRawData.IsTransparent);

            int tilePositionX = Convert.ToInt32(entityRawData.TilePositionX);
            int tilePositionY = Convert.ToInt32(entityRawData.TilePositionY);

            var rectangleX = (tilePositionX - 1) * TileSizeX;
            var rectangleY = (tilePositionY - 1) * TileSizeY;
            var ractangleWidth = TileSizeX * this.FramesCount;
            var rectangleHeight = TileSizeY;
            this.Rectangle = new Rectangle(rectangleX, rectangleY, ractangleWidth, rectangleHeight);
        }
    }
}