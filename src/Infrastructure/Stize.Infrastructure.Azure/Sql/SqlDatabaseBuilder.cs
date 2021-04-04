using Pulumi;
using Pulumi.AzureNative.Sql;
using Pulumi.AzureNative.Sql.Inputs;


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
        /// The database SKU arguments.
        /// </summary>
        public SkuArgs SkuArguments { get; private set; } = new SkuArgs();

        /// <summary>
        /// Used to store server arguments for when a new server is required (used in <see cref="SqlDatabaseExtensions.Server(SqlDatabaseBuilder, Input{string}, Input{string}, Input{string})"/>)
        /// </summary>
        public ServerArgs NewServerArgs { get; set; }

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
        /// Creates a new instance of <see="SqlDatabaseBuilder" />
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SqlDatabaseBuilder(string name, DatabaseArgs arguments, ResourceContext context) : this(name, context)
        {
            Arguments = arguments;
        }

        private void NewServerBuilder()
        {
            var secondaryServer = new SqlServerBuilder(this.NewServerArgs.ServerName.Apply(e => e).GetValueAsync().Result)
                .Name(this.NewServerArgs.ServerName)
                .ResourceGroup(this.Arguments.ResourceGroupName)
                .Location(this.Arguments.Location)
                .AdministratorLogin(this.NewServerArgs.AdministratorLogin)
                .AdministratorPassword(this.NewServerArgs.AdministratorLoginPassword)
                .Build();
            this.Arguments.ServerName = secondaryServer.Name;
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
            Arguments.Sku = SkuArguments;
            if (Arguments.ZoneRedundant == null) 
                Arguments.ZoneRedundant = false;
            if (Arguments.ServerName == null) 
                NewServerBuilder();
            var db = new Database(Name, Arguments, cro);
            return db;
        }
    }
}