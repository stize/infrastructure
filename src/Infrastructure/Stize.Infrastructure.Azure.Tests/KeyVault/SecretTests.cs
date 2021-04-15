using FluentAssertions;
using Pulumi.AzureNative.KeyVault;
using Pulumi.Testing;
using Stize.Infrastructure.Tests.Azure.KeyVault.Stacks;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Stize.Infrastructure.Tests.Azure.KeyVault
{
    public class SecretTests
    {
        [Fact]
        public async Task CreateBasicSecret()
        {
            var resources = await Pulumi.Deployment.TestAsync<SecretBasicStack>(new SecretBasicMock(), new TestOptions { IsPreview = false });
            var secret = resources.OfType<Secret>().FirstOrDefault();
            secret.Should().NotBeNull("Secret not found");
        }

        [Fact]
        public async Task ValueIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<SecretBasicStack>(new SecretBasicMock(), new TestOptions { IsPreview = false });
            var secret = resources.OfType<Secret>().FirstOrDefault();
            (await secret.Properties.GetValueAsync()).Value.Should().Be("supermegasecret");
        }

        [Fact]
        public async Task ContentTypeIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<SecretBasicStack>(new SecretBasicMock(), new TestOptions { IsPreview = false });
            var secret = resources.OfType<Secret>().FirstOrDefault();
            (await secret.Properties.GetValueAsync()).ContentType.Should().Be("Contains a secret.");
        }

        [Fact]
        public async Task IsEnabledIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<SecretBasicStack>(new SecretBasicMock(), new TestOptions { IsPreview = false });
            var secret = resources.OfType<Secret>().FirstOrDefault();
            (await secret.Properties.GetValueAsync()).Attributes?.Enabled.Should().Be(true);
        }

        [Fact]
        public async Task ActivationDateIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<SecretBasicStack>(new SecretBasicMock(), new TestOptions { IsPreview = false });
            var secret = resources.OfType<Secret>().FirstOrDefault();
            (await secret.Properties.GetValueAsync()).Attributes?.NotBefore.Should().Be(1621900800);
        }

        [Fact]
        public async Task ExpiryDateIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<SecretBasicStack>(new SecretBasicMock(), new TestOptions { IsPreview = false });
            var secret = resources.OfType<Secret>().FirstOrDefault();
            (await secret.Properties.GetValueAsync()).Attributes?.Expires.Should().Be(1621987200);
        }
    }
}
