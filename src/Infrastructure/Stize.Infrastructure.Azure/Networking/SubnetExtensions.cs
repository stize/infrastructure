using System;
using Pulumi;
using Pulumi.AzureNative.Network;
using Pulumi.AzureNative.Network.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class SubnetExtensions
    {

        /// <summary>
        /// Sets the builder name. If the builder has an RandomId associated, 
        /// appends the hex value of the RandomId to the end of the name
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="name">Builder name</param>
        /// <returns>The builder argument</returns>
        public static SubnetBuilder Name(this SubnetBuilder builder, Input<string> name)
        {
            builder.Arguments.SubnetName = name;            
            return builder;
        }

        /// <summary>
        /// Assigned the VNet for this
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="vnetName">vnet to attached the subnet to</param>
        /// <returns></returns>
        public static SubnetBuilder InVNet(this SubnetBuilder builder, Input<string> vnetName)
        {
            builder.Arguments.VirtualNetworkName = vnetName;
            return builder;
        }

        /// <summary>
        /// Sets the address prefix for this VNet
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="addressPrefix">Address prefix for subnet</param>
        /// <returns></returns>
        public static SubnetBuilder AddressPrefix(this SubnetBuilder builder, Input<string> addressPrefix)
        {
            /// Fix: Commented line produces an error when trying to perform pulumi update to azure - I think AddressPrefixes is in preview only
            //builder.Arguments.AddressPrefixes.Add(addressPrefix);

            builder.Arguments.AddressPrefix = addressPrefix;
            return builder;
        }

        /// <summary>
        /// Enfoces private link endpoint network policies.
        /// See <see cref="VirtualNetworkPrivateEndpointNetworkPolicies"/> - Valid values: "Enabled" or "Disabled".
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="active"><see cref="VirtualNetworkPrivateEndpointNetworkPolicies"/> - Valid values: "Enabled" or "Disabled".</param>
        /// <returns></returns>
        public static SubnetBuilder EnforcePrivateEndpointNetworkPolicies(this SubnetBuilder builder, InputUnion<string, VirtualNetworkPrivateEndpointNetworkPolicies> active)
        {
            builder.Arguments.PrivateEndpointNetworkPolicies = active; 
            return builder;
        }


        /// <summary>
        /// Enfoces private link service network policies
        /// See <see cref="VirtualNetworkPrivateLinkServiceNetworkPolicies"/> - Valid values: "Enabled" or "Disabled".
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static SubnetBuilder EnforcePrivateLinkServiceNetworkPolicies(this SubnetBuilder builder, InputUnion<string, VirtualNetworkPrivateLinkServiceNetworkPolicies> active)
        {
            builder.Arguments.PrivateLinkServiceNetworkPolicies = active;
            return builder;
        }

        /// <summary>
        /// Sets the resource group this Subnet is created into
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resourceGroup"></param>
        /// <returns></returns>
        public static SubnetBuilder ResourceGroup(this SubnetBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Adds all the possible ServiceEndpoints to the subnet
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <returns>The builder</returns>
        public static SubnetBuilder EnableAllServiceEndpoints(this SubnetBuilder builder)
        {
            EnableAzureActiveDirectoryServiceEndpoint(builder);
            EnableAzureCosmosDbServiceEndpoint(builder);
            EnableContainerRegistryServiceEndpoint(builder);
            EnableEventHubServiceEndpoint(builder);
            EnableKeyVaultServiceEndpoint(builder);
            EnableServiceBusServiceEndpoint(builder);
            EnableSqlServiceEndpoint(builder);
            EnableStorageServiceEndpoint(builder);
            EnableWebServiceEndpoint(builder);
            return builder;
        }

        /// <summary>
        /// Enables the Azure Active Directory Service Endpoint
        /// </summary>
        /// <param name="builder">Subnet builder</param>
        /// <returns></returns>
        public static SubnetBuilder EnableAzureActiveDirectoryServiceEndpoint(this SubnetBuilder builder)
        {
            return EnableServiceEndpoint(builder, "Microsoft.AzureActiveDirectory");
        }

        /// <summary>
        /// Enables the Azure CosmosDB Service Endpoint
        /// </summary>
        /// <param name="builder">Subnet builder</param>
        /// <returns></returns>
        public static SubnetBuilder EnableAzureCosmosDbServiceEndpoint(this SubnetBuilder builder)
        {
            return EnableServiceEndpoint(builder, "Microsoft.AzureCosmosDB");
        }

        /// <summary>
        /// Enables the Azure CosmosDB Service Endpoint
        /// </summary>
        /// <param name="builder">Subnet builder</param>
        /// <returns></returns>
        public static SubnetBuilder EnableContainerRegistryServiceEndpoint(this SubnetBuilder builder)
        {
            return EnableServiceEndpoint(builder, "Microsoft.ContainerRegistry");
        }

        /// <summary>
        /// Enables the Event Hub Service Endpoint
        /// </summary>
        /// <param name="builder">Subnet builder</param>
        /// <returns></returns>
        public static SubnetBuilder EnableEventHubServiceEndpoint(this SubnetBuilder builder)
        {
            return EnableServiceEndpoint(builder, "Microsoft.EventHub");
        }

        /// <summary>
        /// Enables the KeyVault Service Endpoint
        /// </summary>
        /// <param name="builder">Subnet builder</param>
        /// <returns></returns>
        public static SubnetBuilder EnableKeyVaultServiceEndpoint(this SubnetBuilder builder)
        {
            return EnableServiceEndpoint(builder, "Microsoft.KeyVault");
        }

        /// <summary>
        /// Enables the Service Bus Service Endpoint
        /// </summary>
        /// <param name="builder">Subnet builder</param>
        /// <returns></returns>
        public static SubnetBuilder EnableServiceBusServiceEndpoint(this SubnetBuilder builder)
        {
            return EnableServiceEndpoint(builder, "Microsoft.ServiceBus");
        }

        /// <summary>
        /// Enables the Sql Service Endpoint
        /// </summary>
        /// <param name="builder">Subnet builder</param>
        /// <returns></returns>
        public static SubnetBuilder EnableSqlServiceEndpoint(this SubnetBuilder builder)
        {
            return EnableServiceEndpoint(builder, "Microsoft.Sql");
        }

        /// <summary>
        /// Enables the Storage Service Endpoint
        /// </summary>
        /// <param name="builder">Subnet builder</param>
        /// <returns></returns>
        public static SubnetBuilder EnableStorageServiceEndpoint(this SubnetBuilder builder)
        {
            return EnableServiceEndpoint(builder, "Microsoft.Storage");
        }

        /// <summary>
        /// Enables the Web Service Endpoint
        /// </summary>
        /// <param name="builder">Subnet builder</param>
        /// <returns></returns>
        public static SubnetBuilder EnableWebServiceEndpoint(this SubnetBuilder builder)
        {
            return EnableServiceEndpoint(builder, "Microsoft.Web");
        }

        /// <summary>
        /// Adds a custom Service Endpoint to the subnet. 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="serviceEndpoint">Possible values include: `Microsoft.AzureActiveDirectory`, `Microsoft.AzureCosmosDB`, `Microsoft.ContainerRegistry`, `Microsoft.EventHub`, `Microsoft.KeyVault`, `Microsoft.ServiceBus`, `Microsoft.Sql`, `Microsoft.Storage` and `Microsoft.Web`.</param>
        /// <returns></returns>
        public static SubnetBuilder EnableServiceEndpoint(this SubnetBuilder builder, Input<string> serviceEndpoint)
        {
            builder.Arguments.ServiceEndpoints.Add(new ServiceEndpointPropertiesFormatArgs{
                Service = serviceEndpoint
            });
            return builder;
        }

        /// <summary>
        /// Adds the subnet to an existing Network Security Group (NSG), using the NSG's resource ID.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="id">The resource ID of the Network Security Group.</param>
        /// <returns></returns>
        public static SubnetBuilder NetworkSecurityGroupId(this SubnetBuilder builder, Input<string> id)
        {
            builder.Arguments.NetworkSecurityGroup = new Pulumi.AzureNative.Network.Inputs.NetworkSecurityGroupArgs
            {
                Id = id,
            };
            return builder;
        }

        /// <summary>
        /// Adds an array of <see cref="ServiceEndpointPolicy"/>s to the subnet.
        /// List the <see cref="ServiceEndpointPolicy"/> resource IDs in a comma-seperated list of arguments.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="policyIDs"><see cref="ServiceEndpointPolicy"/> resource ID.</param>
        /// <returns></returns>
        public static SubnetBuilder ServiceEndpointPolicies(this SubnetBuilder builder, params Input<string>[] policyIDs)
        {
            foreach (var id in policyIDs)
            {
                builder.Arguments.ServiceEndpointPolicies.Add(new Pulumi.AzureNative.Network.Inputs.ServiceEndpointPolicyArgs { Id = id });
            }
            return builder;
        }

        /// <summary>
        /// Reference to the Route Table.  
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="id">The resource ID of the Route Table.</param>
        /// <returns></returns>
        public static SubnetBuilder RouteTableId(this SubnetBuilder builder, Input<string> id)
        {
            builder.Arguments.RouteTable = new Pulumi.AzureNative.Network.Inputs.RouteTableArgs
            {
                Id = id,
            };
            return builder;
        }


        /// <summary>
        /// Nat Gateway associated with this subnet.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="id">Resource ID of the NatGateway.</param>
        /// <returns></returns>
        public static SubnetBuilder NatGateway(this SubnetBuilder builder, Input<string> id)
        {
            builder.Arguments.NatGateway = new SubResourceArgs
            {
                Id = id,
            };
            return builder;
        }
    }
}
