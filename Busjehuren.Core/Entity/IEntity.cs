namespace Busjehuren.Core.Entity
{
    public interface IEntity : IEntity<int>
    {
    }

    public interface IEntity<T>
    {
        T Id { get; }
    }
}
