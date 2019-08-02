using DotNetNinja.Core.Map;
using System.Collections.Generic;

namespace DotNetNinja.Core.Human
{
    internal interface IPlayer : IHuman
    {
        int Health { get; set; }

        int Mana { get; set; }

        int Knowledge { get; set; }

        int Defense { get; set; }

        int Experience { get; set; }

        int NextUpgrade { get; set; }

        bool HasKey { get; set; }

        bool HasCertificate { get; set; }

        void AttackWithMagic(IArea area, IList<TextPopup> popups);

        void LoadSaveGame(SaveGameData savegame);
    }
}