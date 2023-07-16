using PaperTrade.DataAccess.Repositories;
using PaperTrade.DataAccess;
using PaperTrade.Common.Models;

namespace PaperTrade.BusinessLogic.Services
{
    public class DocumentService
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

        public async Task<Document> CreateDocumentAsync(Stream content, string extension)
        {
            var document = new Document { Id = Guid.NewGuid(), Extension = extension};
            var fileName = document.Id.ToString() + document.Extension;

            await blobStorageService.UploadBlobAsync("documentcontainer", fileName, content);
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
 