using System;
using System.Linq;
using FluentAssertions;
using Pulumi.Azure.Network;
using Stize.Infrastructure.Tests.Azure.Networking.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class SubnetTests
    {        

        [Fact]
        public async System.Threading.Tasks.Task CreateBasicSubnet()
        {

            var resources = await Testing.RunAsync<SubnetBasicStack>();
            var subnet = resources.OfType<Subnet>().FirstOrDefault();

            subnet.Should().NotBeNull("Subnet not found");
            subnet.VirtualNetworkName.Apply(x => x.Should().Be("vnet1"));
            subnet.ResourceGroupName.Apply(x => x.Should().Be("my-resource-group"));
            subnet.AddressPrefix.Apply(x => x.Should().Be("172.16.0.0/24"));
            subnet.ServiceEndpoints.Apply(x => x.Length.Should().Be(0));
        }
    }
}
