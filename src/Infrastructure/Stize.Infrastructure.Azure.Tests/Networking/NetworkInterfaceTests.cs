using System.Linq;
using FluentAssertions;
using Pulumi.AzureNextGen.Network.Latest;
using Stize.Infrastructure.Tests.Azure.Networking.Stacks;
using Xunit;
using Stize.Infrastructure.Test;
using System.Threading.Tasks;
using Stize.Infrastructure.Azure.Tests.Networking.Stacks;
using Pulumi.Testing;
using Pulumi;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class NetworkInterfaceTests
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateBasicNIC()
        {
            var resources = await Deployment.TestAsync<NetworkInterfaceBasicStack>(new NetworkInterfaceBasicMock(), new TestOptions { IsPreview = false });
            var nic = resources.OfType<NetworkInterface>().FirstOrDefault();
            
            nic.Should().NotBeNull("Network Interface not found");
            nic.Name.OutputShould().Be("nic1");
        }
        /// <summary>
        /// Checks the Location of the NIC is correct
        /// </summary>
        /// <returns></returns>
        [Fact]        
        public async Task LocationIsCorrect()
        {
            var resources = await Deployment.TestAsync<NetworkInterfaceBasicStack>(new NetworkInterfaceBasicMock(), new TestOptions { IsPreview = false });
            var nic = resources.OfType<NetworkInterface>().LastOrDefault();
            (await nic.Location.GetValueAsync()).Should().Be("westeurope");
        }

        /// <summary>
        /// Checks the Dns Settings for the NIC by checking if the Dns settings are null.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DnsSettingsIsCorrect()
        {
            var resources = await Deployment.TestAsync<NetworkInterfaceBasicStack>(new NetworkInterfaceBasicMock(), new TestOptions { IsPreview = false });
            var nic = resources.OfType<NetworkInterface>().LastOrDefault();
            var t = await nic.DnsSettings.GetValueAsync();
            t.Should().NotBeNull();            
        }
        /// <summary>
        /// Checks if the Accelerated Networking for the NIC is enabled or disabled.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AcceleratedNetworkingIsCorrect()
        {
            var resources = await Deployment.TestAsync<NetworkInterfaceBasicStack>(new NetworkInterfaceBasicMock(), new TestOptions { IsPreview = false });
            var nic = resources.OfType<NetworkInterface>().LastOrDefault();
            (await nic.EnableAcceleratedNetworking.GetValueAsync()).Should().Be(true);
        }
        /// <summary>
        /// Checks if the IP Forwarding for the NIC is enabled or disabled.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IPForwardingIsCorrect()
        {
            var resources = await Deployment.TestAsync<NetworkInterfaceBasicStack>(new NetworkInterfaceBasicMock(), new TestOptions { IsPreview = false });
            var nic = resources.OfType<NetworkInterface>().LastOrDefault();
            (await nic.EnableIPForwarding.GetValueAsync()).Should().Be(true);
        }
        /// <summary>
        /// Checks the extended location of the NIC by checking if the output is null and checking the name of the Extended Location
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ExtendedLocationIsCorrect()
        {
            var resources = await Deployment.TestAsync<NetworkInterfaceBasicStack>(new NetworkInterfaceBasicMock(), new TestOptions { IsPreview = false });
            var nic = resources.OfType<NetworkInterface>().LastOrDefault();
            (await nic.ExtendedLocation.GetValueAsync()).Should().NotBeNull();
        }
        /// <summary>
        /// Checks the IP Configs of the NIC by checking if the subnet is null and checking the name of the IP Config
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IPConfigIsCorrect()
        {
            var resources = await Deployment.TestAsync<NetworkInterfaceBasicStack>(new NetworkInterfaceBasicMock(), new TestOptions { IsPreview = false });
            var nic = resources.OfType<NetworkInterface>().LastOrDefault();
            var subnet = resources.OfType<Subnet>().LastOrDefault();
            var t = await nic.IpConfigurations.GetValueAsync();
            var id = await subnet.Id.GetValueAsync();
            t[0].Subnet.Id.Should().Be(id);
        }
    }
}
