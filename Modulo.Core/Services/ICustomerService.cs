using System.Collections.Generic;
using Modulo.Core.Model;

namespace Modulo.Core.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
    }
}
