using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Pulumi.AzureNextGen.Network.Latest.Inputs;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Networking
{
    public class PrivateEndpointBuilder : BaseBuilder<PrivateEndpoint>
    {
        /// <summary>
        /// Private endpoint Arguments
        /// </summary>
        public PrivateEndpointArgs Arguments { get; private set; } = new PrivateEndpointArgs();

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
        /// Creates the Pulumi Private Endpoint resource object
        /// </summary>
        /// <param name="cro"></param>
        /// <returns></returns>
        public override PrivateEndpoint Build(CustomResourceOptions cro)
        {
            Arguments.PrivateEndpointName = ResourceStrategy.Naming.GenerateName(Arguments.PrivateEndpointName);
            ResourceStrategy.Tagging.AddTags(Arguments.Tags);
            var endpoint = new PrivateEndpoint(Name, Arguments, cro);
            return endpoint;
        }
    }
}
