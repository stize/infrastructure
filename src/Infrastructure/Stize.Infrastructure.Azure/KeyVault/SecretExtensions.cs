using Pulumi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stize.Infrastructure.Azure.KeyVault
{
    public static class SecretExtensions
    {
        /// <summary>
        /// Assigns the Secret to a resource group
        /// </summary>
        /// <param name="builder">Secret builder</param>
        /// <param name="resourceGroup">Resource group name</param>
        /// <returns></returns>
        public static SecretBuilder ResourceGroup(this SecretBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Sets the Secret tags
        /// </summary>
        /// <param name="builder">Secret builder</param>
        /// <param name="tags">Secret tags</param>
        /// <returns></returns>
        public static SecretBuilder Tags(this SecretBuilder builder, InputMap<string> tags)
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
        public static SecretBuilder VaultName(this SecretBuilder builder, Input<string> name)
        {
            builder.Arguments.VaultName = name;
            return builder;
        }

        /// <summary>
        /// Name of the Secret
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name">Name of the Secret</param>
        /// <returns></returns>
        public static SecretBuilder Name(this SecretBuilder builder, Input<string> name)
        {
            builder.Arguments.SecretName = name;
            return builder;
        }

        /// <summary>
        /// Sets the value of the Secret (i.e., password, code, etc).
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value">Value of the Secret</param>
        /// <returns></returns>
        public static SecretBuilder Value(this SecretBuilder builder, Input<string> value)
        {
            builder.Properties.Value = value;
            return builder;
        }

        /// <summary>
        /// Property to store a note of the type of content that the Secret holds.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="contentType">Note to indicate the type of content within the Secret.</param>
        /// <returns></returns>
        public static SecretBuilder ContentType(this SecretBuilder builder, Input<string> contentType)
        {
            builder.Properties.ContentType = contentType;
            return builder;
        }

        /// <summary>
        /// Property to toggle the accessibility of the Secret.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="isEnabled">Set to true to enable the secret; set to false to disable the secret.</param>
        /// <returns></returns>
        public static SecretBuilder IsEnabled(this SecretBuilder builder, Input<bool> isEnabled)
        {
            builder.SecretAttributes.Enabled = isEnabled;
            return builder;
        }

        /// <summary>
        /// Sets the expiry date of the secret
        /// ISO 8601 format, example: '2021-04-08T13:00:00.00+01:00'
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dateTime">The date and time, in ISO 8601 format, that the secret will expire.</param>
        /// <returns></returns>
        public static SecretBuilder ExpiryDate(this SecretBuilder builder, Input<string> dateTime)
        {
            string dt = dateTime.Apply(e => e).GetValueAsync().Result;
            var seconds = ISO8601toTotalSeconds(dt);
            builder.SecretAttributes.Expires = seconds;
            return builder;
        }

        /// <summary>
        /// Sets the expiry date of the secret
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seconds">Expiry date in seconds since 1970.</param>
        /// <returns></returns>
        public static SecretBuilder ExpiryDate(this SecretBuilder builder, Input<int> seconds)
        {
            builder.SecretAttributes.Expires = seconds;
            return builder;
        }

        /// <summary>
        /// Sets the activation date of the secret.
        /// ISO 8601 format, example: '2021-04-08T13:00:00.00+01:00'
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dateTime">The date and time, in ISO 8601 format, that the secret will activate.</param>
        /// <returns></returns>
        public static SecretBuilder ActivationDate(this SecretBuilder builder, Input<string> dateTime)
        {
            string dt = dateTime.Apply(e => e).GetValueAsync().Result;
            var seconds = ISO8601toTotalSeconds(dt);
            builder.SecretAttributes.NotBefore = seconds;
            return builder;
        }

        /// <summary>
        /// Sets the activation date of the secret
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seconds">Activation date in seconds since 1970.</param>
        /// <returns></returns>
        public static SecretBuilder ActivationDate(this SecretBuilder builder, Input<int> seconds)
        {
            builder.SecretAttributes.NotBefore = seconds;
            return builder;
        }

        /// <summary>
        /// Method to convert ISO 8601 date time to total seconds since 1970.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static int? ISO8601toTotalSeconds(string dateTime)
        {
            DateTime dt = DateTime.Parse(dateTime,styles: System.Globalization.DateTimeStyles.AdjustToUniversal);
            TimeSpan span = dt.Subtract(new DateTime(1970,1,1,0,0,0));
            int secondsSince1970 = (int)span.TotalSeconds;
            return secondsSince1970;
        }
    }
}
