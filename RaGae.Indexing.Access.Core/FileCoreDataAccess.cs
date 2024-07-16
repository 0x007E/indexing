using Microsoft.EntityFrameworkCore;
using RaGae.Indexing.Model;

namespace RaGae.Indexing.Access.Core
{
    public class FileCoreDataAccess : CoreDataAccess<FileModel>
    {
        public FileCoreDataAccess(DataContext dataContext) : base(dataContext) { }
    }
}
