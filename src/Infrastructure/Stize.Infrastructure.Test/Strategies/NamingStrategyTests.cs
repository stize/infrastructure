using System;
using Stize.Infrastructure;
using Stize.Infrastructure.Test;
using Stize.Infrastructure.Strategies;
using Xunit;

namespace Stize.Infrastructure.Test
{
    public class NamingStrategyTests
    {
        [Fact( DisplayName = "Creates a name without RandomID and Environment")]
        public void CreatesNameWithourRidAndEnv()
        {            
            var expectValue = "myresource";

            INamingStrategy namingStrategy = new DefaultNamingStrategy();
            var name = namingStrategy.GenerateName(expectValue);

            name.OutputShould().Be(expectValue);
        }

        [Fact(DisplayName = "Creates a name with RandomId")]
        public void CreatesNameWithRid()
        {            
            var baseName = "myresource";
            var rid = "42dh33gh";
            var expectValue = $"{baseName}-{rid}";

            var context = new ResourceContext(rid);

            INamingStrategy namingStrategy = new DefaultNamingStrategy(context);
            var name = namingStrategy.GenerateName(baseName);
            name.OutputShould().Be(expectValue);
        }

        [Fact(DisplayName = "Creates a name with Environment")]
        public void CreatesNameWithEnv()
        {
            var baseName = "myresource";
            var env = "dev";
            var expectValue = $"{baseName}-{env}";
            var context = new ResourceContext(null, env);

            INamingStrategy namingStrategy = new DefaultNamingStrategy(context);
            var name = namingStrategy.GenerateName(baseName);

            name.OutputShould().Be(expectValue);
        }

        [Fact(DisplayName = "Creates a name with RandomId and Environment")]
        public void CreatesNameWithRidAndEnv()
        {
            var baseName = "myresource";
            var rid = "42dh33gh";
            var env = "dev";
            var expectValue = $"{baseName}-{env}-{rid}";
            var context = new ResourceContext(rid, env);

            INamingStrategy namingStrategy = new DefaultNamingStrategy(context);
            var name = namingStrategy.GenerateName(baseName);

            name.OutputShould().Be(expectValue);
        }
    }
}
