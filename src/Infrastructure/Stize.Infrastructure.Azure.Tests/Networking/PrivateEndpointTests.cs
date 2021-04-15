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
        }

        /// <summary>
        /// Checks the location for the resource is assigned correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task LocationIsCorrect()
        {
            var resources = await Deployment.TestAsync<PrivateEndpointBasicStack>(new PrivateEndpointBasicMock(), new TestOptions { IsPreview = false });
            var endpoint = resources.OfType<PrivateEndpoint>().FirstOrDefault();
            (await endpoint.Location.GetValueAsync()).Should().Be("uksouth");
        }
    }
}
