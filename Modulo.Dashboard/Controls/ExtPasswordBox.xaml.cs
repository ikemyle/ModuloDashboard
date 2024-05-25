using FirstFloor.ModernUI.Presentation;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Modulo.Dashboard.Controls
{
    /// <summary>
    /// Interaction logic for password extender
    /// </summary>
    public partial class ExtPasswordBox : UserControl, INotifyPropertyChanged
    {
        private bool valChangeFromBox = false;
        public ExtPasswordBox()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler Click;
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty SetTypedPasswordProperty =
        DependencyProperty.Register("TypedPassword", typeof(string), typeof(ExtPasswordBox), new
        PropertyMetadata("", new PropertyChangedCallback(OnSetTypedPasswordChanged)));

        private static void OnSetTypedPasswordChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            ExtPasswordBox passControl = d as ExtPasswordBox;
            passControl.OnSetTypedPasswordChanged(e);
        }

        private void OnSetTypedPasswordChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                RaisePropertyChanged("TypedPassword");
                if (!valChangeFromBox)
                {
                    txtPassword.Password = e.NewValue.ToString();
                }
                valChangeFromBox = false;
            }
        }

        public string TypedPassword
        {
            get
            {
                return (string)GetValue(SetTypedPasswordProperty);
            }
            set
            {
                SetValue(SetTypedPasswordProperty, value);
            }
        }
        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SetFocusedBorder();
            txtVisPassword.Text = txtPassword.Password;
            txtPassword.Visibility = Visibility.Collapsed;
            txtVisPassword.Visibility = Visibility.Visible;
        }

        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            SetFocusedBorder();
            txtPassword.Visibility = Visibility.Visible;
            txtVisPassword.Visibility = Visibility.Collapsed;
            txtPassword.Focus();
        }

        private void txtPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SetFocusedBorder();
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            var pbox = (PasswordBox)sender;
            passHolder.BorderBrush = (pbox).BorderBrush;
        }

        private void SetFocusedBorder()
        {
            passHolder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (this.Click != null)
                    this.Click(this, e);
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            valChangeFromBox = true;
            this.TypedPassword = txtPassword.Password;
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            SetFocusedBorder();
        }
    }

}
