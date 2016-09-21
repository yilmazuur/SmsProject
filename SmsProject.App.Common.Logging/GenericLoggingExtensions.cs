namespace SmsProject.App.Common.Logging
{
    public static class GenericLoggingExtensions
    {
        public static ILogger Log<T>(this T thing)
        {
            var log = LogManager.GetLogger<T>();
            return log;
        }
    }
}
