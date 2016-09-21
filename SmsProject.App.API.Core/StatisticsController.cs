using SmsProject.App.Model.Model.BusinessModel;
using SmsProject.App.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmsProject.App.API.Core
{
    /// <summary>
    /// Statistics Controller
    /// </summary>
    [RoutePrefix("statistics.{ext}")]
    public class StatisticsController : ApiController
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public StatisticsController()
        {
        }

        /// <summary>
        /// Custom dispose operations if necessary
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {

        }

        /// <summary>
        /// Get Sms Price Statistics
        /// </summary>
        /// <param name="dateFrom">format: "yyyy-MM-dd"</param>
        /// <param name="dateTo">format: "yyyy-MM-dd"</param>
        /// <param name="mccList">list of mobile country codes, e.g. "262,232"</param>
        /// <returns></returns>
        [Route]
        public async Task<List<Statistic>> GetStatistics(DateTime dateFrom, DateTime dateTo, string mccList)
        {
            return await Task<List<Statistic>>.Run(() =>
            {
                using (var op = new SmsOperations())
                {
                    return op.GetStatistics(dateFrom, dateTo, mccList.Split(',').ToList());
                }
            });
        }
    }
}
