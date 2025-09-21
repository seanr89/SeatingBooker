
using Azure.Storage.Blobs;
using Azure;
using SeatingAPI.Services.Interfaces;

namespace SeatingAPI.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureStorageService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration.GetConnectionString("AzureStorage"));
        }

        public async Task<Response<byte[]>> GetFileAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("locations");
            var blobClient = containerClient.GetBlobClient(fileName);

            if (!await blobClient.ExistsAsync())
            {
                return Response.FromValue<byte[]>(null, new Azure.Response(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.NotFound)));
            }

            using (var ms = new MemoryStream())
            {
                await blobClient.DownloadToAsync(ms);
                return Response.FromValue(ms.ToArray(), new Azure.Response(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)));
            }
        }
    }
}
