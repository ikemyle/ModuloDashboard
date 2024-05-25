using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Modulo.Core.Helpers
{
    public class ConnectionManager
    {
        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStatusChanged;       

        static ConnectionManager()
        {
            ConnManager = new ConnectionManager();
        }

        private ConnectionManager()
        {
            //  Check the Network Connection every 3 seconds
            var interval = Convert.ToInt32(TimeSpan.FromSeconds(3).TotalMilliseconds);
            _currState   = ConnectionState.Unknown;
            _connTimer   = new Timer(OnTimeout, interval, interval, Timeout.Infinite);
        }

        public static ConnectionManager Instance
        {
            get { return ConnManager; }
        }

        public ConnectionState State
        {
            get { return _currState; }
        }

        private void OnTimeout(object state)
        {
            _connTimer.Change(Timeout.Infinite, Timeout.Infinite);

            var newStatus = NativeMethods.GetIsNetworkAvailable() ? ConnectionState.Online : ConnectionState.Offline;
            if (newStatus != _currState)
            {
                var prevStatus = _currState;
                _currState = newStatus;
                OnConnectionStatusChanged(prevStatus, newStatus);
            }

            var interval = Convert.ToInt32(state);
            _connTimer.Change(interval, 0);
        }

        private void OnConnectionStatusChanged(ConnectionState prevState, ConnectionState currState)
        {
            var listeners = ConnectionStatusChanged;
            if (listeners == null) return;

            var handlers = listeners.GetInvocationList();
            var status = new ConnectionStateChangedEventArgs(prevState, currState);
            foreach (var handler in handlers)
            {
                try
                {
                    handler.DynamicInvoke(this, status);
                }
                catch (ApplicationException) { /* nothrow */ }
            }
        }

        private readonly Timer _connTimer;
        private static readonly ConnectionManager ConnManager;
        private ConnectionState _currState;
    }
}
