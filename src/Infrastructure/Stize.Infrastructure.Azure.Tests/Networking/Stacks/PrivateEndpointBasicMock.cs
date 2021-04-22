using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Testing;

namespace Stize.Infrastructure.Azure.Tests.Networking.Stacks
{
    public class PrivateEndpointBasicMock : IMocks
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
                case "azure-native:network:PrivateEndpoint": return NewPrivateEndpoint(args, outputs);
                case "azure-native:network:Subnet": return NewSubnet(args, outputs);
                case "azure-native:storage:StorageAccount": return NewStorageAccount(args, outputs);
                default: return Task.FromResult((args.Id, (object)outputs));
            }
        }

        public Task<object> CallAsync(MockCallArgs args)
        {
            // We don't use this method in this particular test suite.
            // Default to returning whatever we got as input.
            return Task.FromResult((object)args.Args);
        }

        public Task<(string? id, object state)> NewPrivateEndpoint(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["privateEndpointName"]);
            return Task.FromResult((args.Id, (object)outputs));
        }

        public Task<(string? id, object state)> NewStorageAccount(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["accountName"]);
            return Task.FromResult((args.Id, (object)outputs));
        }
        public Task<(string? id, object state)> NewSubnet(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["subnetName"]);

            return Task.FromResult((args.Id, (object)outputs));
        }
    }
}
