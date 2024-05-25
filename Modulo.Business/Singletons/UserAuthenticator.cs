using Modulo.Business.Models;
using System.Collections.Generic;

namespace Modulo.BusinessSingletons
{
    public static class UserAuthenticator
    {
        public static MapUser CurrentUser { get; set; }
        public static bool IsLocked { get; set; }
    }
}
