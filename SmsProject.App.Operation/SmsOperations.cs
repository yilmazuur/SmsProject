using SmsProject.App.Data.DAL.Interface;
using SmsProject.App.Data.Persistence;
using SmsProject.App.Model;
using SmsProject.App.Model.Model.BusinessModel;
using SmsProject.App.Operation.Base;
using SmsProject.App.Operation.Interface;
using SmsProject.App.SmsProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Operation
{
    public class SmsOperations : OperationBase, ISmsOperations
    {
        private IGenericRepository<Int32, Sms> _smsRepo;
        private IGenericRepository<Int32, Country> _countryRepo;

                #region Ctor
        /// <summary>
        /// UnitTest Ctor
        /// Repository mappings will be in Ctor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SmsOperations(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _smsRepo = UnitOfWork.Repositories["Sms"] as IGenericRepository<Int32, Sms>;
            _countryRepo = UnitOfWork.Repositories["Country"] as IGenericRepository<Int32, Country>;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public SmsOperations()
            : base(PersistenceFactory.Create())
        {
            _smsRepo = UnitOfWork.CreateRepository<Int32, Sms>();
            _countryRepo = UnitOfWork.CreateRepository<Int32, Country>();
        }

        #endregion

        public State SendSms(string from, string to, string message)
        {
            var country = _countryRepo.FindBy(x=> x.Cc == to.Substring(1,2));
            var sms = new Sms();
            sms.From = from;
            sms.To = to;
            sms.Message = message;
            sms.DateTime = DateTime.Now;
            sms.Mcc = country.Mcc;
            sms.Price = country.PricePerSms;
            sms.State = State.Failed;
            sms.State = new SmsFactory().Send(sms);
            _smsRepo.Add(sms);
            UnitOfWork.Commit();
            return sms.State;
        }

        public SentSms GetSentSms(DateTime dateTimeFrom, DateTime dateTimeTo, int skip, int take)
        {
            SentSms result = new SentSms();
            var smsList = _smsRepo.FilterBy(x => x.DateTime >= dateTimeFrom && x.DateTime <= dateTimeTo).ToList();
            result.TotalCount = smsList.Count;
            result.Items = new List<Sms>();
            result.Items.AddRange(smsList.Skip(skip).Take(take));
            return result;
        }

        /// <summary>
        /// You will see the beauty of dynamic here
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="mccList"></param>
        /// <returns></returns>
        public List<Statistic> GetStatistics(DateTime dateFrom, DateTime dateTo, List<string> mccList)
        {
            List<Statistic> result = new List<Statistic>();

            //This is a mssql query. If you use mysql you just change pricepersms like limit 1 in the end instead of top(1).
            var values = _smsRepo.ExecuteQuery(
                string.Format(@"SELECT CAST(DATETIME AS DATE) AS DATE, 
                                        MCC, 
                                        (select TOP(1) PRICEPERSMS FROM BS_Country WHERE MCC = A.MCC) AS PRICEPERSMS, 
                                        COUNT(*) AS COUNT, 
                                        SUM(PRICE) AS TOTALPRICE 
                                FROM BS_Sms A 
                                WHERE CAST(DATETIME AS DATE) >= '{0}' AND CAST(DATETIME AS DATE) <= '{1}'
                                GROUP BY CAST(DATETIME AS DATE), MCC", dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd")));
            foreach (var item in values)
            {
                //correction of properties of item will be decided at run time 
                Statistic stat = new Statistic();
                stat.Count = item.COUNT;
                stat.Day = item.DATE;
                stat.Mcc = item.MCC;
                stat.PricePerSms = item.PRICEPERSMS;
                stat.TotalPrice = item.TOTALPRICE;
                result.Add(stat);
            }
            return result;
        }

        public List<Sms> GetAll() 
        {
            return _smsRepo.All().ToList();
        }
    }
}
