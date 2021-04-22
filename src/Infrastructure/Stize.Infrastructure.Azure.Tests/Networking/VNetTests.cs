
using System;
using System.Linq;
using FluentAssertions;
using Pulumi;
using Pulumi.AzureNative.Network;
using Pulumi.Testing;
using Stize.Infrastructure.Tests.Azure.Networking.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class VNetTests
    {
        [Fact]
        public async System.Threading.Tasks.Task CreateBasicVnet()
        {
            
            var resources = await Deployment.TestAsync<VNetBasicStack>(new VNetBasicMock(), new TestOptions { IsPreview = false });
            var vnet = resources.OfType<VirtualNetwork>().FirstOrDefault();

            vnet.Should().NotBeNull("Virtual Network not found");
            vnet.Name.Apply(x => x.Should().Be("vnet1"));
            vnet.Location.Apply(x => x.Should().Be("westeurope"));
        }


    }
}
