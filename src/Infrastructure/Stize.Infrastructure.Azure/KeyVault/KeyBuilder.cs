using Pulumi;
using Pulumi.AzureNative.KeyVault;
using Pulumi.AzureNative.KeyVault.Inputs;
using Stize.Infrastructure.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stize.Infrastructure.Azure.KeyVault
{
    public class KeyBuilder : BaseBuilder<Key>
    {
        /// <summary>
        /// Key arguments
        /// </summary>
        public KeyArgs Arguments { get; private set; } = new KeyArgs();

        public KeyPropertiesArgs Properties { get; private set; } = new KeyPropertiesArgs();

        public KeyAttributesArgs KeyAttributes { get; private set; } = new KeyAttributesArgs();

        /// <summary>
        /// Creates a new instance of <see="KeyBuilder" />
        /// </summary>
        /// <param name="name"></param>
        public KeyBuilder(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of <see="KeyBuilder" />
        /// </summary>
        public KeyBuilder(string name, ResourceContext context) : base(name, context)
        {
        }
        /// <summary>
        /// Creates a new instance of <see="KeyBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        public KeyBuilder(string name, KeyArgs arguments) : this(name)
        {
            Arguments = arguments;
        }
        /// <summary>
        /// Creates a new instance of <see="KeyBuilder" />
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public KeyBuilder(string name, KeyArgs arguments, ResourceContext context) : this(name, context)
        {
            Arguments = arguments;
        }

        public override Key Build(CustomResourceOptions cro)
        {
            Properties.Attributes = KeyAttributes;
            Arguments.Properties = Properties;
            var key = new Key(Name, Arguments, cro);
            return key;
        }
    }
}
