using System;
using Pulumi;

namespace Stize.Infrastructure.Strategies
{
    /// <summary>
    /// Context on which the Pulumi resources are being created
    /// </summary>
    public class ResourceContext
    {
        /// <summary>
        /// RandomId value to use then generating resources
        /// </summary>
        public Input<string> RandomId { get; private set; }

        /// <summary>
        /// Environment these resources belongs to
        /// </summary>
        public Input<string> Environment { get; private set; }

        /// <summary>
        /// Who is managing the resources?
        /// </summary>
        public Input<string> ManagedBy { get; private set; } = "Stize";


        /// <summary>
        /// 
        /// </summary>
        public ResourceContext()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="ResourceContext"/>
        /// </summary>
        /// <param name="randomId"></param>
        /// <param name="environment"></param>
        /// <param name="managedBy"></param>
        public ResourceContext(Input<string> randomId)
        {
            RandomId = randomId;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ResourceContext"/>
        /// </summary>
        /// <param name="randomId"></param>
        /// <param name="environment"></param>
        /// <param name="managedBy"></param>
        public ResourceContext(Input<string> randomId, Input<string> environment, Input<string> managedBy = null)
        {
            RandomId = randomId;
            Environment = environment;
            ManagedBy = managedBy ?? ManagedBy;
        }
    }
}
