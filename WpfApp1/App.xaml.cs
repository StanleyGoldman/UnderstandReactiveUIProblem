using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var schedulerProvider = new SchedulerProvider();
            var applicationViewModel = new ApplicationViewModel(schedulerProvider);
            
            var mainWindow = new MainWindow(schedulerProvider);
            mainWindow.ViewModel = applicationViewModel;
            mainWindow.Show();
        }
    }

    public interface ISchedulerProvider
    {
        IScheduler TaskPool { get; }
        IScheduler MainThreadScheduler { get; }
    }

    public sealed class SchedulerProvider : ISchedulerProvider
    {
        public IScheduler MainThreadScheduler => RxApp.MainThreadScheduler;

        public IScheduler TaskPool => TaskPoolScheduler.Default;
    }

    public class ApplicationViewModel : ReactiveObject
    {
        private readonly ISchedulerProvider _schedulerProvider;

        public ApplicationViewModel(ISchedulerProvider schedulerProvider)
        {
            _schedulerProvider = schedulerProvider;

            SomeCommand = ReactiveCommand.CreateFromObservable<string, Unit>(SomeFunction);
        }

        public ReactiveCommand<string, Unit> SomeCommand { get; }

        private IObservable<Unit> SomeFunction(string addPath)
        {
            return Observable.Return(Unit.Default);
        }
    }

}
