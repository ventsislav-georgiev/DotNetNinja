namespace DotNetNinja.Core.Item
{
    internal interface ICollectable
    {
        EntityCategoryType Category { get; }

        int ItemRate { get; }
    }
}