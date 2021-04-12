using Pulumi;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.KeyVault;
using Stize.Infrastructure.Azure.Networking;

namespace Stize.Infrastructure.Tests.Azure.KeyVault.Stacks
{
    public class SecretBasicStack : Stack
    {
        public SecretBasicStack()
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

            var secret1 = new SecretBuilder("secret1")
                .Name("secret1")
                .VaultName(kv1.Name)
                .ResourceGroup(rg.Name)
                .Value("supermegasecret")
                .ActivationDate("2021-05-25")
                .ExpiryDate("2021-05-26")
                .ContentType("Contains a secret.")
                .IsEnabled(true)
                .Build();
        }
    }
}
