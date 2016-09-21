using FluentNHibernate.Mapping;
using SmsProject.App.Model.Model.LogModel;

namespace SmsProject.App.Model.Map.LogModelMap
{
    public class ServiceCallMap : ClassMap<ServiceCall>
    {
        public ServiceCallMap()
        {
            Table("LG_ServiceCall");
            Id(x => x.Id)
                .Column("ID")
                .GeneratedBy.Identity();
            Map(x => x.Uri)
              .Column("URI")
              .CustomSqlType("NVARCHAR(500)")
              .Not.Nullable();
            Map(x => x.HttpMethod)
              .Column("HTTPMETHOD")
              .CustomSqlType("NVARCHAR(50)")
              .Not.Nullable();

            References(x => x.Error).Column("ERROR_ID").Nullable(); 
            HasMany(x => x.Details).KeyColumn("SERVICECALL_ID").LazyLoad().Inverse().Cascade.None();
        }
    }
}
