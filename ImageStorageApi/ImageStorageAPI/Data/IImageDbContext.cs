using ImageStorageAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageStorageAPI.Data;

public interface IImageDbContext
{
    DbSet<Image> Images { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}