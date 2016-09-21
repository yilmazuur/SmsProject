using System;
using SmsProject.App.Model.Model.LogModel;
using SmsProject.App.Common;

namespace SmsProject.App.Operation.Interface
{
    public interface IServiceCallOperations : IDisposable
    {
        ServiceCall Add(ServiceCall serviceCall);
        ServiceCallDetail Add(CallerInfo callerInfo);
        void UpdateServiceCallErrorId(long id, Guid errorId);
    }
}
