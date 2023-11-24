using MAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface ICRUD<T> where T : class
    {
        Task<Tuple<string, List<T>>> GetData(int UserId);
        Task<Tuple<string, T>> GetSingleUSer(int UserId);
        Task<string> AddData(T t);
        Task<string> UpdateData(T t);
    }
}
