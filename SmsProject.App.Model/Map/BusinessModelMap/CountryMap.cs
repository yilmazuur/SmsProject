using FluentNHibernate.Mapping;
using SmsProject.App.Model.Model.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Model.Map.BusinessModelMap
{
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Table("BS_Country");
            Id(x => x.Id)
                .Column("ID")
                .GeneratedBy.Identity();
            Map(x => x.Name)
                .Column("NAME")
                .CustomSqlType("NVARCHAR(200)")
                .Not.Nullable();
            Map(x => x.Mcc)
                .Column("MCC")
                .CustomSqlType("NVARCHAR(10)")
                .Not.Nullable();
            Map(x => x.Cc)
                .Column("CC")
                .CustomSqlType("NVARCHAR(10)")
                .Not.Nullable();
            Map(x => x.PricePerSms)
                .Column("PRICEPERSMS")
                .Not.Nullable();
        }
    }
}
