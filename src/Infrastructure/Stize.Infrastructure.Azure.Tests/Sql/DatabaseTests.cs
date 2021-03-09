using System;
using System.Linq;
using FluentAssertions;
using Pulumi.AzureNative.Sql;
using Stize.Infrastructure.Tests.Azure.Sql.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Sql
{
    public class DatabaseTests
    {        

        [Fact]
        public async System.Threading.Tasks.Task CreateBasicDatabase()
        {
            var resources = await Stize.Infrastructure.Test.Testing.RunAsync<DatabaseBasicStack>();
            var server= resources.OfType<Server>().FirstOrDefault();
            server.Should().NotBeNull("SQL Server not found");
            var db = resources.OfType<Database>().FirstOrDefault();
            db.Should().NotBeNull("Database not found");
        }
    }
}