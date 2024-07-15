using Microsoft.EntityFrameworkCore;
using RaGae.Indexing.Model;

namespace RaGae.Indexing.Access.Core
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<DirectoryModel> Directories { get; set; }
        public DbSet<FileModel> Files { get; set; }
    }
}
