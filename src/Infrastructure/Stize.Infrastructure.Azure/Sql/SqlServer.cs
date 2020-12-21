using System;
using Pulumi;
using Pulumi.Azure.Sql;

namespace Stize.Infrastructure.Azure.Sql
{
    /// <summary>
    /// Builder responsible for the creationg of SQL Server databases in Azure
    /// </summary>
    public class SqlServerBuilder : BaseBuilder<SqlServer>
    {
        /// <summary>
        /// The database arguments
        /// </summary>
        /// <value></value>
        public SqlServerArgs Arguments { get; private set; } = new SqlServerArgs();

        /// <summary>
        /// Creates a new instance of <see="SqlServerBuilder" />
        /// </summary>
        public SqlServerBuilder(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of <see="SqlServerBuilder" />
        /// </summary>
        /// <param name="arguments">Default arguments to use</param>
        public SqlServerBuilder(string name, SqlServerArgs arguments) : base(name)
        {
            Arguments = arguments;
        }

        /// <summary>
        /// Builds the SQL Server Resource
        /// </summary>
        /// <param name="cro"></param>
        /// <returns></returns>
        public override SqlServer Build(CustomResourceOptions cro)
        {
            var sql = new SqlServer(Name, Arguments, cro);
            return sql;
        }
    }
}