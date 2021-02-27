
using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.AzureNextGen.Resources.Latest;
using Stize.Infrastructure.Test;
using Stize.Infrastructure.Tests.Azure.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure
{
    public class ResourceGroupTests
    {
        [Fact(Skip = "Pulumi Name is being returned null during testing. Mocking issue?")]
        public async Task CreateBasicResourceGroup()
        {
            
            var resources = await Testing.RunAsync<BasicResourceGroupStack>();
            var rg = resources.OfType<ResourceGroup>().FirstOrDefault();

            rg.Should().NotBeNull("Resource group not found");
            rg.Name.OutputShould().Be("rg1");
            rg.Location.OutputShould().Be("westeurope");
        }

        [Fact]
        public async Task LocationIsCorrect()
        {        
            var resources = await Testing.RunAsync<BasicResourceGroupStack>();
            var rg = resources.OfType<ResourceGroup>().FirstOrDefault();

            rg.Should().NotBeNull("Resource group not found");
            rg.Location.OutputShould().Be("westeurope");
        }        

        [Fact(Skip = "Pulumi Name is being returned null during testing. Mocking issue?")]
        public async Task RandomIdNamesIsGeneratedProperly()
        {
            var resources = await Testing.RunAsync<RandomIdNameStack>();
            var rg = resources.OfType<ResourceGroup>().FirstOrDefault();            
            rg.Should().NotBeNull("Resource group not found");
            var name = await rg.Name.GetValueAsync();
            name.Should().StartWith("rg1");
            name.Should().NotBeEquivalentTo("rg1");
        }
    }
}
