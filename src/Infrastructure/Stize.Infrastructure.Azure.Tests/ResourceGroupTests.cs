using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.AzureNextGen.Resources.Latest;
using Pulumi.Random;
using Pulumi.Testing;
using Stize.Infrastructure.Azure.Tests.Azure;
using Stize.Infrastructure.Azure.Tests.Stacks;
using Stize.Infrastructure.Test;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure
{
    public class ResourceGroupTests
    {
        [Fact]
        public async Task CreateBasicResourceGroup()
        {
            var resources = await Pulumi.Deployment.TestAsync<BasicResourceGroupStack>(new BasicResourceGroupMock(), new TestOptions {IsPreview = false});
            var rg = resources.OfType<ResourceGroup>().FirstOrDefault();

            rg.Should().NotBeNull("Resource group not found");
            rg.Name.OutputShould().Be("rg1");
            rg.Location.OutputShould().Be("westeurope");
        }

        [Fact]
        public async Task LocationIsCorrect()
        {        
            var resources = await Pulumi.Deployment.TestAsync<BasicResourceGroupStack>(new BasicResourceGroupMock(), new TestOptions {IsPreview = false});
            var rg = resources.OfType<ResourceGroup>().FirstOrDefault();

            rg.Should().NotBeNull("Resource group not found");
            rg.Location.OutputShould().Be("westeurope");
        }        

        [Fact]
        public async Task RandomIdNamesIsGeneratedProperly()
        {
            var resources = await Pulumi.Deployment.TestAsync<RandomIdNameStack>(new RandomIdNameMock(), new TestOptions {IsPreview = false});
            var rid = resources.OfType<RandomId>().FirstOrDefault();
            rid.Should().NotBeNull("RandomId is not present");

            var ridHexValue = await rid.Hex.GetValueAsync();

            var rg = resources.OfType<ResourceGroup>().FirstOrDefault();
            var tags = await rg.Tags.GetValueAsync();
            tags.Should().NotBeNull("The resource group has no tags");            
            tags.Should().HaveCount(2, "Not all tags are present");

            tags.Should().ContainKey("env");
            tags.Should().ContainValue("dev");
            tags.Should().ContainKeys("uid");
            tags.Should().ContainValue(ridHexValue);

            rg.Name.OutputShould().StartWith("rg1");
            rg.Name.OutputShould().NotBeEquivalentTo("rg1");
            rg.Name.OutputShould().Be($"rg1-{ridHexValue}");
        }
    }
}