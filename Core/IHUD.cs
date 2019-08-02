using DotNetNinja.Core.Human;
using DotNetNinja.UI;

namespace DotNetNinja.Core
{
    internal interface IHUD
    {
        bool GameIsWon { get; set; }

        int Health { get; set; }

        int Mana { get; set; }

        int Knowledge { get; set; }

        int Defense { get; set; }

        int Experience { get; set; }

        bool HasKey { get; set; }

        int Level { get; set; }

        void Initialize();

        void Draw(IRenderer renderer);

        void DrawMessage(IRenderer renderer, string[] text, bool isTemporary = true);

        void Update(IPlayer player);
    }
}