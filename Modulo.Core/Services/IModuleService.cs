using System.Collections.Generic;
using Modulo.Business.Models;

namespace Modulo.Core.Services
{
    public interface IModuleService
    {
        List<MapModule> GetAllModules();
        MapModule GetModuleByName(string name);
        MapModule GetModuleById(string id);
    }
}
