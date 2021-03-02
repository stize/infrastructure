using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.Testing;
using Pulumi.AzureNextGen.Storage.Latest;
using Pulumi.AzureNextGen.Storage.Latest.Outputs;
using Stize.Infrastructure.Azure.Tests.Storage.Stacks;
using Stize.Infrastructure.Test;
using Xunit;
using Pulumi.Random;

namespace Stize.Infrastructure.Azure.Tests.Storage
{
    public class StorageTests
    {
        [Fact]
        public async Task CreateBasicStorageAccount()
        {
            var resources = await Pulumi.Deployment.TestAsync<BasicStorageStack>(new BasicStorageMock(), new TestOptions { IsPreview = false });
            var storage = resources.OfType<StorageAccount>().FirstOrDefault();

            storage.Should().NotBeNull("an storage account should be created");
            storage.Name.OutputShould().Be("account1");
            storage.Location.OutputShould().Be("westeurope");
            storage.Kind.OutputShould().Be("StorageV2");
            var sku = await storage.Sku.GetValueAsync();
            sku.Name.Should().Be("Standard_LRS");
        }


        [Fact]
        public async Task RandomIdNamedStorageAccount()
        {
            var resources = await Pulumi.Deployment.TestAsync<RandomIdStorageStack>(new RandomIdStorageMock(), new TestOptions { IsPreview = false });
            var storage = resources.OfType<StorageAccount>().FirstOrDefault();

            var rid = resources.OfType<RandomId>().FirstOrDefault();
            rid.Should().NotBeNull("a RandomId should be created");
            var ridHexValue = await rid.Hex.GetValueAsync();

            storage.Should().NotBeNull("an storage account should be created");
            storage.Name.OutputShould().Be($"account1-{ridHexValue}");
        }
    }
}