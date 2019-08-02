using DotNetNinja.Core.Human;
using DotNetNinja.UI;
using System.Collections.Generic;
using System.IO;

namespace DotNetNinja.Core.Map
{
    internal abstract class World : IRenderable
    {
        private const string MapFilePath = @"Content\Map.txt";
        private const string StartingAreaKey = "start";

        public World(SaveGameData savegame = null)
        {
            this.WorldMap = new Dictionary<string, IArea>();

            this.ReadMapFile(MapFilePath);

            this.CurrentArea = this.WorldMap[StartingAreaKey];
        }

        protected IArea CurrentArea { get; set; }

        protected Dictionary<string, IArea> WorldMap { get; private set; }

        public virtual void Update(double gameTime, double elapsedTime)
        {
            this.CurrentArea.Update(gameTime, elapsedTime);
        }

        public virtual void Draw(IRenderer renderer)
        {
            this.CurrentArea.Draw(renderer);
        }

        protected SaveGameData SaveGame(IPlayer player)
        {
            var savegame = new SaveGameData(player);
            savegame.Area = this.CurrentArea.Name;
            return savegame;
        }

        private void ReadMapFile(string filePath)
        {
            using (StreamReader stream = new StreamReader(ContentResolver.GetEmbeddedResourceStream(filePath)))
            {
                while (!stream.EndOfStream)
                {
                    Area area = new Area(stream);
                    this.WorldMap.Add(area.Name, area);
                }
            }
        }
    }
}