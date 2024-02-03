using ImageStorageAPI.Models;

namespace ImageStorageAPI.Services;

public interface IRandomImage
{
    Task<List<Image>> GetRandomImages();
}