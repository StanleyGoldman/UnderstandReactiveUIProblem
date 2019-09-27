using System;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<ApplicationViewModel>
    {
        private ISchedulerProvider _schedulerProvider;

        public MainWindow(ISchedulerProvider schedulerProvider)
        {
            _schedulerProvider = schedulerProvider;

            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel,
                    viewModel => viewModel.Name,
                    view => view.TextName.Text));

                d(this.BindValidation(ViewModel,
                    vm => vm.Name,
                    view => view.LabelNameError.Content));
            });
        }
    }
}
