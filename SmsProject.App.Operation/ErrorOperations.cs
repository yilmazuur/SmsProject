using SmsProject.App.Data.DAL.Interface;
using SmsProject.App.Data.Persistence;
using SmsProject.App.Model.Model.LogModel;
using SmsProject.App.Operation.Base;
using SmsProject.App.Operation.Interface;
using System;

namespace SmsProject.App.Operation
{
    /// <summary>
    /// Error işlemleri
    /// </summary>
    public class ErrorOperations : OperationBase, IErrorOperations
    {
        private IGenericRepository<Guid, Error> _errorRepo;

        #region Ctor
        /// <summary>
        /// UnitTest Ctor
        /// Repository mappings will be in Ctor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ErrorOperations(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _errorRepo = UnitOfWork.Repositories["Error"] as IGenericRepository<Guid, Error>;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public ErrorOperations()
            : base(PersistenceFactory.Create())
        {
            _errorRepo = UnitOfWork.CreateRepository<Guid, Error>();
        }

        #endregion

        /// <summary>
        /// Log errors on db. Table: FW_ERROR
        /// </summary>
        /// <param name="serviceCall"></param>
        public void Add(Error error)
        {
            _errorRepo.Add(error);
            UnitOfWork.Commit();
        }
    }
}
