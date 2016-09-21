using Newtonsoft.Json;
using SmsProject.App.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace SmsProject.App.API.Common.Extensibility
{
    /// <summary>
    /// Handles unhandled exceptions in
    ///  - ApiController Level
    ///  - Interceptor and inspector level
    ///  - Operation Level
    ///  - DAL Level
    /// 
    /// ResponseCode is always 500 for security reasons and to be user friendly.
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            ResponseError error = new ResponseError()
            {
                Id = Toolkit.Error.Id,
                IsSuccess = false,
                Message = context.Exception.Message,
                UserFriendlyMessage = "There has been an error. We will track the error with given Id."
            };
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new JsonContent(error),
                ReasonPhrase = context.Exception.GetType().Name
            };

            context.Result = new ExceptionResult(context.Request, responseMessage);
        }
    }

    public class ExceptionResult : IHttpActionResult
    {
        private HttpRequestMessage _request;
        private HttpResponseMessage _httpResponseMessage;

        public ExceptionResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage) 
        {
            _request = request;
            _httpResponseMessage = httpResponseMessage;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_httpResponseMessage);
        }
    }

    public class JsonContent : HttpContent
    {
        private readonly MemoryStream ms;
        public JsonContent(object value)
        {
            ms = new MemoryStream();
            var jw = new JsonTextWriter(new StreamWriter(ms)) { Formatting = Formatting.Indented };
            var serializer = new JsonSerializer();
            serializer.Serialize(jw, value);
            jw.Flush();
            ms.Position = 0;
            
        }
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            ms.CopyTo(stream);
            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }

        protected override bool TryComputeLength(out long length)
        {
            length = ms.Length;
            return true;
        }
    }
}
