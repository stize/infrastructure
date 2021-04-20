using Pulumi;
using Pulumi.AzureNative.Network.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class PrivateDnsZoneRecordExtensions
    {
        /// <summary>
        /// Set the name of the record
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name">Set the name of the record</param>
        /// <returns></returns>
        public static PrivateDnsZoneRecordBuilder Name(this PrivateDnsZoneRecordBuilder builder, Input<string> name)
        {
            builder.Arguments.RelativeRecordSetName = name;
            return builder;
        }

        /// <summary>
        /// Set the resource group of the record
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resourceGroup"></param>
        /// <returns></returns>
        public static PrivateDnsZoneRecordBuilder In(this PrivateDnsZoneRecordBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Set the resource group of the record
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="privateDnsZone"></param>
        /// <returns></returns>
        public static PrivateDnsZoneRecordBuilder InPrivateDnsZone(this PrivateDnsZoneRecordBuilder builder, Input<string> privateDnsZone)
        {
            builder.Arguments.ZoneName = privateDnsZone;
            return builder;
        }

        /// <summary>
        /// Set the type of the record
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="recordType"></param>
        /// <returns></returns>
        public static PrivateDnsZoneRecordBuilder SetRecordType(this PrivateDnsZoneRecordBuilder builder, Input<string> recordType)
        {
            builder.Arguments.RecordType = recordType;
            return builder;
        }

        /// <summary>
        /// Create an A record
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="ipAddress">IP address for the A record</param>
        /// <returns></returns>
        public static PrivateDnsZoneRecordBuilder CreateARecord(this PrivateDnsZoneRecordBuilder builder, Input<string> ipAddress)
        {
            builder.Arguments.ARecords.Add(new ARecordArgs { Ipv4Address = ipAddress });
            return SetRecordType(builder, "A");
        }

        /// <summary>
        /// Create a CNAME record
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="cname"></param>
        /// <returns></returns>
        public static PrivateDnsZoneRecordBuilder CreateCname(this PrivateDnsZoneRecordBuilder builder, Input<string> cname)
        {
            builder.Arguments.CnameRecord = new CnameRecordArgs { Cname = cname };
            return SetRecordType(builder, "CNAME");
        }

        /// <summary>
        /// Set the Time to Live in seconds
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="timeToLive">Time to live in seconds</param>
        /// <returns></returns>
        public static PrivateDnsZoneRecordBuilder TimeToLive(this PrivateDnsZoneRecordBuilder builder, Input<double> timeToLive)
        {
            builder.Arguments.Ttl = timeToLive;
            return builder;
        }
    }
}
