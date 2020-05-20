using System;
using System.Collections.Generic;

namespace LibraryManagementAdministrationWebApi.Models
{
    public partial class AdminRole
    {
        public AdminRole()
        {
            Administrator = new HashSet<Administrator>();
        }

        public int Id { get; set; }
        public int AdminLevel { get; set; }
        public string AdminRole1 { get; set; }

        public virtual ICollection<Administrator> Administrator { get; set; }
    }
}
