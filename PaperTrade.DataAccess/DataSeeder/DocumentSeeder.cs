using PaperTrade.Common.Models;
using PaperTrade.DataAccess;
using PaperTrade.DataAccess.Repositories;

public class DocumentSeeder
{
    private readonly IDocumentRepository documentRepository;
    private readonly IBlobStorageService blobStorageService;

    public DocumentSeeder(IDocumentRepository documentRepository, IBlobStorageService blobStorageService)
    {
        this.documentRepository = documentRepository;
        this.blobStorageService = blobStorageService;
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
                var fileName = document.Id.ToString() + ".txt";
                await using var fileStream = File.Create(fileName);
                var writer = new StreamWriter(fileStream);
                await writer.WriteAsync($"This is the content of {fileName}");
                await writer.FlushAsync();
                fileStream.Seek(0, SeekOrigin.Begin);
                await blobStorageService.UploadBlobAsync("documentcontainer", fileName, fileStream);
                await documentRepository.CreateDocumentAsync(document);
            }
        }
    }
}