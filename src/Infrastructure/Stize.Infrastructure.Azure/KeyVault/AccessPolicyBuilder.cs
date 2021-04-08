using Pulumi;
using Pulumi.AzureNative.KeyVault.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stize.Infrastructure.Azure.KeyVault
{
    public class AccessPolicyBuilder
    {
        public Dictionary<string, Input<string>> Identifiers { get; private set; } = new Dictionary<string, Input<string>> 
        { 
            { "ObjectId", null }, 
            { "TenantId", null }, 
            { "ApplicationId", null } 
        };
        public PermissionsArgs Permissions { get; private set; } = new PermissionsArgs { Certificates = null, Keys = null, Secrets = null, Storage = null };
        public AccessPolicyBuilder()
        {

        }

        public AccessPolicyEntryArgs Build()
        {
            var accessPolicy = new AccessPolicyEntryArgs
            {
                ObjectId = Identifiers["ObjectId"],
                TenantId = Identifiers["TenantId"],
                ApplicationId = Identifiers["ApplicationId"],
                Permissions = Permissions
            };
            return accessPolicy;
        }
    }
}
