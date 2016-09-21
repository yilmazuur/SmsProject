using FluentNHibernate.Mapping;
using SmsProject.App.Model.Model.LogModel;

namespace SmsProject.App.Model.Map.LogModelMap
{
    public class ErrorMap : ClassMap<Error>
    {
        public ErrorMap()
        {
            Table("LG_Error");
            Id(x => x.Id)
                .Column("ID")
                .GeneratedBy.GuidComb();
            Map(x => x.Message)
                .Column("MESSAGE")
                .CustomSqlType("NVARCHAR(1000)")
                .Nullable();
            Map(x => x.Detail)
                .Column("DETAIL")
                .CustomSqlType("NVARCHAR(MAX)")
                .Length(int.MaxValue)
                .Not.Nullable();
            Map(x => x.Type)
                .Column("TYPE")
                .CustomSqlType("NVARCHAR(25)")
                .Not.Nullable();
            Map(x => x.InsertDateTime)
                .Column("INSERT_DATETIME")
                .Default("GETDATE()")
                .Not.Nullable();

            References(x => x.ServiceCall).Column("SERVICECALL_ID").Unique().Not.Nullable();
        }
    }
}
