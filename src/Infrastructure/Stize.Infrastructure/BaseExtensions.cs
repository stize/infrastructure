using System;
using System.Threading.Tasks;
using Pulumi;
using Pulumi.Random;

namespace Stize.Infrastructure
{
    public static class BaseExtensions
    {
        /// <summary>
        /// Extension method extracting the value from a Pulumi Output 
        /// <seealso cref="https://www.pulumi.com/blog/unit-testing-cloud-deployments-with-dotnet/"/>       
        /// </summary>
        /// <example>
        ///     <code>
        ///         [Test]
        ///         public async Task StorageAccountBelongsToResourceGroup()
        ///         {
        ///             var resources = await TestAsync();
        ///             var storageAccount = resources.OfType\Storage.Account>().SingleOrDefault();
        ///             storageAccount.Should().NotBeNull("Storage account not found");
        ///
        ///             var resourceGroupName = await storageAccount.ResourceGroupName.GetValueAsync();
        //              resourceGroupName.Should().Be("www-prod-rg");
        ///         }
        ///     </code>
        /// </example>        
        /// <param name="output"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> GetValueAsync<T>(this Output<T> output)
        {
            var tcs = new TaskCompletionSource<T>();
            output.Apply(v =>
            {
                tcs.SetResult(v);
                return v;
            });
            return tcs.Task;
        }

        /// <summary>
        /// Creates a dependency of the <see cref="Resource"/> that is been built with another <see cref="Resource"/>
        /// </summary>
        /// <typeparam name="B"></typeparam>
        /// <param name="builder"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static B DependsOn<B>(this B builder, Resource resource) where B : BaseBuilder
        {
            builder.CustomResourceOptions.DependsOn = resource;
            return builder;
        }

        /// <summary>
        /// Creates a parent/child relationshipo between two resources
        /// </summary>
        /// <param name="builder">Builder that is creating the new <see="Resource" /></param>
        /// <param name="resource"><see="Resource" /> to set as parent</param>
        /// <typeparam name="B">Bulder type</typeparam>
        /// <returns>The builder</returns>
        public static B Parent<B>(this B builder, Resource resource) where B : BaseBuilder
        {
            builder.CustomResourceOptions.Parent = resource;
            return builder;
        }
    }
}
