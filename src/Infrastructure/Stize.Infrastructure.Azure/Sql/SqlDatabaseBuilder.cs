using Pulumi;
using Pulumi.AzureNextGen.Sql.Latest;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Sql
{
    /// <summary>
    /// Azure SQL database builder
    /// </summary>
    public class SqlDatabaseBuilder : BaseBuilder<Database>
    {
        /// <summary>
        /// Database arguments
        /// </summary>
        /// <returns></returns>
        public DatabaseArgs Arguments {get; private set; } = new DatabaseArgs();

        /// <summary>
        /// Creates a new instance of <see="SqlDatabaseBuilder" />
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SqlDatabaseBuilder(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of <see="SqlServerBuilder" />
        /// </summary>
        public SqlDatabaseBuilder(string name, ResourceContext context) : base(name, context)
        {
        }

        public SqlDatabaseBuilder(string name, DatabaseArgs arguments) : this(name)
        {
            Arguments = arguments;
        }

        /// <summary>
        /// Creates the Pulumi database object
        /// </summary>
        /// <param name="cro">Database's CustomResourceOptions</param>
        /// <returns></returns>
        public override Database Build(CustomResourceOptions cro)
        {
            Arguments.DatabaseName = ResourceStrategy.Naming.GenerateName(Arguments.DatabaseName);
            ResourceStrategy.Tagging.AddTags(Arguments.Tags);
            var db = new Database(Name, Arguments, cro);
            return db;
        }
    }
}