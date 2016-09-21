using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SmsProject.App.Common.Logging;
using SmsProject.App.API.Common.Base;
using Microsoft.Owin;
using SmsProject.App.Common;
using SmsProject.App.Model.Model.LogModel;

namespace SmsProject.App.API.Common.Extensibility
{
    public class LoggingInterceptor : MessageHandlerBase
    {
        /// <summary>
        /// Logs every request and response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override Task IncomingMessageAsync(IOwinContext context, HttpRequestMessage message)
        {
            Toolkit.CallerInfo.Log().Info(Toolkit.CallerInfo);
            return Task.FromResult<object>(null);
        }

        protected override Task OutgoingMessageAsync(IOwinContext context, HttpResponseMessage message)
        {
            Toolkit.CallerInfo.Log().Info(Toolkit.CallerInfo);
            return Task.FromResult<object>(null);
        }
    }
}
