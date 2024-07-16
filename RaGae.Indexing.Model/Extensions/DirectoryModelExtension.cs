namespace RaGae.Indexing.Model.Extensions
{
    public static class DirectoryModelExtension
    {
        public static bool ModelEquals(this DirectoryModel thisModel, DirectoryModel model)
        {
            if (!(thisModel.Id.Equals(model.Id) &&
                 thisModel.Name.Equals(model.Name) &&
                 thisModel.Size.Equals(model.Size) &&
                 thisModel.CreationDate.Equals(model.CreationDate) &&
                 thisModel.LastModified.Equals(model.LastModified)))
                return false;

            foreach (FileModel file in thisModel.Files)
            {
                if (!model.Files.Any(f => f.Id == file.Id &&
                                                f.Name == file.Name &&
                                                f.Size == file.Size &&
                                                f.CreationDate == file.CreationDate &&
                                                f.LastModified == file.LastModified))
                    return false;
            }

            foreach (DirectoryModel directory in thisModel.Directories)
            {
                if (!Equals(directory, model.Directories.FirstOrDefault(d => d.Id == directory.Id)))
                    return false;
            }

            return true;
        }
    }
}
