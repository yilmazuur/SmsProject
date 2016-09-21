using System.Collections.Generic;
using SmsProject.App.Data.DAL.Interface;

namespace SmsProject.App.Model.Model.LogModel
{
    public class ServiceCall : IEntityKey<long>
    {
        public virtual long Id { get; set; }
        public virtual string Uri { get; set; }
        public virtual string HttpMethod { get; set; }
        public virtual Error Error { get; set; }
        public virtual ICollection<ServiceCallDetail> Details { get; set; }

        public ServiceCall()
        {
            Details = new List<ServiceCallDetail>();
        }
    }
}
