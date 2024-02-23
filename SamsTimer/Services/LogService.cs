using MetroLog.Operators;

namespace SamsTimer.Services
{
    public class LogService : ILogService
    {
        protected ILogCompressor? LogCompressor { get; }
        protected ILogOperatorRetriever OperatorRetriever { get; }

        public LogService()
        {
            OperatorRetriever = LogOperatorRetriever.Instance;

            OperatorRetriever.TryGetOperator<ILogCompressor>(out var logCompressor);
            LogCompressor = logCompressor;
        }

        public async Task<MemoryStream?> GetCompressedLogs()
        {
            if (LogCompressor == null)
            {
                return null;
            }

            return await LogCompressor.GetCompressedLogs();
        }
    }
}