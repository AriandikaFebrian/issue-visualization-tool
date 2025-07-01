using BugNest.Application.Interfaces;
using System.IO.Compression;

public class FileStorageService : IFileStorageService
{
    private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

    public async Task<string> SaveArchiveAsync(Stream zipStream, string fileName)
    {
        Directory.CreateDirectory(_basePath);
        var filePath = Path.Combine(_basePath, $"{Guid.NewGuid()}_{fileName}");
        using var fileStream = File.Create(filePath);
        await zipStream.CopyToAsync(fileStream);
        return filePath;
    }

    public async Task<string> ExtractArchiveAsync(string archivePath)
    {
        var extractDir = Path.Combine(_basePath, Path.GetFileNameWithoutExtension(archivePath) + "_extracted");
        ZipFile.ExtractToDirectory(archivePath, extractDir);
        await Task.CompletedTask;
        return extractDir;
    }
}
