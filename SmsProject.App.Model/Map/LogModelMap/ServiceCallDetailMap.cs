using FluentNHibernate.Mapping;
using SmsProject.App.Model.Model.LogModel;

namespace SmsProject.App.Model.Map.LogModelMap
{
    public class ServiceCallDetailMap : ClassMap<ServiceCallDetail>
    {
        public ServiceCallDetailMap()
        {
            Table("LG_ServiceCallDetail");
            Id(x => x.Id)
                .Column("ID")
                .GeneratedBy.Identity();
            Map(x => x.Type)
                .Column("TYPE")
                .CustomSqlType("NVARCHAR(25)")
                .Not.Nullable();
            Map(x => x.InsertDateTime)
                .Column("INSERT_DATETIME")
                .Default("GETDATE()")
                .Not.Nullable();
            Map(x => x.Header)
                .Column("HEADER")
                .CustomSqlType("NVARCHAR(MAX)")
                .Length(int.MaxValue);
            Map(x => x.Body)
                .Column("BODY")
                .CustomSqlType("NVARCHAR(MAX)")
                .Length(int.MaxValue);

            References(x => x.ServiceCall).Column("SERVICECALL_ID").Not.Nullable();
        }
    }
}
