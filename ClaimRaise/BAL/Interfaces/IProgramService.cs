using MAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IProgramService
    {
        Task<UserVM> GetUserByEmailId(string email);
        Task<List<UserProgram>> GetProgramsById(int UserId);

        Task<Tuple<string, List<ProgramMaster>>> GetData();
        Task<Tuple<string, ProgramMaster>> GetSingleProg(int ProgId);
        Task<string> AddProg(ProgramMaster program);
        Task<string> UpdateProg(ProgramMaster program);
    }
}
