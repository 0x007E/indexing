using RaGae.Indexing.Model;
using RaGae.Indexing.Provider.Delegates;
using RaGae.Indexing.Provider.Enumerations;

namespace RaGae.Indexing.Provider
{
    public class DirectoryService
    {
        private string _path;

        public DirectoryService(string path) => Path = path;

        public string Path
        {
            get => _path;
            private set
            {
                if (!Directory.Exists(value))
                {
                    throw new DirectoryNotFoundException(value);
                }
                _path = value;
            }
        }

        public DirectoryServiceStatus Status { get; set; }

        public DirectoryModel ScanPathAndCreateModel() => RecursiveScan(Path);

        private DirectoryModel RecursiveScan(string path, int indentation = 0, ServiceStatusReply type = ServiceStatusReply.Directory)
        {
            indentation += 1;

            try
            {
                DirectoryInfo directoryInfo = new(path);

                DirectoryModel model = new()
                {
                    Name = directoryInfo.Name,
                    LastModified = directoryInfo.LastWriteTime,
                    CreationDate = directoryInfo.CreationTime,
                    Files = new(),
                    Directories = new()
                };

                Status?.Invoke(model.Name, indentation);

                string[] directories = Directory.GetDirectories(directoryInfo.FullName);
                string[] files = Directory.GetFiles(directoryInfo.FullName);

                foreach (string directory in directories)
                {
                    model.Directories.Add(RecursiveScan(directory, indentation));
                }

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    FileModel fileModel = new FileModel
                    {
                        Name = fileInfo.Name,
                        Extension = fileInfo.Extension,
                        LastModified = fileInfo.LastWriteTime,
                        CreationDate = fileInfo.CreationTime,
                        Size = fileInfo.Length
                    };

                    model.Files.Add(fileModel);
                    Status?.Invoke(fileModel.Name, (indentation + 1), ServiceStatusReply.File);
                }
                return model;
            }
            catch (Exception ex)
            {
                Status?.Invoke(ex.Message, (indentation + 1), ServiceStatusReply.Error);

                return new()
                {
                    Name = ex.Message
                };
            }
        }
    }
}
