namespace DotNetNinja.Core
{
    internal class EntityRawData
    {
        public string Name { get; private set; }

        public string Type { get; private set; }

        public string Key { get; private set; }

        public string Category { get; private set; }

        public string FilePath { get; private set; }

        public string TilePositionX { get; private set; }

        public string TilePositionY { get; private set; }

        public string IsTransparent { get; private set; }

        public string Frames { get; private set; }

        public string IsPassable { get; private set; }

        public string Special { get; private set; }

        public string ColorKey { get; private set; }

        public EntityRawData(string[] tileData)
        {
            var dataIndex = 0;
            this.Name = tileData[dataIndex++];
            this.Type = tileData[dataIndex++];
            this.Key = tileData[dataIndex++];
            this.Category = tileData[dataIndex++];
            this.FilePath = tileData[dataIndex++];
            this.TilePositionX = tileData[dataIndex++];
            this.TilePositionY = tileData[dataIndex++];
            this.IsTransparent = tileData[dataIndex++];
            this.Frames = tileData[dataIndex++];
            this.IsPassable = tileData[dataIndex++];
            this.Special = tileData[dataIndex++];
            this.ColorKey = tileData[dataIndex].Trim();
        }
    }
}