using System;
using Pulumi;
using Pulumi.Random;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Storage;

namespace Stize.Infrastructure.Azure.Tests.Storage.Stacks
{
    public class RandomIdStorageStack : Stack
    {
        public RandomIdStorageStack()
        {
            var rg = new ResourceGroupBuilder("rg1")
            .Name("rg1")
            .Location("westeurope")
            .Build();

            var rid = new RandomId("random1", new RandomIdArgs
            {
                ByteLength = 4
            });

            var hexValue = rid.Hex.GetValueAsync();

            var storage = new StorageAccountBuilder("account1", rid)
            .Name("account1")
            .In(rg)
            .Location("westeurope")
            .StorageV2()
            .StandardLRS()
            .Build();
        }
    }
}
