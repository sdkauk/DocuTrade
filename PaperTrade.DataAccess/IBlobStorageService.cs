
namespace PaperTrade.DataAccess
{
    public interface IBlobStorageService
    {
        Task DeleteBlobAsync(string blobContainerName, string blobName);
        Task<Stream> DownloadBlobAsync(string blobContainerName, string blobName);
        Task UploadBlobAsync(string blobContainerName, string blobName, Stream content);
    }
}