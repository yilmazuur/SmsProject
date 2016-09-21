using Microsoft.Owin;
using SmsProject.App.Common;
using SmsProject.App.Model.Model.LogModel;
using SmsProject.App.Operation;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SmsProject.App.API.Common.Base
{
    /// <summary>
    /// Base class of the interceptors.
    /// </summary>
    public abstract class MessageHandlerBase : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            IOwinContext context = Toolkit.OwinContext;
       
            initPipelineMembers(requestMessage, context);

            await IncomingMessageAsync(context, requestMessage);
            var responseMessage = await base.SendAsync(requestMessage, cancellationToken);

            setPipelineMembersOnResponse(responseMessage);

            await OutgoingMessageAsync(context, responseMessage);
            return responseMessage;
        }

        protected abstract Task IncomingMessageAsync(IOwinContext context, HttpRequestMessage message);
        protected abstract Task OutgoingMessageAsync(IOwinContext context, HttpResponseMessage message);

        /// <summary>
        /// Populated CallerInfo response fields
        /// </summary>
        /// <param name="message"></param>
        private static void setPipelineMembersOnResponse(HttpResponseMessage message)
        {
            if (Toolkit.CallerInfo != null)
            {
                Toolkit.CallerInfo.State = CallerInfo.PipelineState.OnResponse;
                Toolkit.CallerInfo.ResponseHeader = message.Headers.ToString();
                Toolkit.CallerInfo.ResponseBody = message.Content != null ? message.Content.ReadAsStringAsync().Result : string.Empty;
            }
        }

        /// <summary>
        /// If requests are coming to the base address or documentation page, do not set initial values.
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="context"></param>
        private void initPipelineMembers(HttpRequestMessage requestMessage, IOwinContext context)
        {
            if (requestMessage.RequestUri.LocalPath.Length > 1 && !requestMessage.RequestUri.AbsoluteUri.Contains("documentation"))
            {
                initCallerInfo(requestMessage);
            }
        }


        /// <summary>
        /// Set CallerInfo in only first interceptor. Baseclass will work for every interceptor.
        /// For example, if we would have logging and authorization interceptors, we don't have to set CallerInfo or Session Values twice.
        /// </summary>
        /// <param name="message"></param>
        private void initCallerInfo(HttpRequestMessage message)
        {
            if (Toolkit.CallerInfo == null)
            {
                CallerInfo callerInfo = new CallerInfo();
                using (var operation = new ServiceCallOperations())
                {
                    var res = operation.Add(new ServiceCall() { Uri = message.RequestUri.AbsoluteUri, HttpMethod=message.Method.Method });
                    callerInfo.ServiceCallId = res.Id;
                }

                callerInfo.HttpMethod = message.Method.Method;
                string[] temp = message.RequestUri.LocalPath.Split('/');
                if (temp.Length >= 2)
                {
                    callerInfo.Service = temp[1];
                    callerInfo.Method = temp.Length == 2 ? string.Empty : temp[2];
                }
                callerInfo.QueryParameters = message.GetQueryNameValuePairs();
                callerInfo.RequestUri = message.RequestUri.AbsoluteUri;
                callerInfo.RequestHeader = message.Headers.ToString();
                callerInfo.State = CallerInfo.PipelineState.OnRequest;
                callerInfo.RequestBody = message.Content.ReadAsStringAsync().Result;
                Toolkit.OwinContext.Set<CallerInfo>("CallerInfo", callerInfo);
            }
        }
    }
}
