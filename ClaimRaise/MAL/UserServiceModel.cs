using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAL
{
    public class UserServiceModel
    {
        public string AdminId { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public string ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
    }
}
