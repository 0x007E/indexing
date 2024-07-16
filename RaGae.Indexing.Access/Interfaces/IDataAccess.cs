namespace RaGae.Indexing.Access.Interfaces
{
    public interface IDataAccess<T> : IDisposable, IAsyncDisposable
    {
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
        Task<IList<T>> ReadAsync();
    }
}
