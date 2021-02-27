using System;
using Pulumi;
using Pulumi.AzureNextGen.Storage.Latest;

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
        /// Creates the Pulumi StorageAccount resource object 
        /// </summary>
        /// <param name="cro">Custom Resource Options</param>
        /// <returns></returns>
        public override StorageAccount Build(CustomResourceOptions cro)
        {
            var subnet = new StorageAccount(Name, Arguments, cro);
            return subnet;
        }       
    }
}
