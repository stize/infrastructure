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
        public async System.Threading.Tasks.Task CreateSecurityRules()
        {

            var resources = await Testing.RunAsync<SecurityRuleBasicStack>();
            var sr = resources.OfType<SecurityRule>().ToArray();

            sr[0].Should().NotBeNull("Security Rule not found");
            sr[0].Name.Apply(x => x.Should().Be("sr1"));
            sr[0].Access.Apply(x => x.Should().Be("Allow"));
            sr[0].Direction.Apply(x => x.Should().Be("Inbound"));
            sr[0].Priority.Apply(x => x.Should().Be(100));
            sr[0].SourcePortRange.Apply(x => x.Should().Be("*"));
            sr[0].DestinationPortRange.Apply(x => x.Should().Be("*"));
            sr[0].SourceAddressPrefix.Apply(x => x.Should().Be("*"));
            sr[0].DestinationAddressPrefix.Apply(x => x.Should().Be("*"));
            sr[0].Protocol.Apply(x => x.Should().Be("*"));
            sr[0].Description.Apply(x => x.Should().Be("test"));

            sr[1].Should().NotBeNull("Security Rule not found");
            sr[1].Name.Apply(x => x.Should().Be("sr2"));
            sr[1].Access.Apply(x => x.Should().Be("Deny"));
            sr[1].Direction.Apply(x => x.Should().Be("Outbound"));
            sr[1].Priority.Apply(x => x.Should().Be(101));
            sr[1].SourcePortRanges.Apply(x => x[0].Should().Be("22, 80-1024"));
            sr[1].DestinationPortRanges.Apply(x => x[0].Should().Be("22, 80-1024"));
            sr[1].SourceAddressPrefix.Apply(x => x.Should().Be("*"));
            sr[1].DestinationAddressPrefix.Apply(x => x.Should().Be("*"));
            sr[1].Protocol.Apply(x => x.Should().Be("Tcp"));
            sr[1].Description.Apply(x => x.Should().Be("test2"));
        }

    }
}
