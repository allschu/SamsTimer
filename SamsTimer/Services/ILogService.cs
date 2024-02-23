namespace SamsTimer.Services
{
    public interface ILogService
    {
        Task<MemoryStream?> GetCompressedLogs();
    }
}