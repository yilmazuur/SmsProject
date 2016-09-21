using System;
using SmsProject.App.Model.Model.LogModel;

namespace SmsProject.App.Operation.Interface
{
    public interface IErrorOperations : IDisposable
    {
        void Add(Error Error);
    }
}
