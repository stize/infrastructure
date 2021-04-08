using Pulumi;
using Pulumi.AzureNative.KeyVault;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.KeyVault;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stize.Infrastructure.Tests.Azure.KeyVault.Stacks
{
    public class VaultBasicStack : Stack
    {
        public VaultBasicStack()
        {
            var rg = new ResourceGroupBuilder("rg1")
                .Name("rg1")
                .Location("westeurope")
                .Build();

            var ap1 = new AccessPolicyBuilder()
                .TenantId("00000000-0000-0000-0000-000000000000")
                .ObjectId("00000000-0000-0000-0000-000000000000")
                .CertificatePermissions(CertificatePermissions.All)
                .KeyPermissions("get", "create", "verify", "sign", "update")
                .SecretPermissions("all")
                .StoragePermissions(StoragePermissions.Backup, StoragePermissions.Get, StoragePermissions.List, StoragePermissions.Recover, StoragePermissions.Update)
                .Build();

            var kv1 = new VaultBuilder("kv1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("kv1")
                .TenantId("00000000-0000-0000-0000-000000000000")
                .AddAccessPolicy(ap1)
                .EnabledForDeployment()
                .EnabledForDiskEncryption()
                .EnabledForTemplateDeployment()
                .EnablePremium()
                .EnablePurgeProtection()
                .SoftDeleteRetentionDays(60)
                .Build();
            var kv2 = new VaultBuilder("kv2")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("kv2")
                .TenantId("00000000-0000-0000-0000-000000000000")
                .DisableSoftDelete()
                .EnableRbacAuthorization()
                .RecoverVault()
                .Build();
        }
    }
}
