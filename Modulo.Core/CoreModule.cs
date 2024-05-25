using System;
using Prism.Ioc;
using Prism.Modularity;
using Modulo.Core.Services;

namespace Modulo.Core
{
    public class CoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IModuleService, ModuleService>();
            containerRegistry.Register<IRoleService, RoleService>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<ICustomerService, CustomerService>();
        }
    }
}