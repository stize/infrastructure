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
            var asg = new ApplicationSecurityGroupBuilder("asg1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("asg1")
                .Build();
            var sr1 = new SecurityRuleBuilder("sr1")
                .Name("sr1")
                .NsgName(nsg.Name)
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

            var sr2 = new SecurityRuleBuilder("sr2")
                .Name("sr2")
                .NsgName(nsg.Name)
                .Direction(SecurityRuleDirection.Outbound)
                .Access(SecurityRuleAccess.Deny)
                .Priority(101)
                .ResourceGroup(rg.Name)
                .SourcePortRanges("22, 80-1024")
                .DestinationPortRanges("22, 80-1024")
                .SourcePrefix("*")
                .DestinationPrefix("*")
                .Protocol(SecurityRuleProtocol.Tcp)
                .Description("test2")
                .SourceASGs(asg.Id)
                .DestinationASGs(asg.Id)
                .Build();

        }
    }
}
