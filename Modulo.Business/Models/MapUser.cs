using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulo.Business.Models
{
    public class MapUser
    {
        public MapUser()
        {

        }

        public MapUser(int id, string username, string firstname, string lastname, string email, DateTime? entrydate, DateTime? modifieddate)
        {
            this.Id = id;
            this.UserName = username;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;
            this.Id = id;
            this.Id = id;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<MapRole> UserRoles { get; set; }
        public List<MapModule> UserModules { get; set; }
    }
}
