using ImageStorageAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ImageDBContext>(options =>
{
    options.UseInMemoryDatabase("ImageDatabase");
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();





app.MapPost("/api/images/upload", async (ImageRequest imageRequest, ImageDBContext dbContext) =>
{
    var image = new Image
    {
        Name = imageRequest.Name,
        Data = imageRequest.Data
    };

    dbContext.Images.Add(image);
    await dbContext.SaveChangesAsync();
    
    return Results.Created($"/api/images/{image.Id}", image);
}).WithOpenApi();

app.MapGet("/api/images", async (ImageDBContext dbContext) =>
{
    var images = await dbContext.Images.ToListAsync();
    return Results.Ok(images);
});`
//.WithName("GetWeatherForecast")

app.MapGet("/api/images/{id}", async (int id, ImageDBContext dbContext) =>
{
    var image = await dbContext.Images.FindAsync(id);
    if (image == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(image);
});

app.MapDelete("/api/images/{id}/delete", async (int id, ImageDBContext dbContext) =>
{
    var image = await dbContext.Images.FindAsync(id);
    if (image == null)
    {
        return Results.NotFound();
    }

    dbContext.Images.Remove(image);
    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}