using Modulo.Dashboard.Controls;
using FirstFloor.ModernUI.Windows.Controls;
using Modulo.BusinessSingletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Modulo.Dashboard.Views
{
    /// <summary>
    /// Interaction logic for LockView.xaml
    /// </summary>
    public partial class LockView : UserControl
    {
        public LockView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var lockWindow = new Window
            {
                WindowStyle = WindowStyle.None,                
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize,
                ShowInTaskbar = false,
                Style = (Style)Application.Current.Resources["WinLogin"],
                Width = Shell.GetWindow(this).Width,                
                Height = 150,
                Content = new  AppLockout()
                //Topmost = true
            };
            lockWindow.ShowDialog();
        }
    }
}
