using PaperTrade.Common.Models;
using PaperTrade.DataAccess.Repositories;

public class ImageSeeder
{
    private readonly IImageRepository imageRepository;

    public ImageSeeder(IImageRepository imageRepository)
    {
        this.imageRepository = imageRepository;
    }

    public async Task SeedAsync()
    {
        var images = new List<Image>
        {
            new Image { Id = Guid.NewGuid()},
            new Image { Id = Guid.NewGuid()},
            new Image { Id = Guid.NewGuid()},
        };

        var existingImages = await imageRepository.GetAllImagesAsync();

        foreach (var image in images)
        {
            // Check if the iamge already exists
            if (!existingImages.Any())
            {
                await imageRepository.CreateImageAsync(image);
            }
        }
    }
}