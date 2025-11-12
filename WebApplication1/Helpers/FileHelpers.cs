using Azure.Storage.Blobs;
using MusicApi.Models;
using System.IO;

namespace MusicApi.Helpers
{
    public static class FileHelpers
    {

        public static async Task<string> UploadImage(IFormFile file)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=musicstorageaccount;AccountKey=1K/41bxUiwqYSL3PlAu4sZndWIvy2aQudGZ4HoI8ItNE8qmhHhNgjSfubxhPdT3Bp/PzbDaHtj49+AStxVNflQ==;EndpointSuffix=core.windows.net";
            string containerName = "songscover";

            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream, true);
            return blobClient.Uri.AbsoluteUri;
        }

        public static async Task<string> UploadFile(IFormFile file)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=musicstorageaccount;AccountKey=1K/41bxUiwqYSL3PlAu4sZndWIvy2aQudGZ4HoI8ItNE8qmhHhNgjSfubxhPdT3Bp/PzbDaHtj49+AStxVNflQ==;EndpointSuffix=core.windows.net";
            string containerName = "audiofiles";

            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream, true);
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
