using System;
using Pulumi.AzureAD;
using Pulumi.AzureNative.ContainerService.Inputs;
using Pulumi.Random;
using Pulumi.Tls;

namespace Stize.Infrastructure.Azure.ContainerService
{
    public static class ManagedClusterExtensions
    {
        public static ManagedClusterBuilder WithNewAppAndServicePrincipal(this ManagedClusterBuilder builder)
        {
            var adApp = new Application($"{builder.Name}-app");

            var adSp = new ServicePrincipal($"{builder.Name}-sp", new ServicePrincipalArgs
            {
                ApplicationId = adApp.Id
            });

            var password = new RandomPassword($"{builder.Name}-password", new RandomPasswordArgs
            {
                Length = 20,
                Special = true
            });

            var adSpPassword = new ServicePrincipalPassword($"{builder.Name}-sp-password", new ServicePrincipalPasswordArgs
            {
                ServicePrincipalId = adSp.Id,
                Value = password.Result,
                EndDateRelative = "8760h" // 1 year from the creation of this password
            });

            builder.Arguments.ServicePrincipalProfile = new ManagedClusterServicePrincipalProfileArgs
            {
                ClientId = adApp.ApplicationId,
                Secret = adSpPassword.Value
            };            
            return builder;
        }

        public static ManagedClusterBuilder WithNewRsaKey(this ManagedClusterBuilder builder, int rsaBits = 4096)
        {
            var sshKey = new PrivateKey($"{builder.Name}-ssh-key", new PrivateKeyArgs
            {
                Algorithm = "RSA",
                RsaBits = rsaBits
            });

            builder.Arguments.LinuxProfile = new ContainerServiceLinuxProfileArgs
            {
                AdminUsername = "stize",
                Ssh = new ContainerServiceSshConfigurationArgs
                {
                    PublicKeys = 
                    {
                        new ContainerServiceSshPublicKeyArgs
                        {
                            KeyData = sshKey.PublicKeyOpenssh,
                        }                        
                    }
                }
            };

            return builder;
        }
    }
}