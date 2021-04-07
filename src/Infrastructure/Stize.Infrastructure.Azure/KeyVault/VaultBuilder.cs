using System;
using System.Collections.Generic;
using System.Text;
using Pulumi;
using Pulumi.AzureNative.KeyVault;
using Pulumi.AzureNative.KeyVault.Inputs;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.KeyVault
{
    public class VaultBuilder : BaseBuilder<Vault>
    {
        
        /// <summary>
        /// Vault arguments
        /// </summary>
        public VaultArgs Arguments { get; private set; } = new VaultArgs();

        /// <summary>
        /// Vault properties
        /// </summary>
        public VaultPropertiesArgs Properties { get; private set; } = new VaultPropertiesArgs() 
        {
            // Default Sku that is used if EnablePremium() isn't called
            Sku = new SkuArgs 
            {                
                Name = SkuName.Standard, Family = SkuFamily.A
            },
            
        };

        /// <summary>
        /// Creates a new instance of <see="VaultBuilder" />
        /// </summary>
        /// <param name="name"></param>
        public VaultBuilder(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of <see="VaultBuilder" />
        /// </summary>
        public VaultBuilder(string name, ResourceContext context) : base(name, context)
        {
        }
        /// <summary>
        /// Creates a new instance of <see="VaultBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        public VaultBuilder(string name, VaultArgs arguments) : this(name)
        {
            Arguments = arguments;
        }
        /// <summary>
        /// Creates a new instance of <see="VaultBuilder" />
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public VaultBuilder(string name, VaultArgs arguments, ResourceContext context) : this(name, context)
        {
            Arguments = arguments;
        }

        public override Vault Build(CustomResourceOptions cro)
        {
            Arguments.Properties = Properties;
            var vault = new Vault(Name, Arguments, cro);
            return vault;
        }
    }
}
