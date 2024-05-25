using System;


namespace Modulo.Core.Helpers
{
    public  class ConnectionStateChangedEventArgs : EventArgs
    {
        private readonly ConnectionState _prevState;
        private readonly ConnectionState _currState;

        public ConnectionStateChangedEventArgs(ConnectionState prevState, ConnectionState currState)
        {
            _prevState = prevState;
            _currState = currState;
        }

        public ConnectionState PreviousState
        {
            get { return _prevState; }   
        }

        public ConnectionState CurrentState
        {
            get { return _currState; }
        }
    }
}
