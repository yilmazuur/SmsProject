using log4net.Appender;
using log4net.Core;
using SmsProject.App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Operation.Log
{
    /// <summary>
    /// Updates serviceCall Detail
    /// </summary>
    public class ServiceCallAppender : AppenderSkeleton
    {
        private static object _syncLock = new object();
        protected override void Append(LoggingEvent loggingEvent)
        {

            Task.Run(() =>
            {
                lock (_syncLock)
                {
                    if (loggingEvent.MessageObject != null)
                    {
                        var callerInfo = ((CallerInfo)loggingEvent.MessageObject).Clone() as CallerInfo; //CallerInfo can change while this operation is running in async, so clone the object
                        using (var operation = new ServiceCallOperations())
                        {
                            operation.Add(callerInfo);
                        }
                    }
                }
            });
        }
    }
}
