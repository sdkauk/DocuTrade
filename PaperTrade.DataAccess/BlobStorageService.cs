using Azure.Storage.Blobs;

namespace PaperTrade.DataAccess
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient blobServiceClient;

        public BlobStorageService(string connectionString)
        {
            blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task UploadBlobAsync(string blobContainerName, string blobName, Stream content)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(content, overwrite: true);
        }

        public async Task<Stream> DownloadBlobAsync(string blobContainerName, string blobName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }

        public async Task DeleteBlobAsync(string blobContainerName, string blobName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}