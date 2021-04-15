using System.Linq;
using FluentAssertions;
using Pulumi.AzureNative.Network;
using Stize.Infrastructure.Test;
using Xunit;
using System.Threading.Tasks;
using Stize.Infrastructure.Azure.Tests.Networking.Stacks;
using Pulumi.Testing;
using Pulumi;
using System.Collections.Generic;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class NetworkSecurityGroupTests
    {
        /// <summary>
        /// Checks that the resource is created correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateBasicNetworkSecurityGroup()
        {
            var resources = await Deployment.TestAsync<NetworkSecurityGroupBasicStack>(new NetworkSecurityGroupBasicMock(), new TestOptions { IsPreview = false });
            var nsg = resources.OfType<NetworkSecurityGroup>().FirstOrDefault();

            nsg.Should().NotBeNull("NSG not found");
            nsg.Name.OutputShould().Be("nsg1");
        }
        /// <summary>
        /// Checks the location for the resource is assigned correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task LocationIsCorrect()
        {
            var resources = await Deployment.TestAsync<NetworkSecurityGroupBasicStack>(new NetworkSecurityGroupBasicMock(), new TestOptions { IsPreview = false });
            var nsg = resources.OfType<NetworkSecurityGroup>().FirstOrDefault();
            (await nsg.Location.GetValueAsync()).Should().Be("westeurope");
        }
        /// <summary>
        /// Checks the tags for the resource have been assigned correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TagsIsCorrect()
        {
            var resources = await Deployment.TestAsync<NetworkSecurityGroupBasicStack>(new NetworkSecurityGroupBasicMock(), new TestOptions { IsPreview = false });
            var nsg = resources.OfType<NetworkSecurityGroup>().FirstOrDefault();
            var testTags = new Dictionary<string, string>() { { "env", "dev"} };
            var tags = await nsg.Tags.GetValueAsync();
            tags?.Should().NotBeNull();

            foreach (var tag in testTags)
            {
                tags?.ContainsKey(tag.Key);
                tags?.ContainsValue(tag.Value);
            }
        }
    }
}
