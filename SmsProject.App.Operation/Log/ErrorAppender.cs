using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Appender;
using log4net.Core;
using SmsProject.App.Common;
using SmsProject.App.Model.Model.LogModel;

namespace SmsProject.App.Operation.Log
{
    public class ErrorAppender : AppenderSkeleton
    {
        private static object _syncLock = new object();
        protected override void Append(LoggingEvent loggingEvent)
        {
            var callerInfo = Toolkit.CallerInfo;
            Task.Run(() =>
            {
                lock (_syncLock)
                {
                    if (loggingEvent.MessageObject != null && callerInfo != null)
                    {
                        var error = ((Error)loggingEvent.MessageObject).Clone() as Error;
                        using (var errorOp = new ErrorOperations())
                        {
                            errorOp.Add(error);
                        }
                        using (var serviceOp = new ServiceCallOperations())
                        {
                            serviceOp.UpdateServiceCallErrorId(callerInfo.ServiceCallId, error.Id);
                        }
                    }
                }
            });
        }
    }
}
