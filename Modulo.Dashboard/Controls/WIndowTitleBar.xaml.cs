using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Modulo.Dashboard.Controls
{
    /// <summary>
    /// Interaction logic for WIndowTitleBar.xaml
    /// </summary>
    public partial class WindowTitleBar : UserControl, INotifyPropertyChanged
    {
        public static DependencyProperty HasCloseProperty =
            DependencyProperty.Register("HasClose", typeof(bool), typeof(WindowTitleBar), new
            PropertyMetadata(true, new PropertyChangedCallback(OnHasCloseChanged)));

        public static DependencyProperty TitleProperty = 
            DependencyProperty.Register("Title", typeof(string), typeof(WindowTitleBar), new
            PropertyMetadata("Window Title", new PropertyChangedCallback(OnTitleChanged)));
        public WindowTitleBar()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void OnHasCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WindowTitleBar winTitleControl = d as WindowTitleBar;
            winTitleControl.OnHasCloseChanged(e);
        }

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WindowTitleBar winTitleControl = d as WindowTitleBar;
            winTitleControl.OnTitleChanged(e);
        }

        private void OnHasCloseChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                RaisePropertyChanged("HasClose");
                CloseButton.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void OnTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                RaisePropertyChanged("Title");
                WindowTitle.Text = e.NewValue.ToString();
            }
        }
        public bool HasClose
        {
            get
            {
                return (bool)GetValue(HasCloseProperty);
            }
            set
            {
                SetValue(HasCloseProperty, value);
            }
        }

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }
    }
}
