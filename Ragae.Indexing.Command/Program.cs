using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RaGae.Indexing.Access.Core;
using RaGae.Indexing.Model;
using RaGae.Indexing.Model.Extensions;
using RaGae.Indexing.Provider;
using RaGae.Indexing.Provider.Enumerations;

namespace RaGae.Indexing.Command
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("SQLConnectionString") ?? throw new ArgumentException("SQLConnectionString");

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(connectionString);

            //DirectoryService directoryService = new(@"C:\Users\sunri\OneDrive - Pädagogische Hochschule Tirol\");
            //directoryService.Status += WriteData;
            //DirectoryModel directoryModel = directoryService.ScanPathAndCreateModel();

            DataContext dataContext = new DataContext(builder.Options);
            DataService<DirectoryModel> directoryDataService = new DataService<DirectoryModel>(new DirectoryCoreDataAccess(dataContext));
            DataService<FileModel> fileDataService = new DataService<FileModel>(new FileCoreDataAccess(dataContext));

            //await dataService.SaveAsync(directoryModel);
            IList<DirectoryModel> directoryModel = await directoryDataService.ReadAsync();
            IList<FileModel> fileModel = await fileDataService.ReadAsync();

            // Get Size of root /
            Console.WriteLine($"Complete file size: {BytesToString(directoryModel?.FirstOrDefault().Size ?? 0)}");

            // Find the oldest file (last modified)
            Console.WriteLine($"Oldest file: {fileModel.OldestFile().Name}");

            // Find the newest file (last modified)
            Console.WriteLine($"Newest file: {fileModel.NewestFile().Name}");

            // Find the biggest file
            Console.WriteLine($"Biggest file: {fileModel.BiggestFile().Name}");

            // Find the smallest file
            Console.WriteLine($"Smallest file: {fileModel.SmallestFile().Name}");

            // Find the Median of all filesizes
            // (n1 + nx)/m
            Console.WriteLine($"Median of all filesizes: {Math.Round(fileModel.Median(), 2)}");

            // Most datatypes used (e.g. *.jpg)
            Console.WriteLine($"Most used datatype: {fileModel.MostUsedExtension()}");

            // Count specific file types (*.jpg, *.exe, *.html, *.*)
            Console.WriteLine($"Count given file type: {fileModel.CountFileType(".docx")}\n");

            // Median filename length
            Console.WriteLine($"\nMedian filename length: {Math.Round(fileModel.MedianFileNameLength(), 2)}");


            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void WriteData(string data, int indentation = 0, ServiceStatusReply type = ServiceStatusReply.Directory)
        {
            for (int i = 0; i < indentation; i++)
            {
                Console.Write(' ');
            }

            switch (type)
            {
                case ServiceStatusReply.Directory:
                    Console.WriteLine($"+ {data}");
                    break;
                case ServiceStatusReply.File:
                    Console.WriteLine($" - {data}");
                    break;
                case ServiceStatusReply.Error:
                    Console.WriteLine($"#->{data}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ServiceStatusReply));
            }
        }

        private static string BytesToString(long byteCount)
        {
            string[] suf = { " B", " KB", " MB", " GB", " TB", " PB", " EB" }; // long runs out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);

            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }
}
