using System;
using System.Reflection;
using System.Windows;
using Prism.Ioc;
using Prism.Modularity;
using FirstFloor.ModernUI.Presentation;
using Modulo.Core.Interfaces;
using Unity;
using System.Linq;
using System.IO;
using Modulo.BusinessSingletons;
using Modulo.Dashboard.Constants;

namespace Modulo.Dashboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private string MODULES_PATH = AppDomain.CurrentDomain.BaseDirectory + @"\modules";
        private LinkGroupCollection linkGroupCollection = null;
        public static LoginWindow LoginForm;
        protected override Window CreateShell()
        {
            var shell = Container.Resolve<Shell>();

            if (linkGroupCollection != null)
            {
                shell.AddLinkGroups(linkGroupCollection);
            }

            return (Window)shell;
        }

        /// <summary>
        /// Dependency injection
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.Register<InterfaceName, ClassName>();
            //containerRegistry.Register<IModuleInitializer, RoleBasedModuleInitializer>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //ModuleCatalog catalog = new ConfigurationModuleCatalog();
            //return catalog;

            ModuleCatalog catalog = new ModuleCatalog();
            //load our newly added assembly
            string[] files = Directory.GetFiles(MODULES_PATH, "*.dll");
            foreach (var modulePath in files)
            {
                var moduleId = Path.GetFileNameWithoutExtension(modulePath);
                //load only modules in roles
                if (UserAuthenticator.CurrentUser.UserModules.Where(m => m.Id.ToLower() == moduleId.ToLower()).Any())
                {
                    Assembly assembly = Assembly.LoadFile(modulePath);
                    var moduleInfos = assembly.GetExportedTypes();
                    foreach (var moduleInfo in moduleInfos)
                    {
                        if ((((TypeInfo)moduleInfo).ImplementedInterfaces).Where(m => m.UnderlyingSystemType.FullName == "Prism.Modularity.IModule").Any())
                        {
                            //add the ModuleInfo to the catalog so it can be loaded
                            catalog.AddModule(moduleInfo);
                            //add ref to dll path
                            ((ModuleInfo)catalog.Items[catalog.Items.Count - 1]).Ref = "file:///" + modulePath.Replace(@"\", "/");
                        }
                    }
                }
            }
            return catalog;

            //ModuleCatalog = new DirectoryModuleCatalog() { ModulePath = MODULES_PATH };
            //return ModuleCatalog;

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // Dynamic Modules are copied to a directory as part of a post-build step.
            // These modules are not referenced in the startup project and are discovered 
            // by examining the assemblies in a directory. The module projects have the 
            // following post-build step in order to copy themselves into that directory:
            //
            // xcopy "$(TargetDir)$(TargetFileName)" "$(TargetDir)modules\" /y
            //
            // WARNING! Do not forget to build the solution (F5) before each running
            // so the modules are copied into the modules folder.

            //var directoryCatalog = (DirectoryModuleCatalog)moduleCatalog;
            var directoryCatalog = (ModuleCatalog)moduleCatalog;

            directoryCatalog.Initialize();
            linkGroupCollection = new LinkGroupCollection();
            var typeFilter = new TypeFilter(InterfaceFilter);

            foreach (ModuleInfo module in directoryCatalog.Items)
            {
                //module.InitializationMode = InitializationMode.OnDemand;
                var asm = Assembly.LoadFrom(module.Ref);

                foreach (Type t in asm.GetTypes())
                {
                    var myInterfaces = t.FindInterfaces(typeFilter, typeof(ILinkGroupService).ToString());

                    if (myInterfaces.Length > 0)
                    {
                        // We found the type that implements the ILinkGroupService interface
                        var groupName = UserAuthenticator.CurrentUser.UserModules.Where(m => m.Name == module.ModuleName).FirstOrDefault()?.ModuleGroup;
                        if (String.IsNullOrEmpty(groupName))
                        {
                            groupName = CommonSettings.GeneralModule;
                        }
                        var findGroup = linkGroupCollection.Where(g => g.GroupKey == groupName);

                        var linkGroupService = (ILinkGroupService)asm.CreateInstance(t.FullName);
                        var linkGroup = linkGroupService.GetLinkGroup();

                        if (findGroup.Any())
                        {
                            findGroup.ToList().Add(linkGroup);
                        }
                        else
                        {
                            //linkGroup.GroupKey = groupName;
                            linkGroup.DisplayName = groupName;
                            linkGroupCollection.Add(linkGroup);
                        }
                    }
                }
            }
            //if (UserAuthenticator.CurrentUser.UserRoles.Where(r => r.Name == CommonSettings.ITAdminRole).Any())
            //{
            moduleCatalog.AddModule(typeof(Core.CoreModule));
            //}
        }

        protected override void Initialize()
        {
            LoginForm = new LoginWindow();
            LoginForm.ShowDialog();
            if (UserAuthenticator.CurrentUser?.Id > 0)
            {
                base.Initialize();
            }
            else
            {
                Application.Current.Shutdown();
                return;
            }
        }

        private bool InterfaceFilter(Type typeObj, Object criteriaObj)
        {
            return typeObj.ToString() == criteriaObj.ToString();
        }
    }
}