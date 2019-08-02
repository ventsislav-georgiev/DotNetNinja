namespace DotNetNinja.Core.Human.Enemies
{
    internal interface IEnemy : IHuman
    {
        int Strength { get; set; }

        int Health { get; set; }

        int Defense { get; set; }

        int GetDamage(int enemyDamage);
    }
}