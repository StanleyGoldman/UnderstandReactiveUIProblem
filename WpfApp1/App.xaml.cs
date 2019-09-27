using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;

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

    public class ApplicationViewModel : ReactiveValidationObject<ApplicationViewModel>
    {
        private readonly ISchedulerProvider _schedulerProvider;

        public ApplicationViewModel(ISchedulerProvider schedulerProvider)
        {
            _schedulerProvider = schedulerProvider;

            NameRule = this.ValidationRule(
                viewModel => viewModel.Name,
                value => !string.IsNullOrWhiteSpace(value),
                value => $"Name must be set");

            SomeCommand = ReactiveCommand.CreateFromObservable<string, Unit>(SomeFunction);
        }

        public ValidationHelper NameRule { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public ReactiveCommand<string, Unit> SomeCommand { get; }

        private IObservable<Unit> SomeFunction(string addPath)
        {
            return Observable.Return(Unit.Default);
        }
    }

}
