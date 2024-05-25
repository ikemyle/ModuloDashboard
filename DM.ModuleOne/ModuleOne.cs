using Modulo.Core.Services;
using Prism.Ioc;
using Prism.Modularity;

namespace DM.ModuleOne
{
    [Module(ModuleName = "Module1", OnDemand = true)]
    public class ModuleOne : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            System.Windows.MessageBox.Show($"{nameof(ModuleOne)} has been initialized.");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.Register<InterfaceName, ClassName>();
            //containerRegistry.Register<ICustomerService, CustomerService>();
            System.Windows.MessageBox.Show($"{nameof(ModuleOne)} has been registered.");
        }
    }
}