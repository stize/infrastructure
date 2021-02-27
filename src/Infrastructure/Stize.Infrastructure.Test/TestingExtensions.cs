using System.Threading.Tasks;
using Pulumi;
using FluentAssertions;
using Stize.Infrastructure;
using System;

namespace Stize.Infrastructure.Test
{
    public static class TestingExtensions
    {
        /// <summary>
        /// Extension method to simplify how the assertions looks like
        /// </summary>
        /// <param name="output"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static FluentAssertions.Primitives.ObjectAssertions OutputShould<T>(this Output<T> output)
        {
            var task = output.GetValueAsync();
            task.Wait();
            return task.Result.Should();
        }

        /// <summary>
        /// Extension method to simplify how the assertions looks like
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public static FluentAssertions.Primitives.StringAssertions OutputShould(this Output<string> output)
        {
            var task = output.GetValueAsync();
            task.Wait();
            return task.Result.Should();
        }
    }
}