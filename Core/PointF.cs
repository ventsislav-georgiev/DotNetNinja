using System;

namespace DotNetNinja.Core
{
    [Serializable]
    internal class PointF
    {
        private static readonly PointF empty = new PointF(0, 0);

        public static PointF Empty => (PointF)empty.MemberwiseClone();

        public float X { get; set; }

        public float Y { get; set; }

        public PointF(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public PointF(PointF point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }
    }
}