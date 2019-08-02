using System.Drawing;
using System.Drawing.Imaging;

namespace DotNetNinja.UI
{
    internal interface IRenderer
    {
        void DrawString(string s, Font font, Brush brush, float x, float y);

        void DrawStringWithOutline(string s, Font font, Brush brush, Brush outlineBrush, float x, float y);

        void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format);

        void DrawImage(Image image, float x, float y, float width, float height);

        void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs);
    }
}