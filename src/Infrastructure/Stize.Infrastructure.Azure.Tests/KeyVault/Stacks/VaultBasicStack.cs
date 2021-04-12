using Pulumi;
using Pulumi.AzureNative.KeyVault;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.KeyVault;
using Stize.Infrastructure.Azure.Networking;

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
            var vnet1 = new VNetBuilder("vnet1")
                .AddressSpace("192.168.30.0/24")
                .Name("vnet1")
                .ResourceGroup(rg.Name)
                .Location("westeurope")
                .Build();
            var subnet1 = new SubnetBuilder("subnet1")
                .AddressPrefix("192.168.30.2")
                .Name("subnet1")
                .ResourceGroup(rg.Name)
                .InVNet(vnet1.Name)
                .EnableKeyVaultServiceEndpoint()
                .Build();
            var ap1 = new AccessPolicyBuilder()
                .TenantId("00000000-0000-0000-0000-000000000000")
                .ObjectId("00000000-0000-0000-0000-000000000000")
                .CertificatePermissions(CertificatePermissions.All)
                .KeyPermissions("get", "create", "verify", "sign", "update")
                .SecretPermissions("all")
                .StoragePermissions(StoragePermissions.Backup, StoragePermissions.Get, StoragePermissions.List, StoragePermissions.Recover, StoragePermissions.Update)
                .Build();
            var ap2 = new AccessPolicyBuilder()
                .TenantId("00000000-1111-1111-1111-000000000000")
                .ObjectId("00000000-1111-1111-1111-000000000000")
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
                .AddAccessPolicy(ap1, ap2)
                .EnabledForDeployment()
                .EnabledForDiskEncryption()
                .EnabledForTemplateDeployment()
                .EnablePremium()
                .EnablePurgeProtection()
                .SoftDeleteRetentionDays(60)
                .ProvisioningState(VaultProvisioningState.Succeeded)
                .DefaultAction("Allow")
                .AllowedIPAddresses("192.168.20.51", "192.169.20.0/16")
                .AllowedVirtualNetworks(subnet1.Id, false)
                .Build();
            var kv2 = new VaultBuilder("kv2")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("kv2")
                .TenantId("00000000-0000-0000-0000-000000000000")
                .DisableSoftDelete()
                .EnableRbacAuthorization()
                .RecoverVault()
                .DisallowBypassForAzureServices()
                .Build();
        }
    }
}
