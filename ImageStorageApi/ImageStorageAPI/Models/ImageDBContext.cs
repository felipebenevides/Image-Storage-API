using System.Net.Mime;
using Microsoft.EntityFrameworkCore;

namespace ImageStorageAPI.Models;

public class ImageDBContext: DbContext
{
    
    public DbSet<Image> Images { get; set; }

    public ImageDBContext(DbContextOptions<ImageDBContext> options) : base(options)
    {
    }
}