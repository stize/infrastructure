using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.AzureNative.Resources;
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
            rid.Should().NotBeNull("a RandomId should be created");

            var ridHexValue = await rid.Hex.GetValueAsync();
            var env = "dev";

            var rg = resources.OfType<ResourceGroup>().FirstOrDefault();
            var tags = await rg.Tags.GetValueAsync();
            tags.Should().NotBeNull("the resource should have tags");
            // Strategy tags
            tags.Should().ContainKey("environment");
            tags.Should().ContainValue(env);
            tags.Should().ContainKeys("instanceId");
            tags.Should().ContainValue(ridHexValue);
            tags.Should().ContainKeys("managedBy");
            tags.Should().ContainValue("Stize");
            tags.Should().ContainKeys("createdWith");
            tags.Should().ContainValue("pulumi");
            // Custom tags
            tags.Should().ContainKeys("my");
            tags.Should().ContainValue("tag");
            // Tag count validation            
            tags.Should().HaveCount(5, "all tags should be present");

            rg.Name.OutputShould().Be($"rg1-{env}-{ridHexValue}");
            rg.Location.OutputShould().Be("westeurope");
        }
    }
}