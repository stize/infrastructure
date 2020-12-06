
using System;
using System.Linq;
using FluentAssertions;
using Pulumi.Azure.Core;
using Stize.Infrastructure.Tests.Azure.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure
{
    public class ResourceGroupTests
    {
        [Fact]
        public async System.Threading.Tasks.Task CreateBasicResourceGroup()
        {
            
            var resources = await Testing.RunAsync<BasicResourceGroupStack>();
            var rg = resources.OfType<ResourceGroup>().FirstOrDefault();

            rg.Should().NotBeNull("Virtual Network not found");
            rg.Name.Apply(x => x.Should().Be("rg1"));
            rg.Location.Apply(x => x.Should().Be("westeurope"));
        }
    }
}
