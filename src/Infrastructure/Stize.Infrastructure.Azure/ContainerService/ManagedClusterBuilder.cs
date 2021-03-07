using System;
using Pulumi;
using Pulumi.AzureNextGen.ContainerService.Latest;
using Pulumi.Random;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.ContainerService
{
    public class ManagedClusterBuilder : BaseBuilder<ManagedCluster>
    {

        public ManagedClusterArgs Arguments { get; private set; } = new ManagedClusterArgs();

        /// <summary>
        /// Creates a new instance of <see="ManagedClusterBuilder" />
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ManagedClusterBuilder(string name) : base(name)
        {
        }
        /// <summary>
        /// Creates a new instance of <see="ManagedClusterBuilder" />
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ManagedClusterBuilder(string name, ResourceContext context) : base(name, context)
        {
        }

        /// <summary>
        /// Creates a new instance of <see="ManagedClusterBuilder" />
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public ManagedClusterBuilder(string name, ManagedClusterArgs arguments) : this(name)
        {
            Arguments = arguments;
        }
        /// <summary>
        /// Creates a new instance of <see="ManagedClusterBuilder" />
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public ManagedClusterBuilder(string name, ManagedClusterArgs arguments, ResourceContext context) : this(name, context)
        {
            Arguments = arguments;
        }

        /// <summary>
        /// Builds and returns the underlying Pulumi object
        /// </summary>
        /// <param name="cro"></param>
        /// <returns></returns>
        public override ManagedCluster Build(CustomResourceOptions cro)
        {
            var cluster = new ManagedCluster(Name, Arguments, cro);
            return cluster;
        }
    }
}