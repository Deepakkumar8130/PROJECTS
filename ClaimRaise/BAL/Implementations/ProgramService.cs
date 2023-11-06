using BAL.Interfaces;
using DAL.Data;
using MAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Implementations
{
    public class ProgramService : IProgramService
    {
        private ApplicationDbContext _context { get; }

        public ProgramService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserProgram>> GetProgramsById(int UserId)
        {
            var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "USP_GET_PROGRAMS";
            command.CommandType = CommandType.StoredProcedure;

            var userId = new SqlParameter("@userId", UserId);
            command.Parameters.Add(userId);
            _context.Database.OpenConnection();
            var result = command.ExecuteReader();
            List<UserProgram> Programs = new List<UserProgram>();
            while (result.Read())
            {
                UserProgram program = new UserProgram();
                program.Id = result["Id"].ToString();
                program.Title = result["Title"].ToString();
                program.Path = result["Path"].ToString();
                program.Description = result["Description"].ToString();
                Programs.Add(program);
            }
            return Programs;
        }

        public async Task<UserVM> GetUserByEmailId(string Useremail)
        {
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_GET_USER_BY_EMAIL";
                command.CommandType = CommandType.StoredProcedure;

                var email = new SqlParameter("@email", Useremail);

                command.Parameters.Add(email);
                _context.Database.OpenConnection();
                var result = command.ExecuteReader();
                //UserVM UserInfo = await _context.Database.SqlQueryRaw<UserVM>($"EXEC USP_GET_USER_BY_EMAIL @email={email}").FirstOrDefaultAsync();
                result.Read();
                UserVM user = new UserVM();
                user.Id = result["Id"].ToString();
                user.Email = result["Email"].ToString();
                user.Mobile = result["Mobile"].ToString();
                user.UserName = result["Nm"].ToString();
                user.ManagerId = result["Manager_Id"].ToString();
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }
    }
}
