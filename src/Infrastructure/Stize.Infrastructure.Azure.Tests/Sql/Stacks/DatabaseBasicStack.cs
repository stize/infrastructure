using System;
using Pulumi;
using Pulumi.AzureNative.Sql;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Sql;

namespace Stize.Infrastructure.Tests.Azure.Sql.Stacks
{
    public class DatabaseBasicStack : Stack
    {
        public DatabaseBasicStack()
        {
            var rg = new ResourceGroupBuilder("rg1")
                .Name("rg1")
                .Location("westeurope")
                .Build();

            var server = new SqlServerBuilder("sql1")
                .Name("my-server")
                .Location("westeurope")
                .ResourceGroup(rg)
                .AdministratorLogin("admin")
                .AdministratorPassword("stize")
                .Parent(rg)
                .Build();

            var db = new SqlDatabaseBuilder("primaryDB")
                .Server(server.Name)
                .ResourceGroup(rg.Name)
                .Location(server.Location)
                .Name("primaryDB")
                .Parent(server)
                .SkuTier("Basic")
                .SkuServiceObjectiveName("S0")
                .StorageAccountType(Pulumi.AzureNative.Sql.RequestedBackupStorageRedundancy.Geo)
                .MaxDatabaseSizeGB(250)
                .MinCapacity(100)
                .DatabaseCollation("SQL_Latin1_General_CP1_CI_AS")
                .SampleData(SampleName.AdventureWorksLT)
                .Build();

            var secondary = new SqlDatabaseBuilder("secondaryDB")
                .Server("secondaryServer", "stize", "pa$5word")
                .ResourceGroup(rg.Name)
                .Location("westeurope")
                .Name("my-db")
                .SkuTier("Basic")
                .SkuServiceObjectiveName("S0")
                .CreateAsSecondary(db.Id)
                .SecondaryType(SecondaryType.Geo)
                .Build();
        }
    }
}
