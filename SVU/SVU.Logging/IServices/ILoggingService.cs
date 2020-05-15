using System;

namespace SVU.Logging.IServices
{
    /// <summary>
    /// The base logging service functions
    /// </summary>
    public interface ILoggingService
    {
        void LogException(Exception ex);
    }
}
