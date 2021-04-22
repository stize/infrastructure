using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.Testing;
using Xunit;
using Pulumi;
using Stize.Infrastructure.Azure.Tests.Networking.Stacks;
using Stize.Infrastructure.Test;
using Pulumi.AzureNative.Network;

namespace Stize.Infrastructure.Azure.Tests.Networking
{
    public class PrivateEndpointTests
    {
        /// <summary>
        /// Checks that the resource is created correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateBasicPrivateEndpoint()
        {
            var resources = await Deployment.TestAsync<PrivateEndpointBasicStack>(new PrivateEndpointBasicMock(), new TestOptions { IsPreview = false });
            var endpoint = resources.OfType<PrivateEndpoint>().FirstOrDefault();

            endpoint.Should().NotBeNull("A private endpoint should be created");
            endpoint.Name.OutputShould().Be("pe1");
            (await endpoint.Location.GetValueAsync()).Should().Be("uksouth");

        }

        [Fact]
        public async Task SubnetIsCorrect() 
        {
            var resources = await Deployment.TestAsync<PrivateEndpointBasicStack>(new PrivateEndpointBasicMock(), new TestOptions { IsPreview = false });
            var endpoint = resources.OfType<PrivateEndpoint>().FirstOrDefault();
            var subnet = resources.OfType<Subnet>().FirstOrDefault();
            var subId = await subnet.Id.GetValueAsync();
            (await endpoint.Subnet.GetValueAsync())?.Id.Should().Be(subId);
        }

        [Fact]
        public async Task PrivateLinkServiceConnectionIsCorrect()
        {
            var resources = await Deployment.TestAsync<PrivateEndpointBasicStack>(new PrivateEndpointBasicMock(), new TestOptions { IsPreview = false });
            var endpoint = resources.OfType<PrivateEndpoint>().FirstOrDefault();
            var sa = resources.OfType<Pulumi.AzureNative.Storage.StorageAccount>().FirstOrDefault();
            var saId = await sa.Id.GetValueAsync();
            (await endpoint.PrivateLinkServiceConnections.GetValueAsync())[0].PrivateLinkServiceId.Should().Be(saId);
            (await endpoint.PrivateLinkServiceConnections.GetValueAsync())[0].Name.Should().Be("pe-connection");
            (await endpoint.PrivateLinkServiceConnections.GetValueAsync())[0].GroupIds.Should().Contain("blob");
            (await endpoint.PrivateLinkServiceConnections.GetValueAsync())[0].RequestMessage.Should().Contain("test message");
        }
    }
}
