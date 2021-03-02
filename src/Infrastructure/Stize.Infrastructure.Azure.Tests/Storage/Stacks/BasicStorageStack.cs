using System;
using Pulumi;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Storage;

namespace Stize.Infrastructure.Azure.Tests.Storage.Stacks
{
    public class BasicStorageStack : Stack
    {
        public BasicStorageStack()
        {
            var rg = new ResourceGroupBuilder("rg1")
            .Name("rg1")
            .Location("westeurope")
            .Build();

            var storage = new StorageAccountBuilder("account1")
            .Name("account1")
            .In(rg)
            .Location("westeurope")
            .StorageV2()
            .StandardLRS()
            .Build();
        }
    }
}
