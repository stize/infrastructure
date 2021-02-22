using System;
using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Networking;

namespace Stize.Infrastructure.Tests.Azure.Networking.Stacks
{
    public class NetworkSecurityGroupBasicStack : Stack
    {
        public NetworkSecurityGroupBasicStack()
        {

            var rg = new ResourceGroupBuilder("rg1")
            .Name("rg1")
            .Location("westeurope")
            .Build();

            var builder = new NetworkSecurityGroupBuilder("nsg1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("nsg1")
                .SecurityRule("DenyAllInBound", SecurityRuleDirection.Inbound, "*", "*", 100, SecurityRuleAccess.Deny, SecurityRuleProtocol.Asterisk)
                .Build();
        }
    }
}
