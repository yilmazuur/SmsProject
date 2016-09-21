using NHibernate;
using SmsProject.App.Data.DAL;
using SmsProject.App.Data.DAL.Interface;

namespace SmsProject.App.Operation.Base
{
    /// <summary>
    /// All operation classes will share these methods, so we created a base class
    /// </summary>
    public class OperationBase
    {
        /// <summary>
        /// Unittests will run on memory not in db. That is why this property is public. We will override it when necessary.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// UnitTest Empty Ctor
        /// </summary>
        protected OperationBase() { }

        /// <summary>
        /// Farklı factoryler alarak farklı veritabanlarına gidebilmeyi sağlar.
        /// Different factories can relate with different databases.
        /// We pass factory as parameter to the unitOfWork, so we can use different databases for each connection. E.g.  SmsProject.Data.Persistence
        /// </summary>
        /// <param name="factory"></param>
        protected OperationBase(ISessionFactory factory)
        {
            UnitOfWork = new UnitOfWork(factory);
        }

        /// <summary>
        /// unitOfWork dispose
        /// </summary>
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
