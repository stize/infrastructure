using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pulumi.Testing;
using Xunit;
using Pulumi;
using Stize.Infrastructure.Azure.Tests.Networking.Stacks;
using Pulumi.AzureNative.Network;
using Stize.Infrastructure.Test;

namespace Stize.Infrastructure.Azure.Tests.Networking
{
    public class PrivateDnsZoneRecordTests
    {
        /// <summary>
        /// Checks that the resource is created correctly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateBasicPrivateDnsZoneRecord()
        {
            var resources = await Deployment.TestAsync<PrivateDnsZoneRecordBasicStack>(new PrivateDnsZoneRecordBasicMock(), new TestOptions { IsPreview = false });
            var record = resources.OfType<RecordSet>().FirstOrDefault();

            record.Should().NotBeNull("An A record should be created");
            record.Name.OutputShould().Be("record1");
        }

        [Fact]
        public async Task ARecordIsCorrect()
        {
            var resources = await Deployment.TestAsync<PrivateDnsZoneRecordBasicStack>(new PrivateDnsZoneRecordBasicMock(), new TestOptions { IsPreview = false });
            var record = resources.OfType<RecordSet>().FirstOrDefault();
            (await record.ARecords.GetValueAsync())[0].Ipv4Address.Should().Be("0.0.0.0");
        }

        [Fact]
        public async Task TtlIsCorrect()
        {
            var resources = await Deployment.TestAsync<PrivateDnsZoneRecordBasicStack>(new PrivateDnsZoneRecordBasicMock(), new TestOptions { IsPreview = false });
            var record = resources.OfType<RecordSet>().FirstOrDefault();
            (await record.Ttl.GetValueAsync()).Should().Be(3600);
        }

        [Fact]
        public async Task CNameIsCorrect()
        {
            var resources = await Deployment.TestAsync<PrivateDnsZoneRecordBasicStack>(new PrivateDnsZoneRecordBasicMock(), new TestOptions { IsPreview = false });
            var record = resources.OfType<RecordSet>().FirstOrDefault();
            (await record.CnameRecord.GetValueAsync())?.Cname.Should().Be("");
        }
    }
}
