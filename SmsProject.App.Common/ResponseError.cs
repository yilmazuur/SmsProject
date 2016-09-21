using SmsProject.App.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Common
{
    /// <summary>
    /// StackTrace and detailed exception message won't be shown to the user for security reasons and to be user friendly.
    /// If a user or owner of the consumer service gives us to error id, we will check the reason in our logs.
    /// </summary>
    public class ResponseError 
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string UserFriendlyMessage { get; set; }
        public ResponseError() 
        {
            this.IsSuccess = false;
        }
    }
}
