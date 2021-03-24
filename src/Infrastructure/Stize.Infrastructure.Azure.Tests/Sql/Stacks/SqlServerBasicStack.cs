using System;
using Pulumi;
using Pulumi.AzureNative.Sql;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Sql;

namespace Stize.Infrastructure.Tests.Azure.Sql.Stacks
{
    public class SqlServerBasicStack : Stack
    {
        public SqlServerBasicStack()
        {

            var rg = new ResourceGroupBuilder("rg1")
            .Name("rg1")
            .Location("westeurope")
            .Build();

            var builder = new SqlServerBuilder("sql1");

            builder
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("sql1")
                .AdministratorLogin("stize")
                .AdministratorPassword("pa$5word")
                .IdentityType(IdentityType.SystemAssigned)
                .PublicNetworkAccess(ServerPublicNetworkAccess.Enabled)
                .TLSVersion_1_0();
            builder.Build();
        }
    }
}
