using System;
using System.Linq;
using FluentAssertions;
using Pulumi.AzureNextGen.Network.Latest;
using Stize.Infrastructure.Tests.Azure.Networking.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class NetworkSecurityGroupTests
    {
        [Fact]
        public async System.Threading.Tasks.Task CreateBasicSecurityGroup()
        {

            var resources = await Testing.RunAsync<NetworkSecurityGroupBasicStack>();
            var nsg = resources.OfType<NetworkSecurityGroup>().FirstOrDefault();

            nsg.Should().NotBeNull("NSG not found");
            nsg.GetResourceName().Should().Be("nsg1");
            nsg.Location.Apply(x => x.Should().Be("westeurope"));
            
            
        }
    }
}
