using ImageStorageAPI.Models;

namespace ImageStorageAPI.Services
{
    public interface IImageService
    {
        Task<Image> UploadImage(ImageRequest imageRequest);
        Task<List<Image>> GetAllImages();
        Task<Image> GetImageById(int id);
        Task<bool> DeleteImage(int id);
        Task InsertRandomImages();
    }
}