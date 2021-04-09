using Pulumi;
using Pulumi.AzureNative.KeyVault;
using Pulumi.AzureNative.KeyVault.Inputs;
using System;
using System.Collections.Generic;

namespace Stize.Infrastructure.Azure.KeyVault
{
    public static class VaultExtensions
    {

        /// <summary>
        /// Sets the resource name
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static VaultBuilder Name(this VaultBuilder builder, Input<string> name)
        {
            builder.Arguments.VaultName = name;
            return builder;
        }

        /// <summary>
        /// Assigns the Vault to a resource group
        /// </summary>
        /// <param name="builder">Vault builder</param>
        /// <param name="resourceGroup">Resource group name</param>
        /// <returns></returns>
        public static VaultBuilder ResourceGroup(this VaultBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Sets the Vault location
        /// </summary>
        /// <param name="builder">Vault builder</param>
        /// <param name="location">Vault location</param>
        /// <returns></returns>
        public static VaultBuilder Location(this VaultBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }

        /// <summary>
        /// Sets the Vault tags
        /// </summary>
        /// <param name="builder">Vault builder</param>
        /// <param name="location">Vault tags</param>
        /// <returns></returns>
        public static VaultBuilder Tags(this VaultBuilder builder, InputMap<string> tags)
        {
            builder.Arguments.Tags = tags;
            return builder;
        }

        /// <summary>
        /// Sets the SkuName (Pricing Tier) to Premium.
        /// Default is Standard if this method isn't called.
        /// </summary>
        /// <param name="builder">Vault builder</param>
        /// <returns></returns>
        public static VaultBuilder EnablePremium(this VaultBuilder builder)
        {
            builder.Properties.Sku = new Pulumi.AzureNative.KeyVault.Inputs.SkuArgs { Family = SkuFamily.A, Name = SkuName.Premium };
            return builder;
        }

        /// <summary>
        /// Sets the Tenant Id that is used for authenticating requests to the vault.
        /// </summary>
        /// <param name="builder">Vault builder</param>
        /// <param name="id">The Azure Active Directory tenant ID that should be used for authenticating requests to the key vault.</param>
        /// <returns></returns>
        public static VaultBuilder TenantId(this VaultBuilder builder, Input<string> id)
        {
            builder.Properties.TenantId = id;
            return builder;
        }

        /// <summary>
        /// Disables soft delete - default value for 'EnableSoftDelete' is true
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static VaultBuilder DisableSoftDelete(this VaultBuilder builder)
        {
            builder.Properties.EnableSoftDelete = false;
            return builder;
        }

        /// <summary>
        /// Enable purge protection (enforce a mandatory retention period for deleted vaults and vault objects).
        /// Enables soft delete as well, as enabling purge protection does nothing without enabling soft delete.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static VaultBuilder EnablePurgeProtection(this VaultBuilder builder)
        {
            builder.Properties.EnableSoftDelete = true;
            builder.Properties.EnablePurgeProtection = true;
            return builder;
        }

        /// <summary>
        /// Allows Azure Disk Encryption to retrieve secrets from the vault and unwrap keys.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static VaultBuilder EnabledForDiskEncryption(this VaultBuilder builder)
        {
            builder.Properties.EnabledForDiskEncryption = true;
            return builder;
        }

        /// <summary>
        /// Allows Azure Virtual Machines to retrieve certificates stored as secrets from the key vault.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static VaultBuilder EnabledForDeployment(this VaultBuilder builder)
        {
            builder.Properties.EnabledForDeployment = true;
            return builder;
        }

        /// <summary>
        /// Allows Azure Resource Manager to retrieve secrets from the key vault.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static VaultBuilder EnabledForTemplateDeployment(this VaultBuilder builder)
        {
            builder.Properties.EnabledForTemplateDeployment = true;
            return builder;
        }

        /// <summary>
        /// When true, the key vault will use Role Based Access Control (RBAC) for authorization of data actions, 
        /// and the access policies specified in vault properties will be ignored (warning: this is a preview feature). 
        /// When false, the key vault will use the access policies specified in vault properties, and any policy stored on Azure Resource Manager will be ignored. 
        /// Default value of false. 
        /// Note that management actions are always authorized with RBAC.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static VaultBuilder EnableRbacAuthorization(this VaultBuilder builder)
        {
            // Property that controls how data actions are authorized. 
            builder.Properties.EnableRbacAuthorization = true;
            return builder;
        }

        /// <summary>
        /// Adds an access policy with all permissions enabled for Certificates, Keys, Storage, and Secrets.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="objectId"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public static VaultBuilder AddAccessPolicyWithAllPermissions(this VaultBuilder builder, Input<string> objectId, Input<string> tenantId)
        {
            builder.Properties.AccessPolicies.Add(new Pulumi.AzureNative.KeyVault.Inputs.AccessPolicyEntryArgs
            {
                ObjectId = objectId,
                TenantId = tenantId,
                Permissions = new Pulumi.AzureNative.KeyVault.Inputs.PermissionsArgs
                {
                    Certificates    = { CertificatePermissions.All },
                    Keys            = { KeyPermissions.All },
                    Storage         = { StoragePermissions.All },
                    Secrets         = { SecretPermissions.All }

                }
            });
            return builder;
        }

        /// <summary>
        /// Adds Access Policies to the Vault. Use the <see cref="AccessPolicyBuilder"/> to construct an AccessPolicy and pass it into this method.
        /// Multiple Access Policies can be passed through this method, through additional arguments.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="args">Add an Access Policy to the Vault by passing an AccessPolicyEntryArgs. Use the <see cref="AccessPolicyBuilder"/> to construct an AccessPolicy and pass it into this method. 
        /// Multiple Access Policies can be passed through additional arguments.</param>
        /// <returns></returns>
        public static VaultBuilder AddAccessPolicy(this VaultBuilder builder, params AccessPolicyEntryArgs[] args)
        {
            foreach (var ap in args)
            {
                builder.Properties.AccessPolicies.Add(ap);
            }
            return builder;
        }
                
        /// <summary>
        /// Sets the create mode of the vault to recover.
        /// Recovers a Vault that is in soft-delete retention by specifying the name of the deleted value using the <see cref="Name(VaultBuilder, Input{string})"/> method.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static VaultBuilder RecoverVault(this VaultBuilder builder)
        {
            builder.Properties.CreateMode = CreateMode.Recover;
            return builder;
        }

        /// <summary>
        /// Number of days data is held in soft delete retention.
        /// Valid values are between 7 and 90 (inclusive).
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static VaultBuilder SoftDeleteRetentionDays(this VaultBuilder builder, Input<int> days)
        {
            builder.Properties.EnableSoftDelete = true;
            builder.Properties.SoftDeleteRetentionInDays = days;
            return builder;
        }

        /// <summary>
        /// Disallow firewall bypass for Microsoft Services. This setting is related to firewall only. 
        /// In order to access this key vault, the trusted service must also be given explicit permissions through Access policies.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static VaultBuilder DisallowBypassForAzureServices(this VaultBuilder builder)
        {
            builder.NetworkRuleSet.Bypass = NetworkRuleBypassOptions.None;
            return builder;
        }

        /// <summary>
        /// The default action when no rule from Ip Rules and from Virtual Network Rules match. This is only used after the bypass property has been evaluated.
        /// Valid values: 'Allow' or 'Deny'
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="action">The default type of action for incoming traffic. Valid values: 'Allow' or 'Deny'</param>
        /// <returns></returns>
        public static VaultBuilder DefaultAction(this VaultBuilder builder, InputUnion<string, NetworkRuleAction> action)
        {
            builder.NetworkRuleSet.DefaultAction = action;
            return builder;
        }

        /// <summary>
        /// Add accessibility to the Vault from network locations using IPv4 address ranges in CIDR notation, such as '126.51.78.91' or '126.51.78.0/24'.
        /// Multiple IP addresses can be passed through this method, through additional arguments.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="ipAddresses">IPv4 address ranges in CIDR notation, such as '126.51.78.91' or '126.51.78.0/24'.
        /// Multiple IP addresses can be passed through additional arguments.</param>
        /// <returns></returns>
        public static VaultBuilder AllowedIPAddresses(this VaultBuilder builder, params Input<string>[] ipAddresses)
        {
            //TODO: Needs functional testing
            var ipRules = new List<Input<IPRuleArgs>>();
            foreach (var ip in ipAddresses)
            {
                ipRules.Add(new IPRuleArgs { Value = ip });
            }
            builder.NetworkRuleSet.IpRules = ipRules;
            return builder;
        }

        /// <summary>
        /// Add accessibility from Virtual Networks (VNets) to the Vault, using the resource ID of the Subnet(s).
        /// Multiple Subnets can be passed through this method, through additional arguments of type (string, bool).
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="subnets">Resource ID of the Subnet(s) and whether missing service endpoints should be ignored. 
        /// Multiple Subnets can be passed through this method, through concatenating additional arguments. Value must be of type tuple (string, bool)</param>
        /// <returns></returns>
        public static VaultBuilder AllowedVirtualNetworks(this VaultBuilder builder, params (Input<string> subnetId, Input<bool> ignoreMissingVnetServiceEndpoint)[] subnets)
        {
            var snetRules = new List<Input<VirtualNetworkRuleArgs>>();
            foreach (var (subnetId, ignoreMissingVnetServiceEndpoint) in subnets)
            {
                snetRules.Add(new VirtualNetworkRuleArgs { Id = subnetId, IgnoreMissingVnetServiceEndpoint = ignoreMissingVnetServiceEndpoint }); 
            }
            builder.NetworkRuleSet.VirtualNetworkRules = snetRules;
            return builder;
        }


        /// <summary>
        /// Provisioning state of the vault. Valid values: 'Succeeded' or 'RegisteringDns'.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="state">Provisioning state of the vault. Valid values: 'Succeeded' or 'RegisteringDns'.</param>
        /// <returns></returns>
        public static VaultBuilder ProvisioningState(this VaultBuilder builder, InputUnion<string, VaultProvisioningState> state)
        {
            builder.Properties.ProvisioningState = state;
            return builder;
        }



        /*             Properties of Vault:
         * <Complete>  Sku                              : SkuArgs
         *                                                  { 
         * <Complete>                                           Family  : SkuFamily
         * <Complete>                                           Name    : SkuName
         *                                                  }
         * <Complete>  TenantId                         : string
         * <Complete>  AccessPolicies                   : List<AccessPolicyEntryArgs 
         *                                                  { 
         * <Complete>                                           ObjectId        : string
         * <Complete>                                           Permissions     : PermissionsArgs
         *                                                                          {
         * <Complete>                                                                   [Certifications]    : List<Union<string, CertificatePermissions>>
         * <Complete>                                                                   [Keys]              : List<Union<string, KeyPermissions>>
         * <Complete>                                                                   [Storage]           : List<Union<string, StoragePermissions>>
         * <Complete>                                                                   [Secrets]           : List<Union<string, SecretPermissions>>
         *                                                                          }
         * <Complete>                                           TenantId        : string
         * <Complete>                                           [ApplicationId] : string
         *                                                  }
         *                                                >
         * <Complete>  [CreateMode]                     : CreateMode
         * <Complete>  [EnablePurgeProtection]          : bool
         * <Complete>  [EnableRbacAuthorization         : bool
         * <Complete>  [EnableSoftDelete]               : bool
         * <Complete>  [EnabledForDeployment]           : bool
         * <Complete>  [EnabledForDiskEncryption]       : bool
         * <Complete>  [EnabledForTemplateDeployment]   : bool
         * <Complete>  [NetworkAcls]                    : NetworkRuleSetArgs 
         *                                                  { 
         * <Complete>                                           ByPass              : NetworkRuleBypassOptions
         * <Complete>                                           DefaultAction       : NetworkRuleAction
         * <Complete>                                           IpRules             : IPRuleArgs 
         *                                                                              {
         * <Complete>                                                                       Value : string
         *                                                                              }
         * <Complete>                                           VirtualNetworkRules : VirtualNetworkRuleArgs
         *                                                                              {
         * <Complete>                                                                       Id : string
         * <Pending>                                                                        [IgnoreMissingVnetServiceEndpoint] : bool
         *                                                                              }
         *                                                  }
         * <Complete>  [ProisioningState]               : VaultProvisioningState
         * <Complete>  [SoftDeleteRetentionDays]        : int
         * <Pending>   [VaultUri]                       : string
         * 
         */
    }
}
