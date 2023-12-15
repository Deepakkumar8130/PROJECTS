using ClaimAPI.Enums;
using MAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResult> Login(UserLogin user);
        void Logout();

        Task<Tuple<string, string>> pageRights(int UserId, string path);
    }
}
