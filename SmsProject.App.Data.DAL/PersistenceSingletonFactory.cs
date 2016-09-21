using System;
using System.Reflection;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace SmsProject.App.Data.DAL
{
    /// <summary>
    /// NHibernate DB connection factory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PersistenceSingletonFactory<T> where T : class, new()
    {
        private static object _syncLock = new object(); //this is for providing async call support
        private static PersistenceSingletonFactory<T> _instance;

        private PersistenceSingletonFactory() { }

        /// <summary>
        /// Multi thread support Singleton Factory Instance
        /// </summary>
        public static PersistenceSingletonFactory<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new PersistenceSingletonFactory<T>();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Creates nhibernate session for database connection
        /// </summary>
        public ISessionFactory SessionFactory
        {
            get
            {
                var assemblyName = Assembly.GetCallingAssembly().GetName().Name;

                LoggerProvider.SetLoggersFactory(new NoLoggingLoggerFactory());
                var config = new Configuration();
                config.Configure(AppDomain.CurrentDomain.BaseDirectory + assemblyName + ".cfg.xml");
                var session = Fluently
                    .Configure(config)
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>())
                    //this line creates missing tables and columns to database. it won't drop or delete. we can comment this line after creation of db. requests will be faster.
                    .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))//code first effect 
                    .BuildSessionFactory();
                return session;
            }
        }
    }
}
