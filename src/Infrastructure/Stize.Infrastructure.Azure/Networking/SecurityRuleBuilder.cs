using System;
using Pulumi;
using Pulumi.AzureNative.Network;
using Stize.Infrastructure.Strategies;

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
        public SecurityRuleBuilder(string name, ResourceContext context) : base(name, context)
        {
        }

        /// <summary>
        /// Builds the Security rule
        /// </summary>
        /// <param name="cro">Custom Resource Object</param>
        /// <returns></returns>
        public override SecurityRule Build(CustomResourceOptions cro)
        {
            Arguments.SecurityRuleName = ResourceStrategy.Naming.GenerateName(Arguments.SecurityRuleName);
            var sr = new SecurityRule(Name, Arguments, cro);
            return sr;
        }
    }
}
