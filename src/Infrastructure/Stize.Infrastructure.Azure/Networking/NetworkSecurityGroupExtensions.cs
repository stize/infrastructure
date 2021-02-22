using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Inputs = Pulumi.AzureNextGen.Network.Latest.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class NetworkSecurityGroupExtensions
    {
        /// <summary>
        /// Adds a security rule to the Network Security Group with the specified parameters
        /// </summary>
        /// <param name="builder">NetworkSecurityGroup builder</param>
        /// <param name="name">Security Rule name</param>
        /// <param name="srcPortRanges">Source Port Ranges. Provide a single port, such as 80; a port range, such as 1024-65535; or a comma-seperated list of single ports and/or ranges.</param>
        /// <param name="dstPortRanges">Destination Port Ranges. Provide a single port, such as 80; a port range, such as 1024-65535; or a comma-seperated list of single ports and/or ranges.</param>
        /// <param name="priority">Priority on which the rule is processed; the lower the number, the higher the priority.</param>
        /// <returns></returns>
        public static NetworkSecurityGroupBuilder SecurityRule(this NetworkSecurityGroupBuilder builder, Input<string> name, InputUnion<string, SecurityRuleDirection> direction, Input<string> srcPortRanges,
            Input<string> dstPortRanges, Input<int> priority, InputUnion<string, SecurityRuleAccess> access, Input<string> srcServiceTag = null, Input<string> destServiceTag = null, 
            Input<string> srcIP = null, Input<string> destIP = null, Input<Inputs.ApplicationSecurityGroupArgs> srcASG = null, Input<Inputs.ApplicationSecurityGroupArgs> destASG = null, 
            InputUnion<string, SecurityRuleProtocol> protocol = null, Input<string> description = null)
        {
            var sr = new Inputs.SecurityRuleArgs
            {
                Name = name,
                Direction = direction,
                SourcePortRanges = srcPortRanges,
                DestinationPortRanges = dstPortRanges,
                Priority = priority,
                Access = access,
                Protocol = protocol,
                Description = description,
            };
            sr.SourceAddressPrefix = srcServiceTag;
            sr.DestinationAddressPrefix = destServiceTag;
            sr.SourceAddressPrefixes = srcIP;
            sr.DestinationAddressPrefixes = destIP;
            sr.SourceApplicationSecurityGroups = srcASG;
            sr.DestinationApplicationSecurityGroups = destASG;

            builder.Arguments.SecurityRules.Add(sr);
            return builder;
        }
        /// <summary>
        /// Sets the resource group the <see cref="Pulumi.Azure.Network.NetworkSecurityGroup" /> will be created on
        /// </summary>
        /// <param name="builder">NetworkSecurityGroup Builder</param>
        /// <param name="resourceGroup">Resource group name</param>
        /// <returns></returns>
        public static NetworkSecurityGroupBuilder ResourceGroup(this NetworkSecurityGroupBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Sets the builder name. If the builder has an RandomId associated, 
        /// appends the hex value of the RandomId to the end of the name
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="name">Builder name</param>
        /// <returns>The builder argument</returns>
        public static NetworkSecurityGroupBuilder Name(this NetworkSecurityGroupBuilder builder, Input<string> name)
        {
            if (builder.RandomId != null)
            {
                builder.Arguments.NetworkSecurityGroupName = builder.RandomId.Hex.Apply(r => $"{name}-{r}");
            }
            else
            {
                builder.Arguments.NetworkSecurityGroupName = name;
            }
            return builder;
        }

        /// <summary>
        /// Sets the location on which the resource should be created on
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="location">Resource location</param>
        /// <returns></returns
        public static NetworkSecurityGroupBuilder Location(this NetworkSecurityGroupBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }
    }
}
