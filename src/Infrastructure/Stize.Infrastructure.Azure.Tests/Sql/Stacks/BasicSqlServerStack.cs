using System;
using Pulumi;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Sql;

namespace Stize.Infrastructure.Tests.Azure.Sql.Stacks
{
    public class SqlServerBasicStack : Stack
    {
        public SqlServerBasicStack()
        {

            var rg = new ResourceGroupBuilder("rg1")
            .Location("westeurope")
            .Build();

            var builder = new SqlServerBuilder("sql1");
            
            builder
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .AdministratorLogin("admin")
                .AdministratorPassword("stize");

            builder.Build();
        }
    }
}
