using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public interface IDocumentRepository
    {
        Task CreateDocumentAsync(Document document);
        Task DeleteDocumentAsync(Guid id);
        Task<Document> GetDocumentAsync(Guid id);
        Task UpdateDocumentAsync(Document document);
    }
}