using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using SmsProject.App.Data.DAL.Interface;

namespace SmsProject.App.Data.DAL
{
    /// <summary>
    /// Distribution logic class between repositories
    /// UnitOfWork Pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITransaction _transaction;
        private bool _disposed;
        private ISession _session;
        public Dictionary<string, object> Repositories { get; set; }

        /// <summary>
        /// Unit test Ctor
        /// </summary>
        public UnitOfWork()
        {

        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sessionFactory"></param>
        public UnitOfWork(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.OpenSession();
            _session.FlushMode = FlushMode.Auto;
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Transaction Commit
        /// </summary>
        public void Commit()
        {
            if (_transaction != null)
            {
                if (!_transaction.IsActive)
                {
                    throw new InvalidOperationException("We don't have an active transaction!");
                }
                _transaction.Commit();
            }
        }

        /// <summary>
        /// Transaction Rollback
        /// </summary>
        public void Rollback()
        {
            if (_transaction != null && _transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_session != null)
                    {
                        if (_session.IsOpen)
                        {
                            _session.Clear();
                            _session.Close();
                        }

                        _session.SessionFactory.Dispose();
                        _session.Dispose();
                    }
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                    }
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Created repositories is gathered on unitOfWork
        /// In this way, same repositories won't be created again and again until related operation ends and unitOfWork disposes
        /// If a repository exists in unitOfWork, we will use it again by this way
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IGenericRepository<TKey, T> CreateRepository<TKey, T>() where T : class, IEntityKey<TKey>
        {
            if (this.Repositories == null)
            {
                this.Repositories = new Dictionary<string, object>();
            }
            var type = typeof(T).Name; //Variable type is the key value of repository 

            if (!this.Repositories.ContainsKey(type))
            {
                Type repositoryType = typeof(Repository<TKey, T>);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _session);
                this.Repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<TKey, T>)this.Repositories[type];
        }
    }
}