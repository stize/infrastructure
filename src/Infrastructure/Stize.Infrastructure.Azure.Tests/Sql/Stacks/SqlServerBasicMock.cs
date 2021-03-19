using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Testing;

namespace Stize.Infrastructure.Tests.Azure.Sql.Stacks
{
    public class SqlServerBasicMock : IMocks
    {
        public Task<(string? id, object state)> NewResourceAsync(string type, string name, ImmutableDictionary<string, object> inputs,
            string? provider, string? id)
        {
            var outputs = ImmutableDictionary.CreateBuilder<string, object>();

            // Forward all input parameters as resource outputs, so that we could test them.
            outputs.AddRange(inputs);

            // Default the resource ID to `{name}_id`.
            if (id == null || id == "")
            {
                id = $"{name}_id";
            }

            switch (type)
            {
                case "azure-native:sql:Server": return NewServer(type, name, inputs, provider, id, outputs);
                default: return Task.FromResult((id, (object)outputs));
            }
        }

        public Task<object> CallAsync(string token, ImmutableDictionary<string, object> inputs, string? provider)
        {
            // We don't use this method in this particular test suite.
            // Default to returning whatever we got as input.
            return Task.FromResult((object)inputs);
        }
        public Task<(string? id, object state)> NewServer(string type, string name, ImmutableDictionary<string, object> inputs,
            string? provider, string? id, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", inputs["serverName"]);
            return Task.FromResult((id, (object)outputs));
        }
    }
}
