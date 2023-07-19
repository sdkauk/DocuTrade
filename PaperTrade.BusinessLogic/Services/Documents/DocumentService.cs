using PaperTrade.DataAccess.Repositories;
using PaperTrade.DataAccess;
using PaperTrade.Common.Models;
using PaperTrade.BusinessLogic.Services.Documents.Requests;

namespace PaperTrade.BusinessLogic.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository documentRepository;
        private readonly IBlobStorageService blobStorageService;
        public DocumentService(IDocumentRepository documentRepository, IBlobStorageService blobStorageService)
        {
            this.documentRepository = documentRepository;
            this.blobStorageService = blobStorageService;
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await documentRepository.GetAllDocumentsAsync();
        }

        public async Task<Document> GetDocumentAsync(Guid id)
        {
            return await documentRepository.GetDocumentAsync(id);
        }
        public async Task<Stream> DownloadDocumentAsync(string documentName)
        {
            return await blobStorageService.DownloadBlobAsync("documentcontainer", documentName);
        }
        public async Task<Document> CreateDocumentAsync(DocumentPostRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                throw new Exception("File is null or empty");

            var extension = Path.GetExtension(request.File.FileName);
            var document = new Document { Id = Guid.NewGuid(), Extension = extension};
            var fileName = document.Id.ToString() + document.Extension;

            using (var stream = new MemoryStream())
            {
                await request.File.CopyToAsync(stream);
                stream.Position = 0;
                await blobStorageService.UploadBlobAsync("documentcontainer", fileName, stream);
            }

            await documentRepository.CreateDocumentAsync(document);

            return document;
        }

        public async Task DeleteDocumentAsync(Guid id)
        {
            var document = await documentRepository.GetDocumentAsync(id);

            if (document == null)
            {
                throw new Exception($"Document with id {id} does not exist.");
            }

            var fileName = document.Id.ToString() + document.Extension;
            await blobStorageService.DeleteBlobAsync("documentcontainer", fileName);
            await documentRepository.DeleteDocumentAsync(id);
        }


    }
}
 