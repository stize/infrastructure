using System;
using Pulumi;
using Pulumi.Random;
using Pulumi.Testing;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Tests.Azure
{
    public class RandomIdNameStack : Stack
    {
        public RandomIdNameStack()
        {
            var rid = new RandomId("random1", new RandomIdArgs {
                ByteLength = 4
            });

            var context = new ResourceContext(rid.Hex, "dev");

            var tags = new InputMap<string> {                
                { "my", "tag" }
            };

            var rg = new ResourceGroupBuilder("rg1", context)
            .Name("rg1") // Builder uses the naming stratety to combine this value and the randomId
            .Location("westeurope")
            .Tags(tags) // Add custom tags
            .Build(); // Buildider adds the default tags using the tagging strategy
        }
    }
}
