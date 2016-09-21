using SmsProject.App.Model.Model.LogModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmsProject.App.Common
{
    public class CallerInfo : ICloneable
    {
        public long ServiceCallId { get; set; }
        public string HttpMethod { get; set; }
        public string RequestUri { get; set; }
        public string Service { get; set; }
        public string Method { get; set; }
        public IEnumerable<KeyValuePair<string, string>> QueryParameters { get; set; }
        public string RequestHeader { get; set; }
        public string RequestBody { get; set; }
        public string ResponseHeader { get; set; }
        public string ResponseBody { get; set; }
        public PipelineState State { get; set; }

        public CallerInfo() 
        {
          
        }

        public enum PipelineState
        {
            OnRequest = 2,
            OnResponse = 4
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
