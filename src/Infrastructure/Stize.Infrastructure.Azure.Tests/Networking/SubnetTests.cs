﻿using System;
using System.Linq;
using FluentAssertions;
using Pulumi;
using Pulumi.AzureNative.Network;
using Pulumi.Testing;
using Stize.Infrastructure.Tests.Azure.Networking.Stacks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.Networking
{
    public class SubnetTests
    {        

        [Fact]
        public async System.Threading.Tasks.Task CreateBasicSubnet()
        {
            var resources = await Deployment.TestAsync<SubnetBasicStack>(new SubnetBasicMock(), new TestOptions { IsPreview = false });
            var subnet = resources.OfType<Subnet>().FirstOrDefault();

            subnet.Should().NotBeNull("Subnet not found");
            subnet.Name.Apply(x => x.Should().Be("vnet1"));
            subnet.AddressPrefix.Apply(x => x.Should().Be("172.16.0.0/24"));
            subnet.ServiceEndpoints.Apply(x => x.Length.Should().Be(0));
        }


    }
}
