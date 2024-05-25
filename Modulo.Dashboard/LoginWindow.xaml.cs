using Modulo.Dashboard.Settings;
using Modulo.Dashboard.ViewModels;
using Modulo.BusinessSingletons;
using ReactiveUI;
using System.Configuration;
using System.Reactive.Disposables;
using System.Windows;
namespace Modulo.Dashboard
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IViewFor<LoginViewModel>
    {
        private LoginViewModel ViewModel;

        LoginViewModel IViewFor<LoginViewModel>.ViewModel { get => ViewModel; set => ViewModel = (LoginViewModel)value; }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (LoginViewModel)value; }
        public LoginWindow()
        {
            InitializeComponent();
            UiAppSetting.SetCurrentTheme();
            
            Loaded += (s, e) =>
            {
                if (ViewModel is ICloseable)
                {
                    (ViewModel as ICloseable).RequestClose += (_, __) => this.Close();
                }
                if (ViewModel is IHidable)
                {
                    (ViewModel as IHidable).RequestHide += (_, __) => OnHiding();
                }
            };

            this
            .WhenActivated(disposables =>
            {
                this.Bind(ViewModel,
                    viewModel => viewModel.Title,
                    view => view.Title)
                    .DisposeWith(disposables);
                this.Bind(ViewModel,
                    viewModel => viewModel.Title,
                    view => view.Title)
                    .DisposeWith(disposables);
                this.Bind(ViewModel,
                    viewModel => viewModel.LoginId,
                    view => view.txtLoginId.Text)
                    .DisposeWith(disposables);
                this.Bind(ViewModel,
                    viewModel => viewModel.Password,
                    view => view.extPasswordBox.TypedPassword)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel,
                    viewModel => viewModel.IsUserInvalid,
                    view => view.dockLoginError.Visibility)
                    .DisposeWith(disposables);
                this
                    .BindCommand(ViewModel, vm => vm.LoginClickCommand, v => v.btnLogin)
                    .DisposeWith(disposables);
                this
                    .BindCommand(ViewModel, vm => vm.LoginClickCommand, v => v.extPasswordBox)
                    .DisposeWith(disposables);
                this
                    .BindCommand(ViewModel, vm => vm.CancelClickCommand, v => v.btnCancel)
                    .DisposeWith(disposables);
                this
                    .BindCommand(ViewModel, vm => vm.CancelClickCommand, v => v.CloseButton)
                    .DisposeWith(disposables);
            });
            ViewModel = new LoginViewModel();
            ViewModel.Title = ConfigurationManager.AppSettings["title"].ToString();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            App.Current.MainWindow.DragMove();
        }

        private void OnHiding()
        {
            if (UserAuthenticator.IsLocked)
            {
                UserAuthenticator.IsLocked = false;
                Shell.GetWindow(this).Show();
            }
            this.Hide();
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            if (UserAuthenticator.IsLocked == true)
            {
                //Shell.GetWindow(this).Hide();
            }
        }
    }
}
