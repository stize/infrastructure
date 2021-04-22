using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Testing;

namespace Stize.Infrastructure.Azure.Tests.Stacks
{
    public class RandomIdNameMock : IMocks
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
                case "random:index/randomId:RandomId": return NewRandomId(args, outputs);
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
        public Task<(string? id, object state)> NewResourceGroup(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["resourceGroupName"]);
            return Task.FromResult((args.Id, (object)outputs));
        }

        public Task<(string? id, object state)> NewRandomId(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            // We set a mocked hex value for this unit test
            outputs.Add("hex", "fh3ue3");

            return Task.FromResult((args.Id, (object)outputs));
        }

    }
}