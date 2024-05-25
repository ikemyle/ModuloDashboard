using LinqToDB;
using MD.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modulo.Business.Identity
{
    public class RoleBl
    {
        public bool RoleExists(string roleName)
        {
            try
            {
                using (var db = new ModuloDB("modulo_conn"))
                {
                    return db.GetTable<Role>().Where(u => u.RoleName.ToLower() == roleName.ToLower()).Any();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertModule(Role newRole)
        {
            using (var db = new ModuloDB("modulo_conn"))
            {
                return db.Insert<Role>(newRole);
            }
        }

        public List<Role> GetModleRoles(string moduleId)
        {
            using (var db = new ModuloDB("modulo_conn"))
            {
                var query =
                from r in db.Roles
                join mr in db.ModuleToRoles on r.Id equals mr.RoleId
                where mr.ModuleId == moduleId
                select r;
                return query.ToList();
            }
        }
    }
}
