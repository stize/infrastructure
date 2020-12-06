using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Moq;
using Pulumi;
using Pulumi.Testing;
using Xunit;

//hack Pulumi Test Runner doesn't parallel execution. Enabling parallel testing throws a NotSupportedException
//TODO Explore if at some point pulumi allows parallel execution and remove
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Stize.Infrastructure.Tests
{
    /// <summary>
    /// <seealso cref="https://www.pulumi.com/docs/guides/testing/unit/"/>
    /// </summary>
    public static class Testing
    {
        public static Task<ImmutableArray<Resource>> RunAsync<T>() where T : Stack, new()
        {
            var mocks = new Mock<IMocks>();
            mocks.Setup(m => m.NewResourceAsync(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<ImmutableDictionary<string, object>>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((string type, string name, ImmutableDictionary<string, object> inputs, string? provider, string? id) => (id ?? "", inputs));
            mocks.Setup(m => m.CallAsync(It.IsAny<string>(), It.IsAny<ImmutableDictionary<string, object>>(), It.IsAny<string>()))
                .ReturnsAsync((string token, ImmutableDictionary<string, object> args, string? provider) => args);
            return Deployment.TestAsync<T>(mocks.Object, new TestOptions { IsPreview = false });
        }
    }
}
