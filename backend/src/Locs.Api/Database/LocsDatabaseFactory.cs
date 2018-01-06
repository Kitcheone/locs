namespace Locs.Api.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Locs.Api.Database.Mappings;
    using Microsoft.Extensions.Configuration;
    using NPoco;
    using NPoco.FluentMappings;

    public static class LocsDatabaseFactory
    {
        public static DatabaseFactory DbFactory { get; set; }

        public static void Setup(IConfiguration configuration)
        {
            var fluentConfig = FluentMappingConfiguration.Configure(new LunchMapping(), new AttendeeMapping());

            DbFactory = DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() => new Database(configuration.GetConnectionString("LocsDatabase"), DatabaseType.SqlServer2012, SqlClientFactory.Instance));
                x.WithFluentConfig(fluentConfig);
            });
        }
    }
}
