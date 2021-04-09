using FluentAssertions;
using Pulumi.AzureNative.KeyVault;
using Pulumi.Testing;
using Stize.Infrastructure.Tests.Azure.KeyVault.Stacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.KeyVault
{
    public class VaultTests
    {
        [Fact]
        public async Task CreateBasicVault()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().FirstOrDefault();
            vault.Should().NotBeNull("Vault not found");
        }

        [Fact]
        public async Task TenantIdIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().FirstOrDefault();
            (await vault.Properties.GetValueAsync()).TenantId.Should().Be("00000000-0000-0000-0000-000000000000");
        }

        [Fact]
        public async Task EnablePremiumIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().FirstOrDefault();
            (await vault.Properties.GetValueAsync()).Sku.Name.Should().Be("premium");
        }

        [Fact]
        public async Task EnablePurgeProtectionIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().FirstOrDefault();
            (await vault.Properties.GetValueAsync()).EnablePurgeProtection.Should().Be(true);
        }

        [Fact]
        public async Task SoftDeleteRetentionDaysIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().FirstOrDefault();
            (await vault.Properties.GetValueAsync()).EnableSoftDelete.Should().Be(true);
            (await vault.Properties.GetValueAsync()).SoftDeleteRetentionInDays.Should().Be(60);
        }

        [Fact]
        public async Task EnabledAccessibilityIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().FirstOrDefault();
            (await vault.Properties.GetValueAsync()).EnabledForTemplateDeployment.Should().Be(true);
            (await vault.Properties.GetValueAsync()).EnabledForDiskEncryption.Should().Be(true);
            (await vault.Properties.GetValueAsync()).EnabledForDeployment.Should().Be(true);
        }

        [Fact]
        public async Task AccessPolicyIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().FirstOrDefault();
            (await vault.Properties.GetValueAsync()).AccessPolicies[0].ObjectId.Should().Be("00000000-0000-0000-0000-000000000000");
            (await vault.Properties.GetValueAsync()).AccessPolicies[0].TenantId.Should().Be("00000000-0000-0000-0000-000000000000");
            (await vault.Properties.GetValueAsync()).AccessPolicies[0].ApplicationId.Should().BeNull();
            (await vault.Properties.GetValueAsync()).AccessPolicies[0].Permissions.Certificates.Should().HaveCount(1);
            (await vault.Properties.GetValueAsync()).AccessPolicies[0].Permissions.Keys.Should().HaveCount(5);
            (await vault.Properties.GetValueAsync()).AccessPolicies[0].Permissions.Secrets.Should().HaveCount(1);
            (await vault.Properties.GetValueAsync()).AccessPolicies[0].Permissions.Storage.Should().HaveCount(5);
        }

        [Fact]
        public async Task DefaultActionIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().FirstOrDefault();
            (await vault.Properties.GetValueAsync()).NetworkAcls?.DefaultAction.Should().Be("Allow");
        }

        [Fact]
        public async Task AllowedVNetsAreCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().FirstOrDefault();
            (await vault.Properties.GetValueAsync()).NetworkAcls?.VirtualNetworkRules.Should().HaveCount(1);
        }

        [Fact]
        public async Task AllowedIPAddressesAreCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().FirstOrDefault();
            (await vault.Properties.GetValueAsync()).NetworkAcls?.IpRules.Should().HaveCount(2);
        }

        [Fact]
        public async Task ProvisioningStateIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().FirstOrDefault();
            (await vault.Properties.GetValueAsync()).ProvisioningState.Should().Be("Succeeded");
        }

        [Fact]
        public async Task DisableSoftDeleteIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().LastOrDefault();
            (await vault.Properties.GetValueAsync()).EnableSoftDelete.Should().Be(false);
        }

        

        [Fact]
        public async Task RecoveryModeIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().LastOrDefault();
            (await vault.Properties.GetValueAsync()).CreateMode.Should().Be("recover");
        }

        [Fact]
        public async Task EnableRbacAuthIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().LastOrDefault();
            (await vault.Properties.GetValueAsync()).EnableRbacAuthorization.Should().Be(true);
        }

        [Fact]
        public async Task DisallowBypassIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<VaultBasicStack>(new VaultBasicMock(), new TestOptions { IsPreview = false });
            var vault = resources.OfType<Vault>().LastOrDefault();
            (await vault.Properties.GetValueAsync()).NetworkAcls?.Bypass.Should().Be("None");
        }
    }
}
