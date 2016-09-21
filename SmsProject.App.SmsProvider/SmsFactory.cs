using SmsProject.App.Model;
using SmsProject.App.Model.Model.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmsProject.App.Common.Logging;

namespace SmsProject.App.SmsProvider
{
    public class SmsFactory
    {
        public State Send(Sms sms)
        {
            try
            {
                this.Log().Info(sms);
                return State.Success;
            }
            catch 
            {
                return State.Failed;
            }
        }
    }
}
