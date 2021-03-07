using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Pulumi.AzureNextGen.Resources.Latest;
using Inputs = Pulumi.AzureNextGen.Network.Latest.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class SecurityRuleExtensions
    {
        /// <summary>
        /// Sets the builder name. If the builder has an RandomId associated, 
        /// appends the hex value of the RandomId to the end of the name
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="name"><see cref="SecurityRule"/> name</param>
        /// <returns></returns>
        public static SecurityRuleBuilder Name(this SecurityRuleBuilder builder, Input<string> name)
        {
            builder.Arguments.SecurityRuleName = name;            
            return builder;
        }

        /// <summary>
        /// Sets the direction of traffic that the <see cref="SecurityRule"/> will affect
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="direction">The direction specifies if the rule will affect Inbound or Outbound traffic. Use <see cref="SecurityRuleDirection"/> struct.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder Direction(this SecurityRuleBuilder builder, InputUnion<string, SecurityRuleDirection> direction)
        {
            builder.Arguments.Direction = direction;
            return builder;
        }

        /// <summary>
        /// Sets the Source Port Range for the <see cref="SecurityRule"/>.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="portRange">Source Port Range. Provide a single port, such as 80; a port range, such as 1024-65535; or an asterisk (*) to indicate any port.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder SourcePortRange(this SecurityRuleBuilder builder, Input<string> portRange)
        {
            builder.Arguments.SourcePortRange = portRange;
            return builder;
        }

        /// <summary>
        /// Sets the Destination Port Range for the <see cref="SecurityRule"/>.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="portRange">Destination Port Range. Provide a single port, such as 80; a port range, such as 1024-65535; or an asterisk (*) to indicate any port.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder DestinationPortRange(this SecurityRuleBuilder builder, Input<string> portRange)
        {
            builder.Arguments.DestinationPortRange = portRange;
            return builder;
        }

        /// <summary>
        /// Sets the Source Port Ranges for the <see cref="SecurityRule"/>.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="portRanges">Source Port Ranges. Provide a single port, such as '80'; a port range, such as '1024-65535'; or a comma seperated list of ports and/or port ranges in a string, such as '22 ,80 , 1024-65535'.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder SourcePortRanges(this SecurityRuleBuilder builder, Input<string> portRanges)
        {
            string[] prs = portRanges.Apply(x => x.Split(',')).GetValueAsync().Result;
            for (int i = 0; i < prs.Length; i++)
            {
                builder.Arguments.SourcePortRanges.Add(prs[i].Trim());
            }
            return builder;
        }

        /// <summary>
        /// Sets the Destination Port Range for the <see cref="SecurityRule"/>.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="portRanges">Destination Port Range. Provide a single port, such as '80'; a port range, such as '1024-65535'; or a comma seperated list of ports and/or port ranges in a string, such as '22 ,80 , 1024-65535'.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder DestinationPortRanges(this SecurityRuleBuilder builder, Input<string> portRanges)
        {
            string[] prs = portRanges.Apply(x => x.Split(',')).GetValueAsync().Result;
            for (int i = 0; i < prs.Length; i++)
            {
                builder.Arguments.DestinationPortRanges.Add(prs[i].Trim());
            }
            return builder;
        }

        /// <summary>
        /// Sets the Priority on which the rule is processed.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="priority">Priority on which the rule is processed; the lower the number, the higher the priority. </param>
        /// <returns></returns>
        public static SecurityRuleBuilder Priority(this SecurityRuleBuilder builder, Input<int> priority)
        {
            builder.Arguments.Priority = priority;
            return builder;
        }

        /// <summary>
        /// Sets the Accessiblity option for the <see cref="SecurityRule"/>.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="access">The network traffic is allowed or denied. Use <see cref="SecurityRuleAccess"/> struct.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder Access(this SecurityRuleBuilder builder, InputUnion<string, SecurityRuleAccess> access)
        {
            builder.Arguments.Access = access;
            return builder;
        }

        /// <summary>
        /// Sets the Source address for the <see cref="SecurityRule"/>.
        /// This is required if <see cref="SourcePrefixes(SecurityRuleBuilder, Input{string})"/> is not specified. 
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="prefix">CIDR or source IP range or * to match any IP. Tags such as ‘VirtualNetwork’, ‘AzureLoadBalancer’ and ‘Internet’ can also be used.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder SourcePrefix(this SecurityRuleBuilder builder, Input<string> prefix)
        {
            builder.Arguments.SourceAddressPrefix = prefix;
            return builder;
        }

        /// <summary>
        /// Sets the Destination address for the <see cref="SecurityRule"/>.
        /// This is required if <see cref="DestinationPrefixes(SecurityRuleBuilder, Input{string})"/> is not specified.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="prefix">CIDR or destination IP range or * to match any IP. Tags such as ‘VirtualNetwork’, ‘AzureLoadBalancer’ and ‘Internet’ can also be used. 
        /// Besides, it also supports all available Service Tags like ‘Sql.WestEurope‘, ‘Storage.EastUS‘, etc. </param>
        /// <returns></returns>
        public static SecurityRuleBuilder DestinationPrefix(this SecurityRuleBuilder builder, Input<string> prefix)
        {
            builder.Arguments.DestinationAddressPrefix = prefix;
            return builder;
        }

        /// <summary>
        /// Sets the Source addresses for the security rule.
        /// This is required if <see cref="SourcePrefix(SecurityRuleBuilder, Input{string})"/> is not specified.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="prefixes">List of source address prefixes. Tags may not be used. </param>
        /// <returns></returns>
        public static SecurityRuleBuilder SourcePrefixes(this SecurityRuleBuilder builder, Input<string> prefixes)
        {
            string[] pfs = prefixes.Apply(x => x.Split(',')).GetValueAsync().Result;
            for (int i = 0; i < pfs.Length; i++)
            {
                builder.Arguments.DestinationAddressPrefixes.Add(pfs[i].Trim());
            }
            return builder;
        }

        /// <summary>
        /// Sets the Destination addresses for the <see cref="SecurityRule"/>.
        /// This is required if DestinationPrefix is not specified.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="prefixes">List of destination address prefixes. Tags may not be used.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder DestinationPrefixes(this SecurityRuleBuilder builder, Input<string> prefixes)
        {
            string[] pfs = prefixes.Apply(x => x.Split(',')).GetValueAsync().Result;
            for (int i = 0; i < pfs.Length; i++)
            {
                builder.Arguments.DestinationAddressPrefixes.Add(pfs[i].Trim());
            }
            return builder;
        }

        /// <summary>
        /// Sets the Source <see cref="ApplicationSecurityGroup"/> for the <see cref="SecurityRule"/>.
        /// Use comma seperated list for multiple ASGs
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="asg">Source <see cref="ApplicationSecurityGroup"/> IDs. Use comma seperated list for multiple ASGs.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder SourceASGs(this SecurityRuleBuilder builder, Input<string> asgIDs)
        {
            string[] asgIDStrings = asgIDs.Apply(x => x.Split(',')).GetValueAsync().Result;
            for (int i = 0; i < asgIDStrings.Length; i++)
            {
                builder.Arguments.SourceApplicationSecurityGroups.Add(new Inputs.ApplicationSecurityGroupArgs { Id = asgIDStrings[i].Trim() });
            }
            return builder;
        }

        /// <summary>
        /// Sets the Destination <see cref="ApplicationSecurityGroup"/> for the <see cref="SecurityRule"/>.
        /// Use comma seperated list for multiple ASGs
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="asg">Destination <see cref="ApplicationSecurityGroup"/> IDs. Use comma seperated list for multiple ASGs.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder DestinationASGs(this SecurityRuleBuilder builder, Input<string> asgIDs)
        {
            string[] asgIDStrings = asgIDs.Apply(x => x.Split(',')).GetValueAsync().Result;
            for (int i = 0; i < asgIDStrings.Length; i++)
            {
                builder.Arguments.DestinationApplicationSecurityGroups.Add(new Inputs.ApplicationSecurityGroupArgs { Id = asgIDStrings[i].Trim() });
            }
            return builder;
        }

        /// <summary>
        /// Sets the network protocol that this <see cref="SecurityRule"/> applies to.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="protocol">Network protocol that this rule applies to. Use <see cref="SecurityRuleProtocol"/> struct.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder Protocol(this SecurityRuleBuilder builder, InputUnion<string, SecurityRuleProtocol> protocol)
        {
            builder.Arguments.Protocol = protocol;
            return builder;
        }

        /// <summary>
        /// Sets the description of <see cref="SecurityRule"/>.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="desc">Description of rule.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder Description(this SecurityRuleBuilder builder, Input<string> desc)
        {
            builder.Arguments.Description = desc;
            return builder;
        }

        /// <summary>
        /// Sets the <see cref="Pulumi.AzureNextGen.Resources.Latest.ResourceGroup"/> the <see cref="SecurityRule"/> will be created on
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="resoureGroupName"><see cref="Pulumi.AzureNextGen.Resources.Latest.ResourceGroup"/> name</param>
        /// <returns></returns>
        public static SecurityRuleBuilder ResourceGroup(this SecurityRuleBuilder builder, Input<string> resoureGroupName)
        {
            builder.Arguments.ResourceGroupName = resoureGroupName;
            return builder;
        }

        /// <summary>
        /// Sets the <see cref="NetworkSecurityGroup"/> that the <see cref="SecurityRule"/> will be assigned to.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="nsg"><see cref="NetworkSecurityGroup"/> name</param>
        /// <returns></returns>
        public static SecurityRuleBuilder NsgName(this SecurityRuleBuilder builder, Input<string> nsg)
        {
            builder.Arguments.NetworkSecurityGroupName = nsg;
            return builder;
        }
    }
}
