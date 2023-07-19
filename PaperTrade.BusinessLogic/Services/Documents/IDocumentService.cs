using PaperTrade.BusinessLogic.Services.Documents.Requests;
using PaperTrade.Common.Models;

namespace PaperTrade.BusinessLogic.Services
{
    public interface IDocumentService
    {
        Task<Document> CreateDocumentAsync(DocumentPostRequest request);
        Task DeleteDocumentAsync(Guid id);
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<Document> GetDocumentAsync(Guid id);
        Task<Stream> DownloadDocumentAsync(string documentName);
    }
}