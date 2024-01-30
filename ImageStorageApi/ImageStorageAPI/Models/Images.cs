namespace ImageStorageAPI.Models;


public record Image
{
    public int Id { get; init; }
    public string Name { get; init; }
    public byte[] Data { get; init; }
}

public record ImageRequest
{
    public string Name { get; init; }
    public byte[] Data { get; init; }
}