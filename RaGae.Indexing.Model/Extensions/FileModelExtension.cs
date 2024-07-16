namespace RaGae.Indexing.Model.Extensions
{
    public static class FileModelExtension
    {
        public static FileModel OldestFile(this IList<FileModel> model) => model.MaxBy(f => f.LastModified);

        public static FileModel NewestFile(this IList<FileModel> model) => model.MinBy(f => f.LastModified);

        public static FileModel BiggestFile(this IList<FileModel> model) => model.MaxBy(f => f.Size);

        public static FileModel SmallestFile(this IList<FileModel> model) => model.MinBy(f => f.Size);

        public static double Median(this IList<FileModel> model) => model.Average(f => f.Size);

        public static string MostUsedExtension(this IList<FileModel> model) => model.Where(c => !string.IsNullOrEmpty(c.Extension)).GroupBy(a => a.Extension).OrderByDescending(b => b.Count()).First().Key;

        public static int CountFileType(this IList<FileModel> model, string extension) => model.Where(f => f.Extension == extension).Count();

        public static List<(string extension, int count)> CountFileTypes(this IList<FileModel> model) => model.GroupBy(f => f.Extension, (key, group) => (key, group.Count())).ToList();

        public static double MedianFileNameLength(this IList<FileModel> model) => model.Average(f => f.Name.Length);
    }
}
