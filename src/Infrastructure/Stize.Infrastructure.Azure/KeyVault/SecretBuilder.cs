using System;
using System.Collections.Generic;
using System.Text;
using Pulumi;
using Pulumi.AzureNative.KeyVault;
using Pulumi.AzureNative.KeyVault.Inputs;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.KeyVault
{
    public class SecretBuilder : BaseBuilder<Secret>
    {
        /// <summary>
        /// Secret arguments
        /// </summary>
        public SecretArgs Arguments { get; private set; } = new SecretArgs();

        public SecretPropertiesArgs Properties { get; private set; } = new SecretPropertiesArgs();

        public SecretAttributesArgs SecretAttributes { get; private set; } = new SecretAttributesArgs();

        /// <summary>
        /// Creates a new instance of <see="SecretBuilder" />
        /// </summary>
        /// <param name="name"></param>
        public SecretBuilder(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of <see="SecretBuilder" />
        /// </summary>
        public SecretBuilder(string name, ResourceContext context) : base(name, context)
        {
        }
        /// <summary>
        /// Creates a new instance of <see="SecretBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        public SecretBuilder(string name, SecretArgs arguments) : this(name)
        {
            Arguments = arguments;
        }
        /// <summary>
        /// Creates a new instance of <see="SecretBuilder" />
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SecretBuilder(string name, SecretArgs arguments, ResourceContext context) : this(name, context)
        {
            Arguments = arguments;
        }

        public override Secret Build(CustomResourceOptions cro)
        {
            Properties.Attributes = SecretAttributes;
            Arguments.Properties = Properties;
            var secret = new Secret(Name, Arguments, cro);
            return secret;
        }
    }
}
