using Pulumi;
using Pulumi.AzureNative.Network;
using Pulumi.AzureNative.Resources;
using Inputs = Pulumi.AzureNative.Network.Inputs;

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
        /// Sets the Source Port Range for the <see cref="SecurityRule"/> to allow/deny all ports.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <returns></returns>
        public static SecurityRuleBuilder AnySourcePorts(this SecurityRuleBuilder builder)
        {
            builder.Arguments.SourcePortRange = "*";
            return builder;
        }

        /// <summary>
        /// Sets the Destination Port Range for the <see cref="SecurityRule"/> to allow/deny all ports.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <returns></returns>
        public static SecurityRuleBuilder AnyDestinationPorts(this SecurityRuleBuilder builder)
        {
            builder.Arguments.DestinationPortRange = "*";
            return builder;
        }

        /// <summary>
        /// Sets the Source Port Ranges for the <see cref="SecurityRule"/>.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="portRanges">Source Port Ranges. Provide a single port, such as '80'; a port range, such as '1024-65535'; or a multiple, using a comma-seperated list of arguments.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder SourcePortRanges(this SecurityRuleBuilder builder, params Input<string>[] portRanges)
        {
            foreach (var pr in portRanges)
            {
                builder.Arguments.SourcePortRanges.Add(pr);
            }
            return builder;
        }

        /// <summary>
        /// Sets the Destination Port Range for the <see cref="SecurityRule"/>.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="portRanges">Destination Port Range. Provide a single port, such as '80'; a port range, such as '1024-65535'; or multiple, using a comma seperated list of arguments.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder DestinationPortRanges(this SecurityRuleBuilder builder, params Input<string>[] portRanges)
        {
            foreach (var pr in portRanges)
            {
                builder.Arguments.DestinationPortRanges.Add(pr);
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
        /// Sets the source traffic for the security rule to 'Any', allowing/denying all source traffic.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static SecurityRuleBuilder AnySourceTraffic(this SecurityRuleBuilder builder)
        {
            builder.Arguments.SourceAddressPrefix = "*";
            return builder;
        }

        /// <summary>
        /// Sets the destination traffic for the security rule to 'Any', allowing/denying all destination traffic.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static SecurityRuleBuilder AnyDestinationTraffic(this SecurityRuleBuilder builder)
        {
            builder.Arguments.DestinationAddressPrefix = "*";
            return builder;
        }

        /// <summary>
        /// Sets the source service tag for the <see cref="SecurityRule"/>.
        /// This is required if <see cref="SourceIPAddresses(SecurityRuleBuilder, Input{string}[])"/> and <see cref="SourceASGs(SecurityRuleBuilder, Input{string}[])"/> are not specified. 
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="tag">Service Tag; i.e. 'VirtualNetwork', 'AzureLoadBalancer' and 'Internet' </param>
        /// <returns></returns>
        public static SecurityRuleBuilder SourceServiceTag(this SecurityRuleBuilder builder, Input<string> tag)
        {
            builder.Arguments.SourceAddressPrefix = tag;
            return builder;
        }

        /// <summary>
        /// Sets the Destination address for the <see cref="SecurityRule"/>.
        /// This is required if <see cref="DestinationIPAddresses(SecurityRuleBuilder, Input{string}[])"/> and <see cref="DestinationASGs(SecurityRuleBuilder, Input{string}[])"/> are not specified.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="tag">Service tags; i.e. 'VirtualNetwork', 'AzureLoadBalancer' and 'Internet' . 
        /// Also supports all available Service Tags like ‘Sql.WestEurope‘, ‘Storage.EastUS‘, etc. </param>
        /// <returns></returns>
        public static SecurityRuleBuilder DestinationServiceTag(this SecurityRuleBuilder builder, Input<string> tag)
        {
            builder.Arguments.DestinationAddressPrefix = tag;
            return builder;
        }

        /// <summary>
        /// Sets the Source addresses for the security rule.
        /// This is required if <see cref="SourceServiceTag(SecurityRuleBuilder, Input{string})"/> and <see cref="SourceASGs(SecurityRuleBuilder, Input{string}[])"/> are not specified.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="ipAddresses">List of source address prefixes. </param>
        /// <returns></returns>
        public static SecurityRuleBuilder SourceIPAddresses(this SecurityRuleBuilder builder, params Input<string>[] ipAddresses)
        {
            foreach (var ip in ipAddresses)
            {
                builder.Arguments.SourceAddressPrefixes.Add(ip);
            }
            return builder;
        }

        /// <summary>
        /// Sets the Destination addresses for the <see cref="SecurityRule"/>.
        /// This is required if DestinationPrefix is not specified.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="ipAddresses">List of destination address prefixes. </param>
        /// <returns></returns>
        public static SecurityRuleBuilder DestinationIPAddresses(this SecurityRuleBuilder builder, params Input<string>[] ipAddresses)
        {
            foreach (var ip in ipAddresses)
            {
                builder.Arguments.DestinationAddressPrefixes.Add(ip);
            }
            return builder;
        }

        /// <summary>
        /// Sets the Source <see cref="ApplicationSecurityGroup"/> for the <see cref="SecurityRule"/>.
        /// Use comma seperated list of arguments for multiple ASGs.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="asg">Source <see cref="ApplicationSecurityGroup"/> IDs. Use comma seperated list of arguments for multiple ASGs.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder SourceASGs(this SecurityRuleBuilder builder, params Input<string>[] asgIDs)
        {
            foreach (var id in asgIDs)
            {
                builder.Arguments.SourceApplicationSecurityGroups.Add(new Inputs.ApplicationSecurityGroupArgs { Id = id });
            }
            return builder;
        }

        /// <summary>
        /// Sets the Destination <see cref="ApplicationSecurityGroup"/> for the <see cref="SecurityRule"/>.
        /// Use comma seperated list of arguments for multiple ASGs.
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="asg">Destination <see cref="ApplicationSecurityGroup"/> IDs. Use comma seperated list of arguments for multiple ASGs.</param>
        /// <returns></returns>
        public static SecurityRuleBuilder DestinationASGs(this SecurityRuleBuilder builder, params Input<string>[] asgIDs)
        {
            foreach (var id in asgIDs)
            {
                builder.Arguments.DestinationApplicationSecurityGroups.Add(new Inputs.ApplicationSecurityGroupArgs { Id = id });
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
        /// Sets the <see cref="Pulumi.AzureNative.Resources.ResourceGroup"/> the <see cref="SecurityRule"/> will be created on
        /// </summary>
        /// <param name="builder"><see cref="SecurityRule"/> builder</param>
        /// <param name="resoureGroupName"><see cref="Pulumi.AzureNative.Resources.ResourceGroup"/> name</param>
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
