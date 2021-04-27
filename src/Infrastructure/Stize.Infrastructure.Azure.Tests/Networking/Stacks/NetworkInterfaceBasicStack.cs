using Pulumi;
using Pulumi.AzureNative.Network;
using Inputs = Pulumi.AzureNative.Network.Inputs;
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
                .AddDynamicIPConfiguration("ipconfig1", subnet2.Id, false)
                .AddStaticIPConfiguration("ipconfig2", subnet2.Id, "172.16.0.69", true)
                .EnableAcceleratedNetworking(true)
                .EnableIPForwarding(true)
                .NetworkSecurityGroup(nsg.Id)
                .ExtendedLocation("edgelocation")
                .DnsSettings("dnsLabel", "192.32.0.0")
                .Tags(tags)
                .Build();
        }
    }
}
