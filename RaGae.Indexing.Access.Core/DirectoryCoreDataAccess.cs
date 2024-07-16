using Microsoft.EntityFrameworkCore;
using RaGae.Indexing.Model;

namespace RaGae.Indexing.Access.Core
{
    public class DirectoryCoreDataAccess : CoreDataAccess<DirectoryModel>
    {
        public DirectoryCoreDataAccess(DataContext dataContext) : base(dataContext) { }

        public override async Task<IList<DirectoryModel>> ReadAsync()
        {
            return await _dataContext.Directories
            .Include(x => x.Directories)
            .ThenInclude(x => x.Directories)
            .Include(x => x.Directories)
            .ThenInclude(x => x.Files)
            .Include(x => x.Files).ToListAsync();
        }
    }
}
