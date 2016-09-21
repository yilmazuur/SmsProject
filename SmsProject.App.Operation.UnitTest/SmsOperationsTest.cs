using Moq;
using NUnit.Framework;
using SmsProject.App.Data.DAL.Interface;
using SmsProject.App.Model;
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
    public class SmsOperationsTest
    {
        private ISmsOperations _smsOperations;
        private Mock<IGenericRepository<Int32, Sms>> _fakeSmsRepo;
        private Mock<IGenericRepository<Int32, Country>> _fakeCountryRepo;
        private Mock<IUnitOfWork> _fakeUnitOfWork;
        private State _smsResult;
        private SentSms _realGetSentSmsResult;

        private const string fromParameter = "+4923123123";
        private const string toParameter = "+431231211";
        private const string message = "Hello John Doe";

        private List<string> mccList = new List<string>() { "262", "232" };
        private DateTime dateFrom = DateTime.Parse("2015-08-19");
        private DateTime dateTo = DateTime.Parse("2016-08-19");
        private const int skip = 5;
        private const int take = 10;

        [SetUp]
        public void Setup()
        {
            //Create Repo
            _fakeSmsRepo = new Mock<IGenericRepository<Int32, Sms>>();
            //Create unitOfWork and add Repo
            _fakeUnitOfWork = new Mock<IUnitOfWork>();
            _fakeUnitOfWork.Setup(x => x.Repositories).Returns(new Dictionary<string, Object>());
            _fakeUnitOfWork.Object.Repositories.Add(typeof(Sms).Name, _fakeSmsRepo.Object); //Repositories mapped by entity names in unitOfWork(for detail check UnitOfWork.cs)
            _fakeUnitOfWork.Object.Repositories.Add(typeof(Country).Name, _fakeCountryRepo.Object); //Repositories mapped by entity names in unitOfWork(for detail check UnitOfWork.cs)

            //Will work for first line of SendSms
            List<Country> countries;
            using (var op = new CountryOperations())
            {
                countries = op.GetAll();
                _fakeCountryRepo.Setup(r => r.FindBy(x => x.Cc == toParameter.Substring(1, 2))).Returns(countries.Find(x=> x.Cc == toParameter));
            }
            
            //get real smses from db and add it to repo memory
            //unit tests should not go to database, all logic should be in memory.
            using (var op = new SmsOperations())
            {
                _realGetSentSmsResult = op.GetSentSms(dateFrom, dateTo, skip, take);
                _fakeSmsRepo.Setup(r => r.FilterBy(x => x.DateTime >= dateFrom && x.DateTime <= dateTo).ToList())
                    .Returns(op.GetAll().FindAll(x => x.DateTime >= dateFrom && x.DateTime <= dateTo).ToList());
            }
            
            //put mocked unitOfWork to the operation
            _smsOperations = new SmsOperations(_fakeUnitOfWork.Object);
        }

        [Test]
        public void CheckSendSmsResult()
        {
            var result = _smsOperations.SendSms(fromParameter, toParameter, message);
            //check counts of realCountries we set earlier and the countries repo gave us
            Assert.AreEqual(result, State.Success);
        }

        [Test]
        public void GetSentSmsTest()
        {
            var result = _smsOperations.GetSentSms(dateFrom, dateTo, skip, take);
            Assert.AreEqual(this._realGetSentSmsResult.Items.Count, result.Items.Count);
            Assert.AreEqual(this._realGetSentSmsResult.TotalCount, result.TotalCount);
        }

        [Test]
        public void GetStatistic()
        {
            using (var op = new SmsOperations())
            {
                var res = op.GetStatistics(dateFrom, dateTo, mccList);
                Assert.AreEqual(res.Count, 1);
            }
        }
    }
}
