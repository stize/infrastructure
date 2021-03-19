using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.AzureNative.Sql;
using Pulumi.Testing;
using Stize.Infrastructure.Test;
using Stize.Infrastructure.Tests.Azure.Sql.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Sql
{
    public class SqlServerTests
    {        

        [Fact]
        public async Task CreateBasicServer()
        {
            var resources = await Pulumi.Deployment.TestAsync<SqlServerBasicStack>(new SqlServerBasicMock(), new TestOptions { IsPreview = false });
            var server = resources.OfType<Server>().FirstOrDefault();
            server.Should().NotBeNull("SQL Server not found");
            
        }

        [Fact]
        public async Task AdminLoginIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<SqlServerBasicStack>(new SqlServerBasicMock(), new TestOptions { IsPreview = false });
            var server = resources.OfType<Server>().FirstOrDefault();
            (await server.AdministratorLogin.GetValueAsync()).Should().Be("admin");
            (await server.AdministratorLoginPassword.GetValueAsync()).Should().Be("stize");
        }
    }
}