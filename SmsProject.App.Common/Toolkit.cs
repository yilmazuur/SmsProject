using Microsoft.Owin;
using Microsoft.Owin.Security;
using SmsProject.App.Model.Model.LogModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmsProject.App.Common
{
    public class Toolkit
    {
        public static IOwinContext OwinContext 
        {
            get
            {
                return HttpContext.Current.GetOwinContext();
            }
        }

        public static CallerInfo CallerInfo
        {
            get
            {
                return Toolkit.OwinContext.Get<CallerInfo>("CallerInfo");
            }
        }

        public static Error Error
        {
            get 
            {
                return Toolkit.OwinContext.Get<Error>("Error");
            }
        }
    }
}
