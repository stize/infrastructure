using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.AzureNextGen.Network.Latest;
using Pulumi.Testing;
using Inputs = Pulumi.AzureNextGen.Network.Latest.Inputs;
using Stize.Infrastructure.Test;
using Stize.Infrastructure.Tests.Azure.Networking.Stacks;
using Xunit;
using Pulumi;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class SecurityRuleTests
    {

        [Fact]
        public async Task CreateSecurityRules()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().ToArray();

            sr.Should().NotBeNull("Security Rules not found");
            sr.Length.Should().Be(2);
        }
        [Fact]
        public async Task NameIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.Name.GetValueAsync()).Should().Be("sr1");
        }
        [Fact]
        public async Task AccessIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.Access.GetValueAsync()).Should().Be("Allow");
        }
        [Fact]
        public async Task DirectionIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.Direction.GetValueAsync()).Should().Be("Inbound");
        }
        [Fact]
        public async Task PriorityIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.Priority.GetValueAsync()).Should().Be(100);
        }
        [Fact]
        public async Task SourcePortRangeIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.SourcePortRange.GetValueAsync()).Should().Be("*");
        }
        [Fact]
        public async Task SourcePortRangesIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().LastOrDefault();
            string[] aps = new string[] { "22", "80-1024" };
            var t = await sr.SourcePortRanges.GetValueAsync();
            if (aps.Length > 0)
            {
                for (int i = 0; i < aps.Length; i++)
                {
                    t.Should().Contain(aps[i].Trim());
                }
            }
        }
        [Fact]
        public async Task DestinationPortRangeIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.DestinationPortRange.GetValueAsync()).Should().Be("*");
        }
        [Fact]
        public async Task DestinationPortRangesIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().LastOrDefault();
            string[] aps = new string[] { "22", "80-1024" };
            var t = await sr.DestinationPortRanges.GetValueAsync();
            if (aps.Length > 0)
            {
                for (int i = 0; i < aps.Length; i++)
                {
                    t.Should().Contain(aps[i].Trim());
                }
            }
        }
        [Fact]
        public async Task SourceAddressPrefixIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.SourceAddressPrefix.GetValueAsync()).Should().Be("*");
        }
        [Fact]
        public async Task SourceAddressPrefixesIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().LastOrDefault();
            string[] aps = new string[] { };
            var t = await sr.SourceAddressPrefixes.GetValueAsync();
            if (aps.Length > 0)
            {
                for (int i = 0; i < aps.Length; i++)
                {
                    t.Should().Contain(aps[i].Trim());
                }
            }
        }
        [Fact]
        public async Task DestinationAddressPrefixIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.DestinationAddressPrefix.GetValueAsync()).Should().Be("*");
        }
        [Fact]
        public async Task DestinationAddressPrefixesIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().LastOrDefault();
            string[] aps = new string[] { };
            var t = await sr.DestinationAddressPrefixes.GetValueAsync();
            if (aps.Length > 0)
            {
                for (int i = 0; i < aps.Length; i++)
                {
                    t.Should().Contain(aps[i].Trim());
                }
            }            
        }
        [Fact]
        public async Task SourceApplicationSecurityGroupsIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().LastOrDefault();
            var asg = resources.OfType<ApplicationSecurityGroup>().ToArray();
            var t = await sr.SourceApplicationSecurityGroups.GetValueAsync();

            for (int i = 0; i < t.Length; i++)
            {
                var id = await asg[i].Id.GetValueAsync();
                t[i].Id.Should().Be(id);
            }
        }
        [Fact]
        public async Task DestinationApplicationSecurityGroupsIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().LastOrDefault();
            var asg = resources.OfType<ApplicationSecurityGroup>().ToArray();
            var t = await sr.DestinationApplicationSecurityGroups.GetValueAsync();

            for (int i = 0; i < t.Length; i++)
            {
                var id = await asg[i].Id.GetValueAsync();
                t[i].Id.Should().Be(id);
            }
        }
        [Fact]
        public async Task ProtocolIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.Protocol.GetValueAsync()).Should().Be("*");
        }
        [Fact]
        public async Task DescriptionIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.Description.GetValueAsync()).Should().Be("test");
        }
    }
}
