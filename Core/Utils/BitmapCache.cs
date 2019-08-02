using System.Collections.Generic;
using System.Drawing;

namespace DotNetNinja.Core.Utils
{
    internal class BitmapCache
    {
        private static readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();

        public Bitmap this[string filename]
        {
            get
            {
                if (!bitmaps.ContainsKey(filename))
                {
                    bitmaps.Add(filename, new Bitmap(ContentResolver.GetEmbeddedResourceStream(filename)));
                }
                return bitmaps[filename];
            }
        }
    }
}