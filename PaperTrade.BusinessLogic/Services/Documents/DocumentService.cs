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

        public async Task<Document> GetDocumentAsync(Guid id)
        {
            var document = await documentRepository.GetDocumentAsync(id);
            blobStorageService.DownloadBlobAsync("documentcontainer", document.Id.ToString());
            return document;
        }
    }
}
 