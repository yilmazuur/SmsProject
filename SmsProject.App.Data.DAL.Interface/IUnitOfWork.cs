using System;
using System.Collections.Generic;

namespace SmsProject.App.Data.DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Dictionary<string, object> Repositories { get; set; }
        IGenericRepository<TKey, T> CreateRepository<TKey, T>() where T : class, IEntityKey<TKey>;
        void Commit();
        void Rollback();
    }
}