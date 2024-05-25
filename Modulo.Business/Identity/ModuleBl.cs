using LinqToDB;
using MD.DataModels;
using Modulo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulo.Business.Identity
{
    public class ModuleBl
    {
        public int InsertModuleToRole(MD.DataModels.ModuleToRole moduleToRole)
        {
            using (var db = new ModuloDB("modulo_conn"))
            {
                return db.Insert<MD.DataModels.ModuleToRole>(moduleToRole);
            }
        }

        public int InsertModule(MD.DataModels.Module moduleToInsert)
        {
            using (var db = new ModuloDB("modulo_conn"))
            {
                return db.Insert<MD.DataModels.Module>(moduleToInsert);
            }
        }

        public MapModule GetModuleById(string moduleId)
        {
            var module = new MapModule();
            using (var db = new ModuloDB("modulo_conn"))
            {
                //var query =
                //    from m in db.Modules
                //    join r in db.ModuleToRoles on m.Id equals r.ModuleId into lj
                //    from lp in lj.DefaultIfEmpty()
                //    where m.Id == moduleId
                //    select m;

                //var dbModule = query.SingleOrDefault();
                var dbModule = db.Modules.Where(m => m.Id == moduleId).SingleOrDefault();
                if (dbModule != null)
                {
                    module = new MapModule(dbModule.Id, dbModule.Name, dbModule.Description, dbModule.Title, dbModule.Needsauthentication, dbModule.Modulegroup);
                }
            }
            return module;
        }
        public List<MapModule> GetAllModules()
        {
            var allModuleList = new List<MapModule>();
            using (var db = new ModuloDB("modulo_conn"))
            {
                var allModules = db.Modules;
                foreach (var module in allModules)
                {
                    var newMapModule = new MapModule(module.Id, module.Name, module.Description, module.Title, module.Needsauthentication,module.Modulegroup);
                    allModuleList.Add(newMapModule);
                }
            }
            return allModuleList;
        }
    }
}
