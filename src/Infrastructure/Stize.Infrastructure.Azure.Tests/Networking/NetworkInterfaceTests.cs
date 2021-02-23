using System.Linq;
using FluentAssertions;
using Pulumi.AzureNextGen.Network.Latest;
using Stize.Infrastructure.Tests.Azure.Networking.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class NetworkInterfaceTests
    {
        [Fact]
        public async System.Threading.Tasks.Task CreateBasicNIC()
        {
            var resources = await Testing.RunAsync<NetworkInterfaceBasicStack>();
            var nic = resources.OfType<NetworkInterface>().FirstOrDefault();
            
            nic.Should().NotBeNull("Network Interface not found");
            nic.GetResourceName().Should().Be("ni1");
            nic.Location.Apply(x => x.Should().Be("westeurope"));
            nic.IpConfigurations.Should().NotBeNull("No Ip Configs");
        }
    }
}
