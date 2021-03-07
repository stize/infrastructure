using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Inputs = Pulumi.AzureNextGen.Network.Latest.Inputs;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Networking;
namespace Stize.Infrastructure.Azure.Tests.Networking.Stacks
{
    public class NetworkInterfaceBasicStack : Stack
    {
        public NetworkInterfaceBasicStack()
        {
            var tags = new InputMap<string> { { "env", "dev" } };

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
            var subnet1 = new SubnetBuilder("subnet1")
                .Parent(vnet)
                .Name("subnet1")
                .ResourceGroup(rg.Name)
                .InVNet(vnet.Name)
                .AddressPrefix("172.16.0.0/26")
                .Build();
            var subnet2 = new SubnetBuilder("subnet2")
                .Parent(vnet)
                .Name("subnet2")
                .ResourceGroup(rg.Name)
                .InVNet(vnet.Name)
                .AddressPrefix("172.16.0.64/26")
                .Build();
            var nsg = new NetworkSecurityGroupBuilder("nsg1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("nsg1")
                .Build();
            var nic1 = new NetworkInterfaceBuilder("nic1")
                .Name("nic1")
                .ResourceGroup(rg.Name)
                .Build();
            var nic2 = new NetworkInterfaceBuilder("nic2")
                .Name("nic2")
                .ResourceGroup(rg.Name)
                .Location("westeurope")
                .IpConfigSubnetID(subnet2.Id)
                .IpConfigName("ipconfig1")
                .IpConfigAddressVersion(IPVersion.IPv4)
                .IpConfigAllocationMethod(IPAllocationMethod.Dynamic)
                .EnableAcceleratedNetworking(true)
                .EnableIPForwarding(true)
                .NetworkSecurityGroup(nsg.Id)
                .ExtendedLocation(new Inputs.ExtendedLocationArgs { Name = "easteurope", Type = ExtendedLocationTypes.EdgeZone})
                .DnsSettings(new Inputs.NetworkInterfaceDnsSettingsArgs { })
                .Tags(tags)
                .Build();
        }
    }
}
