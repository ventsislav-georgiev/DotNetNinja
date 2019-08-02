namespace DotNetNinja.Core.Human.Enemies
{
    internal class Boss : Enemy, IBoss
    {
        private const int HealthMultiplicator = 60;
        private const int StrengthPercentage = 40;
        private const int DefenseDivisor = 5;

        public Boss(float x, float y, EntityType type)
            : base(x, y, new Entity(type))
        {
            this.Health = SetHealth();
            this.StartingHealth = this.Health;
            this.Strength = this.SetStrength();
            this.Defense = this.Health / DefenseDivisor;
        }

        public override void Die()
        {
            this.Health = 0;
            this.OnUpdateTile(EntityType.Key);
            Sounds.Win();
        }

        public override int SetHealth()
        {
            return this.Level * HealthMultiplicator;
        }

        public override int SetStrength()
        {
            return (int)(((float)this.Health / 100) * StrengthPercentage);
        }
    }
}