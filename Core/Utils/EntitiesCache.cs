using System.Collections.Generic;

namespace DotNetNinja.Core.Utils
{
    internal class EntitiesCache
    {
        private static readonly Dictionary<string, Entity> entities = new Dictionary<string, Entity>();

        public Entity this[string key]
        {
            get
            {
                if (!entities.ContainsKey(key))
                {
                    entities.Add(key, new Entity(key));
                }
                return entities[key];
            }
        }
    }
}