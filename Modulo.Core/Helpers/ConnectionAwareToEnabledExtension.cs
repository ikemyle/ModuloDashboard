using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Modulo.Core.Helpers
{
    [MarkupExtensionReturnType(typeof(bool))]
    [System.Diagnostics.DebuggerStepThrough]
    public class ConnectionAwareToEnabledExtension : MarkupExtension
    {
        private DependencyObject TargetObject { get; set; }
        private DependencyProperty TargetProperty { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var provider = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (provider != null)
            {
                //Gets a reference of IProvideValueTarget provider from the IServiceProvider parameter
                if (provider.TargetObject is DependencyObject && provider.TargetProperty is DependencyProperty)
                {
                    //  4) Maintains a reference to the TargetObject and TargetProperty
                    TargetObject = (DependencyObject)provider.TargetObject;
                    TargetProperty = (DependencyProperty)provider.TargetProperty;
                }
                else
                {
                    throw new InvalidOperationException("The binding target is not a " +
                       "DependencyObject or its property is not a DependencyProperty.");
                }
            }
            //  5) Subscribe to the event notification
            ConnectionManager.Instance.ConnectionStatusChanged += OnConnectionStateChanged;

            //  The initial value when the UI element is displayed
            return ConnectionManager.Instance.State == ConnectionState.Online;
        }

        private void OnConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            //  6) Update the DependencyProperty value in event handler
            TargetObject.Dispatcher.BeginInvoke(new Action(() => TargetObject.SetValue(
               TargetProperty, e.CurrentState == ConnectionState.Online)));
        }
    }
}
