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
    }
}
