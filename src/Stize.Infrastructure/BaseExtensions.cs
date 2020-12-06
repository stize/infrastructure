using System;
using Pulumi;

namespace Stize.Infrastructure
{
    public static class BaseExtensions
    {
        /// <summary>
        /// sets the builder name
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="name">Builder name</param>
        /// <returns>The builder argument</returns>
        public static B Name<B>(this B builder, string name) where B : BaseBuilder
        {
            builder.Name = name;
            return builder;
        }

        /// <summary>
        /// Sets the location on which the resource should be created on
        /// </summary>
        /// <typeparam name="B">Builder type</typeparam>
        /// <param name="builder">Builder instance</param>
        /// <param name="location">Resource location</param>
        /// <returns></returns>
        public static B Location<B>(this B builder, string location) where B : BaseBuilder
        {
            builder.Location = location;
            return builder;
        }

        /// <summary>
        /// Creates a dependency of the <see cref="=Resource"/> that is been built with another <see cref="Resource"/>
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
    }
}
