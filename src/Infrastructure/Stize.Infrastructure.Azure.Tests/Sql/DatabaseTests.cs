using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.AzureNative.Sql;
using Pulumi.Testing;
using Stize.Infrastructure.Tests.Azure.Sql.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Sql
{
    public class DatabaseTests
    {        

        [Fact]
        public async Task CreateBasicDatabase()
        {
            var resources = await Pulumi.Deployment.TestAsync<DatabaseBasicStack>(new DatabaseBasicMock(), new TestOptions { IsPreview = false });
            var server = resources.OfType<Database>().FirstOrDefault();
            server.Should().NotBeNull("SQL Server not found");
            var db = resources.OfType<Database>().FirstOrDefault();
            db.Should().NotBeNull("Database not found");
        }

        [Fact]
        public async Task ElasticPoolIdIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<DatabaseBasicStack>(new DatabaseBasicMock(), new TestOptions { IsPreview = false });
            var db = resources.OfType<Database>().FirstOrDefault();
            string? elasticPoolId = null;
            (await db.ElasticPoolId.GetValueAsync()).Should().Be(elasticPoolId);
        }

        [Fact]
        public async Task ReadScaleIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<DatabaseBasicStack>(new DatabaseBasicMock(), new TestOptions { IsPreview = false });
            var db = resources.OfType<Database>().FirstOrDefault();
            string? readScale = null;
            (await db.ReadScale.GetValueAsync()).Should().Be(readScale);
        }

        [Fact]
        public async Task SkuNameIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<DatabaseBasicStack>(new DatabaseBasicMock(), new TestOptions { IsPreview = false });
            var db = resources.OfType<Database>().FirstOrDefault();
            (await db.Sku.GetValueAsync())?.Name.Should().Be("S0");
        }

        [Fact]
        public async Task SkuTierIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<DatabaseBasicStack>(new DatabaseBasicMock(), new TestOptions { IsPreview = false });
            var db = resources.OfType<Database>().FirstOrDefault();
            (await db.Sku.GetValueAsync())?.Tier.Should().Be("Basic");
        }

        [Fact]
        public async Task SkuCapacityIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<DatabaseBasicStack>(new DatabaseBasicMock(), new TestOptions { IsPreview = false });
            var db = resources.OfType<Database>().FirstOrDefault();
            int? capacity = null;
            (await db.Sku.GetValueAsync())?.Capacity.Should().Be(capacity);
        }

        [Fact]
        public async Task SkuSizeIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<DatabaseBasicStack>(new DatabaseBasicMock(), new TestOptions { IsPreview = false });
            var db = resources.OfType<Database>().FirstOrDefault();
            string? size = null;
            (await db.Sku.GetValueAsync())?.Size.Should().Be(size);
        }

        [Fact]
        public async Task SkuFamilyIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<DatabaseBasicStack>(new DatabaseBasicMock(), new TestOptions { IsPreview = false });
            var db = resources.OfType<Database>().FirstOrDefault();
            string? family = null;
            (await db.Sku.GetValueAsync())?.Family.Should().Be(family);
        }

    }
}