using FluentNHibernate.Mapping;
using SmsProject.App.Model.Model.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Model.Map.BusinessModelMap
{
    public class SmsMap : ClassMap<Sms>
    {
        public SmsMap()
        {
            Table("BS_Sms");
            Id(x => x.Id)
                .Column("ID")
                .GeneratedBy.Identity();
            Map(x => x.DateTime)
                .Column("DATETIME")
                .Default("GETDATE()")
                .Not.Nullable();
            Map(x => x.Mcc)
                .Column("MCC")
                .CustomSqlType("NVARCHAR(10)")
                .Not.Nullable();
            Map(x => x.From)
                .Column("MFROM")
                .CustomSqlType("NVARCHAR(200)")
                .Not.Nullable();
            Map(x => x.To)
                .Column("MTO")
                .CustomSqlType("NVARCHAR(200)")
                .Not.Nullable();
            Map(x => x.Message)
                .Column("MESSAGE")
                .CustomSqlType("NVARCHAR(500)")
                .Not.Nullable();
            Map(x => x.Price)
                .Column("PRICE")
                .Not.Nullable();
            Map(x => x.State)
                .Column("STATE")
                .Not.Nullable();
        }
    }
}
