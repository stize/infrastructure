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
            nic.Name.Apply(x =>x.Should().Be("ni1"));
            nic.Location.Apply(x => x.Should().Be("westeurope"));
            nic.IpConfigurations.Apply(x => x[0].Name.Should().Be("ipconfig1"));
            nic.IpConfigurations.Apply(x => x[0].PrivateIPAddressVersion.Should().Be("IPv4"));
            nic.IpConfigurations.Apply(x => x[0].PrivateIPAllocationMethod.Should().Be("Dynamic"));
            nic.IpConfigurations.Apply(x => x[0].Subnet.Should().NotBeNull());
        }
    }
}
