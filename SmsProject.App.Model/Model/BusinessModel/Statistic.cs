using Newtonsoft.Json;
using SmsProject.App.Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Model.Model.BusinessModel
{
    public class Statistic
    {
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime Day { get; set; }
        public string Mcc { get; set; }
        public decimal PricePerSms { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
