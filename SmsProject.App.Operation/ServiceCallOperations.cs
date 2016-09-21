    using System;
using SmsProject.App.Data.DAL.Interface;
using SmsProject.App.Data.Persistence;
using SmsProject.App.Model.Model.LogModel;
using SmsProject.App.Operation.Base;
using SmsProject.App.Operation.Interface;
using SmsProject.App.Common;

namespace SmsProject.App.Operation
{
    /// <summary>
    /// Servis Call işlemleri
    /// </summary>
    public class ServiceCallOperations : OperationBase, IServiceCallOperations
    {
        private IGenericRepository<long, ServiceCall> _serviceCallRepo;
        private IGenericRepository<long, ServiceCallDetail> _serviceCallDetailRepo;

        /// <summary>
        /// UnitTest Ctor
        /// Repository mappings will be in Ctor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ServiceCallOperations(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _serviceCallRepo = UnitOfWork.Repositories["ServiceCall"] as IGenericRepository<long, ServiceCall>;
            _serviceCallDetailRepo = UnitOfWork.Repositories["ServiceCallDetail"] as IGenericRepository<long, ServiceCallDetail>;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public ServiceCallOperations()
            : base(PersistenceFactory.Create())
        {
            _serviceCallRepo = UnitOfWork.CreateRepository<long, ServiceCall>();
            _serviceCallDetailRepo = UnitOfWork.CreateRepository<long, ServiceCallDetail>();
        }

        /// <summary>
        /// Logs servicecall in db. Table: FW_ServiceCall
        /// </summary>
        /// <param name="serviceCall"></param>
        public ServiceCall Add(ServiceCall serviceCall)
        {
            _serviceCallRepo.Add(serviceCall);
            UnitOfWork.Commit();
            return serviceCall;
        }

        /// <summary>
        /// Logs servicecall detail in db. Table: FW_ServiceCallDetail
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public ServiceCallDetail Add(CallerInfo callerInfo) 
        {
            ServiceCallDetail detail = new ServiceCallDetail();
            detail.ServiceCall = new ServiceCall() { Id = callerInfo.ServiceCallId };
            detail.InsertDateTime = DateTime.Now;
            if (callerInfo.State == CallerInfo.PipelineState.OnRequest)
            {
                detail.Body = callerInfo.RequestBody;
                detail.Header = callerInfo.RequestHeader;
                detail.Type = ServiceCallDetail.Types.Request;
            }
            else
            {
                detail.Body = callerInfo.ResponseBody;
                detail.Header = callerInfo.ResponseHeader;
                detail.Type = ServiceCallDetail.Types.Response;
            }
            _serviceCallDetailRepo.Add(detail);
            UnitOfWork.Commit();
            return detail;
        }

        /// <summary>
        /// Update error id of servicecall if we encounter any error.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorId"></param>
        public void UpdateServiceCallErrorId(long id, Guid errorId) 
        {
            var serviceCall = _serviceCallRepo.FindBy(id);
            serviceCall.Error = new Error() { Id = errorId };
            _serviceCallRepo.Update(serviceCall);
            UnitOfWork.Commit();
        }
    }
}
