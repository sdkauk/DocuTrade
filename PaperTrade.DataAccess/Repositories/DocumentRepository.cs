using MongoDB.Driver;
using PaperTrade.Common.Models;

namespace PaperTrade.DataAccess.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IMongoCollection<Document> documents;

        public DocumentRepository(IDbConnection db)
        {
            documents = db.DocumentCollection;
        }

        public async Task<List<Document>> GetAllDocumentsAsync()
        {
            var results = await documents.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<Document> GetDocumentAsync(Guid id)
        {
            var results = await documents.FindAsync(d => d.Id == id);
            return results.FirstOrDefault();
        }

        public async Task CreateDocumentAsync(Document document)
        {
            await documents.InsertOneAsync(document);
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            var filter = Builders<Document>.Filter.Eq("Id", document.Id);
            await documents.ReplaceOneAsync(filter, document);
        }

        public async Task DeleteDocumentAsync(Guid id)
        {
            var filter = Builders<Document>.Filter.Eq("Id", id);
            await documents.DeleteOneAsync(filter);
        }
    }

}

