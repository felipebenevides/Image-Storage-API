using ImageStorageAPI.Data;
using ImageStorageAPI.Models;
using ImageStorageAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient<RandomImage>();

builder.Services.AddDbContext<ImageDbContext>(options =>
{
    options.UseInMemoryDatabase("ImageDatabase");
});

builder.Services.AddTransient<IRandomImage, RandomImage>();

builder.Services.AddTransient<IImageService, ImageService>();

builder.Services.AddScoped<IImageDbContext, ImageDbContext>();

var Origins = "All";

builder.Services.AddCors(options =>
{
    options.AddPolicy(Origins,
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


var app = builder.Build();


app.UseCors(Origins);


using (var scope = app.Services.CreateScope())
{
    var imageService = scope.ServiceProvider.GetRequiredService<IImageService>();

    await imageService.InsertRandomImages();
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapPost("/api/images/upload", async (ImageRequest imageRequest, IServiceScopeFactory scopeFactory) =>
{
    using var scope = scopeFactory.CreateScope();
    var imageService = scope.ServiceProvider.GetRequiredService<IImageService>();
    var image = await imageService.UploadImage(imageRequest);

    return Results.Created($"/api/images/{image.Id}", image);
}).WithOpenApi();

app.MapGet("/api/images", async (IServiceScopeFactory scopeFactory) =>
{
    using var scope = scopeFactory.CreateScope();
    var imageService = scope.ServiceProvider.GetRequiredService<IImageService>();
    var images = await imageService.GetAllImages();
    return Results.Ok(images);
});


app.MapGet("/api/images/{id}", async (int id, IServiceScopeFactory scopeFactory) =>
{
    using var scope = scopeFactory.CreateScope();
    var imageService = scope.ServiceProvider.GetRequiredService<IImageService>();
    
    var image = await imageService.GetImageById(id);
    if (image == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(image);
});


app.MapDelete("/api/images/{id}/delete", async (int id, IServiceScopeFactory scopeFactory) =>
{
    using var scope = scopeFactory.CreateScope();
    var imageService = scope.ServiceProvider.GetRequiredService<IImageService>();
    
    var result = await imageService.DeleteImage(id);
    if (!result)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});

app.Run();
