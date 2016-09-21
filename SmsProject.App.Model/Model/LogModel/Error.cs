using System;
using SmsProject.App.Data.DAL.Interface;

namespace SmsProject.App.Model.Model.LogModel
{
    public class Error : IEntityKey<Guid>, ICloneable
    {
        public virtual Guid Id { get; set; }
        public virtual string Message { get; set; }
        public virtual string Detail { get; set; }
        public virtual ErrorTypes Type { get; set; }
        public virtual DateTime InsertDateTime { get; set; }
        public virtual ServiceCall ServiceCall { get; set; }

        public Error()
        {
        }

        #region Enums
        public enum ErrorTypes
        {
            Warning = 0,
            Error = 10,
        }
        #endregion

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
