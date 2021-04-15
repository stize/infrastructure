using System.Collections.Generic;
using System.Linq;
using Pulumi;
using Pulumi.AzureNative.Storage;
using Pulumi.Random;
using Stize.Infrastructure.Strategies;
using Inputs = Pulumi.AzureNative.Storage.Inputs;

namespace Stize.Infrastructure.Azure.Storage
{
    public class StorageAccountBuilder : BaseBuilder<StorageAccount>
    {
        /// <summary>
        /// Storage account arguments
        /// </summary>
        public StorageAccountArgs Arguments { get; private set; } = new StorageAccountArgs();

        /// <summary>
        /// Storage account network rules arguments
        /// </summary>
        public Inputs.NetworkRuleSetArgs NetworkRulesArguments { get; private set; } = new Inputs.NetworkRuleSetArgs();

        /// <summary>
        /// List of trusted Microsoft services that are allowed to bypass the network rules
        /// </summary>
        public List<Bypass> NetworkRulesExceptions = new List<Bypass>();
        
        /// <summary>
        /// Creates a new instance of <see cref="SubnetBuilder"/>
        /// </summary>
        /// <param name="name">Subnet internal name</param>
        public StorageAccountBuilder(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="SubnetBuilder"/>
        /// </summary>
        /// <param name="name">Subnet internal name</param>
        /// <param name="context">The resource context</param>
        public StorageAccountBuilder(string name, ResourceContext context) : base(name, context)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="SubnetBuilder"/>
        /// </summary>
        /// <param name="name">Subnet internal name</param>
        /// <param name="context">The resource context</param>
        /// <param name="cro">The CustomResourceOptions</param>
        public StorageAccountBuilder(string name, ResourceContext context, CustomResourceOptions cro) : base(name, context, cro)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="SubnetBuilder"/>
        /// </summary>
        /// <param name="name">Subnet internal name</param>
        public StorageAccountBuilder(string name, CustomResourceOptions cro) : base(name, cro)
        {
        }

        /// <summary>
        /// Generate the value required for NetworkRulesArguments.Bypass
        /// </summary>
        /// <param name="exceptions"></param>
        /// <returns></returns>
        private string GenerateNetworkRulesExceptions(List<Bypass> exceptions)
        {
            if(exceptions.Any())
            {
                return string.Join(",", exceptions.ConvertAll(e => e.ToString()));
            }
            return Bypass.None.ToString();
        }

        /// <summary>
        /// Creates the Pulumi StorageAccount resource object 
        /// </summary>
        /// <param name="cro">Custom Resource Options</param>
        /// <returns></returns>
        public override StorageAccount Build(CustomResourceOptions cro)
        {
            Arguments.AccountName = ResourceStrategy.Naming.GenerateName(Arguments.AccountName);
            NetworkRulesArguments.Bypass = GenerateNetworkRulesExceptions(NetworkRulesExceptions);
            Arguments.NetworkRuleSet = NetworkRulesArguments;
            ResourceStrategy.Tagging.AddTags(Arguments.Tags);
            var account = new StorageAccount(Name, Arguments, cro);
            return account;
        }

    }
}
