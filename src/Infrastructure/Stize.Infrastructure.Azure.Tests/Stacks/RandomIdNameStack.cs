using System;
using Pulumi;
using Pulumi.Random;
using Stize.Infrastructure.Azure;

namespace Stize.Infrastructure.Tests.Azure.Stacks
{
    public class RandomIdNameStack : Stack
    {
        public RandomIdNameStack()
        {
            var rid = new RandomId("random1", new RandomIdArgs {
                ByteLength = 4
            });

            var rg = new ResourceGroupBuilder("rg1")
            .UseRandomId(rid)
            .Name("rg1")
            .Location("westeurope")
            .Build();
        }
    }
}
