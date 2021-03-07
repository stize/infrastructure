using System;
using Pulumi;
using Pulumi.AzureNextGen.Storage.Latest;
using Pulumi.Random;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Storage
{
    public class StorageAccountBuilder : BaseBuilder<StorageAccount>
    {
        /// <summary>
        /// VNet arguments
        /// </summary>
        public StorageAccountArgs Arguments { get; private set; } = new StorageAccountArgs();

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
        /// Creates the Pulumi StorageAccount resource object 
        /// </summary>
        /// <param name="cro">Custom Resource Options</param>
        /// <returns></returns>
        public override StorageAccount Build(CustomResourceOptions cro)
        {
            Arguments.AccountName = ResourceStrategy.Naming.GenerateName(Arguments.AccountName);
            ResourceStrategy.Tagging.AddTags(Arguments.Tags);
            var account = new StorageAccount(Name, Arguments, cro);
            return account;
        }

    }
}
