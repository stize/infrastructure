using Pulumi;
using Pulumi.AzureNative.KeyVault;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stize.Infrastructure.Azure.KeyVault
{
    public static class AccessPolicyExtensions
    {
        public static AccessPolicyBuilder ObjectId(this AccessPolicyBuilder builder, Input<string> objectId)
        {
            builder.Identifiers["ObjectId"] = objectId;
            return builder;
        }
        public static AccessPolicyBuilder TenantId(this AccessPolicyBuilder builder, Input<string> tenantId)
        {
            builder.Identifiers["TenantId"] = tenantId;
            return builder;
        }
        public static AccessPolicyBuilder ApplicationId(this AccessPolicyBuilder builder, Input<string> applicationId)
        {
            builder.Identifiers["ApplicationId"] = applicationId;
            return builder;
        }
        /// <summary>
        /// Set the Key Permissions for the Access Policy.
        /// The params Union<string, KeyPermissions>[] means that the user can list the desired Key Permissions one after another.
        /// For example:
        /// 'KeyPermissions("get", "list", "create", "update" "import")'
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="perms">Add a Key Permission to the access policy by string or by using the struct KeyPermissions. For example: "get" or KeyPermissions.Get</param>
        /// <returns></returns>
        public static AccessPolicyBuilder KeyPermissions(this AccessPolicyBuilder builder, params Union<string, KeyPermissions>[] perms)
        {
            builder.Permissions.Keys = perms;
            return builder;
        }

        /// <summary>
        /// Set the Secret Permissions for the Access Policy.
        /// The params Union<string, SecretPermissions>[] means that the user can list the desired Secret Permissions one after another.
        /// For example:
        /// 'SecretPermissions("get", "list", "create", "update" "import")'
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="perms">Add a Secret Permission to the access policy by string or by using the struct SecretPermissions. For example: "get" or SecretPermissions.Get</param>
        /// <returns></returns>
        public static AccessPolicyBuilder SecretPermissions(this AccessPolicyBuilder builder, params Union<string, SecretPermissions>[] perms)
        {
            builder.Permissions.Secrets = perms;
            return builder;
        }

        /// <summary>
        /// Set the Certificate Permissions for the Access Policy.
        /// The params Union<string, CertificatePermissions>[] means that the user can list the desired Certificate Permissions one after another.
        /// For example:
        /// 'CertificatePermissions("get", "list", "create", "update" "import")'
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="perms">Add a Certificate Permission to the access policy by string or by using the struct CertificatePermissions. For example: "get" or CertificatePermissions.Get</param>
        /// <returns></returns>
        public static AccessPolicyBuilder CertificatePermissions(this AccessPolicyBuilder builder, params Union<string, CertificatePermissions>[] perms)
        {
            builder.Permissions.Certificates = perms;
            return builder;
        }

        /// <summary>
        /// Set the Storage Permissions for the Access Policy.
        /// The params Union<string, StoragePermissions>[] means that the user can list the desired Storage Permissions one after another.
        /// For example:
        /// 'StoragePermissions("get", "list", "create", "update" "import")'
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="perms">Add a Storage Permission to the access policy by string or by using the struct StoragePermissions. For example: "get" or StoragePermissions.Get</param>
        /// <returns></returns>
        public static AccessPolicyBuilder StoragePermissions(this AccessPolicyBuilder builder, params Union<string, StoragePermissions>[] perms)
        {
            builder.Permissions.Storage = perms;
            return builder;
        }
    }
}
