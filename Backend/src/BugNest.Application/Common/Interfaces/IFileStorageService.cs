public interface IFileStorageService
{
    Task<string> SaveArchiveAsync(Stream zipStream, string fileName);
    Task<string> ExtractArchiveAsync(string archivePath);
}
