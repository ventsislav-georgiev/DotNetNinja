namespace DotNetNinja.Core.Human.Enemies
{
    internal class Student : Enemy
    {
        private const int HealthMultiplicator = 20;
        private const int StrengthPercentage = 80;
        private const int DefenseDivisor = 5;

        public Student(float x, float y, EntityType type)
            : base(x, y, new Entity(type))
        {
            this.Health = SetHealth();
            this.StartingHealth = this.Health;
            this.Strength = this.SetStrength();
            this.Defense = this.Health / DefenseDivisor;
        }

        public override int SetHealth()
        {
            return (this.Level * HealthMultiplicator);
        }

        public override int SetStrength()
        {
            return (int)(((float)this.Health / 100) * StrengthPercentage);
        }
    }
}