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
    /// CountriesController
    /// </summary>
    [RoutePrefix("countries.{ext}")]
    public class CountriesController : ApiController
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public CountriesController()
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
        /// Get countries
        /// </summary>
        /// <returns>array of country</returns>
        [Route]
        public async Task<List<Country>> Get()
        {
            //Added non-blocking code for scalability
            return await Task<List<Country>>.Run(() =>
            {
                using (var op = new CountryOperations())
                {
                    return op.GetAll();
                }
            });
        }
    }
}
