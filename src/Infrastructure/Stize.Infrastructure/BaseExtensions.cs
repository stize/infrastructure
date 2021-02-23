using System;
using Pulumi;
using Pulumi.Random;

namespace Stize.Infrastructure
{
    public static class BaseExtensions
    {

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

        /// <summary>
        /// Instructs the builder to use a given RandomId
        /// </summary>
        /// <param name="builder">Builder</param>
        /// <param name="randomId">RandomId</param>
        /// <typeparam name="B">Builder type</typeparam>
        /// <returns></returns>
        public static B UseRandomId<B>(this B builder, RandomId randomId) where B : BaseBuilder
        {
            builder.RandomId = randomId;
            return builder;
        }
    }
}
