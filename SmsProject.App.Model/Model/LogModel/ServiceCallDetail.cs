using System;
using SmsProject.App.Data.DAL.Interface;

namespace SmsProject.App.Model.Model.LogModel
{
    public class ServiceCallDetail : IEntityKey<long>
    {
        public virtual long Id { get; set; }
        public virtual string Header { get; set; }
        public virtual string Body { get; set; }
        public virtual Types Type { get; set; }
        public virtual DateTime InsertDateTime { get; set; }

        public virtual ServiceCall ServiceCall { get; set; }

        public ServiceCallDetail()
        {
        }

        #region Enums
        public enum Types
        {
            Request = 0,
            Response = 10
        }
        #endregion
    }
}
