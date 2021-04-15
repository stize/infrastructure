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
            zone.Name.OutputShould().Be("zone1");
        }

        /// <summary>
        /// Checks the location for the resource is assigned correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task LocationIsCorrect()
        {
            var resources = await Deployment.TestAsync<PrivateDnsZoneBasicStack>(new PrivateDnsZoneBasicMock(), new TestOptions { IsPreview = false });
            var zone = resources.OfType<PrivateZone>().FirstOrDefault();
            (await zone.Location.GetValueAsync()).Should().Be("global");
        }
    }
}
