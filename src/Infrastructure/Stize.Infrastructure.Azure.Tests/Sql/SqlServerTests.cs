using System;
using System.Linq;
using FluentAssertions;
using Pulumi.AzureNextGen.Sql.Latest;
using Stize.Infrastructure.Test;
using Stize.Infrastructure.Tests.Azure.Sql.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Sql
{
    public class SqlServerTests
    {        

        [Fact]
        public async System.Threading.Tasks.Task CreateBasicServer()
        {
            var resources = await Testing.RunAsync<SqlServerBasicStack>();
            var server= resources.OfType<Server>().FirstOrDefault();
            server.Should().NotBeNull("SQL Server not found");
        }
    }
}