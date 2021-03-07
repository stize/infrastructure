using FluentAssertions;
using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Pulumi.Testing;
using Stize.Infrastructure.Tests.Azure.Networking.Stacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class ApplicationSecurityGroupTests
    {
        /// <summary>
        /// Checks that the resource is created correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async System.Threading.Tasks.Task CreateBasicApplicationSecurityGroup()
        {

            var resources = await Deployment.TestAsync<ApplicationSecurityGroupBasicStack>(new ApplicationSecurityGroupBasicMock(), new TestOptions { IsPreview = false });
            var asg = resources.OfType<ApplicationSecurityGroup>().FirstOrDefault();

            asg.Should().NotBeNull("ASG not found");
            asg.Name.Apply(x => x.Should().Be("asg1"));
        }
        /// <summary>
        /// Checks the location for the resource is assigned correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task LocationIsCorrect()
        {
            var resources = await Deployment.TestAsync<ApplicationSecurityGroupBasicStack>(new ApplicationSecurityGroupBasicMock(), new TestOptions { IsPreview = false });
            var asg = resources.OfType<ApplicationSecurityGroup>().FirstOrDefault();
            (await asg.Location.GetValueAsync()).Should().Be("westeurope");
        }
        /// <summary>
        /// Checks the tags for the resource have been assigned correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TagsIsCorrect()
        {
            var resources = await Deployment.TestAsync<ApplicationSecurityGroupBasicStack>(new ApplicationSecurityGroupBasicMock(), new TestOptions { IsPreview = false });
            var asg = resources.OfType<ApplicationSecurityGroup>().FirstOrDefault();
            var testTags = new Dictionary<string, string>() { { "env", "dev" } };
            var tags = await asg.Tags.GetValueAsync();
            foreach (var tag in testTags)
            {
                tags.ContainsKey(tag.Key);
                tags.ContainsValue(tag.Value);
            }
        }
    }
}
