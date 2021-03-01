using System;
using Pulumi;
using Pulumi.Random;
using Pulumi.Testing;
using Stize.Infrastructure.Azure;

namespace Stize.Infrastructure.Azure.Tests.Azure
{
    public class RandomIdNameStack : Stack
    {
        public RandomIdNameStack()
        {
            var rid = new RandomId("random1", new RandomIdArgs {
                ByteLength = 4
            });            

            var tags = new InputMap<string> {                
                { "env", "dev" },
                { "uid", rid.Hex.Apply(r => r) }   
            };

            var rg = new ResourceGroupBuilder("rg1", rid)
            .Name("rg1")
            .Location("westeurope")
            .Tags(tags)
            .Build();
        }
    }
}
