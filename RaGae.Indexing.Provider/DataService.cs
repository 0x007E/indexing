using RaGae.Indexing.Access.Interfaces;
using RaGae.Indexing.Model;

namespace RaGae.Indexing.Provider
{
    public class DataService<T>
    {
        private readonly IDataAccess<T> _dataAccess;

        public DataService(IDataAccess<T> dataAccess)
        {
            _dataAccess = dataAccess ?? throw new NullReferenceException(nameof(IDataAccess<T>));
        }

        public Task SaveAsync(T model) => _dataAccess.CreateAsync(model);

        public Task<IList<T>> ReadAsync() => _dataAccess.ReadAsync();
    }
}
