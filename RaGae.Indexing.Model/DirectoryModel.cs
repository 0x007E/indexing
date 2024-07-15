using System.IO;

namespace RaGae.Indexing.Model
{
    public class DirectoryModel : AbstractModel
    {
        public override long Size
        {
            get
            {
                try
                {
                    return Directories.Sum(d => d.Size) + Files.Sum(f => f.Size);
                }
                catch { }
                return 0;
            }
        }

        public List<DirectoryModel> Directories { get; set; }
        public List<FileModel> Files { get; set; }
    }
}
