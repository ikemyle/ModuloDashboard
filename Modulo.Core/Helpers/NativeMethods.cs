using System;
using System.Runtime.InteropServices;


namespace Modulo.Core.Helpers
{
    public static class NativeMethods
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(ref uint flags, uint dwReserved);

        public static bool GetIsNetworkAvailable()
        {
            uint flags = 0;
            return InternetGetConnectedState(ref flags, 0);
        }
    }
}
