using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RaGae.Indexing.Access.Core;
using RaGae.Indexing.Model;
using RaGae.Indexing.Provider;
using RaGae.Indexing.Provider.Enumerations;

namespace Ragae.Indexing.Command
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
            DataService<DirectoryModel> dataService = new DataService<DirectoryModel>(new DirectoryCoreDataAccess(dataContext));

            //await dataService.SaveAsync(directoryModel);
            DirectoryModel directoryModel = await dataService.ReadAsync();


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
    }
}
