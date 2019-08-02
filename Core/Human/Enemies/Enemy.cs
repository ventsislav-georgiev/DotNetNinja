namespace DotNetNinja.Core.Human.Enemies
{
    internal abstract class Enemy : Human, IEnemy
    {
        public Enemy(float x, float y, Entity entity)
            : base(x, y, entity, false)
        {
            this.IsAnimationForced = true;
        }

        public int StartingHealth { get; set; }

        public int Strength { get; set; }

        public int Defense { get; set; }

        public bool IsAlive()
        {
            return (this.Health > 0) ? true : false;
        }

        public abstract int SetHealth();

        public abstract int SetStrength();

        public int GetDamage(int enemyDamage)
        {
            this.Health -= enemyDamage;
            if (this.IsAlive() == false)
            {
                this.Die();
                return this.StartingHealth / 3;
            }
            return 0;
        }
    }
}