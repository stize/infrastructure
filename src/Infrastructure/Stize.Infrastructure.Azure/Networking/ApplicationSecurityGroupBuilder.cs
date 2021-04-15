﻿using Pulumi;
using Pulumi.AzureNative.Network;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Networking
{
    public class ApplicationSecurityGroupBuilder : BaseBuilder<ApplicationSecurityGroup>
    {
        /// <summary>
        /// NI arguments
        /// </summary>
        public ApplicationSecurityGroupArgs Arguments { get; private set; } = new ApplicationSecurityGroupArgs();

        /// <summary>
        /// Creates a new instance of <see cref="ApplicationSecurityGroupBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public ApplicationSecurityGroupBuilder(string name) : base(name)
        {

        }
        /// <summary>
        /// Creates a new instance of <see cref="ApplicationSecurityGroupBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public ApplicationSecurityGroupBuilder(string name, ResourceContext context) : base(name, context)
        {
        }

        /// <summary>
        /// Builds the ASG
        /// </summary>
        /// <param name="cro">Custom Resource Object</param>
        /// <returns>ASG</returns>
        public override ApplicationSecurityGroup Build(CustomResourceOptions cro)
        {
            var asg = new ApplicationSecurityGroup(Name, Arguments, cro);
            return asg;
        }
    }
}
