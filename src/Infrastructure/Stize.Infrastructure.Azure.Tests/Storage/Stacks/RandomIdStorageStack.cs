using System;
using Pulumi;
using Pulumi.Random;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Storage;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Tests.Storage.Stacks
{
    public class RandomIdStorageStack : Stack
    {
        public RandomIdStorageStack()
        {
            var rid = new RandomId("random1", new RandomIdArgs
            {
                ByteLength = 4
            });

            var context = new ResourceContext(rid.Hex);

            var rg = new ResourceGroupBuilder("rg1", context)
            .Name("rg1")
            .Location("westeurope")
            .Build();      

            var storage = new StorageAccountBuilder("account1", context)
            .Name("account1")
            .In(rg)
            .Location("westeurope")
            .StorageV2()
            .StandardLRS()
            .Build();
        }
    }
}
