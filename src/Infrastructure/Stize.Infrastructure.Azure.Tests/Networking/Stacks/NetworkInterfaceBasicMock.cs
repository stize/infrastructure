using Pulumi.Testing;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace Stize.Infrastructure.Azure.Tests.Networking.Stacks
{
    public class NetworkInterfaceBasicMock :IMocks
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

            switch (args.Type)
            {
                case "azure-native:network:NetworkInterface": return NewNetworkInterface(args, outputs);
                case "azure-native:network:NetworkSecurityGroup": return NewNetworkSecurityGroup(args, outputs);
                case "azure-native:network:Subnet": return NewSubnet(args, outputs);
                default: return Task.FromResult((args.Id, (object)outputs));
            }
        }

        public Task<object> CallAsync(MockCallArgs args)
        {
            // We don't use this method in this particular test suite.
            // Default to returning whatever we got as input.
            return Task.FromResult((object)args.Args);
        }

        public Task<(string? id, object state)> NewNetworkInterface(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["networkInterfaceName"]);
            return Task.FromResult((args.Id, (object)outputs));
        }
        public Task<(string? id, object state)> NewNetworkSecurityGroup(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["networkSecurityGroupName"]);
            return Task.FromResult((args.Id, (object)outputs));
        }
        public Task<(string? id, object state)> NewSubnet(MockResourceArgs args, ImmutableDictionary<string, object>.Builder outputs)
        {
            outputs.Add("name", args.Inputs["subnetName"]);
            return Task.FromResult((args.Id, (object)outputs));
        }
    }
}
