using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Testing;

namespace Stize.Infrastructure.Azure.Tests.Storage.Stacks
{
    public class RandomIdStorageMock : IMocks
    {
        public Task<(string? id, object state)> NewResourceAsync(string type, string name, ImmutableDictionary<string, object> inputs,
            string? provider, string? id)
        {
            var outputs = ImmutableDictionary.CreateBuilder<string, object>();

            // Forward all input parameters as resource outputs, so that we could test them.
            outputs.AddRange(inputs);

            // Default the resource ID to `{name}_id`.
            id ??= $"{name}_id";

            switch (type)
            {
                case "azure-nextgen:resources/latest:ResourceGroup": return NewResourceGroup(type, name, inputs, provider, id, outputs);
                case "random:index/randomId:RandomId": return NewRandomId(type, name, inputs, provider, id, outputs);
                case "azure-nextgen:storage/latest:StorageAccount": return NewStorageAccount(type, name, inputs, provider, id, outputs);                
                default: return Task.FromResult((id, (object)outputs));
            }
        }

        public Task<object> CallAsync(string token, ImmutableDictionary<string, object> inputs, string? provider)
        {
            // We don't use this method in this particular test suite.
            // Default to returning whatever we got as input.
            return Task.FromResult((object)inputs);
        }

        public Task<(string? id, object state)> NewResourceGroup(string type, string name, ImmutableDictionary<string, object> inputs,
            string? provider, string? id, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", inputs["resourceGroupName"]);
            return Task.FromResult((id, (object)outputs));
        }

        public Task<(string? id, object state)> NewRandomId(string type, string name, ImmutableDictionary<string, object> inputs,
            string? provider, string? id, ImmutableDictionary<string, object>.Builder outputs)
        {
            // We set a mocked hex value for this unit test
            outputs.Add("hex", "adfr423g");

            return Task.FromResult((id, (object)outputs));
        }

        public Task<(string? id, object state)> NewStorageAccount(string type, string name, ImmutableDictionary<string, object> inputs,
            string? provider, string? id, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", inputs["accountName"]);
            return Task.FromResult((id, (object)outputs));
        }        
    }
}