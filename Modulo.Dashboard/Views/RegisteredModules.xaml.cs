using Modulo.Dashboard.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows.Controls;

namespace Modulo.Dashboard.Views
{
    /// <summary>
    /// Interaction logic for RegisteredModules.xaml
    /// </summary>
    public partial class RegisteredModules : UserControl, IViewFor<ModulesViewModel>
    {
        ModulesViewModel ViewModel;
        public RegisteredModules()
        {
            InitializeComponent();
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel,
                    viewModel => viewModel.Modules,
                    view => view.grdModules.ItemsSource)
                    .DisposeWith(disposables);
            });
            ViewModel = new ModulesViewModel();
        }

        ModulesViewModel IViewFor<ModulesViewModel>.ViewModel { get => ViewModel; set => ViewModel = (ModulesViewModel)value; }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (ModulesViewModel)value; }
    }
}
