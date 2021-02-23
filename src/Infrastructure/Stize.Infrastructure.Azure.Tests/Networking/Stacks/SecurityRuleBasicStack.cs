using System;
using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Networking;

namespace Stize.Infrastructure.Tests.Azure.Networking.Stacks
{
    public class SecurityRuleBasicStack : Stack
    {
        public SecurityRuleBasicStack()
        {

            var rg = new ResourceGroupBuilder("rg1")
            .Name("rg1")
            .Location("westeurope")
            .Build();

            var nsg = new NetworkSecurityGroupBuilder("nsg1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("nsg1")
                .Build();

            var sr1 = new SecurityRuleBuilder("sr1")
                .Name("sr1")
                .NsgName(nsg.GetResourceName())
                .Direction(SecurityRuleDirection.Inbound)
                .Access(SecurityRuleAccess.Allow)
                .Priority(100)
                .ResourceGroup(rg.Name)
                .SourcePortRange("*")
                .DestinationPortRange("*")
                .SourcePrefix("*")
                .DestinationPrefix("*")
                .Protocol(SecurityRuleProtocol.Asterisk)
                .Description("test")
                .Build();

        }
    }
}
