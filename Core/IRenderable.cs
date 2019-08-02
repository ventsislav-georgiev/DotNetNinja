using DotNetNinja.UI;

namespace DotNetNinja.Core
{
    internal interface IRenderable
    {
        void Update(double gameTime, double elapsedTime);

        void Draw(IRenderer renderer);
    }
}