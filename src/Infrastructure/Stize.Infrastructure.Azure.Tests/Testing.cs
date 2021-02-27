using Xunit;

//hack Pulumi Test Runner doesn't parallel execution. Enabling parallel testing throws a NotSupportedException
//TODO Explore if at some point pulumi allows parallel execution and remove
[assembly: CollectionBehavior(DisableTestParallelization = true)]

