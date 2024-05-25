using LinqToDB;
using LinqToDB.Data;
using MD.DataModels;
using Modulo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modulo.Business.Identity
{
    public class UserBl
    {
        public int AddUserRole(int userId, int roleId)
        {
            using (var db = new ModuloDB("modulo_conn"))
            {
                return db.Insert<UserToRole>(new UserToRole() { UserId = userId, RoleId = roleId });
            }
        }
        public int AddUser(string username, string password, string firstname, string lastname, string email)
        {
            try
            {
                using (var db = new ModuloDB("modulo_conn"))
                {                    
                    var loginParams = new DataParameter[]
                                {
                            new DataParameter("@pLogin", username) { DbType = DataType.VarChar.ToString() },
                            new DataParameter("@pPassword", password) { DbType = DataType.VarChar.ToString() },
                            new DataParameter("@pFirstName", firstname) { DbType = DataType.VarChar.ToString() },
                            new DataParameter("@pLastName", lastname) { DbType = DataType.VarChar.ToString() },
                            new DataParameter("@Email", email) { DbType = DataType.VarChar.ToString() },
                            new DataParameter("@pSalt", Guid.NewGuid())
            };
                    string sql = @"INSERT INTO dbo.[User] (UserName, PasswordHash, Salt, FirstName, LastName, Email, EntryDate, ModifiedDate)
                    VALUES(@pLogin, HASHBYTES('SHA2_512', @pPassword+CAST(@pSalt AS NVARCHAR(36))), @pSalt, @pFirstName, @pLastName, @Email, GETDATE(),GETDATE());
                    SELECT @@IDENTITY AS LoginId;";
                    var drInsert = db.ExecuteReader(sql, loginParams);
                    var reader = drInsert.Reader;
                    decimal nLogin = 0;
                    while (reader.Read())
                    {
                        nLogin = reader.GetDecimal(reader.GetOrdinal("LoginId"));
                    }
                    return (int)nLogin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UserExists(string login)
        {
            try
            {
                using (var db = new ModuloDB("modulo_conn"))
                {
                    return db.GetTable<User>().Where(u => u.UserName.ToLower() == login.ToLower()).Any();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MapUser LoginUser(string login, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    return null;
                }

                MapUser usr = null;
                using (var db = new ModuloDB("modulo_conn"))
                {
                    var sql = @"SELECT UserID,UserName,FirstName,LastName,Email,EntryDate,ModifiedDate FROM [User] WHERE UserName=@pLogin AND PasswordHash=HASHBYTES('SHA2_512', @pPassword+CAST(Salt AS NVARCHAR(36)))";
                    var loginParams = new DataParameter[]
                    {
                            new DataParameter("@pLogin", login) { DbType = DataType.VarChar.ToString() },
                            new DataParameter("@pPassword", password) { DbType = DataType.VarChar.ToString() }
                    };
                    var usrDb = db.Query<User>(sql, loginParams).SingleOrDefault();

                    if (usrDb != null)
                    {
                        usr = new MapUser(usrDb.UserID, usrDb.UserName, usrDb.FirstName, usrDb.LastName, usrDb.Email, usrDb.EntryDate, usrDb.ModifiedDate);
                    }
                    else
                    {
                        return null;
                    }
                    var usrRoles =
                         from us in db.Users
                         from utr in db.UserToRoles.Where(urr => urr.UserId == us.UserID)
                         from rl in db.Roles.Where(r => r.Id == utr.RoleId)
                         where us.UserName == login
                         select new
                         {
                             utr.RoleId,
                             rl.RoleName
                         };

                    usr.UserRoles = new List<MapRole>();
                    //get roles
                    foreach (var role in usrRoles)
                    {
                        var rlMap = new MapRole(role.RoleId, role.RoleName);
                        usr.UserRoles.Add(rlMap);
                    }

                    var usrModules =
                     from us in db.Users
                     from utr in db.UserToRoles.Where(urr => urr.UserId == us.UserID)
                     from rl in db.Roles.Where(r => r.Id == utr.RoleId)
                     from mtr in db.ModuleToRoles.Where(urr => urr.RoleId == rl.Id)
                     from mod in db.Modules.Where(modl => modl.Id == mtr.ModuleId || modl.Needsauthentication == false)
                     where us.UserName == login
                     select new
                     {
                         mod.Id,
                         mod.Name,
                         mod.Title,
                         mod.Needsauthentication,
                         mod.Description,
                         mod.Modulegroup
                     };

                    //get Modules
                    usr.UserModules = new List<MapModule>();
                    //get roles
                    foreach (var module in usrModules)
                    {
                        var modMap = new MapModule(module.Id, module.Name, module.Description, module.Title, module.Needsauthentication, module.Modulegroup);
                        usr.UserModules.Add(modMap);
                    }

                }

                return usr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
