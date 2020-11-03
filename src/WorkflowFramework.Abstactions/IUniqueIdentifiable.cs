namespace WorkflowFramework.Abstractions
{
    public interface IUniqueIdentifiable
    {
        object Id { get; }
    }

    public interface IUniqueIdentifiable<TId> : IUniqueIdentifiable
    {
        new TId Id { get; }
    }
}