using RaGae.Indexing.Model;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RaGae.Indexing.Access.File
{
    public class FileDataAccess<T> : ValidateDataAccess<T> where T : AbstractModel, new()
    {
        private readonly string _file;

        public FileDataAccess(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path).Close();
            }

            _file = path;
        }

        public override async Task CreateAsync(T item)
        {
            await base.CreateAsync(item);

            await using FileStream writeStream = System.IO.File.OpenWrite(_file);
            await JsonSerializer.SerializeAsync(writeStream, item);
        }

        public override async Task UpdateAsync(T item)
        {
            await base.UpdateAsync(item);

            using FileStream readStream = System.IO.File.OpenRead(_file);
            IList<T> data = await JsonSerializer.DeserializeAsync<IList<T>>(readStream);

            foreach (T i in data)
            {
                if(i.Id == item.Id)
                {


                    break;
                }
            }
        }

        public override async Task DeleteAsync(T item)
        {

        }

        public override async Task<T> ReadAsync()
        {
            using FileStream readStream = System.IO.File.OpenRead(_file);
            IList<T> data = await JsonSerializer.DeserializeAsync<IList<T>>(readStream);

            return data.AsParallel().FirstOrDefault(i => i.Id == 1);
        }

        protected override void DisposeCore()
        {

        }

        protected override ValueTask DisposeAsyncCore()
        {
            DisposeCore();
            return ValueTask.CompletedTask;
        }
    }
}
