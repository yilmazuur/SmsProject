using SmsProject.App.Model;
using SmsProject.App.Model.Model.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Operation.Interface
{
    public interface ISmsOperations : IDisposable
    {
        State SendSms(string from, string to, string message);
        List<Statistic> GetStatistics(DateTime dateFrom, DateTime dateTo, List<string> mccList);
        SentSms GetSentSms(DateTime dateTimeFrom, DateTime dateTimeTo, int skip, int take);
    }
}
