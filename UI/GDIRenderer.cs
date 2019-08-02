using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime;

namespace DotNetNinja.UI
{
    internal class GDIRenderer : IRenderer
    {
        private Graphics graphics;

        public GDIRenderer()
        {
        }

        public void SetGraphics(Graphics graphics)
        {
            this.graphics = graphics;
        }

        public void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            this.DrawString(s, font, brush, new RectangleF(x, y, 0f, 0f), null);
        }

        public void DrawStringWithOutline(string s, Font font, Brush brush, Brush outlineBrush, float x, float y)
        {
            this.DrawString(s, font, outlineBrush, x + 2, y);
            this.DrawString(s, font, outlineBrush, x - 1, y);
            this.DrawString(s, font, outlineBrush, x, y + 2);
            this.DrawString(s, font, outlineBrush, x, y - 2);
            this.DrawString(s, font, brush, x, y);
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            this.graphics.DrawString(s, font, brush, layoutRectangle, format);
        }

        public void DrawImage(Image image, float x, float y, float width, float height)
        {
            this.graphics.DrawImage(image, x, y, width, height);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs)
        {
            this.graphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, null);
        }
    }
}