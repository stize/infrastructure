using Pulumi;
using Stize.Infrastructure.Azure.KeyVault;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stize.Infrastructure.Azure.Tests.KeyVault.Stacks
{
    public class VaultBasicStack : Stack
    {
        public VaultBasicStack()
        {
            var tags = new InputMap<string> { { "env", "dev" } };
            var rg = new ResourceGroupBuilder("rg1")
                .Name("rg1")
                .Location("westeurope")
                .Build();

            var kv1 = new VaultBuilder("kv1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("kv1")
                .Tags(tags)
                .Build();
        }
    }
}
