using System;
using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Pulumi.Random;
using Inputs = Pulumi.AzureNextGen.Network.Latest.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public class SecurityRuleBuilder : BaseBuilder<SecurityRule>
    {
        /// <summary>
        /// Security Rule arguments
        /// </summary>
        public SecurityRuleArgs Arguments { get; private set; } = new SecurityRuleArgs();

        /// <summary>
        /// Creates a new instance of <see cref="SecurityRuleBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public SecurityRuleBuilder(string name) : base(name)
        {

        }
        /// <summary>
        /// Creates a new instance of <see cref="SecurityRuleBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public SecurityRuleBuilder(string name, RandomId rid) : base(name, rid)
        {

        }

        /// <summary>
        /// Builds the Security rule
        /// </summary>
        /// <param name="cro">Custom Resource Object</param>
        /// <returns></returns>
        public override SecurityRule Build(CustomResourceOptions cro)
        {
            var sr = new SecurityRule(Name, Arguments, cro);
            return sr;
        }
    }
}
