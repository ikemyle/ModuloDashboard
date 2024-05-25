using Modulo.Business.Identity;
using Modulo.Business.Models;
using MD.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modulo.Core.Services
{
    public class ModuleService : IModuleService
    {
        public List<MapModule> GetAllModules()
        {
            var mDl = new ModuleBl();
            return mDl.GetAllModules();
        }

        public MapModule GetModuleByName(string name)
        {
            throw new NotImplementedException();
        }

        public MapModule GetModuleById(string moduleId)
        {
            var mDl = new ModuleBl();
            var rDl = new RoleBl();
            var returnedModule = mDl.GetModuleById(moduleId);
            var roles = rDl.GetModleRoles(moduleId);
            returnedModule.Roles = new List<MapRole>();
            foreach (var role in roles)
            {
                var mapRole = new MapRole(role.Id, role.RoleName);
                returnedModule.Roles.Add(mapRole);
            }
            return returnedModule;
        }
    }
}
