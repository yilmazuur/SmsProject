using Moq;
using NUnit.Framework;
using SmsProject.App.Data.DAL.Interface;
using SmsProject.App.Model.Model.BusinessModel;
using SmsProject.App.Operation.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Operation.UnitTest
{
    [TestFixture]
    public class CountryOperationsTest
    {
        private ICountryOperations _countryOperations;
        private Mock<IGenericRepository<Int32, Country>> _fakeCountryRepo;
        private Mock<IUnitOfWork> _fakeUnitOfWork;
        private List<Country> _realCountries;

        [SetUp]
        public void Setup()
        {
            //Create Repo
            _fakeCountryRepo = new Mock<IGenericRepository<Int32, Country>>();
            //Create unitOfWork and add Repo
            _fakeUnitOfWork = new Mock<IUnitOfWork>();
            _fakeUnitOfWork.Setup(x => x.Repositories).Returns(new Dictionary<string, Object>());
            _fakeUnitOfWork.Object.Repositories.Add(typeof(Country).Name, _fakeCountryRepo.Object); //Repositories mapped by entity names in unitOfWork(for detail check UnitOfWork.cs)

            //put mocked unitOfWork to the operation
            _countryOperations = new CountryOperations(_fakeUnitOfWork.Object);

            //get real countries from db and add it to repo memory
            //unit tests should not go to database, all logic should be in memory.
            using (var op = new CountryOperations())
            {
                this._realCountries = op.GetAll();
            }
            _fakeCountryRepo.Setup(r => r.All().ToList()).Returns(this._realCountries);
        }



        [Test]
        public void ShouldGetCountries()
        {
            List<Country> countries = _countryOperations.GetAll();
            //check counts of realCountries we set earlier and the countries repo gave us
            Assert.AreEqual(countries.Count, this._realCountries.Count);
        }
    }
}
