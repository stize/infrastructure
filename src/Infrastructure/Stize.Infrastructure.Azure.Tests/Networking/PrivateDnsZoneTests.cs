using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.Testing;
using Xunit;
using Pulumi;
using Stize.Infrastructure.Azure.Tests.Networking.Stacks;
using Pulumi.AzureNative.Network;
using Stize.Infrastructure.Test;

namespace Stize.Infrastructure.Azure.Tests.Networking
{
    public class PrivateDnsZoneTests
    {
        /// <summary>
        /// Checks that the resource is created correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateBasicPrivateDnsZone()
        {
            var resources = await Deployment.TestAsync<PrivateDnsZoneBasicStack>(new PrivateDnsZoneBasicMock(), new TestOptions { IsPreview = false });
            var zone = resources.OfType<PrivateZone>().FirstOrDefault();

            zone.Should().NotBeNull("A private DNS zone should be created");
            zone.Name.OutputShould().Be("privatelink.blob.core.windows.net");
            (await zone.Location.GetValueAsync()).Should().Be("global");
        }

        [Fact]
        public async Task VnetLinksareCorrect()
        {
            var resources = await Deployment.TestAsync<PrivateDnsZoneBasicStack>(new PrivateDnsZoneBasicMock(), new TestOptions { IsPreview = false });
            var links = resources.OfType<VirtualNetworkLink>().ToArray();
            var vnetId = await resources.OfType<VirtualNetwork>().FirstOrDefault().Id.GetValueAsync();
            links[0].Name.OutputShould().Be("link1");
            (await links[0].Id.GetValueAsync()).Should().Be("link1_id");
            (await links[0].RegistrationEnabled.GetValueAsync()).Should().Be(true);
            (await links[0].VirtualNetwork.GetValueAsync())?.Id.Should().Be(vnetId);
        }
    }
}
