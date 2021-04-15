using Pulumi;
using Pulumi.AzureNative.Network;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Networking
{
    public class PrivateDnsZoneRecordBuilder : BaseBuilder<RecordSet>
    {
        /// <summary>
        /// DNS Record Set Arguments
        /// </summary>
        public RecordSetArgs Arguments { get; private set; } = new RecordSetArgs();

        /// <summary>
        /// Creates a new instance of <see cref="PrivateDnsZoneRecordBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public PrivateDnsZoneRecordBuilder(string name) : base(name)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="PrivateDnsZoneRecordBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        public PrivateDnsZoneRecordBuilder(string name, ResourceContext context) : base(name, context)
        {

        }

        /// <summary>
        /// Creates the Pulumi Private DNS Zone Record Set resource object
        /// </summary>
        /// <param name="cro"></param>
        /// <returns></returns>
        public override RecordSet Build(CustomResourceOptions cro)
        {
            Arguments.RelativeRecordSetName = ResourceStrategy.Naming.GenerateName(Arguments.RelativeRecordSetName);
            var recordSet = new RecordSet(Name, Arguments, cro);
            return recordSet;
        }
    }
}
