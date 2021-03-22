using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.Testing;
using Xunit;
using Pulumi;
using Stize.Infrastructure.Azure.Tests.Networking.Stacks;
using Pulumi.AzureNextGen.Network.Latest;
using Stize.Infrastructure.Test;

namespace Stize.Infrastructure.Azure.Tests.Networking
{
    public class PrivateDnsZoneVnetLinkTests
    {
        /// <summary>
        /// Checks that the resource is created correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateBasicPrivateDnsZoneVnetLink()
        {
            var resources = await Deployment.TestAsync<PrivateDnsZoneVnetLinkBasicStack>(new PrivateDnsZoneVnetLinkBasicMock(), new TestOptions { IsPreview = false });
            var link = resources.OfType<VirtualNetworkLink>().FirstOrDefault();

            link.Should().NotBeNull("A private DNS zone vnet link should be created");
            link.Name.OutputShould().Be("link1");
        }

        /// <summary>
        /// Checks the location for the resource is assigned correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task LocationIsCorrect()
        {
            var resources = await Deployment.TestAsync<PrivateDnsZoneVnetLinkBasicStack>(new PrivateDnsZoneVnetLinkBasicMock(), new TestOptions { IsPreview = false });
            var link = resources.OfType<VirtualNetworkLink>().FirstOrDefault();
            (await link.Location.GetValueAsync()).Should().Be("global");
        }


        /// <summary>
        /// Checks the location for the resource is assigned correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RegistrationIsEnabled()
        {
            var resources = await Deployment.TestAsync<PrivateDnsZoneVnetLinkBasicStack>(new PrivateDnsZoneVnetLinkBasicMock(), new TestOptions { IsPreview = false });
            var link = resources.OfType<VirtualNetworkLink>().FirstOrDefault();
            (await link.RegistrationEnabled.GetValueAsync()).Should().Be(true);
        }
    }
}
