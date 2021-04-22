using Pulumi.Testing;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Stize.Infrastructure.Tests.Azure.KeyVault.Stacks
{
    public class SecretBasicMock : IMocks
    {
        public Task<(string? id, object state)> NewResourceAsync(MockResourceArgs args)
        {
            var outputs = ImmutableDictionary.CreateBuilder<string, object>();

            // Forward all input parameters as resource outputs, so that we could test them.
            outputs.AddRange(args.Inputs);

            // Default the resource ID to `{name}_id`.
            if (args.Id == null || args.Id == "")
            {
                args.Id = $"{args.Name}_id";
            }
            outputs.Add("id", args.Id);

            switch (args.Type)
            {
                case "azure-native:keyvault:Secret": return NewSecret(args, outputs);
                case "azure-native:keyvault:Vault": return NewVault(args, outputs);
                case "azure-native:resources:ResourceGroup": return NewResourceGroup(args, outputs);
                default: return Task.FromResult((args.Id, (object)outputs));
            }
        }
        public Task<object> CallAsync(MockCallArgs args)
        {
            // We don't use this method in this particular test suite.
            // Default to returning whatever we got as input.
            return Task.FromResult((object)args.Args);
        }
        public Task<(string? id, object state)> NewSecret(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["secretName"]);

            return Task.FromResult((args.Id, (object)outputs));
        }
        public Task<(string? id, object state)> NewVault(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["vaultName"]);

            return Task.FromResult((args.Id, (object)outputs));
        }
        public Task<(string? id, object state)> NewResourceGroup(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["resourceGroupName"]);

            return Task.FromResult((args.Id, (object)outputs));
        }

    }
}
