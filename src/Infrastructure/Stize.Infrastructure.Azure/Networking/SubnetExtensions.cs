using System;
using Pulumi;
using Pulumi.AzureNextGen.Network.Latest.Inputs;

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
            if (builder.RandomId != null)
            {
                builder.Arguments.SubnetName = builder.RandomId.Hex.Apply(r => $"{name}-{r}");
            }
            else
            {
                builder.Arguments.SubnetName = name;
            }

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
        /// Enfoces private link endpoint network policies
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static SubnetBuilder EnforcePrivateEndpointNetworkPolicies(this SubnetBuilder builder, bool active = true)
        {
            builder.Arguments.PrivateEndpointNetworkPolicies = active.ToString(); // TODO Confirm if this is correct after the migration to nextgen
            return builder;
        }


        /// <summary>
        /// Enfoces private link service network policies
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static SubnetBuilder EnforcePrivateLinkServiceNetworkPolicies(this SubnetBuilder builder, bool active = true)
        {
            builder.Arguments.PrivateLinkServiceNetworkPolicies = active.ToString(); // TODO Confirm if this is correct after the migration to nextgen
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
    }
}
