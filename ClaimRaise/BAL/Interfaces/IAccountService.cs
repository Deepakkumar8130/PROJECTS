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
        //Task<UserVM> GetUserByEmailId(string email);
        //Task<List<UserProgram>> GetProgramsById(int UserId);
        void Logout();
    }
}
