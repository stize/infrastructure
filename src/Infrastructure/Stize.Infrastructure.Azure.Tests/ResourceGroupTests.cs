
using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.Azure.Core;
using Stize.Infrastructure.Tests.Azure.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure
{
    public class ResourceGroupTests
    {
        [Fact]
        public async Task CreateBasicResourceGroup()
        {
            
            var resources = await Testing.RunAsync<BasicResourceGroupStack>();
            var rg = resources.OfType<ResourceGroup>().FirstOrDefault();

            rg.Should().NotBeNull("Resource group not found");
            rg.Name.Apply(x => x.Should().Be("rg1"));
            rg.Location.Apply(x => x.Should().Be("westeurope"));
        }

        [Fact]
        public async Task RandomIdNamesIsGeneratedProperly()
        {
            var resources = await Testing.RunAsync<RandomIdNameStack>();
            var rg = resources.OfType<ResourceGroup>().FirstOrDefault();
            rg.Should().NotBeNull("Resource group not found");
            rg.Name.Apply(x => x.Should().StartWith("rg1"));
            rg.Name.Apply(x => x.Should().NotBeEquivalentTo("rg1"));
        }
    }
}
