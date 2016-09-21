using Newtonsoft.Json;
using SmsProject.App.Data.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Model.Model.BusinessModel
{
    /// <summary>
    /// Will be used for classmap in fluentnhibernate 
    /// That is the cause of virtual properties
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Country : IEntityKey<Int32>
    {
        public virtual int Id { get; set; }
        [JsonProperty]
        public virtual string Mcc { get; set; }
        [JsonProperty]
        public virtual string Cc { get; set; }
        [JsonProperty]
        public virtual string Name { get; set; }
        [JsonProperty]
        public virtual decimal PricePerSms { get; set; }
    }
}
