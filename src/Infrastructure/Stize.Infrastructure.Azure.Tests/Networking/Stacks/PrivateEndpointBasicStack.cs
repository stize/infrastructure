using Pulumi;
using Pulumi.AzureNative.Network;
using Stize.Infrastructure.Azure.Networking;
using Stize.Infrastructure.Azure.Storage;
using System;
using System.Collections.Generic;

namespace Stize.Infrastructure.Azure.Tests.Networking.Stacks
{
    class PrivateEndpointBasicStack : Stack
    {
        public PrivateEndpointBasicStack()
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
                .DisablePrivateEndpointNetworkPolicies()
                .Build();

            var storageAccount = new StorageAccountBuilder("sa1")
                .Name("sa1")
                .In(rg)
                .StandardLRS()
                .StorageV2()
                .DenyAccessByDefault()
                .Build();

            var zone = new PrivateDnsZoneBuilder("zone1")
                .ForBlobStorage()
                .In(rg)
                .LinkTo(vnet, "link1", true)
                .Build();

            var endpoint = new PrivateEndpointBuilder("pe1")
                .Name("pe1")
                .In(rg)
                .Location("uksouth")
                .InSubnet(subnet)
                .ForResource(storageAccount, "pe-connection", new InputList<string> { "blob" })
                //.WithARecord(storageAccount.Name, zone.Name, 3600)
                .Build();
        }
    }
}
