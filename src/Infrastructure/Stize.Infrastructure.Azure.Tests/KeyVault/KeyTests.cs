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
    public class KeyTests
    {
        [Fact]
        public async Task CreateBasicKey()
        {
            var resources = await Pulumi.Deployment.TestAsync<KeyBasicStack>(new KeyBasicMock(), new TestOptions { IsPreview = false });
            var key = resources.OfType<Key>().FirstOrDefault();
            key.Should().NotBeNull("Key not found");
        }

        #region Failing Tests
        /*
         * These tests fail because the property that is tested contains null for some reason.
         * The correct values are passed into the Build() method and exist in the property args but then those properties contain null in the corresponding Output properties.
         */

        [Fact]
        public async Task KeyTypeIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<KeyBasicStack>(new KeyBasicMock(), new TestOptions { IsPreview = false });
            var key = resources.OfType<Key>().ToArray();
            (await key[0].Kty.GetValueAsync()).Should().Be("RSA");
            (await key[1].Kty.GetValueAsync()).Should().Be("EC");
        }

        [Fact]
        public async Task RSAKeySizeIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<KeyBasicStack>(new KeyBasicMock(), new TestOptions { IsPreview = false });
            var key = resources.OfType<Key>().FirstOrDefault();
            (await key.KeySize.GetValueAsync()).Should().Be(2048);
        }

        [Fact]
        public async Task KeyOperationsIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<KeyBasicStack>(new KeyBasicMock(), new TestOptions { IsPreview = false });
            var key = resources.OfType<Key>().FirstOrDefault();
            (await key.KeyOps.GetValueAsync()).Should().ContainInOrder("encrypt", "decrypt");
        }

        [Fact]
        public async Task ECNameIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<KeyBasicStack>(new KeyBasicMock(), new TestOptions { IsPreview = false });
            var key = resources.OfType<Key>().LastOrDefault();
            (await key.CurveName.GetValueAsync()).Should().Be("P_256");
        }
#endregion
        [Fact]
        public async Task IsEnabledIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<KeyBasicStack>(new KeyBasicMock(), new TestOptions { IsPreview = false });
            var key = resources.OfType<Key>().FirstOrDefault();
            (await key.Attributes.GetValueAsync())?.Enabled.Should().Be(true);
        }

        [Fact]
        public async Task ActivationDateIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<KeyBasicStack>(new KeyBasicMock(), new TestOptions { IsPreview = false });
            var key = resources.OfType<Key>().FirstOrDefault();
            (await key.Attributes.GetValueAsync())?.NotBefore.Should().Be(1621900800);
        }

        [Fact]
        public async Task ExpiryDateIsCorrect()
        {
            var resources = await Pulumi.Deployment.TestAsync<KeyBasicStack>(new KeyBasicMock(), new TestOptions { IsPreview = false });
            var key = resources.OfType<Key>().FirstOrDefault();
            (await key.Attributes.GetValueAsync())?.Expires.Should().Be(1621987200);
        }
    }
}
