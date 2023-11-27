using MAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IRole
    {
        Task<Tuple<string, List<Role>>> GetData();
        Task<Tuple<string, Role>> GetSingleRole(int RoleId);
        Task<string> AddData(Role role);
        Task<string> UpdateData(Role role);
    }
}
