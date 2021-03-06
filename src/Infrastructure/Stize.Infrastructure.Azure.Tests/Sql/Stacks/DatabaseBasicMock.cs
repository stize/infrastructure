﻿using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Testing;

namespace Stize.Infrastructure.Tests.Azure.Sql.Stacks
{
    public class DatabaseBasicMock : IMocks
    {
        public Task<(string? id, object state)> NewResourceAsync(MockResourceArgs args)
        {
            var outputs = ImmutableDictionary.CreateBuilder<string, object>();

            // Forward all input parameters as resource outputs, so that we could test them.
            outputs.AddRange(args.Inputs);

            // Default the resource ID to `{args.Inputs}_id`.
            if (args.Id == null || args.Id == "")
            {
                args.Id = $"{args.Name}_id";
            }

            switch (args.Type)
            {
                case "azure-native:sql:Database": return NewDatabase(args, outputs);
                case "azure-native:sql:Server": return NewServer(args, outputs);
                default: return Task.FromResult((args.Id, (object)outputs));
            }
        }

        public Task<object> CallAsync(MockCallArgs args)
        {
            // We don't use this method in this particular test suite.
            // Default to returning whatever we got as input.
            return Task.FromResult((object)args.Args);
        }

        public Task<(string? id, object state)> NewDatabase(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["databaseName"]);
            return Task.FromResult((args.Id, (object)outputs));
        }
        public Task<(string? id, object state)> NewServer(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["serverName"]);
            return Task.FromResult((args.Id, (object)outputs));
        }
    }
}
