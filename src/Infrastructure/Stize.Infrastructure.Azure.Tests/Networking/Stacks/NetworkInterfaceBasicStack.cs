using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Inputs = Pulumi.AzureNextGen.Network.Latest.Inputs;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Networking;
namespace Stize.Infrastructure.Tests.Azure.Networking.Stacks
{
    public class NetworkInterfaceBasicStack : Stack
    {
        public NetworkInterfaceBasicStack()
        {
            var rg = new ResourceGroupBuilder("rg1")
                .Name("rg1")
                .Location("westeurope")
                .Build();
            var vnet = new VNetBuilder("vnet1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("vnet1")
                .AddressSpace("172.16.0.0/24")
                .Build();
            var subnet = new SubnetBuilder("subnet1")
                .Parent(vnet)
                .Name("subnet1")
                .ResourceGroup(rg.Name)
                .InVNet(vnet.Name)
                .AddressPrefix("172.16.0.0/24")
                .Build();
            var nsg = new NetworkSecurityGroupBuilder("nsg1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("nsg1")
                .Build();
            var ni = new NetworkInterfaceBuilder("ni1")
                .Name("ni1")
                .ResourceGroup(rg.Name)
                .Location("westeurope")
                .IpConfigSubnetID(subnet.Name)
                .IpConfigName("ipconfig1")
                .IpConfigAddressVersion(IPVersion.IPv4)
                .IpConfigAllocationMethod(IPAllocationMethod.Dynamic)
                .NetworkSecurityGroup(nsg.Id)
                .Build();
        }
    }
}
