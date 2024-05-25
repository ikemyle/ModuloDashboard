using Modulo.Business.Models;
using System.Collections.Generic;

namespace Modulo.Core.Services
{
    public interface IUserService
    {
        List<UserService> GetAllUsers();
        UserService GetUserByName(string name);
        UserService GetUserById(int id);
    }
}
