using MAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IEntity
    {
        Task<Tuple<string, List<Entity>>> GetEntities(int UserId, string role);
        Task<Tuple<string, List<UserVM>>> GetUsersWithRole();
    }
}
