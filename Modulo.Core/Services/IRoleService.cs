using Modulo.Business.Models;
using System.Collections.Generic;

namespace Modulo.Core.Services
{
    public interface IRoleService
    {
        List<RoleService> GetAllRoles();
        RoleService GetRoleByName(string name);
        RoleService GetRoleById(int id);
    }
}
