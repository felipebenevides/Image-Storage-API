using ImageStorageAPI.Data;
using ImageStorageAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageStorageAPI.Services;

public class ImageService : IImageService
{
    private readonly IImageDbContext _dbContext;
    private readonly IRandomImage _randomImage;

    public ImageService(IImageDbContext dbContext,IRandomImage randomImage)
    {
        _dbContext = dbContext;
        _randomImage = randomImage;
    }

    
    public async Task<Image> UploadImage(ImageRequest imageRequest)
    {
        var image = new Image
        {
            Name = imageRequest.Name,
            Data = imageRequest.Data
        };

        _dbContext.Images.Add(image);
        await _dbContext.SaveChangesAsync();

        return image;
    }
    
    public async Task<List<Image>> GetAllImages()
    {
       // await InsertRandomImages();
        return await _dbContext.Images.ToListAsync();
    }

    public async Task<Image> GetImageById(int id)
    {
         return await _dbContext.Images.FindAsync(id);
    }

    public async Task<bool> DeleteImage(int id)
    {
        var image = await _dbContext.Images.FindAsync(id);
        if (image == null)
        {
            return false; 
        }

        _dbContext.Images.Remove(image);
        await _dbContext.SaveChangesAsync();

        return true; 
    }

    public async Task InsertRandomImages()
    {
        var images = await _randomImage.GetRandomImages();
        await _dbContext.Images.AddRangeAsync(images);
        await _dbContext.SaveChangesAsync();
    }
}
