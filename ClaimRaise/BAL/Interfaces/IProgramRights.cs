using MAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IProgramRights
    {
        Task<Tuple<string, List<ProgramRight>>> GetProgramsRightsByUserId(int UserId);
        Task<Tuple<string, List<ProgramRight>>> GetProgramsRightsByRoleId(int RoleId);
        Task<Tuple<string>> AssignIndividualProgramsRights(int UserId, string XML);
        Task<Tuple<string>> AssignGroupProgramsRights(int RoleId, string XML);
    }
}
