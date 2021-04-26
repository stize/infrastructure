using System;
using Pulumi;
using Pulumi.AzureNative.Network;
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
            var asg1 = new ApplicationSecurityGroupBuilder("asg1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("asg1")
                .Build();
            var asg2 = new ApplicationSecurityGroupBuilder("asg2")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("asg2")
                .Build();
            var sr1 = new SecurityRuleBuilder("sr1")
                .Name("sr1")
                .NsgName(nsg.Name)
                .Direction(SecurityRuleDirection.Inbound)
                .Access(SecurityRuleAccess.Allow)
                .ResourceGroup(rg.Name)
                .AnySourcePorts()
                .AnyDestinationPorts()
                .AnySourceTraffic()
                .AnyDestinationTraffic()
                .Protocol(SecurityRuleProtocol.Asterisk)
                .Build();

            var sr2 = new SecurityRuleBuilder("sr2")
                .Name("sr2")
                .NsgName(nsg.Name)
                .Direction(SecurityRuleDirection.Outbound)
                .Access(SecurityRuleAccess.Deny)
                .Priority(101)
                .ResourceGroup(rg.Name)
                .AnySourcePorts()
                .AnyDestinationPorts()
                .Protocol(SecurityRuleProtocol.Tcp)
                .Description("test2")
                .SourceIPAddresses("172.68.0.0/28", "172.68.0.16/28")
                .DestinationIPAddresses("172.68.0.0/28", "172.68.0.16/28")
                .Build();

            var sr3 = new SecurityRuleBuilder("sr3")
                .Name("sr3")
                .NsgName(nsg.Name)
                .Direction(SecurityRuleDirection.Outbound)
                .Access(SecurityRuleAccess.Deny)
                .Priority(101)
                .ResourceGroup(rg.Name)
                .SourcePortRanges("22", "80-1024")
                .DestinationPortRanges("22","80-1024")
                .Protocol(SecurityRuleProtocol.Tcp)
                .Description("test3")
                .SourceASGs(asg1.Id)
                .DestinationASGs(asg2.Id)
                .Build();



        }
    }
}
