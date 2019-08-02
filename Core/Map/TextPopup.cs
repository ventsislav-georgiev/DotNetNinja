using DotNetNinja.UI;
using System.Drawing;

namespace DotNetNinja.Core.Map
{
    internal struct TextPopup
    {
        private static readonly Font font = new Font("Arial", 18);
        private static readonly Brush blackBrush = new SolidBrush(Color.Black);
        private static readonly Brush redBrush = new SolidBrush(Color.Red);
        private static readonly Brush whiteBrush = new SolidBrush(Color.White);

        public TextPopup(float x, float y, string text)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Text = text;
        }

        public float Y { get; set; }

        public float X { get; set; }

        public string Text { get; set; }

        public void Draw(IRenderer renderer)
        {
            var textBrush = whiteBrush;
            if (this.Text != GameState.MISS_MESSAGE)
            {
                textBrush = redBrush;
            }

            renderer.DrawStringWithOutline(this.Text, font, textBrush, blackBrush, this.X, this.Y);
        }
    }
}