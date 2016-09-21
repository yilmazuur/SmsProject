using SmsProject.App.Data.DAL.Interface;
using SmsProject.App.Data.Persistence;
using SmsProject.App.Model.Model.BusinessModel;
using SmsProject.App.Operation.Base;
using SmsProject.App.Operation.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Operation
{
    public class CountryOperations : OperationBase, ICountryOperations
    {
        private IGenericRepository<Int32, Country> _countryRepo;

                #region Ctor
        /// <summary>
        /// UnitTest Ctor
        /// Repository mappings will be in Ctor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CountryOperations(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _countryRepo = UnitOfWork.Repositories["Country"] as IGenericRepository<Int32, Country>;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public CountryOperations()
            : base(PersistenceFactory.Create())
        {
            _countryRepo = UnitOfWork.CreateRepository<Int32, Country>();
        }

        #endregion

        public List<Country> GetAll()
        {
            return _countryRepo.All().ToList();
        }
    }
}
