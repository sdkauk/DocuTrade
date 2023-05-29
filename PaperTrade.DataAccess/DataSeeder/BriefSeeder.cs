using PaperTrade.Common.Models;
using PaperTrade.DataAccess.Repositories;

public class BriefSeeder
{
    private readonly IBriefRepository briefRepository;
    private readonly IUserRepository userRepository;
    private readonly IDocumentRepository documentRepository;
    private readonly IImageRepository imageRepository;

    public BriefSeeder(IBriefRepository briefRepository, IUserRepository userRepository, IDocumentRepository documentRepository, IImageRepository imageRepository)
    {
        this.briefRepository = briefRepository;
        this.userRepository = userRepository;
        this.documentRepository = documentRepository;
        this.imageRepository = imageRepository;
    }

    public async Task SeedAsync()
    {
        var users = await userRepository.GetAllUsersAsync();
        var documents = await documentRepository.GetAllDocumentsAsync();
        var images = await imageRepository.GetAllImagesAsync();

        if (!users.Any() || !documents.Any() || !images.Any())
        {
            throw new InvalidOperationException("Cannot seed Briefs because prerequisite data is missing.");
        }

        var briefs = new List<Brief>
        {
            new Brief
            {
                Id = Guid.NewGuid(),
                Name = "Brief1",
                Author = new BasicUser(users[0]),
                Document = documents[0],
                Preview = images[0],
                Description = "This is Brief 1",
                Owners = new List<BasicUser> { new BasicUser(users[0]) }
            },
            new Brief
            {
                Id = Guid.NewGuid(),
                Name = "Brief2",
                Author = new BasicUser(users[1]),
                Document = documents[1],
                Preview = images[1],
                Description = "This is Brief 2",
                Owners = new List<BasicUser> { new BasicUser(users[1]) }
            }
        };

        var existingBriefs = await briefRepository.GetAllBriefsAsync();

        foreach (var brief in briefs)
        {
            if (!existingBriefs.Any())
            {
                await briefRepository.CreateBriefAsync(brief);

                var author = users.First(u => u.Id == brief.Author.Id);
                author.Briefs.Add(new BasicBrief(brief));
                await userRepository.UpdateUserAsync(author);
            }
        }
    }
}