using System;

namespace SmsProject.App.Common.Logging
{
    public interface ILogManager
    {
        ILogger GetLogger(Type type);
    }
}
