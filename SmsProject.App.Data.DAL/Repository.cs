using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;
using SmsProject.App.Data.DAL.Interface;

namespace SmsProject.App.Data.DAL
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="TKey">Key variable type</typeparam>
    /// <typeparam name="T">Repository object</typeparam>
    public class Repository<TKey, T> : IGenericRepository<TKey, T> where T : class, IEntityKey<TKey>
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public bool Add(T entity)
        {
            _session.Save(entity);
            return true;
        }

        public bool Add(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                _session.Save(item);
            }
            return true;
        }

        /// <summary>
        /// We don't have to send nullable fields that won't be updated by this.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Merge(T entity)
        {
            _session.Merge(entity);
            return true;
        }

        public bool Update(T entity)
        {
            _session.Update(entity);
            return true;
        }

        public bool Delete(T entity)
        {
            _session.Delete(entity);
            return true;
        }

        public bool Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                _session.Delete(entity);
            }
            return true;
        }

        public IQueryable<T> All()
        {
            return _session.Query<T>();
        }

        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).SingleOrDefault();
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return All().Where(expression).AsQueryable();
        }

        public T FindBy(TKey id)
        {
            return _session.Get<T>(id);
        }

        public IList<dynamic> ExecuteQuery(string query)
        {
            return _session.CreateSQLQuery(query).DynamicList();
        }

        public int ExecuteNonQuery(string query) 
        {
            return _session.CreateSQLQuery(query).ExecuteUpdate();
        }
    }
}