using System;
using Pulumi;
using Pulumi.AzureNextGen.ContainerService.Latest;

namespace Stize.Infrastructure.Azure.ContainerServices
{
    public class ManagedClusterBuilder : BaseBuilder<ManagedCluster>
    {
        /// <summary>
        /// ManagedCluster arguments
        /// </summary>
        public ManagedClusterArgs Arguments { get; private set; } = new ManagedClusterArgs();

        /// <summary>
        /// Creates a new instance of <see cref="ManagedClusterBuilder"/>
        /// </summary>
        /// <param name="name">ManagedCluster internal name</param>
        public ManagedClusterBuilder(string name) : base(name)
        {

        }
        /// <summary>
        /// Creates the Pulumi ManagedCluster resource object 
        /// </summary>
        /// <param name="cro">Custom Resource Options</param>
        /// <returns></returns>
        public override ManagedCluster Build(CustomResourceOptions cro)
        {
            var mgc = new ManagedCluster(Name, Arguments, cro);
            return mgc;
        }
    }
}