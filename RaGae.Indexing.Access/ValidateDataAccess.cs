using RaGae.Indexing.Access.Extensions;
using RaGae.Indexing.Access.Interfaces;

namespace RaGae.Indexing.Access
{
    public abstract class ValidateDataAccess<T> : IDataAccess<T>
    {
        public virtual Task CreateAsync(T item)
        {
            item.Validate();
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(T item)
        {
            item.Validate();
            return Task.CompletedTask;
        }

        public abstract Task DeleteAsync(T item);

        public abstract Task<T> ReadAsync();


        public virtual void Dispose()
        {
            DisposeCore();
            GC.SuppressFinalize(this);
        }
        protected abstract void DisposeCore();

        public virtual async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            GC.SuppressFinalize(this);
        }
        protected abstract ValueTask DisposeAsyncCore();
    }
}
