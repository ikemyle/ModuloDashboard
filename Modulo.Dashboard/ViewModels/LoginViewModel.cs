using System;
using System.Collections.Generic;
using System.Reactive;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Modulo.Core.Model;
using Modulo.Business.Identity;
using Modulo.Business.Models;
using Modulo.BusinessSingletons;
using ReactiveUI;
using MD.DataModels;
using System.Data;

namespace Modulo.Dashboard.ViewModels
{
    public interface ICloseable
    {
        event EventHandler<EventArgs> RequestClose;
    }

    public interface IHidable
    {
        event EventHandler<EventArgs> RequestHide;
    }

    public class LoginViewModel : ReactiveObject, ICloseable, IHidable
    {
        private string _loginId;
        private string _password;
        private bool _userInvalidValid;
        private string _title = "Login";

        public event EventHandler<EventArgs> RequestClose;
        public event EventHandler<EventArgs> RequestHide;

        public ReactiveCommand<Unit, Unit> LoginClickCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelClickCommand { get; }
        public LoginViewModel()
        {
            LoginClickCommand = ReactiveCommand.Create(Login);
            CancelClickCommand = ReactiveCommand.Create(Cancel);
        }

        private void UpdateLoginFlag()
        {
            this.IsUserInvalid = false;
        }

        public bool IsUserInvalid
        {
            get => _userInvalidValid;
            set => this.RaiseAndSetIfChanged(ref _userInvalidValid, value);
        }

        public string LoginId
        {
            get => _loginId;
            set { this.RaiseAndSetIfChanged(ref _loginId, value); UpdateLoginFlag(); }
        }

        public string Title
        {
            get => _title;
            set { this.RaiseAndSetIfChanged(ref _title, value); UpdateLoginFlag(); }
        }

        public string Password
        {
            get => _password;
            set { this.RaiseAndSetIfChanged(ref _password, value); UpdateLoginFlag(); }
        }

        public void Login()
        {
            if (this.IsPasswordValid().Result)
            {
                var handler = RequestHide;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        public void Cancel()
        {
            var handler = RequestClose;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        private async Task<bool> IsPasswordValid()
        {
            await LoadTestData();
            var userBl = new UserBl();
            var usr = userBl.LoginUser(this.LoginId.ToLower(), this.Password);
            if (usr != null && usr.UserName == this.LoginId.ToLower())
            {
                UserAuthenticator.CurrentUser = usr;
                this.IsUserInvalid = false;
                return await Task.FromResult(true);
            }
            else
            {
                this.IsUserInvalid = true;
                return await Task.FromResult(false);
            }
        }

        private Task<bool> LoadTestData()
        {
            //TO BE REMOVED
            //ADD DEFAULT Users, Roles, and Modules

            var role1 = "ItAdmins";
            var role2 = "Guests";
            var roleBl = new RoleBl();

            //script already ran
            if (roleBl.RoleExists(role1))
                return Task.FromResult(true);

            Task.Run(() =>
        {
            //load roles
            if (!roleBl.RoleExists(role1))
                roleBl.InsertModule(new Role() { RoleName = role1 });

            if (!roleBl.RoleExists(role2))
                roleBl.InsertModule(new Role() { RoleName = role2 });

            var usr = new UserBl();
            int addedUser = 0;
            //load users
            if (!usr.UserExists("admin"))
            {
                addedUser = usr.AddUser("admin", "admin", "admin", "admin", "admin@admin.com");
            }
            //assign user in roles
            usr.AddUserRole(addedUser, 1);

            if (!usr.UserExists("guest"))
            {
                addedUser = usr.AddUser("guest", "guest", "guest", "guest", "guest@guest.com");
            }
            //assign user in roles
            usr.AddUserRole(addedUser, 2);

            //load modules
            var modOne1 = new MD.DataModels.Module()
            {
                Id = "DM.ModuleOne",
                Needsauthentication = true,
                Title = "Module One",
                Name = "DM.ModuleOne",
                Modulegroup = "Group 1"
            };

            var modOne2 = new MD.DataModels.Module()
            {
                Id = "DM.ModuleTwo",
                Needsauthentication = true,
                Title = "Module Two",
                Name = "DM.ModuleTwo",
                Modulegroup = "Group 1"
            };

            ModuleBl modBl = new ModuleBl();
            modBl.InsertModule(modOne1);
            modBl.InsertModule(modOne2);

            //assign modules in roles
            modBl.InsertModuleToRole(new ModuleToRole() { ModuleId = modOne1.Id, RoleId = 1 });
            modBl.InsertModuleToRole(new ModuleToRole() { ModuleId = modOne2.Id, RoleId = 1 });

        });
            return Task.FromResult(true);
            //END REMOVAL
        }
    }
}
