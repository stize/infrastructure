using Pulumi;
using Stize.Infrastructure.Azure.Networking;
namespace Stize.Infrastructure.Tests.Azure.Networking.Stacks
{
    public class NetworkInterfaceBasicStack : Stack
    {
        public NetworkInterfaceBasicStack()
        {
            var rg = "my-resource-group";
            var vnet = new VNetBuilder("vnet1")
                .Location("westeurope")
                .ResourceGroup(rg)
                .Name("vnet1")
                .AddressSpace("172.16.0.0/24")
                .Build();
            var subnet = new SubnetBuilder("subnet1")
                .Parent(vnet)
                .Name("subnet1")
                .ResourceGroup(rg)
                .InVNet(vnet.Name)
                .AddressPrefix("172.16.0.0/24")
                .Build();
            var ni = new NetworkInterfaceBuilder("ni1")
                .Name("ni1")
                .ResourceGroup(rg)
                .Location("westeurope")
                .IpConfigurations("ipconfig1", subnet.GetResourceName(), "IPv4", "Dynamic")
                .Build();
        }
    }
}
