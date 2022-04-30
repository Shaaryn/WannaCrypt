using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace AzureControl
{
    public static class AzureController
    {
        private static string connectionString = BuildConnString();

        private static string BuildConnString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            return configuration.GetConnectionString("StorageAccount");
        }

        public static void UploadFile(string nameToUpload, string pathToUpload, string containerName)
        {

            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            container.CreateIfNotExists();

            BlobServiceClient client = new BlobServiceClient(connectionString);
            var clientContainer = client.GetBlobContainerClient(containerName);
            var blockBlob = clientContainer.GetBlobClient(nameToUpload + ".txt");
            using (FileStream fs = File.Open(pathToUpload, FileMode.Open))
            {
                blockBlob.DeleteIfExists();
                blockBlob.Upload(fs);
            }
            
        }
    }

    public class Program
    {
        public const string CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=wannacryptstorage;AccountKey=kpT1/4dzMJYm7QZa1EqsGW5utqQQ4dE3z+LSaWERbV6yS5qiVdMcIht6kOh6Be6ontApCk/ksRPOA6CwDTCXGw==;EndpointSuffix=core.windows.net";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("StorageAccount");
            string containerName = "Photos";
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            container.CreateIfNotExists();

            //string containerName = "...";
            //BlobContainerClient container = new BlobContainerClient(CONNECTION_STRING, containerName);

            //var blobs = container.GetBlobs();
            //foreach (var blob in blobs)
            //{
            //    Console.WriteLine($"{blob.Name} --> Created On: {blob.Properties.CreatedOn:YYYY-MM-dd HH:mm:ss}  Size: {blob.Properties.ContentLength}");
            //}
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
