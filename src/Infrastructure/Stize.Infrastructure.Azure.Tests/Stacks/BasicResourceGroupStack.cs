using System;
using Pulumi;
using Stize.Infrastructure.Azure;

namespace Stize.Infrastructure.Azure.Tests.Stacks
{
    public class BasicResourceGroupStack : Stack
    {
        public BasicResourceGroupStack()
        {
            var rg = new ResourceGroupBuilder("rg1")
            .Name("rg1")
            .Location("westeurope")
            .Build();
        }
    }
}
