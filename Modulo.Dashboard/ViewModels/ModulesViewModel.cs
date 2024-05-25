using System.Collections.ObjectModel;
using Modulo.Core.Services;
using Modulo.Business.Models;
using ReactiveUI;

namespace Modulo.Dashboard.ViewModels
{
    public class ModulesViewModel : ReactiveObject
    {
        private ObservableCollection<MapModule> _modules;
        public ObservableCollection<MapModule> Modules
        {
            get { return _modules; }
            set => this.RaiseAndSetIfChanged(ref _modules, value);
        }
        public ModulesViewModel()
        {
            var service = new ModuleService();
            Modules = new ObservableCollection<MapModule>();
            Modules.AddRange(service.GetAllModules());
        }

        public ModulesViewModel(IModuleService service)
        {
            Modules = new ObservableCollection<MapModule>();
            Modules.AddRange(service.GetAllModules());
        }

        public void Login()
        {

        }
    }
}
