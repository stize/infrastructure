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
            (await server.AdministratorLogin.GetValueAsync()).Should().Be("stize");
        }

        [Fact]
        public async Task VersionIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<SqlServerBasicStack>(new SqlServerBasicMock(), new TestOptions { IsPreview = false });
            var server = resources.OfType<Server>().FirstOrDefault();
            (await server.Version.GetValueAsync()).Should().Be("12.0");
        }

        [Fact]
        public async Task IdentityType()
        {
            var resources = await Pulumi.Deployment.TestAsync<SqlServerBasicStack>(new SqlServerBasicMock(), new TestOptions { IsPreview = false });
            var server = resources.OfType<Server>().FirstOrDefault();
            (await server.Identity.GetValueAsync())?.Type.Should().Be("SystemAssigned");
        }
        
        [Fact]
        public async Task PublicNetworkAccessIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<SqlServerBasicStack>(new SqlServerBasicMock(), new TestOptions { IsPreview = false });
            var server = resources.OfType<Server>().FirstOrDefault();
            (await server.PublicNetworkAccess.GetValueAsync()).Should().Be("Enabled");
        }
        [Fact]
        public async Task TlsVersionIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<SqlServerBasicStack>(new SqlServerBasicMock(), new TestOptions { IsPreview = false });
            var server = resources.OfType<Server>().FirstOrDefault();
            (await server.MinimalTlsVersion.GetValueAsync()).Should().Be("1.0");
        }
    }
}