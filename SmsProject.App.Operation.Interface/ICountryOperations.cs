using SmsProject.App.Model.Model.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Operation.Interface
{
    public interface ICountryOperations : IDisposable
    {
        List<Country> GetAll();
    }
}
