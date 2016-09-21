using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Common.Base
{
    public abstract class ExceptionBase : Exception
    {
        public Guid Id { get; set; }

        protected ExceptionBase(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
