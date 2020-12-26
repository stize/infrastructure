using System;
using System.Linq;
using FluentAssertions;
using Pulumi.Azure.Sql;
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
            var server= resources.OfType<SqlServer>().FirstOrDefault();
            server.Should().NotBeNull("SQL Server not found");
        }
    }
}