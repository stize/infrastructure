using Pulumi;
using Pulumi.AzureNative.KeyVault;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.KeyVault;
using Stize.Infrastructure.Azure.Networking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stize.Infrastructure.Tests.Azure.KeyVault.Stacks
{
    public class KeyBasicStack : Stack
    {
        public KeyBasicStack()
        {
            var rg = new ResourceGroupBuilder("rg1")
                .Name("rg1")
                .Location("westeurope")
                .Build();
            var ap1 = new AccessPolicyBuilder()
                .TenantId("00000000-0000-0000-0000-000000000000")
                .ObjectId("00000000-0000-0000-0000-000000000000")
                .CertificatePermissions("all")
                .KeyPermissions("all")
                .SecretPermissions("all")
                .StoragePermissions("all")
                .Build();

            var kv1 = new VaultBuilder("kv1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("kv1")
                .TenantId("00000000-0000-0000-0000-000000000000")
                .AddAccessPolicy(ap1)
                .Build();

            var key = new KeyBuilder("key1")
                .Name("key1")
                .VaultName(kv1.Name)
                .KeyType(JsonWebKeyType.RSA)
                .KeySizeInBits(2048)
                .KeyOps(JsonWebKeyOperation.Encrypt, JsonWebKeyOperation.Decrypt)
                .ActivationDate("2021-04-10")
                .ExpiryDate("2021-04-11")
                .IsEnabled(true)
                .Build();

            var key2 = new KeyBuilder("key2")
                .Name("key2")
                .VaultName(kv1.Name)
                .KeyType(JsonWebKeyType.EC)
                .CurveName(JsonWebKeyCurveName.P_256)
                .IsEnabled(true)
                .Build();
        }
    }
}
