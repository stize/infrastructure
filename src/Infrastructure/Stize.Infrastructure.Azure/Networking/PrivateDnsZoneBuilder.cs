using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Networking
{
    public class PrivateDnsZoneBuilder : BaseBuilder<PrivateZone>
    {
        /// <summary>
        /// Private DNS Zone Arguments
        /// </summary>
        public PrivateZoneArgs Arguments { get; private set; } = new PrivateZoneArgs();

        /// <summary>
        /// Creates a new instance of <see cref="PrivateDnsZoneBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public PrivateDnsZoneBuilder(string name) : base(name)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="PrivateDnsZoneBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        public PrivateDnsZoneBuilder(string name, ResourceContext context) : base(name, context)
        {

        }

        /// <summary>
        /// Creates the Pulumi Private DNS Zone resource object
        /// </summary>
        /// <param name="cro"></param>
        /// <returns></returns>
        public override PrivateZone Build(CustomResourceOptions cro)
        {
            ResourceStrategy.Tagging.AddTags(Arguments.Tags);
            var zone = new PrivateZone(Name, Arguments, cro);
            return zone;
        }
    }
}


