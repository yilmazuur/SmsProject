using SmsProject.App.Common;
using SmsProject.App.Model;
using SmsProject.App.Model.Model.BusinessModel;
using SmsProject.App.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmsProject.App.API.Core.Controllers
{
    /// <summary>
    /// SmsController
    /// </summary>
    [RoutePrefix("sms")]
    public class SmsController : ApiController
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public SmsController()
        {
        }

        /// <summary>
        /// Custom dispose operations if necessary
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {

        }

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="from">the sender of the message</param>
        /// <param name="to">the receiver of the message</param>
        /// <param name="text">the text which should be sent</param>
        /// <returns>state: enum (Success, Failed)</returns>
        [Route("send.{ext}")]
        [HttpGet]
        public async Task<State> SendSms(string from, string to, string text)
        {
            //Added non-blocking code for scalability
            return await Task<State>.Run(() =>
            {
                using (var op = new SmsOperations())
                {
                    return op.SendSms(from, to, text);
                }
            });
        }

        /// <summary>
        /// List sent sms
        /// </summary>
        /// <param name="dateTimeFrom">format: "yyyy-MM-ddTHH:mm:ss", UTC</param>
        /// <param name="dateTimeTo">format: "yyyy-MM-ddTHH:mm:ss", UTC</param>
        /// <param name="skip">skip n records</param>
        /// <param name="take">take n records</param>
        /// <returns></returns>
        [Route("sent.{ext}")]
        [HttpGet]
        public async Task<SentSms> GetSentSms(DateTime dateTimeFrom, DateTime dateTimeTo, int skip, int take)
        {
            //Added non-blocking code for scalability
            return await Task<SentSms>.Run(() =>
            {
                using (var op = new SmsOperations())
                {
                    return op.GetSentSms(dateTimeFrom, dateTimeTo, skip, take);
                }
            });
        }
    }
}
