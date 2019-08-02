namespace DotNetNinja.Core.Item
{
    internal interface IObstacle
    {
        bool IsStateChangable { get; }

        void ChangeState();

        bool State { get; }
    }
}