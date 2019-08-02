namespace DotNetNinja.Core.Human
{
    internal interface IHuman : ISprite
    {
        int Level { get; }

        bool IsFighting { get; set; }

        double FightStartTime { get; set; }

        void Die();

        void MoveRight(int speed);

        void MoveLeft(int speed);

        void MoveUp(int speed);

        void MoveDown(int speed);

        void Move(Direction direction, int speed);

        bool CanMoveRight(int mapSizeXMaxIndex);

        bool CanMoveLeft(int mapSizeXMinIndex);

        bool CanMoveUp(int mapSizeYMinIndex);

        bool CanMoveDown(int mapSizeYMaxIndex);

        bool CanMoveDirection(Direction direction, int indexToCheck);

        void CalculateSpriteLocation(bool setTheLocation, int tileSizeX, int tileSizeY, int offsetX, int offsetY);
    }
}