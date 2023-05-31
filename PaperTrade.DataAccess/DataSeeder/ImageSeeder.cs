using PaperTrade.Common.Models;
using PaperTrade.DataAccess;
using PaperTrade.DataAccess.Repositories;

public class ImageSeeder
{
    private readonly IImageRepository imageRepository;
    private readonly IBlobStorageService blobStorageService;

    public ImageSeeder(IImageRepository imageRepository, IBlobStorageService blobStorageService)
    {
        this.imageRepository = imageRepository;
        this.blobStorageService = blobStorageService;
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
                var fileName = image.Id.ToString() + ".png";
                var filePath = "ImageSeed.png";
                await using var fileStream = File.OpenRead(filePath);
                await blobStorageService.UploadBlobAsync("imagecontainer", fileName, fileStream);
                await imageRepository.CreateImageAsync(image);
            }
        }
    }
}