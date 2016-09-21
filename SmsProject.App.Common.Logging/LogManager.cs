using System;

namespace SmsProject.App.Common.Logging
{
    public class LogManager : ILogManager
    {
        private static readonly ILogManager LogManagerInstance;

        static LogManager()
        {
            LogManagerInstance = new LogManager();
        }

        public static ILogger GetLogger<T>()
        {
            return LogManagerInstance.GetLogger(typeof(T));
        }

        public ILogger GetLogger(Type type)
        {
            var logger = log4net.LogManager.GetLogger(type);
            return new LoggerAdapter(logger);
        }
    }
}
