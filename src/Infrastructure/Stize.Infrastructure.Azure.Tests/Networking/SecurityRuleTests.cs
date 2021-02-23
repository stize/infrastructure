using System;
using System.Linq;
using FluentAssertions;
using Pulumi.AzureNextGen.Network.Latest;
using Stize.Infrastructure.Tests.Azure.Networking.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class SecurityRuleTests
    {
        [Fact]
        public async System.Threading.Tasks.Task CreateBasicSecurityRule()
        {

            var resources = await Testing.RunAsync<SecurityRuleBasicStack>();
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();

            sr.Should().NotBeNull("Security Rule not found");
            sr.Name.Apply(x => x.Should().Be("sr1"));
            sr.Access.Apply(x => x.Should().Be("Allow"));
            sr.Direction.Apply(x => x.Should().Be("Inbound"));
            sr.Priority.Apply(x => x.Should().Be(100));
            sr.SourcePortRange.Apply(x => x.Should().Be("*"));
            sr.DestinationPortRange.Apply(x => x.Should().Be("*"));
            sr.SourceAddressPrefix.Apply(x => x.Should().Be("*"));
            sr.DestinationAddressPrefix.Apply(x => x.Should().Be("*"));
            sr.Protocol.Apply(x => x.Should().Be("*"));
            sr.Description.Apply(x => x.Should().Be("test"));
            
        }
    }
}
