using FluentAssertions;
using Pulumi.AzureNextGen.Network.Latest;
using Stize.Infrastructure.Tests.Azure.Networking.Stacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class ApplicationSecurityGroupTests
    {
        [Fact]
        public async System.Threading.Tasks.Task CreateBasicApplicationSecurityGroup()
        {

            var resources = await Testing.RunAsync<ApplicationSecurityGroupBasicStack>();
            var nsg = resources.OfType<ApplicationSecurityGroup>().FirstOrDefault();

            nsg.Should().NotBeNull("ASG not found");
            nsg.Name.Apply(x => x.Should().Be("asg1"));
            var x = nsg.Location.Apply(x => x.Should().Be("westeurope"));
        }
    }
}
