using Microsoft.EntityFrameworkCore;
using RaGae.Indexing.Model;

namespace RaGae.Indexing.Access.Core
{
    public abstract class CoreDataAccess<T> : ValidateDataAccess<T> where T : AbstractModel
    {
        protected readonly DataContext _dataContext;

        public CoreDataAccess(DataContext dataContext) => _dataContext = dataContext ?? throw new NullReferenceException(nameof(CoreDataAccess<T>));

        public override async Task CreateAsync(T item)
        {
            await base.CreateAsync(item);
            await _dataContext.Set<T>().AddAsync(item);
            await _dataContext.SaveChangesAsync();
        }

        public override async Task UpdateAsync(T item)
        {
            await base.UpdateAsync(item);
            T currentItem = await _dataContext.FindAsync<T>(item.Id);

            if (currentItem is not null)
            {
                _dataContext.Entry(currentItem).CurrentValues.SetValues(item);
                await _dataContext.SaveChangesAsync();
            }
        }

        public override async Task DeleteAsync(T item)
        {
            T currentItem = await _dataContext.FindAsync<T>(item.Id);

            if (currentItem is not null)
            {
                _dataContext.Set<T>().Remove(currentItem);
                await _dataContext.SaveChangesAsync();
            }
        }

        public override abstract Task<T> ReadAsync();

        protected async override ValueTask DisposeAsyncCore()
        {
            if (_dataContext is IAsyncDisposable disposable)
            {
                await disposable.DisposeAsync().ConfigureAwait(false);
                return;
            }
            _dataContext?.Dispose();
        }

        protected override void DisposeCore()
        {
            if (_dataContext is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
