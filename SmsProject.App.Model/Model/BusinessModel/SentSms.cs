using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Model.Model.BusinessModel
{
    public class SentSms
    {
        public int TotalCount { get; set; }
        public List<Sms> Items { get; set; }
    }
}
