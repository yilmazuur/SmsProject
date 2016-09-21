using System;

namespace SmsProject.App.Common.Logging
{
    public static class LoggerExtensions
    {
        public static void Debug(this ILogger logger, Func<string> action)
        {
            if (!logger.IsDebugEnabled)
            {
                return;
            }

            var logMessage = action();
            logger.Debug(logMessage);
        }

        public static void Info(this ILogger logger, Func<string> action)
        {
            if (!logger.IsInfoEnabled)
            {
                return;
            }

            var logMessage = action();
            logger.Info(logMessage);
        }

        public static void Warn(this ILogger logger, Func<string> action)
        {
            if (!logger.IsWarnEnabled)
            {
                return;
            }

            var logMessage = action();
            logger.Warn(logMessage);
        }

        public static void Error(this ILogger logger, Func<string> action)
        {
            if (!logger.IsErrorEnabled)
            {
                return;
            }

            var logMessage = action();
            logger.Error(logMessage);
        }

        public static void Fatal(this ILogger logger, Func<string> action)
        {
            if (!logger.IsFatalEnabled)
            {
                return;
            }

            var logMessage = action();
            logger.Fatal(logMessage);
        }
    }
}
