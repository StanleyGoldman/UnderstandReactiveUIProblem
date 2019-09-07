using System;
using System.Reactive.Concurrency;
using Microsoft.Reactive.Testing;
using Xunit;

namespace WpfApp1.Tests
{
    public sealed class TestSchedulerProvider : ISchedulerProvider
    {
        public TestScheduler TaskPool { get; } = new TestScheduler();
        public TestScheduler MainThreadScheduler { get; } = new TestScheduler();

        IScheduler ISchedulerProvider.TaskPool => TaskPool;
        IScheduler ISchedulerProvider.MainThreadScheduler => MainThreadScheduler;
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var testSchedulerProvider = new TestSchedulerProvider();

            var applicationViewModel = new ApplicationViewModel(testSchedulerProvider);
        }
    }
}
