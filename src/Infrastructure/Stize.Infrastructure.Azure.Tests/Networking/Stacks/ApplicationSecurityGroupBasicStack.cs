using Pulumi;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Networking;

namespace Stize.Infrastructure.Tests.Azure.Networking.Stacks
{
    public class ApplicationSecurityGroupBasicStack : Stack
    {
        public ApplicationSecurityGroupBasicStack()
        {
            var rg = new ResourceGroupBuilder("rg1")
                .Name("rg1")
                .Location("westeurope")
                .Build();

            var asg = new ApplicationSecurityGroupBuilder("asg1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("asg1")
                .Build();
        }
        
    }
}
