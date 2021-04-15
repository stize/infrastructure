using Pulumi;
using Pulumi.AzureNative.Network;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Networking
{
    public class PrivateEndpointBuilder : BaseBuilder<PrivateEndpoint>
    {
        /// <summary>
        /// Private endpoint Arguments
        /// </summary>
        public PrivateEndpointArgs Arguments { get; private set; } = new PrivateEndpointArgs();

       public PrivateDnsZoneRecordBuilder RecordBuilder { get; private set; } = new PrivateDnsZoneRecordBuilder("recordBuilder");
        /// <summary>
        /// Creates a new instance of <see cref="PrivateEndpointBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public PrivateEndpointBuilder(string name) : base(name)
        {
            
        }

        /// <summary>
        /// Creates a new instance of <see cref="PrivateEndpointBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        public PrivateEndpointBuilder(string name, ResourceContext context) : base(name, context)
        {

        }

        /// <summary>
        ///  Builds the A record using the record builder
        /// </summary>
        /// <param name="recordBuilder"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private RecordSet BuildARecord(PrivateDnsZoneRecordBuilder recordBuilder, Input<string> ipAddress)
        {
            recordBuilder.Name = recordBuilder.Arguments.RelativeRecordSetName.Apply(n => n).GetValueAsync().Result;
            return recordBuilder.CreateARecord(ipAddress)
                                .Build();
        }

        /// <summary>
        /// Creates the Pulumi Private Endpoint resource object and its corresponding A record resource object
        /// </summary>
        /// <param name="cro"></param>
        /// <returns></returns>
        public override PrivateEndpoint Build(CustomResourceOptions cro)
        {
            Arguments.PrivateEndpointName = ResourceStrategy.Naming.GenerateName(Arguments.PrivateEndpointName);
            ResourceStrategy.Tagging.AddTags(Arguments.Tags);
            var endpoint = new PrivateEndpoint(Name, Arguments, cro);
            var record = BuildARecord(RecordBuilder, endpoint.CustomDnsConfigs.Apply(configs => configs).First()
                                                                              .Apply(dns => dns.IpAddresses).First());
            return endpoint;
        }
    }
}
