using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Modulo.Core.Helpers
{
    /// <summary>
    /// A WeakEventManager implementation for ConnectionStateChanged event handler
    /// </summary>
    public class ConnectionStateEventManager : WeakEventManager
    {
        protected ConnectionStateEventManager()
        {            
        }

        /// <summary>
        /// Add a handler for the given source's event.
        /// </summary>
        public static void AddHandler(ConnectionManager source, IWeakEventListener handler)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (handler == null)
                throw new ArgumentNullException("handler");

            CurrentManager.ProtectedAddListener(source, handler);
        }

        /// <summary>
        /// Remove a handler for the given source's event.
        /// </summary>
        public static void RemoveHandler(ConnectionManager source, IWeakEventListener handler)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (handler == null)
                throw new ArgumentNullException("handler");

            CurrentManager.ProtectedRemoveListener(source, handler);
        }

        protected override void StartListening(object source)
        {
            var connectionManager = (ConnectionManager) source;
            connectionManager.ConnectionStatusChanged += OnConnectionStatusChanged;
        }

        protected override void StopListening(object source)
        {
            var connectionManager = (ConnectionManager)source;
            connectionManager.ConnectionStatusChanged -= OnConnectionStatusChanged;
        }

        /// <summary>
        /// Get the event manager for the current thread.
        /// </summary>
        private static ConnectionStateEventManager CurrentManager
        {
            get
            {
                var managerType = typeof(ConnectionStateEventManager);
                var manager = (ConnectionStateEventManager)GetCurrentManager(managerType);

                // Lazy initialization, create and register a new manager
                if (manager == null)
                {
                    manager = new ConnectionStateEventManager();
                    SetCurrentManager(managerType, manager);
                }

                return manager;
            }
        }

        private void OnConnectionStatusChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            DeliverEvent(sender, e);
        }
    }
}
