using PaperTrade.Common.Models;
using PaperTrade.DataAccess.Repositories;

public class DocumentSeeder
{
    private readonly IDocumentRepository documentRepository;

    public DocumentSeeder(IDocumentRepository documentRepository)
    {
        this.documentRepository = documentRepository;
    }

    public async Task SeedAsync()
    {
        var documents = new List<Document>
        {
            new Document { Id = Guid.NewGuid()},
            new Document { Id = Guid.NewGuid()},
            new Document { Id = Guid.NewGuid()},
        };

        var existingDocuments = await documentRepository.GetAllDocumentsAsync();

        foreach (var document in documents)
        {
            if (!existingDocuments.Any())
            {
                await documentRepository.CreateDocumentAsync(document);
            }
        }
    }
}