using Pulumi;
using Pulumi.AzureNative.KeyVault;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stize.Infrastructure.Azure.KeyVault
{
    public static class KeyExtensions
    {
        /// <summary>
        /// Assigns the Key to a resource group
        /// </summary>
        /// <param name="builder">Key builder</param>
        /// <param name="resourceGroup">Resource group name</param>
        /// <returns></returns>
        public static KeyBuilder ResourceGroup(this KeyBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Sets the Key tags
        /// </summary>
        /// <param name="builder">Key builder</param>
        /// <param name="tags">Key tags</param>
        /// <returns></returns>
        public static KeyBuilder Tags(this KeyBuilder builder, InputMap<string> tags)
        {
            builder.Arguments.Tags = tags;
            return builder;
        }

        /// <summary>
        /// Name of the Vault
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name">Name of the Vault</param>
        /// <returns></returns>
        public static KeyBuilder VaultName(this KeyBuilder builder, Input<string> name)
        {
            builder.Arguments.VaultName = name;
            return builder;
        }

        /// <summary>
        /// Name of the Key
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name">Name of the Key</param>
        /// <returns></returns>
        public static KeyBuilder KeyName(this KeyBuilder builder, Input<string> name)
        {
            builder.Arguments.KeyName = name;
            return builder;
        }

        /// <summary>
        /// The type of the key. For valid values, see <see cref="JsonWebKeyType"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="type"> The type of the key. For valid values, see <see cref="JsonWebKeyType"/>.</param>
        /// <returns></returns>
        public static KeyBuilder KeyType(this KeyBuilder builder, InputUnion<string, JsonWebKeyType> type)
        {
            builder.Properties.Kty = type;
            return builder;
        }

        /// <summary>
        /// The key size in bits. For example: 2048, 3072, or 4096 for RSA.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="size">The key size in bits. For example: 2048, 3072, or 4096 for RSA.</param>
        /// <returns></returns>
        public static KeyBuilder KeySizeInBits(this KeyBuilder builder, Input<int> size)
        {
            builder.Properties.KeySize = size;
            return builder;
        }

        /// <summary>
        /// Permitted Operations for the Key. For valid values, see <see cref="JsonWebKeyOperation"/>.
        /// The params InputUnion<string, <see cref="JsonWebKeyOperation"/>>[] means that the user can list the desired cryptographic operations one after another.
        /// For example:
        /// 'KeyOps("encrypt", "decrypt", "sign", "verify" "wrapKey")'
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="ops">Add an operation to the Key. For valid values, see <see cref="JsonWebKeyOperation"/>.</param>
        /// <returns></returns>
        public static KeyBuilder KeyOps(this KeyBuilder builder, params InputUnion<string, JsonWebKeyOperation>[] ops)
        {
            builder.Properties.KeyOps = ops;
            return builder;
        }

        /// <summary>
        /// The elliptic curve name. For valid values, see <see cref="JsonWebKeyCurveName"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="ops"></param>
        /// <returns></returns>
        public static KeyBuilder CurveName(this KeyBuilder builder, params InputUnion<string, JsonWebKeyOperation>[] ops)
        {
            builder.Properties.KeyOps = ops;
            return builder;
        }

        /// <summary>
        /// Property to toggle the accessibility of the Key.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="isEnabled">Set to true to enable the Key; set to false to disable the Key.</param>
        /// <returns></returns>
        public static KeyBuilder IsEnabled(this KeyBuilder builder, Input<bool> isEnabled)
        {
            builder.KeyAttributes.Enabled = isEnabled;
            return builder;
        }

        /// <summary>
        /// Sets the expiry date of the Key
        /// ISO 8601 format, example: '2021-04-08T13:00:00.00+01:00'
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dateTime">The date and time, in ISO 8601 format, that the Key will expire.</param>
        /// <returns></returns>
        public static KeyBuilder ExpiryDate(this KeyBuilder builder, Input<string> dateTime)
        {
            string dt = dateTime.Apply(e => e).GetValueAsync().Result;
            var seconds = ISO8601toTotalSeconds(dt);
            builder.KeyAttributes.Expires = seconds;
            return builder;
        }

        /// <summary>
        /// Sets the expiry date of the Key
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seconds">Expiry date in seconds since 1970.</param>
        /// <returns></returns>
        public static KeyBuilder ExpiryDate(this KeyBuilder builder, Input<double> seconds)
        {
            builder.KeyAttributes.Expires = seconds;
            return builder;
        }

        /// <summary>
        /// Sets the activation date of the Key.
        /// ISO 8601 format, example: '2021-04-08T13:00:00.00+01:00'
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dateTime">The date and time, in ISO 8601 format, that the Key will activate.</param>
        /// <returns></returns>
        public static KeyBuilder ActivationDate(this KeyBuilder builder, Input<string> dateTime)
        {
            string dt = dateTime.Apply(e => e).GetValueAsync().Result;
            var seconds = ISO8601toTotalSeconds(dt);
            builder.KeyAttributes.NotBefore = seconds;
            return builder;
        }

        /// <summary>
        /// Sets the activation date of the Key
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seconds">Activation date in seconds since 1970.</param>
        /// <returns></returns>
        public static KeyBuilder ActivationDate(this KeyBuilder builder, Input<double> seconds)
        {
            builder.KeyAttributes.NotBefore = seconds;
            return builder;
        }

        /// <summary>
        /// Method to convert ISO 8601 date time to total seconds since 1970.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static double? ISO8601toTotalSeconds(string dateTime)
        {
            DateTime dt = DateTime.Parse(dateTime, styles: System.Globalization.DateTimeStyles.AdjustToUniversal);
            TimeSpan span = dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            double secondsSince1970 = span.TotalSeconds;
            return secondsSince1970;
        }
    }
}
