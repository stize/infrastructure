using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.AzureNative.Network;
using Pulumi.Testing;
using Inputs = Pulumi.AzureNative.Network.Inputs;
using Stize.Infrastructure.Test;
using Stize.Infrastructure.Tests.Azure.Networking.Stacks;
using Xunit;
using Pulumi;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class SecurityRuleTests
    {
        /// <summary>
        /// Checks that the resource is created correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateSecurityRules()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().ToArray();

            sr.Should().NotBeNull("Security Rules not found");
            sr.Length.Should().Be(2);
        }
        /// <summary>
        /// Checks that the name of the resource is assigned correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task NameIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.Name.GetValueAsync()).Should().Be("sr1");
        }
        /// <summary>
        /// Checks the access string is correctly assigned
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AccessIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.Access.GetValueAsync()).Should().Be("Allow");
        }
        /// <summary>
        /// Checks the direction string is correctly assigned
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DirectionIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.Direction.GetValueAsync()).Should().Be("Inbound");
        }
        /// <summary>
        /// Checks the priority integer is correctly assigned
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PriorityIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().LastOrDefault();
            (await sr.Priority.GetValueAsync()).Should().Be(101);
        }
        /// <summary>
        /// Checks the SourcePortRange is correctly assigned
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SourcePortRangeIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.SourcePortRange.GetValueAsync()).Should().Be("*");
        }
        /// <summary>
        /// Checks the SourcePortRanges are correctly assigned
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Checks the DestinationPortRange is correctly assigned
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DestinationPortRangeIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.DestinationPortRange.GetValueAsync()).Should().Be("*");
        }
        /// <summary>
        /// Checks the DestinationPortRanges are correctly assigned
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DestinationPortRangesIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().LastOrDefault();
            (await sr.DestinationPortRanges.GetValueAsync()).Should().Contain("22", "80-1024");
        }
        /// <summary>
        /// Checks the SourceAddressPrefix is correctly assigned
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SourceAddressPrefixIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.SourceAddressPrefix.GetValueAsync()).Should().Be("*");
        }
        /// <summary>
        /// Checks the SourceAddressPrefixes are correctly assigned
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SourceAddressPrefixesIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().ToArray()[1];
            (await sr.SourceAddressPrefixes.GetValueAsync()).Should().Contain("172.68.0.0/28", "172.68.0.16/28");

        }
        /// <summary>
        /// Checks the DestinationAddressPrefix is correctly assigned
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DestinationAddressPrefixIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.DestinationAddressPrefix.GetValueAsync()).Should().Be("*");
        }
        /// <summary>
        /// Checks the DestinationAddressPrefixes are correctly assigned
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DestinationAddressPrefixesIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().ToArray()[1];
            (await sr.DestinationAddressPrefixes.GetValueAsync()).Should().Contain("172.68.0.0/28", "172.68.0.16/28");
        }
        /// <summary>
        /// Checks the first Source ASG in the list by using the properties from the last SR and the first ASG defined
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SourceApplicationSecurityGroupsIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().LastOrDefault();
            var asg = resources.OfType<ApplicationSecurityGroup>().FirstOrDefault();
            var t = await sr.SourceApplicationSecurityGroups.GetValueAsync();

            var id = await asg.Id.GetValueAsync();
            t[0].Id.Should().Be(id);
        }
        /// <summary>
        /// Checks the first Destination ASG in the list by using the properties from the last SR and the last ASG defined
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DestinationApplicationSecurityGroupsIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().LastOrDefault();
            var asg = resources.OfType<ApplicationSecurityGroup>().LastOrDefault();
            var t = await sr.DestinationApplicationSecurityGroups.GetValueAsync();
            var id = await asg.Id.GetValueAsync();
            t[0].Id.Should().Be(id);
        }
        /// <summary>
        /// Checks the protocol string is assigned correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ProtocolIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().FirstOrDefault();
            (await sr.Protocol.GetValueAsync()).Should().Be("*");
        }
        /// <summary>
        /// Checks the description string is assigned correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DescriptionIsCorrect()
        {
            var resources = await Deployment.TestAsync<SecurityRuleBasicStack>(new SecurityRuleBasicMock(), new TestOptions { IsPreview = false });
            var sr = resources.OfType<SecurityRule>().LastOrDefault();
            (await sr.Description.GetValueAsync()).Should().Be("test3");
        }
    }
}
