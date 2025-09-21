
using Azure;

namespace SeatingAPI.Services.Interfaces
{
    public interface IAzureStorageService
    {
        Task<Response<byte[]>> GetFileAsync(string fileName);
    }
}
