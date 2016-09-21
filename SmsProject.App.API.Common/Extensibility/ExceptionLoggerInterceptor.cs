using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using SmsProject.App.Common.Logging;
using SmsProject.App.Common;
using SmsProject.App.Model.Model.LogModel;
using SmsProject.App.Common.Base;

namespace SmsProject.App.API.Common.Extensibility
{
    /// <summary>
    /// Catches unhandled exceptions
    /// </summary>
    public class ExceptionLoggerInterceptor : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var exception = context.Exception;
            Error error = new Error()
            {
                Id = Guid.NewGuid(),
                Message = exception.Message,
                InsertDateTime = DateTime.Now,
                Type = exception is ExceptionBase ? Error.ErrorTypes.Warning : Error.ErrorTypes.Error,
                ServiceCall = new ServiceCall() { Id = Toolkit.CallerInfo == null ? -1 : Toolkit.CallerInfo.ServiceCallId },
                Detail = string.Format("{0}\n{1}", exception.Message, exception.StackTrace)
            };
            while (exception.InnerException != null)
            {
                exception = exception.InnerException; //get all innerExceptions
                error.Detail += string.Format("\n{0}\n{1}", exception.Message, exception.StackTrace);
            }
            Toolkit.OwinContext.Set<Error>("Error", error);
            this.Log().Info(error);
        }
    }
}
