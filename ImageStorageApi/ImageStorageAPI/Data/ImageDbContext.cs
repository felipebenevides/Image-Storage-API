using ImageStorageAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageStorageAPI.Data;

public class ImageDbContext : DbContext, IImageDbContext
{
    public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options)
    {
    }

    public DbSet<Image> Images { get; set; }
}

