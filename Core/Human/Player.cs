using DotNetNinja.Core.Human.Enemies;
using DotNetNinja.Core.Map;
using System.Collections.Generic;

namespace DotNetNinja.Core.Human
{
    internal class Player : Human, IPlayer
    {
        private const int DefaultHealth = 200;
        private const int DefaultMana = 2;
        private const int DefaultKnowledge = 7;
        private const int DefaultDefense = 6;
        private const int DefaultExperience = 0;
        private const int DefaultLevel = 1;
        private const int DefaultNextUpgrade = 50;
        private const int UpgradeMultiplicator = 2;
        private const int LevelUPMultiplicator = 2;

        private int experience;

        public Player(float x, float y)
            : base(x, y, new Entity(EntityType.Player), true)
        {
            this.Health = DefaultHealth;
            this.Mana = DefaultMana;
            this.Knowledge = DefaultKnowledge;
            this.Defense = DefaultDefense;
            this.Experience = DefaultExperience;
            this.NextUpgrade = DefaultNextUpgrade;
            this.Level = DefaultLevel;
        }

        public int Mana { get; set; }

        public int Knowledge { get; set; }

        public int Defense { get; set; }

        public int Experience
        {
            get
            {
                return experience;
            }
            set
            {
                experience = value;
                if (experience > NextUpgrade)
                {
                    this.Knowledge += this.Level * LevelUPMultiplicator;
                    this.Health += this.Level * LevelUPMultiplicator;
                    this.Defense += this.Level * LevelUPMultiplicator;
                    this.Level++;
                    Sounds.LevelUp();
                    this.NextUpgrade *= UpgradeMultiplicator;
                }
            }
        }

        public int NextUpgrade { get; set; }

        public bool HasKey { get; set; }

        public bool HasCertificate { get; set; }

        public void AttackWithMagic(IArea currentArea, IList<TextPopup> popups)
        {
            if (this.Mana > 1)
            {
                Sounds.Magic();
                this.Mana -= 2;
                this.IsFighting = true;
                popups.Clear();

                for (int col = 0; col < Area.MapSizeX; col++)
                {
                    for (int row = 0; row < Area.MapSizeY; row++)
                    {
                        Enemy enemy = currentArea.GetMapTile(col, row).Sprite as Enemy;
                        if (enemy != null)
                        {
                            int damage = this.Knowledge * 2;
                            int experiance = enemy.GetDamage(damage);
                            this.Experience += experiance;
                            popups.Add(new TextPopup(enemy.Location.X + 40, enemy.Location.Y + 20, damage.ToString()));
                        }
                    }
                }
            }

            this.FightStartTime = -1;
        }

        public void LoadSaveGame(SaveGameData savegame)
        {
            this.Health = savegame.Health;
            this.Mana = savegame.Mana;
            this.Knowledge = savegame.Knowledge;
            this.Defense = savegame.Defense;
            this.Experience = savegame.Experience;
            this.IsFighting = savegame.IsFighting;
            this.Level = savegame.Level;
            this.Location = new PointF(savegame.Location);
            this.Position = new Point(savegame.Position);
            this.NextUpgrade = savegame.NextUpgradeExperience;
            this.IsAnimationEnabled = false;
        }

        public override void Die()
        {
            this.Health = 0;
            this.CreateFrames(this.Position.X, this.Position.Y, new Entity(EntityType.Bones));
        }
    }
}