using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulo.Business.Models
{
    public class MapModule
    {
        public MapModule()
        {

        }
        public MapModule(string id, string name, string description, string title, bool needsAuthenticate, string modulegroup)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Title = title;
            this.NeedsAuthenticate = needsAuthenticate;
            this.Roles = new List<MapRole>();
            this.ModuleGroup = modulegroup;
        }
        public bool NeedsAuthenticate { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public string Title { get; set; }
        public string ModuleGroup { get; set; }
        public string Description { get; set; }
        public List<MapRole> Roles { get; set; }
    }
}
