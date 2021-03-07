using System;
using Pulumi;
using Pulumi.AzureNextGen.Sql.Latest;
using Pulumi.Random;

namespace Stize.Infrastructure.Azure.Sql
{
    /// <summary>
    /// Builder responsible for the creationg of SQL Server databases in Azure
    /// </summary>
    public class SqlServerBuilder : BaseBuilder<Server>
    {
        /// <summary>
        /// The database arguments
        /// </summary>
        /// <value></value>
        public ServerArgs Arguments { get; private set; } = new ServerArgs();

        /// <summary>
        /// Indicates the replica location
        /// </summary>
        /// <value></value>
        public Input<string> ReplicaLocation {get; set; }

        /// <summary>
        /// Creates a new instance of <see="SqlServerBuilder" />
        /// </summary>
        public SqlServerBuilder(string name) : base(name)
        {            
            Arguments.Version = "12.0";
        }
        /// <summary>
        /// Creates a new instance of <see="SqlServerBuilder" />
        /// </summary>
        public SqlServerBuilder(string name, RandomId rid) : base(name, rid)
        {
            Arguments.Version = "12.0";
        }

        /// <summary>
        /// Creates a new instance of <see="SqlServerBuilder" />
        /// </summary>
        /// <param name="arguments">Default arguments to use</param>
        public SqlServerBuilder(string name, ServerArgs arguments) : base(name)
        {
            Arguments = arguments;
        }
        /// <summary>
        /// Creates a new instance of <see="SqlServerBuilder" />
        /// </summary>
        /// <param name="arguments">Default arguments to use</param>
        public SqlServerBuilder(string name, ServerArgs arguments, RandomId rid) : base(name, rid)
        {
            Arguments = arguments;
        }

        /// <summary>
        /// Builds the SQL Server Resource
        /// </summary>
        /// <param name="cro"></param>
        /// <returns></returns>
        public override Server Build(CustomResourceOptions cro)
        {
            var sql = new Server(Name, Arguments, cro);            
            return sql;
        }
    }
}