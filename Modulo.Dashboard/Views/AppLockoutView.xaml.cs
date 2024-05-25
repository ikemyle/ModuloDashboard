using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Modulo.Dashboard.Views
{
    /// <summary>
    /// Interaction logic for AppLockoutView.xaml
    /// </summary>
    public partial class AppLockoutView : UserControl
    {
        public AppLockoutView()
        {
            InitializeComponent();
            var window = App.Current.MainWindow as ModernWindow;
            window.Style = (Style)App.Current.Resources["EmptyWindow"];
            window.Content = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = App.Current.MainWindow as ModernWindow;
            window.Style = null;
        }
    }
}
