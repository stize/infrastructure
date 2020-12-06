using System;
using Pulumi;
using Stize.Infrastructure.Azure;

namespace Stize.Infrastructure.Tests.Azure.Stacks
{
    public class BasicResourceGroupStack : Stack
    {
        public BasicResourceGroupStack()
        {

            var rg = new ResourceGroupBuilder("rg1")
            .Location("westeurope")
            .Build();
        }
    }
}
