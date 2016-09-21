using log4net.Appender;
using log4net.Core;
using SmsProject.App.Model.Model.BusinessModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.SmsProvider.Log
{
    public class SmsAppender : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            var sms = (Sms)loggingEvent.MessageObject;
            var filename = System.String.Format(@"{0}\bin\SentSms.csv", System.AppDomain.CurrentDomain.BaseDirectory);

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0};", sms.From));
            sb.Append(string.Format("{0};", sms.To));
            sb.Append(string.Format("{0};", sms.Message));

            if (!File.Exists(filename))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filename))
                {
                    sw.WriteLine("from;to;message;");
                }
            }
            using (StreamWriter sw = File.AppendText(filename))
            {
                sw.WriteLine(sb.ToString());
            }
        }
    }
}

